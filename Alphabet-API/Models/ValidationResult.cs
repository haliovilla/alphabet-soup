using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alphabet_API.Models
{
    public class ValidationResult
    {
        public ValidationResult() { }

        public ValidationResult(string word)
        {
            Word = word;
            WordExists = false;
            Direction = "";
        }

        public ValidationResult(string word, bool wordExists, string direction)
        {
            Word = word;
            WordExists = wordExists;
            Direction = direction;
        }

        [JsonProperty("Word")]
        public string Word { get; set; }

        [JsonProperty("WordExists")]
        public bool WordExists { get; set; }

        [JsonProperty("Direction")]
        public string Direction { get; set; }


    }
}