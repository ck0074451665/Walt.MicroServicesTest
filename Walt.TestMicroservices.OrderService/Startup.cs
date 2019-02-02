using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Steeltoe.Discovery.Client;
using Swashbuckle.AspNetCore.Swagger;


namespace Walt.TestMicroservices.OrderService
{
    public class Startup
    {
        public Startup(IConfiguration configuration
            ,IHostingEnvironment hostingEn
            , ILoggerFactory loggerFac )
        {
            Configuration = configuration;
            HostingEn = hostingEn;
            LoggerFac = loggerFac; 
        }
 
        public IConfiguration Configuration { get; }

        public IHostingEnvironment HostingEn { get; }

        public ILoggerFactory LoggerFac { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(mvcOptions=>{
                
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        //     services.AddAuthorization();
        //     services.AddAuthentication("Bearer")
        //   .AddIdentityServerAuthentication(options =>
        //   {
        //       options.Authority = "http://localhost:64433";
        //       options.RequireHttpsMetadata = false;
        //       options.ApiName = "api1";
        //   });
            // Add Steeltoe Discovery Client service
            services.AddDiscoveryClient(Configuration);
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
            var log = LoggerFac.CreateLogger<Startup>();
            log.LogDebug("服务配置完成");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            var log = LoggerFac.CreateLogger<Startup>();
            log.LogInformation("infomation");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseDiscoveryClient();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
           {
               c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
               c.RoutePrefix = string.Empty;
           });
            app.UseMvc(routes => {
                        routes.MapRoute(
                            name: "default",
                            template: "api/{controller=Home}/{action=Index}/{id?}");
                    });


            app.UseAuthentication();
            log.LogDebug("通道配置完毕");
        }
    }
}
