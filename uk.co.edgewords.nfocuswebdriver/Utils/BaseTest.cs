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
        //This base class holds code that is common to all tests that inherit from it
        //Typically setup/teardown - i.e. things to do that are not the responsibility of a test
        //but need to be done before/after a test 
        protected IWebDriver driver; //field(variable belonging to an instance of a class) available to all methods
        protected string baseUrl; //'protected' is an 'access modifier' it defines if other classes can see this field and manipulate it
        protected string browser; //a field that wil be set via a commandline option/environment variable

        [SetUp]
        public void SetUp()
        {

            browser = TestContext.Parameters["browser"]; //Reads value from runsettings file or commandline
                                                         //note that if neither is set this will be null
                                                         //ToDo: there should be a null check (if null then...) to handle this anticipated problem
            
            //browser = Environment.GetEnvironmentVariable("browser"); //or the browser could be set by an env var. Again there should be a null check.
            
            //You may also want to get and set the baseUrl(starting point for tests) here

            Console.WriteLine("Read in browser var from commandline: " + browser);
            browser = browser.ToLower().Trim(); //anticipate args/env vars being mixed case and potentially having whitespace e.g. 'firefox '

            switch (browser)
            {
                case "firefox":
                    //WebDrievr 4.6+ : no need to add an appropriate driver server nuget package to project
                    //Selenium Manager will fetch the appropriate driver for us
                    driver = new FirefoxDriver(); break;
                case "chrome":
                    //Choosing a different browser is now as simple as instantiating it's driver class
                    //This is why we don't declare driver as a browser specific class, but instead the more generic IWebDriver
                    //ChromeDriver, FirefoxDriver,IEDriver etc are all types of IWebDriver, but not each other
                    driver = new ChromeDriver(); break;
                case "firefoxbrowseroptions":
                    //Drivers can be passed options to set/change behaviour
                    //In Sel v4 this is done via a browser specific options class
                    //In Sel v3 and lowe it was done via DesiredCapabilities class

                    //First make the appropriate options object
                    FirefoxOptions firefoxOptions = new FirefoxOptions();

                    //Then start setting options e.g.
                    //GeckoDriver cant find the path to firefox.exe for some reason - so set it manually
                    //options.BrowserExecutableLocation = @"c:\path\to\browser\firefox.exe";

                    //Headless mode - you wont see fx running but check task manager - it is. Screenshots etc still work.
                    //may even work better as I don't think elements need to be scrolled in to view first, whereas in headed mode they may need to be
                    //Headless runs a little bit faster (no screen painting required) and is desireable for systems not connected to a display e.g. a CI server
                    firefoxOptions.AddArgument("--headless"); //also works for chrome
                    
                    driver = new FirefoxDriver(firefoxOptions); break;
                case "chromeremote":
                    //Using the Selenium Grid/Server on another machine
                    //Broserstack, saucelabs etc provide commercial instances
                    //and would need extra arguments set - e.g. an api key granting access to their service
                    //as well as options to set the requested OS, and versions (OS & browser)

                    ChromeOptions chromeOptions = new ChromeOptions(); //In selv4 when setting up the remote connection pass an opropriate options object to get that browser
                    driver = new RemoteWebDriver(new Uri("http://addressofselserver:andport/"), chromeOptions);
                    break;
                default:
                    Console.WriteLine("Unknown browser - starting chrome");
                    driver = new ChromeDriver();
                    break;
            }
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
