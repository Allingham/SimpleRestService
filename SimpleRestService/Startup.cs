using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.VisualBasic;

namespace SimpleRestService
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
            services.AddControllers();

            services.AddCors(options => {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => 
                        builder.WithOrigins("http://zealand.dk")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
                    
                options.AddPolicy("AllowAnyOrigin",
                        builder => builder.AllowAnyOrigin().
                    AllowAnyMethod().
                    AllowAnyHeader());
                options.AddPolicy("AllowAnyOriginGetPut",
                        builder => builder.AllowAnyOrigin().
                            WithMethods("GET", "PUT").
                    AllowAnyHeader());
            });

            //services.AddDbContext<ItemContext>(opt => opt.UseInMemoryDatabase("ItemList"));

            services.AddDbContext<ItemContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("ItemContext")));

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo{
                    Title = "Items API",
                    Version = "v1.0",
                    Description = "Test Beskrivelse",
                    Contact = new OpenApiContact(){
                        Name = "Christian",
                        Email = "christianallingham@gmail.com",
                    },
                    License = new OpenApiLicense(){
                        Name = "No Licence Required"
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Items API v1.0");
                c.RoutePrefix = "api/help";
            });

            app.UseRouting();

            app.UseCors("AllowAnyOriginGetPut");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
