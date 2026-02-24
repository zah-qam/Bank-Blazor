using BankBlazor.Api.Data;
using BankBlazor.Api.Services;
using BankBlazor.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;

namespace BankBlazor.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<BankBlazorContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new InvalidOperationException("Connection string 'DefaultConnection' is missing. Set it in appsettings.json/appsettings.Development.json under ConnectionStrings:DefaultConnection, or via the environment variable ConnectionStrings__DefaultConnection.");
                }
                options.UseSqlServer(
                    connectionString,
                    sqlOptions => sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null));
            });

            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<ITransactionService, TransactionService>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowBlazorClient", policy =>
                {
                    policy.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            if (app.Environment.IsEnvironment("Docker"))
            {
                using var scope = app.Services.CreateScope();
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                var db = scope.ServiceProvider.GetRequiredService<BankBlazorContext>();

                var configuredConnectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
                logger.LogInformation("Using connection string (sanitized): {ConnectionString}", SanitizeConnectionString(configuredConnectionString));

                try
                {
                    var dbContextConnectionString = db.Database.GetDbConnection().ConnectionString;
                    logger.LogInformation("DbContext connection string (sanitized): {ConnectionString}", SanitizeConnectionString(dbContextConnectionString));
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex, "Failed to read DbContext connection string.");
                }

                const int maxAttempts = 40;
                var delaySeconds = 2;

                for (var attempt = 1; attempt <= maxAttempts; attempt++)
                {
                    try
                    {
                        db.Database.Migrate();
                        logger.LogInformation("Database migration complete.");
                        SeedData.SeedIfEmpty(db, logger);
                        break;
                    }
                    catch (Exception ex)
                    {
                        if (attempt == maxAttempts)
                        {
                            logger.LogError(ex, "Database migration failed after {MaxAttempts} attempts.", maxAttempts);
                            throw;
                        }

                        logger.LogWarning(ex, "Database not ready (attempt {Attempt}/{MaxAttempts}). Retrying in {DelaySeconds}s...", attempt, maxAttempts, delaySeconds);
                        Thread.Sleep(TimeSpan.FromSeconds(delaySeconds));
                        delaySeconds = Math.Min(delaySeconds + 1, 10);
                    }
                }
            }

            if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Docker"))
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            if (!app.Environment.IsEnvironment("Docker"))
            {
                app.UseHttpsRedirection();
            }

            app.UseCors("AllowBlazorClient");
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }

        private static string SanitizeConnectionString(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                return string.Empty;
            }

            var safe = connectionString;
            var passwordIndex = safe.IndexOf("Password=", StringComparison.OrdinalIgnoreCase);
            if (passwordIndex < 0)
            {
                return safe;
            }

            var end = safe.IndexOf(';', passwordIndex);
            if (end < 0) end = safe.Length;

            return safe.Remove(passwordIndex, end - passwordIndex).Insert(passwordIndex, "Password=***");
        }
    }
}
