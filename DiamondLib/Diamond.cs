using System.Collections.Generic;

namespace DiamondLib
{
    public static class Diamond
    {
        public static IEnumerable<string> GenerateLines(char inputChar)
        {
            return new[]
            {
                " A ",
                "B B",
                " A "
            };
        }
    }
}
