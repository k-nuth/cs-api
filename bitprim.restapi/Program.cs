using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace api
{
    public class Program
    {
        private const string DEFAULT_URL = "https://*:1549";

        public static void Main(string[] args)
        {
            var config = GetServerUrlsFromCommandLine(args);
            var serverUrl = config.GetValue<string>("server.urls");
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseUrls(serverUrl)
                .Build();
            host.Run();
        }

        private static IConfigurationRoot GetServerUrlsFromCommandLine(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .Build();
            var serverurls = config.GetValue<string>("server.urls") ?? DEFAULT_URL;
            var configDictionary = new Dictionary<string, string>
            {
                {"server.urls", serverurls}
            };            
            return new ConfigurationBuilder()
                .AddCommandLine(args)
                .AddInMemoryCollection(configDictionary)
                .Build(); 
        }
    }
}
