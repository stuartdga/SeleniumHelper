using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium.Helper;

namespace SeleniumHelper.Tests
{
    /// <summary>
    /// Summary description for UtilityTests
    /// </summary>
    [TestClass]
    public class UtilityTests
    {
        public IWebDriver driver;
        public string html = System.IO.Directory.GetCurrentDirectory().ToLower().Replace("\\", "/").Replace("\\", "/").Replace("c:", "file:///c:/") + "/tests.html";

        public UtilityTests()
        {
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        // Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void Initialize()
        {
            //driver = Connector.Initialize("phantomjs");
            //driver = Connector.Initialize(Browser.Firefox);
            driver = Connector.Initialize(Browser.Chrome);
        }

        // Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void Cleanup()
        {
            driver = Utility.ResetDriver(driver);
        }

        [TestMethod]
        public void WaitForElementReadyTest()
        {
            Assert.IsNotNull(driver);
            driver.Navigate().GoToUrl(html);
            Assert.IsTrue(driver.Title.ToLower() == "tests");
            Assert.IsTrue(Utility.WaitForElementReady(driver, "Text1"));
            Assert.IsTrue(Utility.WaitForElementReady(driver, "//*[@id='Text1']", FindByType.XPath));
        }

        [TestMethod]
        public void WaitForElementByIdReadyTest()
        {
            Assert.IsNotNull(driver);
            driver.Navigate().GoToUrl(html);
            Assert.IsTrue(driver.Title.ToLower() == "tests");
            Assert.IsTrue(Utility.WaitForElementByIdReady(driver, "Text1"));
        }

        [TestMethod]
        public void WaitForElementByXPathReadyTest()
        {
            Assert.IsNotNull(driver);
            driver.Navigate().GoToUrl(html);
            Assert.IsTrue(driver.Title.ToLower() == "tests");
            Assert.IsTrue(Utility.WaitForElementByXpathReady(driver, "//*[@id='Text1']"));
        }

        [TestMethod]
        public void GoToTest()
        {
            Assert.IsNotNull(driver);
            Utility.GoTo(driver, html);
            Assert.IsTrue(driver.Title.ToLower() == "tests");
        }

        [TestMethod]
        public void InputValueTest()
        {
            // this test currently fails in PhantomJS
            Assert.IsNotNull(driver);
            Utility.GoTo(driver, html);
            var element = Utility.InputValue(driver, "Text1", "asdf");
            Assert.AreEqual(element.GetAttribute("value"), "asdf");
        }

        [TestMethod]
        public void SetDropDownItem()
        {
            Assert.IsNotNull(driver);
            Utility.GoTo(driver, html);
            var element = Utility.SetDropDownItem(driver, "dropdown", "2");
            Assert.AreEqual(element.SelectedOption.Text, "2");
        }

        [TestMethod]
        public void ExtractManifestResourceToDisk()
        {
            Assert.IsTrue(Utility.ExtractManifestResourceToDisk("embedded.txt"));
            Assert.IsTrue(Utility.ExtractManifestResourceToDisk("embedded.txt", false));
            Assert.IsTrue(Utility.ExtractManifestResourceToDisk("embeddedinfolder.txt", "Data"));
        }

        [TestMethod]
        public void ResetDriver()
        {
            driver = Utility.ResetDriver(driver);
            Assert.IsNull(driver);
            driver = Utility.ResetDriver(driver);
            Assert.IsNull(driver);
        }

        #region Element helper tests

        [TestMethod]
        public void GetDriver()
        {
            string id = "label1";
            Assert.IsNotNull(driver);
            Utility.GoTo(driver, html);
            if (Utility.WaitForElementByIdReady(driver, id))
            {
                var element = driver.FindElement(By.Id(id));
                var driver2 = Utility.GetDriver(element);
                Assert.AreEqual(driver, driver2);
            }
        }

        [TestMethod]
        public void GetClasses()
        {
            string id = "label1";
            Assert.IsNotNull(driver);
            Utility.GoTo(driver, html);
            if (Utility.WaitForElementByIdReady(driver, id))
            {
                var element = driver.FindElement(By.Id(id));
                var classes = Utility.GetClasses(element);
                Assert.AreEqual(classes.Count, 2);
            }
        }

        [TestMethod]
        public void HasClass()
        {
            string id = "label1";
            string className = "class1"; 
            Assert.IsNotNull(driver);
            Utility.GoTo(driver, html);
            if (Utility.WaitForElementByIdReady(driver, id))
            {
                var element = driver.FindElement(By.Id(id));
                Assert.IsTrue(Utility.HasClass(element, className));
            }
            id = "Text1";
            if (Utility.WaitForElementByIdReady(driver, id))
            {
                var element = driver.FindElement(By.Id(id));
                Assert.IsFalse(Utility.HasClass(element, className));
            }
        }

        [TestMethod]
        public void SetAttribute()
        {
            string id = "Text1";
            string attribute = "class";
            string value = "class1";
            Assert.IsNotNull(driver);
            Utility.GoTo(driver, html);
            if (Utility.WaitForElementByIdReady(driver, id))
            {
                var element = driver.FindElement(By.Id(id));
                Utility.SetAttribute(element, attribute, value);
                Assert.IsTrue(Utility.HasClass(element, value));
            }
        }

        [TestMethod]
        public void CaptureScreenShot()
        {
            Assert.AreEqual(Utility.CaptureScreenShot(null, ""), "");
            Assert.AreEqual(Utility.CaptureScreenShot(driver, ""), "");
            Utility.ResetDriver(driver);

            //// a grid must be running for the following code to execute
            //// app.config must be updated with CaptureScreenshot = true
            //var seleniumHubURL = new Uri(ConfigurationManager.AppSettings["SeleniumHubURL"].ToString().ToLower());
            //driver = Connector.Initialize(Browser.Chrome, seleniumHubURL);
            //Assert.IsNotNull(driver);
            //Utility.GoTo(driver, "http://127.0.0.1:4444/grid/console");
            //string result = Utility.CaptureScreenShot(driver, System.Reflection.MethodInfo.GetCurrentMethod().Name);
            //string path = ConfigurationManager.AppSettings["ScreenShotPath"].ToString();
            //Assert.IsTrue(result.StartsWith(path));
        }

        #endregion
    }
}
