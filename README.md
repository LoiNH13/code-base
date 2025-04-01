# DXProjects Solution

## Overview
The OdooPayment solution is a .NET 8 based application that includes multiple projects to handle various aspects of the Odoo payment system. The solution is designed to be modular and scalable, leveraging dependency injection, background services, and other modern .NET features.

## Projects

### Common Libraries
These projects contain shared code and utilities used across the solution.

#### Key Features:
- Shared utilities and extensions
- Common models and DTOs

### MOT
This project contains core functionalities and abstractions that are used by other projects in the solution.

#### Key Features:
- Core business logic
- Abstractions for services and repositories

### Odoo
This project handles the integration with the Odoo system. It includes services and repositories specific to Odoo.

#### Key Features:
- Integration with Odoo APIs
- Odoo-specific business logic

### OdooPayment
This project is the main API service for the Odoo payment system. It handles HTTP requests and routes them to the appropriate services and repositories.

#### Key Features:
- ASP.NET Core Web API
- Swagger/OpenAPI for API documentation
- Dependency Injection for service management

### Payment
This project handles payment processing logic. It includes services and repositories for managing payments.

#### Key Features:
- Payment processing
- Integration with payment gateways

### Sale
This project contains code related to sales operations. It includes services and repositories for managing sales data.

#### Key Features:
- Sales data management
- Business logic for sales operations

### SMS
This project handles SMS functionalities. It includes services for sending and receiving SMS messages.

#### Key Features:
- SMS sending and receiving
- Integration with SMS gateways

### OdooPayment.Background.Tasks
This project contains background services that run periodic tasks. It uses the `BackgroundService` class to implement long-running operations.

#### Key Features:
- Background services for periodic tasks
- Hosted services for continuous operations

## Getting Started

### Prerequisites
- .NET 8 SDK
- Visual Studio 2022

### Building the Solution
1. Clone the repository.
2. Open the solution in Visual Studio 2022.
3. Restore the NuGet packages.
4. Build the solution.

### Running the Application
1. Set `OdooPayment.ApiService` as the startup project.
2. Run the application using Visual Studio or the .NET CLI.

### Configuration
The application uses `appsettings.json` for configuration. Ensure that the necessary settings for database connections and other services are correctly configured.

### Dependency Injection
Ensure that all required services are registered in the `ConfigureServices` method in `Program.cs`. For example:

