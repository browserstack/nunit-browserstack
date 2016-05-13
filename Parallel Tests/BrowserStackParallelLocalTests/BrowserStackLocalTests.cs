using BrowserStack;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;

namespace BrowserStackParallelLocalTests
{
  [SetUpFixture]
  public class SetupLocal
  {
    Local browserStackLocal;

    [OneTimeSetUp]
    public void startLocal()
    {
      browserStackLocal = new Local();
      List<KeyValuePair<string, string>> bsLocalArgs = new List<KeyValuePair<string, string>>() {
        new KeyValuePair<string, string>("key", BROWSERSTACK_ACCESS_KEY),
        new KeyValuePair<string, string>("forcelocal", ""),
        new KeyValuePair<string, string>("logfile", @"C:\Users\Admin\Desktop\local.log")
      };
      browserStackLocal.start(bsLocalArgs);
    }
    [OneTimeTearDown]
    public void stopLocal()
    {
      if (browserStackLocal != null)
      {
        browserStackLocal.stop();
      }
    }
  }

  [TestFixture, Parallelizable(ParallelScope.Fixtures)]
  public class BrowserStackLocalTests
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

      capability.SetCapability("browserstack.local", true);

      capability.SetCapability("build", "Sample NUnit Tests");
      capability.SetCapability("name", "Sample NUnit Local Test");
      capability.SetCapability("browserstack.user", BROWSERSTACK_USERNAME);
      capability.SetCapability("browserstack.key", BROWSERSTACK_ACCESS_KEY);

      Console.WriteLine("Capabilities" + capability.ToString());

      driver = new RemoteWebDriver(new Uri("http://hub.browserstack.com:80/wd/hub/"), capability);
    }


    [Test]
    public void SearchGoogleLocal()
    {
      driver.Navigate().GoToUrl("http://www.google.com");
      StringAssert.Contains("Google", driver.Title);
      IWebElement query = driver.FindElement(By.Name("q"));
      query.SendKeys("Browserstack");
      query.Submit();
    }

    [Test]
    public void SearchGoogleLocalClone()
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
