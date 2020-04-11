using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MicroserviceTestMVC.Model
{
    public class User
    {
        public long id { get; set; }
        [JsonPropertyName("name")]
        public String Name { get; set; }
        [JsonPropertyName("address")]
        public String Address { get; set; }
        [JsonPropertyName("contact")]
        public String Contact { get; set; }
    }
}
