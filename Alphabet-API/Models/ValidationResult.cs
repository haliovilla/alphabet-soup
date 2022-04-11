using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alphabet_API.Models
{
    public class ValidationResult
    {
        [JsonProperty("Word")]
        public string Word { get; set; }

        [JsonProperty("WordExists")]
        public bool WordExists { get; set; }


    }
}