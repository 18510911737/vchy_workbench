using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HtmlParse
{
    public class ConfigHelper
    {
        public IConfigurationRoot Configuration;
        public ConfigHelper()
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("spider.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
        }

        public ConfigHelper(string json)
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile(json, optional: true, reloadOnChange: true);
            Configuration = builder.Build();
        }
    }
}
