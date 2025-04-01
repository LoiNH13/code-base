using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ApiService.Extensions;
/// <summary>
/// This class provides extension methods for SwaggerGenOptions to include XML comments from all projects.
/// </summary>
public static class SwaggerExtensions
{
    /// <summary>
    /// Includes XML comments from all projects in the Swagger documentation.
    /// </summary>
    /// <param name="swaggerGenOptions">The SwaggerGenOptions instance.</param>
    public static void IncludeXmlCommentsFromAllProjects(this SwaggerGenOptions swaggerGenOptions)
    {
        // Get all XML files in the solution directory and its subdirectories
        string[] xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.AllDirectories);

        // Include XML comments from each XML file in the Swagger documentation
        foreach (var xmlFile in xmlFiles)
        {
            if (File.Exists(xmlFile))
            {
                swaggerGenOptions.IncludeXmlComments(xmlFile);
                Console.WriteLine($"Included XML comments from: {xmlFile}");
            }
        }
    }
}