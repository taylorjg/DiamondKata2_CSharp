using System;
using System.Collections.Generic;
using System.Linq;
using DiamondLib;
using FsCheck;
using FsCheck.Fluent;
using NUnit.Framework;

namespace PropertyTestsCs
{
    using PropertyFunc = Func<char, bool>;
    using Property =  Gen<Rose<Result>>;

    [TestFixture]
    public class PropertyTests
    {
        [SetUp]
        public void SetUp()
        {
            Arb.register<OverrideDefaultCharArbitrary>();
        }

        [FsCheck.NUnit.Property]
        public Property ResultAlwaysHasAtLeastOneLineProperty()
        {
            PropertyFunc property = c => Diamond.GenerateLines(c).Any();
            return Spec.ForAny(property).Build();
        }

        [FsCheck.NUnit.Property]
        public Property ResultIsAlwaysSquareProperty()
        {
            PropertyFunc property = c => Diamond.GenerateLines(c).All(line => line.Length == Diamond.GenerateLines(c).ToList().Count);
            return Spec.ForAny(property).Build();
        }

        [FsCheck.NUnit.Property]
        public Property AllLinesAreHorizontallySymetricalProperty()
        {
            PropertyFunc property = c => Diamond.GenerateLines(c).All(line => line.SequenceEqual(line.Reverse()));
            return Spec.ForAny(property).Build();
        }

        [FsCheck.NUnit.Property]
        public Property AllLinesAreVerticallySymetricalProperty()
        {
            PropertyFunc property = c =>
            {
                var lines = Diamond.GenerateLines(c).ToList();
                var transformedLines = TransformLines(lines);
                return transformedLines.All(line => line.SequenceEqual(line.Reverse()));
            };
            return Spec.ForAny(property).Build();
        }

        [FsCheck.NUnit.Property]
        public Property SquareSizeShouldBeRelatedToTheInputCharProperty()
        {
            PropertyFunc property = c =>
            {
                var squareSize = Diamond.CalculateSquareSize(c);
                return Diamond.GenerateLines(c).Count() == squareSize;
            };
            return Spec.ForAny(property).Build();
        }

        private static IEnumerable<string> TransformLines(IReadOnlyCollection<string> lines)
        {
            var transformedLines = new List<string>();
            for (var colIndex = 0; colIndex < lines.Count; colIndex++) transformedLines.Add(lines.Aggregate(string.Empty, (line, row) => line + row[colIndex]));
            return transformedLines;
        }
    }
}
