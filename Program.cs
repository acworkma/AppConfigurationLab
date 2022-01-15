﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;

static void Main(string[] args)
{
    var builder = new ConfigurationBuilder();
    builder.AddAzureAppConfiguration(Environment.GetEnvironmentVariable("ConnectionString"));

    var config = builder.Build();
    Console.WriteLine(config["TestApp:Settings:Message"] ?? "Hello world!");
}


// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");
