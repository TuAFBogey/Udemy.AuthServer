using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SharedLibrary.Configurations;
using SharedLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniApp2.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CustomTokenOption>(Configuration.GetSection("TokenOption"));
            var tokenOptions = Configuration.GetSection("TokenOption").Get<CustomTokenOption>();

            services.AddCustomTokenAuth(tokenOptions);

            services.AddHealthChecks();
            

            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = (doc =>
                {
                    doc.Info.Title = "MiniApp2 API";
                    doc.Info.Version = "1.0";
                    doc.Info.Contact = new NSwag.OpenApiContact()
                    {
                        Name = "Hayrullah Uður Güvenen",
                        Url = "https://www.linkedin.com/in/guvenenugur/",
                        Email = "uguvenen@gmail.com"
                    };
                });
            });

            services.AddControllers();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseOpenApi();



            app.UseSwaggerUi3();


            app.UseStaticFiles();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
