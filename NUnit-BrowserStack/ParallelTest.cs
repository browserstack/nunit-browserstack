using NUnit.Framework;
using OpenQA.Selenium;

namespace BrowserStack
{
  [TestFixture("chrome")]
  [TestFixture("firefox")]
  [TestFixture("safari")]
  [TestFixture("ie")]
  [Parallelizable(ParallelScope.Fixtures)]
  public class ParallelTest : SingleTest
  {
    public ParallelTest(string environment) : base(environment) { }
  }
}
