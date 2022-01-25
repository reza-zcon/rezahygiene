namespace OpsManagement.Extensions.Services;

using OpsManagement.Databases;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using OpsManagement.Resources;
using Microsoft.EntityFrameworkCore;

public static class ServiceRegistration
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {
        // DbContext -- Do Not Delete
        if (env.IsEnvironment(LocalConfig.FunctionalTestingEnvName) || env.IsDevelopment())
        {
            services.AddDbContext<OpsDbContext>(options =>
                options.UseInMemoryDatabase($"OpsManagement"));
        }
        else
        {
            services.AddDbContext<OpsDbContext>(options =>
                options.UseSqlServer(
                    Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? "placeholder-for-migrations",
                    builder => builder.MigrationsAssembly(typeof(OpsDbContext).Assembly.FullName))
                            .UseSnakeCaseNamingConvention());
        }

        // Auth -- Do Not Delete
            if (!env.IsEnvironment(LocalConfig.FunctionalTestingEnvName))
            {
                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.Authority = Environment.GetEnvironmentVariable("AUTH_AUTHORITY");
                        options.Audience = Environment.GetEnvironmentVariable("AUTH_AUDIENCE");
                    });
            }

            services.AddAuthorization(options =>
            {
            options.AddPolicy("CurrenciesFullAccess",
                    policy => policy.RequireClaim("scope", "currencies.fullaccess"));
            options.AddPolicy("CurrenciesReadOnly",
                    policy => policy.RequireClaim("scope", "currencies.readonly"));
            options.AddPolicy("CountriesFullAccess",
                    policy => policy.RequireClaim("scope", "countries.fullaccess"));
            options.AddPolicy("CountriesReadOnly",
                    policy => policy.RequireClaim("scope", "countries.readonly"));
            });
    }
}
