using NUnit.Framework;
using OpenQA.Selenium;

namespace BrowserStack
{
  [TestFixture("chrome")]
  public class LocalTest : BrowserStackNUnitTest
  {
    public LocalTest(string environment) : base(environment, true) { }

    [Test]
    public void HealthCheck()
    {
      driver.Navigate().GoToUrl("http://bs-local.com:45691/check");
      StringAssert.Contains("Up and Running", driver.PageSource);
    }
  }
}
