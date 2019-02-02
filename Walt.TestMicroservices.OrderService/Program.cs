
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
using Steeltoe.Discovery.Client;
using Walt.TestMicroservices.OrderService;

namespace Walt.TestMicroServoces.Webapi
{
    public class Program
    { 
        public static void Main(string[] args)
        { 
             
            var host = new WebHostBuilder()
            .UseEnvironment(EnvironmentName.Development)
            .ConfigureAppConfiguration((hostingContext, configContext) =>{
                 var en=hostingContext.HostingEnvironment;
                configContext.AddJsonFile($"appsettings.{en.EnvironmentName}.json");
                 configContext.AddCommandLine(args)
                 .AddEnvironmentVariables()
                 .SetBasePath(Directory.GetCurrentDirectory()).Build(); 
              
            }).ConfigureServices((context,configureServices)=>{
                   
            })
            .ConfigureLogging((hostingContext, logging) => {

                logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"))
                .AddConsole();
            }).UseKestrel(KestrelServerOption=>{
                KestrelServerOption.ListenAnyIP(802);
            })
            .UseStartup<Startup>().Build(); 
            host.Run(); 
        }
    }

}
