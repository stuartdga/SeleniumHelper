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
        public void InitializeRemoteTest()
        {
            var seleniumHubURL = new Uri(ConfigurationManager.AppSettings["SeleniumHubURL"].ToString().ToLower());
            var driver = Connector.Initialize(Browser.Chrome, seleniumHubURL);
            Assert.IsNotNull(driver);
            Assert.IsTrue((((OpenQA.Selenium.Remote.RemoteWebDriver)(driver)).Capabilities).BrowserName.ToLower() == Browser.Chrome.ToString().ToLower());
            driver = Utility.ResetDriver(driver);

            Uri EmptyUri = null; // Url comes from app.config
            driver = Connector.Initialize(Browser.Chrome, EmptyUri);
            Assert.IsNotNull(driver);
            Assert.IsTrue((((OpenQA.Selenium.Remote.RemoteWebDriver)(driver)).Capabilities).BrowserName.ToLower() == Browser.Chrome.ToString().ToLower());
            driver = Utility.ResetDriver(driver);

            //clear app.config to execute the following
            //try
            //{
            //    Eriver = Connector.Initialize(Browser.Chrome, EmptyUri);
            //}
            //catch(Exception ex)
            //{
            //    Assert.AreEqual(ex.Message, "URL for SeleniumHub must be provided");
            //}
        }
    }

}
