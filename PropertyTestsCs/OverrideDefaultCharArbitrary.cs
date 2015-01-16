using System.Diagnostics.CodeAnalysis;
using FsCheck;

namespace PropertyTestsCs
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
    internal class OverrideDefaultCharArbitrary
    {
        public static Arbitrary<char> UpperCaseAlphaCharArbitrary()
        {
            return new UpperCaseAlphaCharArbitrary();
        }
    }
}
