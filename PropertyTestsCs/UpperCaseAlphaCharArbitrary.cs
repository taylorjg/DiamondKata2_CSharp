using System;
using System.Collections.Generic;
using System.Linq;
using FsCheck;
using FsCheck.Fluent;

namespace PropertyTestsCs
{
    internal class UpperCaseAlphaCharArbitrary : Arbitrary<char>
    {
        private static readonly Gen<char> GenUpperCaseAlphaChar = Any.ValueIn(Enumerable.Range('A', 26).Select(Convert.ToChar));

        public override Gen<char> Generator
        {
            get { return GenUpperCaseAlphaChar; }
        }

        public override IEnumerable<char> Shrinker(char c)
        {
            return Arb.Default.Char().Shrinker(c);
        }
    }
}
