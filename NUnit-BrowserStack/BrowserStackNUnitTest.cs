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
    
    [SetUp]
    public void Init()
    {
      /* change here to add the following from config

            1. Browsers - ie / chrome
            2. os / version - windows x -> 10. ios on iphone 7-8. samsung android (real mobile)
            3. 

      */
      NameValueCollection caps = ConfigurationManager.GetSection("capabilities/" + profile) as NameValueCollection;
      NameValueCollection environments = ConfigurationManager.GetSection("environments/" + environment) as NameValueCollection;
      NameValueCollection mobileEnvironments = ConfigurationManager.GetSection("mobileEnvironments/" + environment) as NameValueCollection;
      NameValueCollection browsers = ConfigurationManager.GetSection("browsers") as NameValueCollection;

      DesiredCapabilities capability = new DesiredCapabilities();

      foreach (string key in caps.AllKeys)
      {
        capability.SetCapability(key, caps[key]);
      }

      foreach (string environmentKey in environments.AllKeys)
      {
        capability.SetCapability(environmentKey, environments[environmentKey]);

        foreach(string browserKey in browsers.AllKeys)
        {
          capability.SetCapability(browserKey, browsers[browserKey]);
        }

      }

    foreach (string mobileEnvironmentKey in mobileEnvironments.AllKeys)
    {
        capability.SetCapability(mobileEnvironmentKey, mobileEnvironments[mobileEnvironmentKey]);
    }

      String username = Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME");
      if(username == null)
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

      driver = new RemoteWebDriver(new Uri("http://"+ ConfigurationManager.AppSettings.Get("server") +"/wd/hub/"), capability);
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
