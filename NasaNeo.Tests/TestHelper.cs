using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace NasaNeo.Tests
{
    public static class TestHelper
    {
        public static IConfigurationRoot GetIconfigurationRoot(string outputPath)
        {
            return new ConfigurationBuilder()
                .SetBasePath(outputPath)
                .AddJsonFile("appsettings.json", optional: true)
                .Build();
        }

        public static IConfiguration GetApplicationConfiguration()
        {
            string basePath = System.IO.Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName;
            return GetIconfigurationRoot(basePath);
        }
    }
}
