using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using TourAgency.Contexts;
using TourAgency.Repositories;
using TourAgency.Services;

namespace TourAgency {
    public class Startup {
        public IConfigurationRoot Configuration { get; }
        
        public Startup(IHostingEnvironment env) {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            
            Configuration = builder.Build();
            
            using (var dbContext = new TourAgencyDbContext(Configuration["DbConnection:connectionString"])){
                dbContext.Database.EnsureCreated();
            }
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            // Add framework services.
            services.AddMvc();
            
            services.AddScoped<OrderRepository>()
                    .AddScoped<RoleRepository>()
                    .AddScoped<TourRepository>()
                    .AddScoped<TypeRepository>()
                    .AddScoped<UserRepository>();

            services.AddScoped<OrderService>()
                    .AddScoped<RoleService>()
                    .AddScoped<TourService>()
                    .AddScoped<TypeService>()
                    .AddScoped<UserService>();
                    
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
                options => {
                    options.LoginPath = new PathString("/login");
                });
            
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddProvider(new DebugLoggerProvider());

            app.UseAuthentication();
            app.UseMvc().UseWelcomePage("/tours");
        }
    }
}
