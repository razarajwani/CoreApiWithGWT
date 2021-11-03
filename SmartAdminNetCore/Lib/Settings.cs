using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Lib
{
    public class Settings
    {
        public static string getAppSettings(string key)
        {
            return new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("settings")[key];
        }
        public static string baseUrl
        {
            get { return getAppSettings("baseUrl"); }
        }

        public static string lang
        {
            get { return "ar"; }
        }

        
    }
}
