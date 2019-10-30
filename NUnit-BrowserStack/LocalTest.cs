using NUnit.Framework;
using OpenQA.Selenium;
using System.Text.RegularExpressions;

namespace BrowserStack
{
  [TestFixture("local", "windows10", "chrome")]
  public class LocalTest : BrowserStackDesktopNUnitTest
  {
    public LocalTest(string profile, string os, string browser) : base(profile, os, browser) { }

    [Test]
    public void HealthCheck()
    {
      driver.Navigate().GoToUrl("http://bs-local.com:45691/check");
      Assert.IsTrue(Regex.IsMatch(driver.PageSource, "Up and running", RegexOptions.IgnoreCase));
    }
  }
}
