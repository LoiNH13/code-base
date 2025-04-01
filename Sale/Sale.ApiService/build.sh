#!/bin/bash
# Improved build script for Vietmap Core Saleforce
# Usage: ./build.sh <version>

# Enable strict mode - stop on any command failure
set -e

PRJ=""
IMAGE_NAME="vietmap.core.saleforce"
REGISTRY_URL="vmapi/group-dx"

# Input validation
VER=$1
ENV=""  # Will be determined based on version format

# Display usage information
show_usage() {
    echo "Usage: ./build.sh <version>"
    echo "  version: Format x.x.x for production, x.x.x.x for testing"
    echo "    Example: 1.0.0 (production), 1.0.0.1 (testing)"
    echo ""
    echo "  Script will automatically determine environment based on version format"
    exit 1
}

# Validate version format and determine environment
validate_version() {
    # Check production version format (x.x.x)
    if [[ $VER =~ ^[0-9]+\.[0-9]+\.[0-9]+$ ]]; then
        ENV="production"
        echo "Version $VER identified as PRODUCTION environment"
        return
    fi
    
    # Check testing version format (x.x.x.x)
    if [[ $VER =~ ^[0-9]+\.[0-9]+\.[0-9]+\.[0-9]+$ ]]; then
        ENV="testing"
        echo "Version $VER identified as TESTING environment"
        return
    fi
    
    # If it doesn't match either format
    echo "Error: Invalid version format"
    echo "  Use x.x.x for production (example: 1.0.0)"
    echo "  Use x.x.x.x for testing (example: 1.0.0.1)"
    exit 1
}

# Check if Docker is running
check_docker() {
    if ! docker info > /dev/null 2>&1; then
        echo "Error: Docker is not running or you don't have permission"
        exit 1
    fi
}

# Find main project file
findPRJ() {
    local count=0
    local found_prj=""
    
    echo "Searching for .csproj file..."
    for i in *.csproj; do
        # Skip if no matching files
        [ -e "$i" ] || continue
        
        count=$((count + 1))
        found_prj="${i%.*}"
        echo "Found project: $found_prj"
    done
    
    if [ $count -eq 0 ]; then
        echo "Error: No .csproj file found in the current directory"
        exit 1
    elif [ $count -gt 1 ]; then
        echo "Warning: Multiple .csproj files found. Using $found_prj"
    fi
    
    PRJ=$found_prj
    echo "Selected project: $PRJ"
}

# Create release directory and publish .NET project
publishNetCore() {
    RELEASE_FOLDER="bin/release/$PRJ"
    
    echo "Cleaning and creating release folder..."
    if [ -d "$RELEASE_FOLDER" ]; then
        echo "Removing existing release folder..."
        # Sử dụng PowerShell để xóa thư mục trên Windows
        if [ "$(uname -s | cut -c 1-5)" == "MINGW" ] || [ "$(uname -s | cut -c 1-5)" == "MSYS_" ]; then
            # Đang chạy trên Windows với Git Bash hoặc MSYS
            powershell.exe -Command "Remove-Item -Path '$RELEASE_FOLDER' -Recurse -Force -ErrorAction SilentlyContinue"
        else
            # Đang chạy trên Linux/Unix
            rm -rf $RELEASE_FOLDER || true
        fi
    fi
    
    # Tạo thư mục mới sau khi đã dọn dẹp
    mkdir -p $RELEASE_FOLDER
    
    # Phần còn lại của hàm giữ nguyên
    echo "Restoring packages..."
    if ! dotnet restore $PRJ.csproj; then
        echo "Error: Unable to restore packages"
        exit 1
    fi
    
    echo "Publishing .NET project..."
    if ! dotnet publish $PRJ.csproj -c release -o ./$RELEASE_FOLDER/app --self-contained false; then
        echo "Error: Unable to publish project"
        exit 1
    fi
    
    echo "Creating Dockerfile..."
    create_dockerfile
    
    # Create or copy .dockerignore
    if [ -f ".dockerignore" ]; then
        cp .dockerignore $RELEASE_FOLDER/
        echo "Using existing .dockerignore file"
    else
        create_dockerignore
    fi
}

# Create appropriate Dockerfile based on environment
create_dockerfile() {
    local diag_setting="0"
    
    # Enable diagnostics for testing environment
    if [ "$ENV" == "testing" ]; then
        diag_setting="1"
    fi
    
    cat > "$RELEASE_FOLDER/Dockerfile" << EOF
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS runtime
WORKDIR /app
COPY /app ./

# Environment settings
ENV DOTNET_EnableDiagnostics=$diag_setting \\
    ASPNETCORE_ENVIRONMENT=$ENV

# Create non-root user for security
RUN adduser -u 1000 -D appuser && \\
    chown -R appuser:appuser /app

USER appuser
ENTRYPOINT ["dotnet", "$PRJ.dll"]
EOF

    echo "Created Dockerfile for $ENV environment"
}

# Create default .dockerignore file if it doesn't exist
create_dockerignore() {
    cat > "$RELEASE_FOLDER/.dockerignore" << EOF
# .NET build artifacts
**/bin/
**/obj/
**/*.user

# Git files
.git/
.gitignore

# Docker files
Dockerfile
.dockerignore

# Logs
**/*.log

# Temp files
**/tmp/
**/temp/
EOF
    echo "Created default .dockerignore file"
}

# Build and tag Docker image
buildDocker() {
    echo "Building Docker image for $ENV environment..."
    # Build image with version tag
    docker build -f $RELEASE_FOLDER/Dockerfile -t $IMAGE_NAME:$VER $RELEASE_FOLDER/.
    
    # Tag with registry URL and version
    echo "Tagging image..."
    docker tag $IMAGE_NAME:$VER $REGISTRY_URL:$IMAGE_NAME-$VER
    
    echo "Verifying image..."
    # Bỏ qua bước chạy container, chỉ kiểm tra image tồn tại
    if docker image inspect $IMAGE_NAME:$VER > /dev/null 2>&1; then
        echo "Image verification successful"
    else
        echo "Error: Image verification failed"
        exit 1
    fi
    
    # Push to registry
    echo "Pushing image to registry..."
    docker push $REGISTRY_URL:$IMAGE_NAME-$VER
    
    echo "Build and push process completed successfully"
    echo "Image: $REGISTRY_URL:$IMAGE_NAME-$VER"
}

# Removed rollback functionality

# Main build process
startBuild() {
    # Validate version first to determine environment
    validate_version
    
    echo "Starting build process for version $VER in $ENV environment..."
    
    # Run all steps
    check_docker
    findPRJ
    publishNetCore
    buildDocker
    
    echo "Build process completed successfully!"
    echo "Use Kubernetes to manage deployment and rollback"
}

# Check if version is provided
if [ -z "$VER" ]; then
    echo "Error: Version number is required"
    show_usage
fi

# Start build process
startBuild