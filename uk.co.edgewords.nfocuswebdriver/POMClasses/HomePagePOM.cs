using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.co.edgewords.nfocuswebdriver.POMClasses
{
    internal class HomePagePOM
    {
        private IWebDriver _driver; //Field to hold a webdriver instance

        public HomePagePOM(IWebDriver driver) //Get the webdriver instance from the calling test
        {
            this._driver = driver;
        }

        //Locators
        IWebElement loginLink => _driver.FindElement(By.LinkText("Login To Restricted Area"));

        //ServiceMethod
        public void GoLogin()
        {
            loginLink.Click();
        }
        

    }
}
