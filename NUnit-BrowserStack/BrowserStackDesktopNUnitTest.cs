using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;


namespace BrowserStack
{
    [TestFixture]
    public class BrowserStackDesktopNUnitTest
    {
        protected IWebDriver driver;
        protected string profile;
        protected string os;
        protected string osVersion;
        protected string browserName;
        protected string browserVersion;
        private Local browserStackLocal;

        public BrowserStackDesktopNUnitTest(string profile, string os, string browserName) //todo replace string with enum
        {
            this.profile = profile;
            this.os = os;
            this.browserName = browserName;
        }

        [SetUp]
        public void Init()
        {
            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            NameValueCollection caps = ConfigurationManager.GetSection("capabilities/" + profile) as NameValueCollection;
            NameValueCollection environment = ConfigurationManager.GetSection("desktopEnvironments/" + os) as NameValueCollection;
            NameValueCollection browser = ConfigurationManager.GetSection("desktopBrowsers/" + browserName) as NameValueCollection;
                       
            DesiredCapabilities capability = new DesiredCapabilities();

            foreach (string key in caps.AllKeys)
            {
                capability.SetCapability(key, caps[key]);
            }

            foreach (string key in environment.AllKeys)
            {
                capability.SetCapability(key, environment[key]);
            }

            foreach (string key in browser.AllKeys)
            {
                capability.SetCapability(key, browser[key]);
            }

            String username = Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME");
            if (username == null)
            {
                username = ConfigurationManager.AppSettings.Get("user");
            }

            String accesskey = Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY");
            if (accesskey == null)
            {
                accesskey = ConfigurationManager.AppSettings.Get("key");
            }

            capability.SetCapability("browserstack.user", username);
            capability.SetCapability("browserstack.key", accesskey);

            if (capability.GetCapability("browserstack.local") != null && capability.GetCapability("browserstack.local").ToString() == "true")
            {
                browserStackLocal = new Local();
                List<KeyValuePair<string, string>> bsLocalArgs = new List<KeyValuePair<string, string>>() {
          new KeyValuePair<string, string>("key", accesskey)
        };
                browserStackLocal.start(bsLocalArgs);
            }

            driver = new RemoteWebDriver(new Uri("http://" + ConfigurationManager.AppSettings.Get("server") + "/wd/hub/"), capability);
        }

        [TearDown]
        public void Cleanup()
        {
            driver.Quit();
            if (browserStackLocal != null)
            {
                browserStackLocal.stop();
            }
        }
    }
}
