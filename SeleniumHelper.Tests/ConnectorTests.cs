using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Selenium.Helper;

namespace SeleniumHelper.Tests
{
    [TestClass]
    public class ConnectorTests
    {
        [TestMethod]
        public void InitializeFirefoxTest()
        {
            var driver = Connector.Initialize(Browser.Firefox);
            Assert.IsNotNull(driver);
            Assert.IsTrue(((OpenQA.Selenium.Firefox.FirefoxDriver)(driver)).Url == "about:blank");
            driver = Utility.ResetDriver(driver);
        }

        [TestMethod]
        public void InitializeChromeTest()
        {
            var driver = Connector.Initialize(Browser.Chrome);
            Assert.IsNotNull(driver);
            Assert.IsTrue(((OpenQA.Selenium.Chrome.ChromeDriver)(driver)).Url == "data:,");
            driver = Utility.ResetDriver(driver);
        }

        [TestMethod]
        public void InitializePhantomJSTest()
        {
            var driver = Connector.Initialize(Browser.PhantomJS);
            Assert.IsNotNull(driver);
            driver = Utility.ResetDriver(driver);
        }

        [TestMethod]
        public void InitializeRemoteFirefoxTest()
        {
            var seleniumHubURL = new Uri(ConfigurationManager.AppSettings["SeleniumHubURL"].ToString().ToLower());
            var driver = Connector.Initialize(Browser.Firefox, seleniumHubURL);
            Assert.IsNotNull(driver);
            Assert.IsTrue((((OpenQA.Selenium.Remote.RemoteWebDriver)(driver)).Capabilities).BrowserName.ToLower() == Browser.Firefox.ToString().ToLower());
            driver = Utility.ResetDriver(driver);
        }
    }

}
