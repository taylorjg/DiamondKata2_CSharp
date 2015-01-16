using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DiamondLib;
using FsCheck;
using FsCheck.Fluent;
using FsCheck.NUnit;

namespace PropertyTestsCs
{
    using Property =  Gen<Rose<Result>>;

    [NUnit.Framework.TestFixture]
    public class PropertyTests
    {
        [NUnit.Framework.SetUp]
        public void SetUp()
        {
            Arb.register<MyArbitraries>();
        }

        [Property]
        public Property ResultAlwaysHasAtLeastOneLineProperty()
        {
            Func<char, bool> property = c => Diamond.GenerateLines(c).Any();

            return Spec
                .ForAny(property)
                .Build();
        }

        [Property]
        public Property ResultIsAlwaysSquareProperty()
        {
            Func<char, bool> property = c =>
            {
                var lines = Diamond.GenerateLines(c).ToList();
                return lines.All(line => line.Length == lines.Count);
            };

            return Spec
                .ForAny(property)
                .Build();
        }

        private static readonly Gen<char> GenUpperCaseAlphaChar = Any.ValueIn(Enumerable.Range('A', 26).Select(Convert.ToChar));

        private class UpperCaseAlphaCharArbitrary : Arbitrary<char>
        {
            public override Gen<char> Generator
            {
                get { return GenUpperCaseAlphaChar; }
            }

            public override IEnumerable<char> Shrinker(char c)
            {
                return Arb.Default.Char().Shrinker(c);
            }
        }

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
        private class MyArbitraries
        {
            public static Arbitrary<char> UpperCaseAlphaChar()
            {
                return new UpperCaseAlphaCharArbitrary();
            }
        }
    }
}
