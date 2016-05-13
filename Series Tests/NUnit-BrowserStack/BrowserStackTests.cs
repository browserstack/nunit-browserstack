using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;

namespace NUnit_BrowserStack
{
  [TestFixture]
  public class BrowserStackTests
  {
    private IWebDriver driver;
    
    [SetUp]
    public void Init()
    {
      string browser = "chrome";
      string version = "50";
      string os = "Windows";
      string os_version = "10";
      DesiredCapabilities capability = new DesiredCapabilities();
      capability.SetCapability("browser", browser);
      capability.SetCapability("browser_version", version);
      capability.SetCapability("os", os);
      capability.SetCapability("os_version", os_version);

      capability.SetCapability("build", "Sample NUnit Tests");
      capability.SetCapability("name", "Sample NUnit Test");
      capability.SetCapability("browserstack.user", BROWSERSTACK_USERNAME);
      capability.SetCapability("browserstack.key", BROWSERSTACK_ACCESS_KEY);

      Console.WriteLine("Capabilities" + capability.ToString());

      driver = new RemoteWebDriver(new Uri("http://hub.browserstack.com:80/wd/hub/"), capability);
    }


    [Test]
    public void SearchGoogle()
    {
      driver.Navigate().GoToUrl("http://www.google.com");
      StringAssert.Contains("Google", driver.Title);
      IWebElement query = driver.FindElement(By.Name("q"));
      query.SendKeys("Browserstack");
      query.Submit();
    }

    [Test]
    public void SearchGoogleClone()
    {
      driver.Navigate().GoToUrl("http://www.google.com");
      StringAssert.Contains("Google", driver.Title);
      IWebElement query = driver.FindElement(By.Name("q"));
      query.SendKeys("Browserstack");
      query.Submit();
    }

    [TearDown]
    public void Cleanup()
    {
      driver.Quit();
    }
  }
}
