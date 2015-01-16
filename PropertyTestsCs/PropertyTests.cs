using System;
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
            Arb.register<OverrideDefaultCharArbitrary>();
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
    }
}
