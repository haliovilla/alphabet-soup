using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Alphabet_API.Models
{
    public class AlphabetModel
    {
        [Required]
        [JsonProperty("AlphabetSoup")]
        public string[,] AlphabetSoup { get; set; }

        [Required]
        [JsonProperty("WordToFind")]
        public string WordToFind { get; set; }
    }
}