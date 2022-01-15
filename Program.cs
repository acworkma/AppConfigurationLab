// v3
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using System;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        private static IConfiguration _configuration = null;
        private static IConfigurationRefresher _refresher = null;

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.AddAzureAppConfiguration(options =>
            {
                options.Connect(Environment.GetEnvironmentVariable("ConnectionString"))
                        .ConfigureRefresh(refresh =>
                        {
                            refresh.Register("TestApp:Settings:Message")
                                   .SetCacheExpiration(TimeSpan.FromSeconds(10));
                        });

                _refresher = options.GetRefresher();
            });

            _configuration = builder.Build();
            PrintMessage().Wait();
        }

        private static async Task PrintMessage()
        {
            Console.WriteLine(_configuration["TestApp:Settings:Message"] ?? "Hello world!");

            // Wait for the user to press Enter
            Console.ReadLine();

            await _refresher.TryRefreshAsync();
            Console.WriteLine(_configuration["TestApp:Settings:Message"] ?? "Hello world!");
        }
    }
}


// v2 
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.Configuration.AzureAppConfiguration;
// public class Program
// {   static void Main(string[] args)
//    {
//        var builder = new ConfigurationBuilder();
//        builder.AddAzureAppConfiguration(Environment.GetEnvironmentVariable("ConnectionString"));
//
//        var config = builder.Build();
//        Console.WriteLine(config["TestApp:Settings:Message"] ?? "Hello world!");
//    }
//}

// v1
// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");
