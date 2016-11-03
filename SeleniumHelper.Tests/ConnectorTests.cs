using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Selenium.Helper;

namespace SeleniumHelper.Tests
{
    [TestClass]
    public class ConnectorTests
    {
        public const string CATEGORYCLASS = "Connector";

        [TestMethod, TestCategory(CATEGORYCLASS)]
        public void InitializeFirefoxTest()
        {
            var driver = Connector.Initialize(Browser.Firefox);
            Assert.IsNotNull(driver);
            Assert.IsTrue(((OpenQA.Selenium.Firefox.FirefoxDriver)(driver)).Url == "about:blank");
            driver = Utility.ResetDriver(driver);
        }

        [TestMethod, TestCategory(CATEGORYCLASS)]
        public void InitializeChromeTest()
        {
            var driver = Connector.Initialize(Browser.Chrome);
            Assert.IsNotNull(driver);
            Assert.IsTrue(((OpenQA.Selenium.Chrome.ChromeDriver)(driver)).Url == "data:,");
            driver = Utility.ResetDriver(driver);
        }

        [TestMethod, TestCategory(CATEGORYCLASS)]
        public void InitializePhantomJSTest()
        {
            var driver = Connector.Initialize(Browser.PhantomJS);
            Assert.IsNotNull(driver);
            driver = Utility.ResetDriver(driver);
        }

        [TestMethod, TestCategory(CATEGORYCLASS), TestCategory("RemoteWebDriver")]
        public void InitializeRemoteTest()
        {
            // a grid must be running for the following code to execute
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

            //Remove appSettings key SeleniumHubURL in app.config to execute the following
            //try
            //{
            //    driver = Connector.Initialize(Browser.Chrome, EmptyUri);
            //    driver = Utility.ResetDriver(driver);
            //}
            //catch (Exception ex)
            //{
            //    Assert.AreEqual(ex.Message, "URL for SeleniumHub must be provided");
            //}
        }

        [TestMethod, TestCategory(CATEGORYCLASS), TestCategory("RemoteWebDriver")]
        public void CaptureScreenshotTest()
        {
            // a grid must be running for the following code to execute
            var seleniumHubURL = new Uri(ConfigurationManager.AppSettings["SeleniumHubURL"].ToString().ToLower());
            var driver = Connector.Initialize(Browser.Chrome, seleniumHubURL);
            Assert.IsNotNull(driver);
            string file = RemoteWebDriverAugmented.CaptureScreenshot(driver as RemoteWebDriverAugmented, "C:\\Temp\\screenshot", "testimage.txt");
            Assert.IsTrue(System.IO.File.Exists(file));
            driver = Utility.ResetDriver(driver);
        }
    }

}
