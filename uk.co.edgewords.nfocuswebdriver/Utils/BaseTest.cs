using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace uk.co.edgewords.nfocuswebdriver.Utils
{
    public class BaseTest
    {
        //This base clas holds code that is common to all tests that inherit from it
        //Typically setup/teardown
        protected IWebDriver driver;
        protected string baseUrl;
        protected string browser;

        [SetUp]
        public void SetUp()
        {

            browser = TestContext.Parameters["browser"];
            //browser = Environment.GetEnvironmentVariable("browser");


            Console.WriteLine("Read in browser var from commandline: " + browser);
            browser = browser.ToLower().Trim();

            switch (browser)
            {
                case "firefox":
                    driver = new FirefoxDriver(); break;
                case "chrome":
                    driver = new ChromeDriver(); break;
                default:
                    Console.WriteLine("Unknown browser - starting chrome");
                    driver = new ChromeDriver();
                    break;
            }

            //driver.Url= baseUrl;

        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            //string something = "something";
        }
    }
}
