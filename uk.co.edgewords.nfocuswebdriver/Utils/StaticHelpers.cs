using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.co.edgewords.nfocuswebdriver.Utils
{
    internal static class StaticHelpers
    {
        //Static classes dont need to be isntantiated before use
        //However member methods will need to be passed the driver to use
        //As there is no constructor to set a field
        public static void WaitForElement(By locator, int timeToWaitInSeconds, IWebDriver driver)
        {
            WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(timeToWaitInSeconds));
            wait2.Until(drv => drv.FindElement(locator).Displayed);
        }

        public static void TakeScreenshotOfElement(IWebDriver driver, By locator, string filename)
        {
            IWebElement form = driver.FindElement(locator);
            ITakesScreenshot formss = form as ITakesScreenshot;
            var screenshotForm = formss.GetScreenshot();
            screenshotForm.SaveAsFile(@"D:\screenshots\" + filename, ScreenshotImageFormat.Png);
            TestContext.WriteLine("Screenshot taken - see report");
            TestContext.AddTestAttachment(@"D:\screenshots\" + filename);
        }
    }
}
