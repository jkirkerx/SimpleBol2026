using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBol.Models
{
    public class NLogTrace
    {
        [JsonProperty("Time")]
        public string? Time { get; set; }

        [JsonProperty("Trace")]
        public string? Level { get; set; }

        [JsonProperty("Message")]
        public string? Message { get; set; }
    }
}
