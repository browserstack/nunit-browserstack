using NUnit.Framework;
using OpenQA.Selenium;
using System.Text.RegularExpressions;

namespace BrowserStack
{
  [TestFixture]
  [Category("sample-local-test")]
  public class SampleLocalTest : BrowserStackNUnitTest
  {
    public SampleLocalTest() : base() { }

    [Test]
    public void TunnelCheck()
    {
      driver.Navigate().GoToUrl("http://bs-local.com:45454/");
      Assert.True(Regex.IsMatch(driver.Title, @"/BrowserStack Local/i", RegexOptions.IgnoreCase));
    }
  }
}
