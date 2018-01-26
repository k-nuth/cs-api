using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using Bitprim;

namespace api
{
    public class Startup
    {
        private Executor exec_;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add functionality to inject IOptions<T>
            services.AddOptions();

            // Add our Config object so it can be injected
            services.Configure<NodeConfig>(Configuration);

            // Add framework services.
            services.AddMvc();

            // Register the Swagger generator, defining one or more Swagger documents  
            services.AddSwaggerGen(c =>  
            {  
                c.SwaggerDoc("v1", new Info { Title = "bitprim", Version = "v1" });  
            });

            // Initialize and register chain service
            string configFile = Configuration.Get<NodeConfig>().NodeConfigFile;
            exec_ = new Executor(configFile, 0, 0);
            bool ok = exec_.InitChain();
            if(!ok)
            {
                throw new ApplicationException("Executor::InitChain failed; check log");
            }
            int result = exec_.RunWait();
            if (result != 0)
            {
                throw new ApplicationException("Executor::RunWait failed; error code: " + result);
            }
            services.AddSingleton<Chain>(exec_.Chain);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime applicationLifetime)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // Enable middleware to serve generated Swagger as a JSON endpoint.  
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.  
            app.UseSwaggerUI(c =>  
            {  
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "bitprim V1");
            });

            // Register shutdown handler
            applicationLifetime.ApplicationStopping.Register(OnShutdown);

            app.UseMvc();
        }

        private void OnShutdown()
        {
            Console.WriteLine("Stopping node...");
            exec_.Stop();
            exec_.Dispose();
            Console.WriteLine("Node stopped!");
        }
    }
}
