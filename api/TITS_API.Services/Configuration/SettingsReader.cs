using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TITS_API.Api.Configuration
{
    public static class SettingsReader
    {      

        public static Credentials GetCredentials(string sectionName)
        {
            StreamReader reader = new StreamReader("../TITS_API.Api/appsettings.json");
            var json = reader.ReadToEnd();

            var jObject = JObject.Parse(json).Property(sectionName).Value.ToString();             

            return JsonConvert.DeserializeObject<Credentials>(jObject);
        }
    }
}
