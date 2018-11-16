using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;

namespace BrowserStack
{
    [TestFixture("single", "chrome")]
    public class SingleTest : BrowserStackNUnitTest
    {
        public SingleTest(string profile, string environment) : base(profile, environment) { }

        [Test]
        public void SearchGoogle()
        {
            driver.Navigate().GoToUrl("https://www.google.com/ncr");
            IWebElement query = driver.FindElement(By.Name("q"));
            query.SendKeys("BrowserStack");
            query.Submit();
            System.Threading.Thread.Sleep(5000);
            Assert.AreEqual("BrowserStack - Google Search", driver.Title);
        }

        [Test]
        public void GoToBbc()
        {
            DesiredCapabilities capability = new DesiredCapabilities();
            capability.SetCapability("browserName", "iPhone");
            capability.SetCapability("device", "iPhone 8 Plus");
            capability.SetCapability("realMobile", "true");
            capability.SetCapability("os_version", "11.0");
            capability.SetCapability("browserstack.user", "oliverbaylis3");
            capability.SetCapability("browserstack.key", "E6kpheLGzKeVeFJsAVfE");
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
