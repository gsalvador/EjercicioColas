using EjercicioColas.Services;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using PeriodicBackgroundTaskSample;
using Serilog;

namespace EjercicioColas
{
    public class Program
    {
        [Obsolete]
        public static void Main(string[] args)
        {
            // Reading configuration items from appsettings.json file
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var precision = configuration.GetValue<int>("Formatting:Number:Precision");
            var builder = WebApplication.CreateBuilder(args);

            // Define log file, template and rolling up time interval
            var logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File(@"C:\Logs\logColas-.txt",
                                rollingInterval: RollingInterval.Day,
                                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
                                )
                .CreateLogger();
            
            // EntityFramework connection string configuration for DB Context
            var connectionString = configuration.GetValue<string>("ConnectionString:DefaultConnection");

            // Add services to the container.
            builder.Services.AddScoped<SimpleService>();

            // Singleton design patern used for PriodicHostService timed class
            builder.Services.AddSingleton<PeriodicHostedService>();

            // Add as hosted service using the instance registered as singleton before
            builder.Services.AddHostedService(
                provider => provider.GetRequiredService<PeriodicHostedService>());

            builder.Services.AddRazorPages().AddNToastNotifyNoty(new NotyOptions
            {
                ProgressBar = true,
                Timeout = 5000
            });

            builder.Services.AddDbContext<ManejoColasContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            builder.Host.UseSerilog(logger);

            var app = builder.Build();

            app.MapGet("/background", (PeriodicHostedService service) =>
            {
                return new PeriodicHostedServiceState(service.IsEnabled);
            });

            app.MapMethods("/background", new[] { "PATCH" }, (
                PeriodicHostedServiceState state,
                PeriodicHostedService service) =>
            {
                service.IsEnabled = state.IsEnabled;
            });

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseNToastNotify();

            app.MapRazorPages();

            app.Run();
        }
    }
}