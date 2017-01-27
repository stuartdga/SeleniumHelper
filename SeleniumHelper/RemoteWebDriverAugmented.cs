using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Selenium.Helper
{
    public class RemoteWebDriverAugmented : RemoteWebDriver, ITakesScreenshot
    {
        public RemoteWebDriverAugmented(Uri RemoteAdress, ICapabilities capabilities)
            : base(RemoteAdress, capabilities)
        { }

        /// <summary>
        /// Gets a <see cref="Screenshot"/> object representing the image of the page on the screen.
        /// </summary>
        /// <returns>A <see cref="Screenshot"/> object containing the image.</returns>
        public new Screenshot GetScreenshot()
        {
            // Get the screenshot as base64.
            Response screenshotResponse = this.Execute(DriverCommand.Screenshot, null);
            string base64 = screenshotResponse.Value.ToString();
            return new Screenshot(base64);
        }

        /// <summary>
        /// Captures a screenshot of the browser running on the Selenium Grid node as base64.  
        /// Use an online coverter to view the actual image:  http://codebeautify.org/base64-to-image-converter
        /// </summary>
        /// <param name="driver">Remote driver</param>
        /// <param name="filePath">path where file will be written</param>
        /// <param name="fileName">the file name to be saved with datetime: fileName-yyyy-MM-dd_hh-mm-ss-tt.txt</param>
        /// <returns>name of the file containing the image data</returns>
        public static string CaptureScreenshot(RemoteWebDriverAugmented driver, string filePath = "", string fileName = "")
        {
            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            string file = "";
            if (filePath != "" && fileName != "")
            {
                if (!System.IO.Directory.Exists(filePath))
                {
                    System.IO.Directory.CreateDirectory(filePath);
                }
                var newFileName = string.Format(fileName + "-{0:yyyy-MM-dd_hh-mm-ss-tt}.txt", DateTime.Now);
                file = string.Format("{0}\\{1}", filePath, newFileName);
                System.IO.File.WriteAllText(file, ss.ToString());
            }
            return file;
        }
    }
}
