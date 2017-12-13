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
        public static IWebDriver WebDriver { get; set; }

        private const Uri EmptyUri = null;

        public static IWebDriver Initialize(Browser browser)
        {
            return Initialize(browser, false, "", EmptyUri);
        }

        public static IWebDriver Initialize(Browser browser, string driverPath)
        {
            return Initialize(browser, false, driverPath, EmptyUri);
        }

        public static IWebDriver Initialize(Browser browser, Uri seleniumHubURL, string operatingSystem = "", bool maximize = false)
        {
            return Initialize(browser, true, "", seleniumHubURL, operatingSystem, maximize);
        }

        public static IWebDriver Initialize(Browser browser, bool remote, string driverPath, Uri seleniumHubURL, string operatingSystem = "", bool maximize = true)
        {
            if (remote)
            {
                WebDriver = InitializeRemote(browser, seleniumHubURL);
            }
            else
            {
                switch (browser)
                {
                    case Browser.PhantomJS:
                        WebDriver = (driverPath == "") ? new PhantomJSDriver(System.IO.Directory.GetCurrentDirectory()) : new PhantomJSDriver(driverPath);
                        break;
                    case Browser.Firefox:
                        var firefoxOptions = new FirefoxOptions();
                        WebDriver = new FirefoxDriver(firefoxOptions);
                        break;
                    default:  // default is Chrome
                        // prevent annoying popup from Chrome regarding Developer Extensions
                        var chromeOptions = new ChromeOptions();
                        chromeOptions.AddArgument("--disable-extensions");
                        chromeOptions.AddArgument("--test-type");
                        if (maximize)
                        {
                            chromeOptions.AddArgument("--start-maximized");
                        }
                        WebDriver = new ChromeDriver(chromeOptions);
                        break;
                }
            }

            if (maximize)
            {
                WebDriver.Manage().Window.Maximize();
            }
            return WebDriver;
        }

        public static IWebDriver InitializeRemote(Browser browser, Uri seleniumHubURL, string operatingSystem = "")
        {
            var capabilities = new DesiredCapabilities();
            var url = seleniumHubURL;
            if (url == null || url.ToString() == "")
            {
                try
                {
                    url = new Uri(ConfigurationManager.AppSettings["SeleniumHubURL"].ToLower(System.Globalization.CultureInfo.InvariantCulture));
                }
                catch
                {
                    url = null;
                }
            }
            if (url != null && url.ToString() != "")
            {
                if (operatingSystem != "")
                {
                    capabilities.SetCapability(CapabilityType.Platform, operatingSystem);
                }
                capabilities = (browser == Browser.Firefox) ? DesiredCapabilities.Firefox() : DesiredCapabilities.Chrome();
                WebDriver = new RemoteWebDriverAugmented(url, capabilities);
            }
            else
            {
                throw new Exception("URL for SeleniumHub must be provided");
            }
            return WebDriver;

        }

        public static IWebDriver GetRemoteChromeDriver()
        {
            var options = new ChromeOptions
            {
                BinaryLocation = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe"
            };
            DesiredCapabilities capabilities = DesiredCapabilities.Chrome();
            capabilities.SetCapability(ChromeOptions.Capability, options);
            WebDriver = new RemoteWebDriver(new Uri("http://192.168.2.4:4444/wd/hub"), capabilities);
            return WebDriver;
        }
    }
}
