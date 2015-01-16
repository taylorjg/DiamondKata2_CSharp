using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DiamondLib;
using FsCheck;
using FsCheck.Fluent;
using FsCheck.NUnit;

namespace PropertyTestsCs
{
    [NUnit.Framework.TestFixture]
    public class PropertyTests
    {
        [Property(Verbose = true, MaxTest = 5)]
        public bool Property1(char c)
        {
            return Diamond.GenerateLines(c).Any();
        }

        [Property(Verbose = true, MaxTest = 5)]
        public Gen<Rose<Result>> Property2()
        {
            var genUpperCaseAlphaChar = Any.ValueIn(Enumerable.Range('A', 26).Select(Convert.ToChar));
            return Spec.For(genUpperCaseAlphaChar, c => Diamond.GenerateLines(c).Any()).Build();
        }

        private class UpperCaseAlphaCharArbitrary : Arbitrary<char>
        {
            public override Gen<char> Generator
            {
                get { return Any.ValueIn(Enumerable.Range('A', 26).Select(Convert.ToChar)); }
            }
        }

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private class MyArbitraries
        {
            public static Arbitrary<char> UpperCaseAlphaChar()
            {
                return new UpperCaseAlphaCharArbitrary();
            }
        }

        [Property(Verbose = true, MaxTest = 5, Arbitrary = new[] { typeof(MyArbitraries) })]
        public bool Property3(char c)
        {
            return Diamond.GenerateLines(c).Any();
        }
    }
}
