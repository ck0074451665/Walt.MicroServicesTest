using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Walt.Framework.Service;
using Walt.Framework.Configuration;
using Walt.Framework.Service.Kafka;

namespace Walt.TestMcroServoces.Webapi
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
             services.AddSingleton<IKafkaService,KafkaService>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddAuthorization();
            services.AddAuthentication("Bearer")
          .AddIdentityServerAuthentication(options =>
          {
              options.Authority = "http://localhost:64433";
              options.RequireHttpsMetadata = false;

              options.ApiName = "api1";
          });
 
             services.AddKafka(KafkaBuilder=>{
                    var kafkaConfig= Configuration.GetSection("KafkaService");
                    KafkaBuilder.AddConfiguration(kafkaConfig); 
            });
       
           var log= LoggerFac.CreateLogger<Startup>();
            log.LogDebug("服务配置完成");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        { 
            
            var log= LoggerFac.CreateLogger<Startup>();
            log.LogInformation("infomation");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseAuthentication();  
            app.UseMvc();
            log.LogDebug("通道配置完毕");
        }
    }
}
