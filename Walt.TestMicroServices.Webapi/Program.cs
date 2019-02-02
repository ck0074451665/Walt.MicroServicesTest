using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging; 
using Newtonsoft.Json;
using Walt.Framework.Log;
using Walt.Framework.Configuration;
using Walt.Framework.Service;
using Steeltoe.Discovery.Client;

namespace Walt.TestMicroServoces.Webapi
{
    public class Program
    { 
        public static void Main(string[] args)
        { 
             
            var host = new WebHostBuilder()
            .ConfigureAppConfiguration((hostingContext, configContext) =>{
                 var en=hostingContext.HostingEnvironment;
                configContext.AddJsonFile($"appsettings.{en.EnvironmentName}.json");
                 configContext.AddCommandLine(args)
                 .AddEnvironmentVariables()
                 .SetBasePath(Directory.GetCurrentDirectory()).Build(); 
              
            }).ConfigureServices((context,configureServices)=>{
                   configureServices.AddKafka(KafkaBuilder=>{
                    KafkaBuilder.AddConfiguration(context.Configuration.GetSection("KafkaService"));
                   });
                    configureServices.AddZookeeper(zooBuilder=>{
                        zooBuilder.AddConfiguration(context.Configuration.GetSection("zookeeperService"));
                    });
                   
            })
            .ConfigureLogging((hostingContext, logging) => {
 
                logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"))
                .AddConsole()
                .AddCustomizationLogger();
                

            }).UseKestrel(KestrelServerOption=>{
                KestrelServerOption.ListenAnyIP(801);
            })
            .UseStartup<Startup>().Build(); 
            host.Run(); 
        }
    }

}
