using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.co.edgewords.nfocuswebdriver.Utils
{
    internal class MyHelpers
    {
        //This is an instance class - it will nee to be instantiated (turned in to an object) before use

        //Methods need access to a WebDriver instance. They can use this field, although an actual web driver will have to be placed in it first...
        private IWebDriver _driver;
        //...which can be done via the constructor
        //When the class is instantiated have the driver passed in
        public MyHelpers(IWebDriver driver)
        {
            this._driver = driver;
        }
        //A generic helper method that can be reused by any tests that need it
        public void WaitForElement(By locator, int timeToWaitInSeconds)
        {
            WebDriverWait wait2 = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeToWaitInSeconds));
            wait2.Until(drv => drv.FindElement(locator).Enabled);
        }
    }
}
