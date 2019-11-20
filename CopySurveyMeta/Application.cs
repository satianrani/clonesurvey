using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace CopySurveyMeta
{
    [JsonObject("Application")]
    public class Application
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("ConnectionStringDestination")]
        public string ConnectionStringDestination { get; set; }
        [JsonProperty("ConnectionStringSource")]
        public string ConnectionStringSource { get; set; }
    }

    public class ApplicationInit
    {
        private static Application appConfig = null;
        public ApplicationInit()
        {

        }
        public static Application GetAppConfig
        {
            get
            {
                if (appConfig == null)
                { 
                    //     var builder = new ConfigurationBuilder()
                    //.SetBasePath(Directory.GetCurrentDirectory())
                    //.AddJsonFile("appsettings.json");
                    IConfiguration config = new ConfigurationBuilder()
                      .AddJsonFile("appsettings.json", true, true)
                      .Build();

                    appConfig = config.GetSection("application").Get<Application>();

                }
                return appConfig;
            }
        }
    }
}
