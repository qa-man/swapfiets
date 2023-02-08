using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace SwapfietsTests.Helpers
{
    public static class ConfigHelper
    {
        private static IConfigurationRoot Config => new ConfigurationBuilder().SetBasePath(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.FullName!).AddJsonFile("config.json").Build();

        public static string PathToEdge => Config["EdgePath"];
        public static string Url => Config["Url"];
        public static string GbUrl => Config["GbUrl"];

    }
}