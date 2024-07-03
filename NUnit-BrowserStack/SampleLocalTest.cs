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
    public void BStackTunnelCheck()
    {
      driver.Navigate().GoToUrl("http://bs-local.com:45454/");
      StringAssert.Contains("BrowserStack Local", driver.Title);
    }
  }
}
