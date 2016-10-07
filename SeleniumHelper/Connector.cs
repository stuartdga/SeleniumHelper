using System;
using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Remote;

namespace Selenium.Helper
{
    public enum Browser
    {
        Chrome,
        Firefox,
        PhantomJS
    }
    public class Connector
    {
        public static IWebDriver driver { get; set; }

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
            return Initialize(browser, true, "", seleniumHubURL);
        }

        public static IWebDriver Initialize(Browser browser, Uri seleniumHubURL, string operatingSystem = "", bool maximize = false)
        {
            return Initialize(browser, true, "", seleniumHubURL, operatingSystem, maximize);
        }

        public static IWebDriver Initialize(Browser browser, bool remote, string driverPath, Uri seleniumHubURL, string operatingSystem = "", bool maximize = true)
        {
            if (remote)
            {
                driver = InitializeRemote(browser, seleniumHubURL);
            }
            else
            {
                switch (browser)
                {
                    case Browser.PhantomJS:
                        if (driverPath == "")
                        {
                            driverPath = System.IO.Directory.GetCurrentDirectory();
                        }
                        driver = new PhantomJSDriver(driverPath);
                        break;
                    case Browser.Firefox:
                        var firefoxOptions = new FirefoxOptions();
                        firefoxOptions.UseLegacyImplementation = true;
                        driver = new FirefoxDriver(firefoxOptions);
                        break;
                    case Browser.Chrome:
                        // prevent annoying popup from Chrome regarding Developer Extensions
                        var chromeOptions = new ChromeOptions();
                        chromeOptions.AddArgument("--disable-extensions");
                        chromeOptions.AddArgument("--test-type");
                        if (maximize)
                            chromeOptions.AddArgument("--start-maximized");
                        driver = new ChromeDriver(chromeOptions);
                        break;
                }
            }

            if (maximize)
                driver.Manage().Window.Maximize();
            return driver;
        }

        public static IWebDriver InitializeRemote(Browser browser, Uri seleniumHubURL, string operatingSystem = "")
        {
            var capabilities = new DesiredCapabilities();
            if (seleniumHubURL == null || seleniumHubURL.ToString() == "")
            {
                try
                {
                    seleniumHubURL = new Uri(ConfigurationManager.AppSettings["SeleniumHubURL"].ToString().ToLower());
                }
                catch
                {
                    seleniumHubURL = null;
                }
            }
            if (seleniumHubURL != null && seleniumHubURL.ToString() != "")
            {
                if (operatingSystem != "")
                    capabilities.SetCapability(CapabilityType.Platform, operatingSystem);
                switch (browser)
                {
                    case Browser.Chrome:
                        capabilities = DesiredCapabilities.Chrome();
                        //capabilities.SetCapability(CapabilityType.BrowserName, "chrome");
                        break;
                    case Browser.Firefox:
                        capabilities = DesiredCapabilities.Firefox();
                        //capabilities.SetCapability(CapabilityType.BrowserName, "firefox");
                        break;
                }
                driver = new RemoteWebDriver(seleniumHubURL, capabilities);
            }
            else
                throw new Exception("URL for SeleniumHub must be provided");
            
            return driver;

        }

    }
}
