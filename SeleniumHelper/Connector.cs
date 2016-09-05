using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Remote;

namespace Selenium.Helper
{
    public enum Browser
    {
        Firefox,
        Chrome,
        PhantomJS
    }
    public class Connector
    {
        public static IWebDriver driver { get; set; }

        private const string WINDOWS = "WINDOWS";

        private const Uri EmptyUri = null;

        public static IWebDriver Initialize(Browser browser)
        {
            return Initialize(browser, false, "", EmptyUri, "");
        }

        public static IWebDriver Initialize(Browser browser, string driverPath)
        {
            return Initialize(browser, false, driverPath, EmptyUri, "");


        }

        public static IWebDriver Initialize(Browser browser, Uri seleniumHubURL)
        {
            return Initialize(browser, true, "", seleniumHubURL, WINDOWS);
        }

        public static IWebDriver Initialize(Browser browser, Uri seleniumHubURL, string operatingSystem)
        {
            return Initialize(browser, true, "", seleniumHubURL, operatingSystem);
        }

        public static IWebDriver Initialize(Browser browser, bool remote, string driverPath, Uri seleniumHubURL, string operatingSystem)
        {
            if (driverPath == "")
            {
                driverPath = System.IO.Directory.GetCurrentDirectory();
            }

            DesiredCapabilities capabilities = new DesiredCapabilities();

            if (remote)
            {
                if (seleniumHubURL != null)
                {
                    switch (browser)
                    {
                        case Browser.Firefox:
                            capabilities = new DesiredCapabilities();
                            capabilities = DesiredCapabilities.Firefox();
                            capabilities.SetCapability(CapabilityType.BrowserName, "firefox");
                            capabilities.SetCapability(CapabilityType.Platform, operatingSystem);
                            driver = new RemoteWebDriver(seleniumHubURL, capabilities);
                            break;
                        case Browser.Chrome:
                            capabilities = new DesiredCapabilities();
                            capabilities = DesiredCapabilities.Chrome();
                            capabilities.SetCapability(CapabilityType.BrowserName, "chrome");
                            capabilities.SetCapability(CapabilityType.Platform, operatingSystem);
                            driver = new RemoteWebDriver(seleniumHubURL, capabilities);
                            break;
                    }
                }
                else
                    throw new Exception("URL for SeleniumHub must be provided");
            }
            else
            {
                switch (browser)
                {
                    case Browser.PhantomJS:
                        if (driverPath != "")
                        {
                            driver = new PhantomJSDriver(driverPath);
                        }
                        else
                            throw new Exception("Path for PhantomJS must be provided");
                        break;
                    case Browser.Firefox:
                        driver = new FirefoxDriver();
                        break;
                    case Browser.Chrome:
                        driver = new ChromeDriver();
                        break;
                }
            }

            driver.Manage().Window.Maximize();
            return driver;
        }

    }
}
