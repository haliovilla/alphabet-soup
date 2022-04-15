using System.Web.Http.Cors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Alphabet_API.Models;
using System.Threading.Tasks;
using Alphabet_API.Helpers;

namespace Alphabet_API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [AllowAnonymous]
    [RoutePrefix("api/Alphabet")]
    public class AlphabetController : ApiController
    {
        private char[,] soup;
        private char[,] invertedSoup;
        private int soupSize;
        private string randomWord;

        [HttpPost]
        [ResponseType(typeof(ValidationResult))]
        public async Task<IHttpActionResult> Post([FromBody] AlphabetModel data)
        {
            if (ModelState.IsValid && data != null)
            {
                try
                {
                    var result = Validate(data);
                    return Content(HttpStatusCode.OK, result);
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
            return BadRequest(ModelState);
        }

        private ValidationResult Validate(AlphabetModel data)
        {
            //randomWord = data.WordToFind;
            CreateAlphabetSoup(data.SoupSize, data.AlphabetSoup);
            var result = FindWord(data.WordToFind);
            return result;
        }

        private void CreateAlphabetSoup(int size, List<string> alphabetSoup)
        {
            soup = new char[size, size];
            invertedSoup = new char[size, size];
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

                j = 0;
                string invertedLine = new string(line.Reverse().ToArray());
                foreach (char letter in invertedLine)
                {
                    invertedSoup[i, j] = letter;
                    j++;
                }
                i++;
            }
        }

        private ValidationResult FindWord(string word)
        {
            var result = new ValidationResult(word);
            bool exists = false;
            string direction = "";
            string invertedWord = new string(word.Reverse().ToArray());

            if (RegularHorizontalSearch(word))
            {
                return new ValidationResult(word, true, Directions.Horizontal);
            }


            if (RegularHorizontalSearch(invertedWord))
            {
                return new ValidationResult(word, true, Directions.InvertedHorizontal);
            }

            if (RegularVerticalSearch(word))
            {
                return new ValidationResult(word, true, Directions.Vertical);
            }

            if (RegularVerticalSearch(invertedWord))
            {
                return new ValidationResult(word, true, Directions.InvertedVertical);
            }

            if (DiagonalSearch(word))
            {
                return new ValidationResult(word, true, Directions.Diagonal);
            }

            if (InvertedDiagonalSearch(word))
            {
                return new ValidationResult(word, true, Directions.Diagonal);
            }

            if (DiagonalSearch(invertedWord))
            {
                return new ValidationResult(word, true, Directions.InvertedDiagonal);
            }

            if (InvertedDiagonalSearch(invertedWord))
            {
                return new ValidationResult(word, true, Directions.InvertedDiagonal);
            }

            if (SpecialSearch(word))
            {
                return new ValidationResult(word, true, Directions.Special);
            }

            if (SpecialSearch(invertedWord))
            {
                return new ValidationResult(word, true, Directions.InvertedSpecial);
            }

            return result;
        }

        // Search
        #region Search

        private bool RegularHorizontalSearch(string word)
        {
            return CreateHorizontalString().Contains(word);
        }

        private bool RegularVerticalSearch(string word)
        {
            return CreateVerticalString().Contains(word);
        }

        private bool DiagonalSearch(string word)
        {
            return CreateDiagonalString().Contains(word);
        }

        private bool InvertedDiagonalSearch(string word)
        {
            return CreateInvertedDiagonalString().Contains(word);
        }

        private bool SpecialSearch_Deprecated(string word)
        {
            bool result = false;
            var dictionary = GetLettersLocation(word);
            for (int i = 0; i < word.Length; i++)
            {
                string letter = word[i].ToString();
                string nextLetter = "";
                if (i<word.Length - 1)
                    nextLetter = word[i + 1].ToString();
                else
                    break;

                result = NextLetterExists(dictionary[letter], dictionary[nextLetter]);
            }
            return result;
        }

        private bool SpecialSearch(string word)
        {
            randomWord = word;
            return Find();
        }

        #endregion

        // Special search
        #region Special search

        private bool Find()
        {
            for (int i = 0; i < soupSize; i++)
            {
                for (int j = 0; j < soupSize; j++)
                {
                    if (soup[i,j]==randomWord[0])
                    {
                        if (FindNext(i, j))
                            return true;
                    }
                }
            }
            return false;
        }

        private bool FindNext(int row, int col, int wordIndex = 0)
        {
            wordIndex++;
            if (wordIndex == randomWord.Length)
                return true;
            else
            {
                var up = CreateUp(row, col);
                var down = CreateDown(row, col);
                var left = CreateLeft(row, col);
                var right = CreateRight(row, col);

                if (CompareLetter(randomWord[wordIndex],up))
                {
                    return FindNext(up.Row, up.Column, wordIndex);
                }
                if (CompareLetter(randomWord[wordIndex], right))
                {
                    return FindNext(right.Row, right.Column, wordIndex);
                }
                if (CompareLetter(randomWord[wordIndex], down))
                {
                    return FindNext(down.Row, down.Column, wordIndex);
                }
                if (CompareLetter(randomWord[wordIndex], left))
                {
                    return FindNext(left.Row, left.Column, wordIndex);
                }
            }
            return false;
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

        private string CreateDiagonalString()
        {
            string result = "";

            // over
            #region over
            int over_max = soupSize;
            int over_startCol = 0;
            while (over_max>=0)
            {
                int over_col = over_startCol;
                for (int over_row = 0; over_row < over_max; over_row++)
                {
                    result += soup[over_row, over_col].ToString();
                    over_col++;
                }
                over_startCol++;
                over_max--;
            }
            #endregion

            // under
            #region under
            int max = soupSize - 1;
            int startCol = 1;
            while (max >= 0)
            {
                int col = startCol;
                for (int row = 0; row < max; row++)
                {
                    result += soup[col, row].ToString();
                    col++;
                }
                startCol++;
                max--;
            }
            #endregion

            return result;
        }

        private string CreateInvertedDiagonalString()
        {
            string result = "";

            // over
            #region over
            int over_max = soupSize;
            int over_startCol = 0;
            while (over_max >= 0)
            {
                int over_col = over_startCol;
                for (int over_row = 0; over_row < over_max; over_row++)
                {
                    result += invertedSoup[over_row, over_col].ToString();
                    over_col++;
                }
                over_startCol++;
                over_max--;
            }
            #endregion

            // under
            #region under
            int max = soupSize - 1;
            int startCol = 1;
            while (max >= 0)
            {
                int col = startCol;
                for (int row = 0; row < max; row++)
                {
                    result += invertedSoup[col, row].ToString();
                    col++;
                }
                startCol++;
                max--;
            }
            #endregion

            return result;
        }

        #endregion

        // Find letters
        #region Find letters

        private Dictionary<string, List<LetterLocation>> GetLettersLocation(string word)
        {
            var result = new Dictionary<string, List<LetterLocation>>();
            foreach (char letter in word)
            {
                if (!result.ContainsKey(letter.ToString()))
                    result.Add(letter.ToString(), GetLetterLocation(letter));
            }
            return result;
        }

        private List<LetterLocation> GetLetterLocation(char letter)
        {
            var result = new List<LetterLocation>();
            for (int i = 0; i < soupSize; i++)
            {
                for (int j = 0; j < soupSize; j++)
                {
                    if (soup[i,j]==letter)
                    {
                        result.Add(new LetterLocation(i, j));
                    }
                }
            }
            return result;
        }

        private bool NextLetterExists(List<LetterLocation> currentLetter, List<LetterLocation> nextLetter)
        {
            bool exists = false;

            foreach (LetterLocation item in currentLetter)
            {
                if (nextLetter.Exists(x => x.Column == item.Column + 1)
                    && nextLetter.Exists(x => x.Row == item.Row + 1))
                    exists = true;
                
            }

            return exists;
        }

        private bool LookAround(List<LetterLocation> currentLetterLocations, char nextLetter)
        {
            bool result = false;
            foreach (LetterLocation location in currentLetterLocations)
            {
                if (CompareLetter(nextLetter,CreateUpLocation(location)))
                    result = true;

                if (CompareLetter(nextLetter, CreateRightLocation(location)))
                    result = true;

                if (CompareLetter(nextLetter, CreateDownLocation(location)))
                    result = true;

                if (CompareLetter(nextLetter, CreateLeftLocation(location)))
                    result = true;
            }
            return result;
        }

        private bool CompareLetter(char letter, LetterLocation location)
        {
            return soup[location.Row, location.Column] == letter;
        }

        private LetterLocation CreateUp(int row, int col)
        {
            if (row > 0)
                row--;
            return new LetterLocation(row, col);
        }

        private LetterLocation CreateDown(int row, int col)
        {
            if (row < soupSize - 1)
                row++;
            return new LetterLocation(row, col);
        }

        private LetterLocation CreateLeft(int row, int col)
        {
            if (col > 0)
                col--;
            return new LetterLocation(row, col);
        }

        private LetterLocation CreateRight(int row, int col)
        {
            if (col < soupSize - 1)
                col++;
            return new LetterLocation(row, col);
        }

        private LetterLocation CreateUpLocation(LetterLocation location)
        {
            if (location.Row > 0)
                location.Row = location.Row - 1;
            return location;
        }

        private LetterLocation CreateDownLocation(LetterLocation location)
        {
            if (location.Row < soupSize - 1)
                location.Row = location.Row + 1;
            return location;
        }

        private LetterLocation CreateLeftLocation(LetterLocation location)
        {
            if (location.Column > 0)
                location.Column = location.Column - 1;
            return location;
        }

        private LetterLocation CreateRightLocation(LetterLocation location)
        {
            if (location.Column < soupSize - 1)
                location.Column = location.Column + 1;
            return location;
        }

        #endregion
    }
}