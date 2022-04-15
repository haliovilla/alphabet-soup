using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alphabet_API.Models
{
    public class LetterLocation
    {
        public LetterLocation() { }

        public LetterLocation(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public int Row { get; set; }
        public int Column { get; set; }
    }
}