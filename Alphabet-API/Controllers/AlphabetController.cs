using System.Web.Http.Cors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Alphabet_API.Models;
using System.Threading.Tasks;

namespace Alphabet_API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [AllowAnonymous]
    [RoutePrefix("api/Alphabet")]
    public class AlphabetController : ApiController
    {
        private char[,] soup;
        private int soupSize;

        [HttpPost]
        [ResponseType(typeof(ValidationResult))]
        public async Task<IHttpActionResult> Post([FromBody] AlphabetModel data)
        {
            if (ModelState.IsValid && data != null)
            {
                var result = Validate(data);
                return Content(HttpStatusCode.OK, result);
            }
            return BadRequest(ModelState);
        }

        private ValidationResult Validate(AlphabetModel data)
        {
            CreateAlphabetSoup(data.SoupSize, data.AlphabetSoup);
            bool exists = FindWord(data.WordToFind);
            var result = new ValidationResult(data.WordToFind, exists);
            return result;
        }

        private void CreateAlphabetSoup(int size, List<string> alphabetSoup)
        {
            soup = new char[size, size];
            soupSize = size;
            int i = 0;
            foreach (string line in alphabetSoup)
            {
                int j = 0;
                foreach (char letter in line)
                {
                    soup[i, j] = letter;
                    j++;
                }
                i++;
            }
        }

        private bool FindWord(string word)
        {
            bool exists = false;

            if (RegularHorizontalSearch(word))
                exists = true;

            if (InvertedHorizontalSearch(word))
                exists = true;

            if (RegularVerticalSearch(word))
                exists = true;

            if (InvertedVerticalSearch(word))
                exists = true;

            if (DiagonalSearch(word))
                exists = true;

            if (SpecialSearch(word))
                exists = true;

            return exists;
        }

        // Search
        #region Search

        private bool RegularHorizontalSearch(string word)
        {
            return CreateHorizontalString().Contains(word);
        }

        private bool InvertedHorizontalSearch(string word)
        {
            return CreateInvertedHorizontalString().Contains(word);
        }

        private bool RegularVerticalSearch(string word)
        {
            return CreateVerticalString().Contains(word);
        }

        private bool InvertedVerticalSearch(string word)
        {
            return CreateInvertedVerticalString().Contains(word);
        }

        private bool DiagonalSearch(string word)
        {
            bool result = false;
            return result;
        }

        private bool SpecialSearch(string word)
        {
            bool result = false;
            return result;
        }

        #endregion

        // Helpers
        #region Helpers

        private string CreateHorizontalString()
        {
            string result = "";
            for (int i = 0; i < soupSize; i++)
            {
                for (int j = 0; j < soupSize; j++)
                {
                    result += soup[i, j].ToString();
                }
            }
            return result;
        }

        private string CreateInvertedHorizontalString()
        {
            string result = "";
            for (int i = 0; i < soupSize; i++)
            {
                for (int j = 0; j < soupSize; j++)
                {
                    result += soup[i, j].ToString();
                }
            }
            return result.Reverse().ToString();
        }

        private string CreateVerticalString()
        {
            string result = "";
            for (int j = 0; j < soupSize; j++)
            {
                for (int i = 0; i < soupSize; i++)
                {
                    result += soup[i, j].ToString();
                }
            }
            return result;
        }

        private string CreateInvertedVerticalString()
        {
            string result = "";
            for (int j = 0; j < soupSize; j++)
            {
                for (int i = 0; i < soupSize; i++)
                {
                    result += soup[i, j].ToString();
                }
            }
            return result.Reverse().ToString();
        }

        #endregion
    }
}