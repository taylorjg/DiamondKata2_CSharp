using System;
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
        private static readonly Func<char, bool> BasicProperty = c => Diamond.GenerateLines(c).Any();

        private static readonly Gen<char> GenUpperCaseAlphaChar = Any.ValueIn(Enumerable.Range('A', 26).Select(Convert.ToChar));

        private class UpperCaseAlphaCharArbitrary : Arbitrary<char>
        {
            public override Gen<char> Generator
            {
                get { return GenUpperCaseAlphaChar; }
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

        [Property(Verbose = true, MaxTest = 5)]
        public bool Property1(char c)
        {
            return BasicProperty(c);
        }

        [Property(Verbose = true, MaxTest = 5, Arbitrary = new[] { typeof(MyArbitraries) })]
        public bool Property2(char c)
        {
            return BasicProperty(c);
        }

        [Property(Verbose = true, MaxTest = 5)]
        public Property Property3()
        {
            return Spec
                .For(GenUpperCaseAlphaChar, BasicProperty)
                .Build();
        }

        [Property(Verbose = true, MaxTest = 5)]
        public Property Property4()
        {
            Arb.register<MyArbitraries>();
            return Spec
                .ForAny(BasicProperty)
                .Build();
        }
    }
}
