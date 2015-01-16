using System;
using System.Collections.Generic;
using System.Linq;

namespace DiamondLib
{
    public static class Diamond
    {
        public static IEnumerable<string> GenerateLines(char inputChar)
        {
            var squareSize = CalculateSquareSize(inputChar);
            var line = new String(' ', squareSize);
            return Enumerable.Repeat(line, squareSize);
        }

        public static int CalculateSquareSize(char inputChar)
        {
            return (inputChar - 'A') * 2 + 1;
        }
    }
}
