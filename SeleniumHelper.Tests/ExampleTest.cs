using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.PhantomJS;
using Selenium.Helper;

namespace SeleniumHelper.Tests
{
	[TestClass]
	public class ExampleTest
	{
		public IWebDriver driver;
		public string html = System.IO.Directory.GetCurrentDirectory().Replace("\\", "/").Replace("C:", "file:///C:/") + "/tests.html";

		// Use TestInitialize to run code before running each test 
		[TestInitialize()]
		public void Initialize()
		{
			driver = Connector.Initialize(Browser.Firefox);
		}

		// Use TestCleanup to run code after each test has run
		[TestCleanup()]
		public void Cleanup()
		{
			driver = Utility.ResetDriver(driver);
		}

		// Example test of the PlaneBiz logon page
		[TestMethod]
		public void WikipediaSearch()
		{
			Utility.GoTo(driver, "https://www.wikipedia.org/");
			Assert.IsTrue(driver.Title.ToLower() == "wikipedia");
			Utility.InputValue(driver, "searchInput", "Chattanooga");
			driver.FindElement(By.CssSelector("button.pure-button.pure-button-primary-progressive")).Click();

            Thread.Sleep(500);
            Assert.IsTrue(driver.Title.StartsWith("Chattanooga"));
            Assert.AreEqual(driver.Url, "https://en.wikipedia.org/wiki/Chattanooga,_Tennessee");
        }
	}
}
