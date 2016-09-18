using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Internal;

namespace Selenium.Helper
{
    public enum FindByType
    {
        Id,
        XPath,
        CSSSelector
    }

    public static class Utility
    {
        public static bool WaitForElementReady(IWebDriver driver, string findElement, FindByType findByType = FindByType.Id, int sleetMilleseconds = 500, int waitSeconds = 30)
        {
            Thread.Sleep(sleetMilleseconds);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitSeconds));
            return wait.Until<bool>((d) =>
            {
                switch (findByType)
                {
                    case FindByType.Id:
                        return driver.FindElement(By.Id("" + findElement + "")).Displayed == true;
                    case FindByType.XPath:
                        return driver.FindElement(By.XPath("" + findElement + "")).Displayed == true;
                    case FindByType.CSSSelector:
                        return driver.FindElement(By.CssSelector("" + findElement + "")).Displayed == true;
                    default:
                        return driver.FindElement(By.Id("" + findElement + "")).Displayed == true;
                }
            });
        }

        public static bool WaitForElementByXpathReady(IWebDriver driver, string xPathString, int sleetMilleseconds = 500, int waitSeconds = 30)
        {
            Thread.Sleep(sleetMilleseconds);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitSeconds));
            return wait.Until<bool>((d) =>
            {
                return driver.FindElement(By.XPath("" + xPathString + "")).Displayed == true;
            });
        }

        public static bool WaitForElementByIdReady(IWebDriver driver, string id, int sleetMilleseconds = 500, int waitSeconds = 30)
        {
            Thread.Sleep(sleetMilleseconds);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitSeconds));
            return wait.Until<bool>((d) =>
            {
                return driver.FindElement(By.Id("" + id + "")).Displayed == true;
            });
        }

        public static bool GoTo(IWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
            return (driver.Url == url);


        }

        public static IWebElement InputValue(IWebDriver driver, string id, string value)
        {
            WaitForElementByIdReady(driver, id);
            var element = driver.FindElement(By.Id(id));
            element.Click();
            Thread.Sleep(250);
            element.Clear();
            Thread.Sleep(250);
            element.SendKeys(value);
            return element;
        }

        public static SelectElement SetDropDownItem(IWebDriver driver, string id, string elementvalue)
        {
            WaitForElementByIdReady(driver, id);
            var dropdown = new SelectElement(driver.FindElement(By.Id(id)));
            dropdown.SelectByText(elementvalue);
            return dropdown;
        }

        public static IWebDriver ResetDriver(IWebDriver driver)
        {
            try
            {
                if (driver != null)
                {
                    driver.Quit();
                    driver.Dispose();
                    driver = null;
                }
            }
            catch (Exception ex) { }
            return driver;
        }

        public static void Wait(IWebDriver driver, int miliseconds, int maxTimeOutSeconds = 60)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 1, maxTimeOutSeconds));
            var delay = new TimeSpan(0, 0, 0, 0, miliseconds);
            var timestamp = DateTime.Now;
            wait.Until(webDriver => (DateTime.Now - timestamp) > delay);
        }

        #region Element methods

        public static IWebDriver GetDriver(this IWebElement element)
        {
            return ((IWrapsDriver)element).WrappedDriver;
        }

        public static string GetID(IWebElement element)
        {
            return element.GetAttribute("id");
        }

        public static List<string> GetClasses(IWebElement element)
        {
            return element.GetAttribute("class").Split(' ').ToList<string>();
        }

        public static bool HasClass(IWebElement element, string className)
        {
            var classes = Utility.GetClasses(element);
            if (classes == null || classes.Count == 0)
                return false;
            return classes.Contains(className);
        }

        public static void SetAttribute(IWebElement element, string attribute, string value)
        {
            var driver = Utility.GetDriver(element);
            var executor = (IJavaScriptExecutor)driver;
            string script = "arguments[0].setAttribute(arguments[1], arguments[2])";
            executor.ExecuteScript(script, element, attribute, value);
        }
        #endregion

        public static bool ExtractManifestResourceToDisk(string filename, string folder, bool replace = true)
        {
            var targetFolder = @".\" + folder;
            var fullFileName = @".\" + folder + @"\" + filename;
            var assembly = Assembly.GetCallingAssembly();
            if (File.Exists(fullFileName) && !replace)
                return true;

            var uri = assembly.GetName().Name + "." + folder + "." + filename;

            using (Stream input = assembly.GetManifestResourceStream(uri))
            {
                if (input == null)
                    return false;

                using (Stream output = File.Create(fullFileName))
                {
                    input.CopyTo(output);
                }
            }
            return true;
        }

        public static bool ExtractManifestResourceToDisk(string filename, bool replace = true)
        {
            var fullFileName = @".\" + @"\" + filename;
            var assembly = Assembly.GetCallingAssembly();
            if (File.Exists(fullFileName) && !replace)
                return true;

            var uri = assembly.GetName().Name + "." + filename;

            using (Stream input = assembly.GetManifestResourceStream(uri))
            {
                if (input == null)
                    return false;

                using (Stream output = File.Create(fullFileName))
                {
                    input.CopyTo(output);
                }
            }
            return true;
        }
    }
}
