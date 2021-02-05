using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCServices.ViewModels
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ViewModelDetalleCovid
    {
        [JsonProperty]
        public string Country { get; set; }
        [JsonProperty]
        public int Deaths { get; set; }
        [JsonProperty]
        public DateTime Date { get; set; }

    }
}