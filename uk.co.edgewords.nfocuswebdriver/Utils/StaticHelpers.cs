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

        //This method now returns an IWebElement rather than void(nothing) so if the wait passes you get the element!
        public static IWebElement WaitForElement(By locator, int timeToWaitInSeconds, IWebDriver driver)
        {
            WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(timeToWaitInSeconds));
            return wait2.Until(drv => drv.FindElement(locator));
        }

        //In sel v4 we can take screenshots of elements - not just the whole page.
        public static void TakeScreenshotOfElement(IWebDriver driver, By locator, string filename)
        {
            IWebElement form = driver.FindElement(locator);
            
            //In sel v3 we might have to find the position and size of an element
            //before then cutting it out of the full page screenshot with some other image manip library
            //but not now... might still be ueful to know size/location at times though
            //int xFromLeft = form.Location.X;
            //int width = form.Size.Width;


            ITakesScreenshot formss = form as ITakesScreenshot;
            var screenshotForm = formss.GetScreenshot();
            screenshotForm.SaveAsFile(@"D:\screenshots\" + filename, ScreenshotImageFormat.Png); //ToDo: Timstamp screenshots so they are not overwritten
            TestContext.WriteLine("Screenshot taken - see report");
            TestContext.AddTestAttachment(@"D:\screenshots\" + filename);
        }
    }
}
