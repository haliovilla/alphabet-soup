using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alphabet_API.Models
{
    public class AlphabetModel
    {
        [JsonProperty("AlphabetSoup")]
        public string[,] AlphabetSoup { get; set; }

        [JsonProperty("WordToFind")]
        public string WordToFind { get; set; }
    }
}