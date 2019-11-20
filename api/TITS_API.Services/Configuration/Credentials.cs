using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TITS_API.Api.Configuration
{
    public class Credentials
    {
        [JsonProperty(PropertyName = "Login")]
        public string Login { get; set; }

        [JsonProperty(PropertyName = "Key")]
        public string Key { get; set; }
    }
}
