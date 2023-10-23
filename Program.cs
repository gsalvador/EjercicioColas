using Microsoft.EntityFrameworkCore;
using PeriodicBackgroundTaskSample;
using Serilog;

namespace EjercicioColas
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Utilizado en la lectura del archivo de configuración
            // appsettings.json
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var precision = configuration.GetValue<int>("Formatting:Number:Precision");
            var builder = WebApplication.CreateBuilder(args);
            // Definición del archivo, plantilla y cada cuanto tiempo
            // se genera un nuevo archivo de log.
            var logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File(@"C:\Logs\logColas-.txt",
                                rollingInterval: RollingInterval.Day,
                                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
                                )
                .CreateLogger();
            
            // Cadena de conexión para el contexto de EntityFramework
            var connectionString = configuration.GetValue<string>("ConnectionString:DefaultConnection");
            // Add services to the container.
            builder.Services.AddScoped<SimpleService>();

            // Register as singleton first so it can be injected through Dependency Injection
            builder.Services.AddSingleton<PeriodicHostedService>();

            // Add as hosted service using the instance registered as singleton before
            builder.Services.AddHostedService(
                provider => provider.GetRequiredService<PeriodicHostedService>());

            builder.Services.AddRazorPages();
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

            app.MapRazorPages();

            app.Run();
        }
    }
}