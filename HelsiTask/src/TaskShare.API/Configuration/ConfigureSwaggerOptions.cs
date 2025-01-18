using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TaskShare.API.Configuration;

public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    {
        _provider = provider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(
                $"v{description.ApiVersion}",
                new OpenApiInfo
                {
                    Title = "TaskShare API",
                    Version = $"v{description.ApiVersion}",
                    Description = "TaskShare API with versioning",
                    Contact = new OpenApiContact
                    {
                        Name = "Artur",
                        Email = "test4455@gmail.com"
                    }
                });
        }
    }

    public void Configure(string? name, SwaggerGenOptions options)
    {
        Configure(options);
    }
}