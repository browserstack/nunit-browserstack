using NUnit.Framework;
using OpenQA.Selenium;

namespace BrowserStack
{
  [TestFixture("parallel", "chrome")]
  [TestFixture("parallel", "firefox")]
  [TestFixture("parallel", "safari")]
  [Parallelizable(ParallelScope.Fixtures)]
  public class ParallelTest : SingleTest
  {
    public ParallelTest(string profile, string environment) : base(profile, environment) { }
  }
}
