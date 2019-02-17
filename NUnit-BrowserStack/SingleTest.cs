using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;

namespace BrowserStack
{
    [TestFixture("single", "windows10", "chrome")]
    public class SingleTest : BrowserStackDesktopNUnitTest
    {
        public SingleTest(string profile, string os, string browserName) : base(profile, os, browserName) { }

        [Test]
        public void SearchGoogle()
        {
            driver.Navigate().GoToUrl("https://www.google.com/ncr");
            IWebElement query = driver.FindElement(By.Name("q"));
            query.SendKeys("Ipswich Town");
            query.Submit();
            System.Threading.Thread.Sleep(5000);
            Assert.AreEqual("Ipswich Town - Google Search", driver.Title);
        }

        [Test]
        public void Bbc()
        {
            DesiredCapabilities capability = new DesiredCapabilities();
            capability.SetCapability("browserName", "iPhone");
            capability.SetCapability("device", "iPhone 8 Plus");
            capability.SetCapability("realMobile", "true");
            capability.SetCapability("os_version", "11.0");
            capability.SetCapability("browserstack.user", "oli181");
            capability.SetCapability("browserstack.key", "KTrC3Dz9WiqhyT4m8rFU");
            capability.SetCapability("browserstack.console", "errors");

            driver = new RemoteWebDriver(
              new Uri("http://hub-cloud.browserstack.com/wd/hub/"), capability
            );
            driver.Navigate().GoToUrl("http://www.bbc.co.uk");
            Console.WriteLine(driver.Title);
            StringAssert.Contains("bbc", driver.Title.ToLowerInvariant());

            driver.Quit();
        }
    }
}
