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
    public class BrowserStackNUnitTest
    {
        protected IWebDriver driver;
        protected string profile;
        protected string environment;
        private Local browserStackLocal;

        public BrowserStackNUnitTest(string profile, string environment)
        {
            this.profile = profile;
            this.environment = environment;
        }

        static DriverOptions getBrowserOption(String browser)
        {
            switch (browser)
            {
                case "chrome":
                    return new OpenQA.Selenium.Chrome.ChromeOptions();
                case "firefox":
                    return new OpenQA.Selenium.Firefox.FirefoxOptions();
                case "safari":
                    return new OpenQA.Selenium.Safari.SafariOptions();
                case "edge":
                    return new OpenQA.Selenium.Edge.EdgeOptions();
                default:
                    return new OpenQA.Selenium.Chrome.ChromeOptions();
            }
        }


        [SetUp]
        public void Init()
        {
            NameValueCollection caps = ConfigurationManager.GetSection("capabilities/" + profile) as NameValueCollection;
            NameValueCollection settings = ConfigurationManager.GetSection("environments/" + environment) as NameValueCollection;
            DriverOptions capability = getBrowserOption(settings["browser"]);

            capability.BrowserVersion = "latest";
            System.Collections.Generic.Dictionary<string, object> browserstackOptions = new Dictionary<string, object>();


            foreach (string key in caps.AllKeys)
            {
                browserstackOptions.Add(key, caps[key]);
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

            browserstackOptions.Add("userName", username);
            browserstackOptions.Add("accessKey", accesskey);

            if (caps.Get("local").ToString() == "true")
            {
                browserStackLocal = new Local();
                List<KeyValuePair<string, string>> bsLocalArgs = new List<KeyValuePair<string, string>>() {
                  new KeyValuePair<string, string>("key", accesskey)
                };
                browserStackLocal.start(bsLocalArgs);
            }
            capability.AddAdditionalOption("bstack:options", browserstackOptions);
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