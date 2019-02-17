using NUnit.Framework;

namespace BrowserStack
{
    [TestFixture("parallel", "windows10", "chrome")]
    [TestFixture("parallel", "windows10", "ie")]
    [TestFixture("parallel", "windows7", "chrome")]
    [TestFixture("parallel", "windows7", "ie")]
    [Parallelizable(ParallelScope.Fixtures)]
    public class ParallelTest : SingleTest
    {
        public ParallelTest(string profile, string os, string browserName) : base(profile, os, browserName) { }
    }
}
