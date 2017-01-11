using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RoboBank.Account.Application.Adapters.NetStandard;
using RoboBank.Account.Application.Ports;
using RoboBank.Account.Domain.Adapters.NetStandard;
using RoboBank.Account.Domain.Ports;
using RoboBank.Account.Service.Custom;

namespace RoboBank.Account.Service.NetCore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            if (env.IsDevelopment())
            {
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<ICardRepository, CardRepository>();
            services.AddTransient<IMapper, Mapper>();
            services.AddTransient<IExchangeRatesService, FixerExchangeRatesService>();
            services.AddTransient<IEventService, ServiceBusEventService>();
            services.AddScoped<UnitOfWork>();
            services.AddScoped<SaveUnitOfWorkChanges>();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseApplicationInsightsRequestTelemetry();
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseApplicationInsightsExceptionTelemetry();
            app.UseMvc();
            app.UseSwaggerUi();
        }
    }
}
