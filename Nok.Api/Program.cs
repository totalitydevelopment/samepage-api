using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using Nok.Api.Controllers;
using Nok.Core.Extensions;
using Nok.Infrastructure.Data;

namespace Nok.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Adds Microsoft Identity platform (Azure AD B2C) support to protect this Api
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(options =>
            {
                builder.Configuration.Bind("AzureAdB2C", options);
                options.TokenValidationParameters.NameClaimType = "name";
            },
            options =>
            {
                builder.Configuration.Bind("AzureAdB2C", options);
            });

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();

        ConfigureOpenApiGeneration(builder);

        // Add CORS services
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowLocalDev",
                builder => builder.WithOrigins("http://localhost:4200") // Replace with your client app's URL
                                  .AllowAnyHeader()
                                  .AllowAnyMethod());
        });

        builder.Services.AddDbContext<DatabaseContext>(options =>
        {
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("SqlConnection"),
                b => b.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName));
        });

        builder.Services
            .AddSingleton<IAccessIdentifierService, AccessIdentifierService>();

        var app = builder.Build();

        ConfigureSwaggerUi(builder, app);
        app.UseCors("AllowLocalDev");
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }

    private static void ConfigureOpenApiGeneration(WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo { Title = "nok", Version = "v1" });

            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header using the Bearer scheme. <br /> 
                      Enter 'Bearer' [space] and then your token in the text input below. <br /> 
                      Example: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            opt.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Description = "OAuth2.0 Auth Code with PKCE",
                Name = "oauth2",
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri(builder.Configuration["Swagger:AuthorizationUrl"]!),
                        TokenUrl = new Uri(builder.Configuration["Swagger:TokenUrl"]!),
                        Scopes = new Dictionary<string, string>
                        {
                            [builder.Configuration["Swagger:ApiScope"]!] = "read the api",
                        }
                    }
                }
            });

            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                    },
                    new[] { builder.Configuration["Swagger:ApiScope"] }
                }
            });

            opt.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
        });
    }

    private static void ConfigureSwaggerUi(WebApplicationBuilder builder, WebApplication app)
    {
        if (EnvironmentExtensions.IsLocalOrDev)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "nok v1");
                c.OAuthClientId(builder.Configuration["Swagger:OpenIdClientId"]);
                c.OAuthUsePkce();
                c.OAuthScopeSeparator(" ");
                c.DisplayRequestDuration();
            });
        }
    }
}
