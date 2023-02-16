using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using uk.co.edgewords.nfocuswebdriver.Utils;
using static uk.co.edgewords.nfocuswebdriver.Utils.StaticHelpers; //bring in static helper methods for use in this class file

namespace uk.co.edgewords.nfocuswebdriver.TestCases
{
    [TestFixture]
    internal class HelloWebDriver : BaseTest //Inherit setup/teardown etc from BaseTest
    {
        

        [Test, Order(1), Category("Regression")]
        public void LoginLogout()
        {

            //Assert.Ignore("Not ready - WIP");
            //Attempt login

            //Console.WriteLine("Starting Test");
            //Debug.WriteLine("Warning");
            //TestContext.WriteLine("Hello");
            //var output = TestContext.Progress;
            //output.WriteLine("Hi");
            


            //Launch Chrome - may need appropriate NuGet package if Selenium Manager (4.6+) doesn't work
            //IWebDriver driver = new ChromeDriver();
            //Now handled by SetUp()

            driver.Url = "https://www.edgewordstraining.co.uk/webdriver2/"; //Navigate to URL.
            //driver.Navigate().GoToUrl("https://www.edgewordstraining.co.uk/webdriver2/"); //Alternative way
            
            driver.FindElement(By.LinkText("Login To Restricted Area")).Click();

            Console.WriteLine("On login form - perform login");
            
            string headingText = driver.FindElement(By.CssSelector("h1")).Text;

            Console.WriteLine("Heading of page is:" + headingText); //Write out the captured text to the output console
            Assert.IsTrue(headingText == "Access and Authentication","Wrong heading"); //Classical
            //Constraint
            //Assert.That(headingText == "Access and authentication","Wrong heading");
            try
            {
                Assert.That(headingText, Is.EqualTo("Access and authentication").IgnoreCase, "Wrong heading");
            }
            catch (AssertionException)
            {
                //Do nothing - allow test to continue if assertion fails
                //The test will still fail at the end
            }

            

            //Various asserts
            //Assert.Ignore("Cant be bothered");
            //Assert.Inconclusive("Not enough data to complete test");

            //Enter username
            driver.FindElement(By.Id("username")).Clear(); //Clear text box before use
            driver.FindElement(By.Id("username")).SendKeys("moretext"); //Then Type - Note if you sendkeys again they will be added - clear doesn't happen automatically
            driver.FindElement(By.Id("username")).SendKeys(Keys.Control + "a"); //Sending "special" keys
            driver.FindElement(By.Id("username")).SendKeys(Keys.Backspace); //This more accurately "simulates" 
            driver.FindElement(By.Id("username")).SendKeys("edgewords"); //The real username

            //Log the typed in username
            //string usernameText = driver.FindElement(By.Id("username")).Text; //WOnt work - inputs dont have a closing tag, therefore no inner Text
            string usernameText = driver.FindElement(By.Id("username")).GetAttribute("value");
            Console.WriteLine("The username typed in was: " + usernameText);

            //Enter password
            IWebElement passwordField = driver.FindElement(By.Id("password"));
            passwordField.Clear(); //can now reuse "passwordField"
            passwordField.SendKeys("edgewords123"); //= Cleaner more readable code

            //Screenshots
            ITakesScreenshot ssdriver = driver as ITakesScreenshot; //Cast the 'plain' webdrievr to a type that takes screenshots
            var screenshot = ssdriver.GetScreenshot(); //Get a page screenshot
            screenshot.SaveAsFile(@"D:\screenshots\fullpage.png",ScreenshotImageFormat.Png);

            //Screenshot an element
            //IWebElement form = driver.FindElement(By.Id("right-column"));
            //ITakesScreenshot formss = form as ITakesScreenshot;
            //var screenshotForm = formss.GetScreenshot();
            //screenshotForm.SaveAsFile(@"D:\screenshots\formscreenshot.png", ScreenshotImageFormat.Png);
            //TestContext.WriteLine("Screenshot taken - see report");
            //TestContext.AddTestAttachment(@"D:\screenshots\formscreenshot.png");

            TakeScreenshotOfElement(driver, By.Id("right-column"), "formusinghelper.png");

            //Click submit "button" - it is actually an a (anchor/link) element
            driver.FindElement(By.LinkText("Submit")).Click();

            //Need a wait
            //Thread.Sleep(3000);
            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            //wait.Until(drv => drv.FindElement(By.LinkText("Log Out")).Displayed);
            
            //Using an instance helper class
            //First instantiate the helper class and pass the driver
            MyHelpers help = new MyHelpers(driver);
            //Then using your help object you have access to the methods of the instance class
            help.WaitForElement(By.LinkText("Log Out"), 3);

            Console.WriteLine("Should now be on Add Record Page");
            //Attempt to logout
            driver.FindElement(By.LinkText("Log Out")).Click();

            //Handle the alert
            driver.SwitchTo().Alert().Accept();

            //Wait for interstial log out page to go
            //Thread.Sleep(7000);

            //Should be back on Log in
            help.WaitForElement(By.LinkText("Register"), 7); //Wait for something on the login page that is not on the Add record page
            driver.FindElement(By.Id("username")).Click(); //Will now be the username field on the login page

            string endHeadingText = driver.FindElement(By.CssSelector("h1")).Text;
            Assert.That(endHeadingText, Is.EqualTo("Access and Authentication"));

            //ToDo: custom waits
            //WebDriverWait wait3 = new WebDriverWait(driver, TimeSpan.FromSeconds(3)).IgnoreExceptionTypes(TypeOf(StaleElementReferenceException));
            //IWebElement username = wait3.Until(drv =>
            //{
            //    return driver.FindElement(By.Id("username"));
            //});

            //Finishing Test
            Console.WriteLine("Finished Test");

            
            //Quit the driver instance and clean up
            //driver.Close(); //Close current tab/window
            //driver.Quit(); //Closes browser (and connection) even if there are multiple tabs/windows
            //Now handled by TearDown()
        }



        [Test, Order(3)]
        public void DragDropDemo()
        {
            
            //Browser setup handled by SetUp()

            driver.Url = "https://www.edgewordstraining.co.uk/webdriver2/docs/cssXPath.html";
            //Using the static version in StaticHelpers.cs
            //No need to create an instance of an object
            //Just use the methods - but you'll have to pass the driver to the methods
            WaitForElement(By.CssSelector("#slider > a"), 3,driver);
            IWebElement gripper = driver.FindElement(By.CssSelector("#slider > a"));

            Actions action = new Actions(driver);

            IAction dragDrop = action.ClickAndHold(gripper)
                                    .MoveByOffset(10, 0)
                                    .MoveByOffset(10, 0)
                                    .MoveByOffset(10, 0)
                                    .MoveByOffset(10, 0)
                                    .Pause(TimeSpan.FromSeconds(1))
                                    .MoveByOffset(10, 0)
                                    .MoveByOffset(10, 0)
                                    .MoveByOffset(10, 0)
                                    .MoveByOffset(10, 0)
                                    .Release()
                                    .Build();

            dragDrop.Perform();

            Thread.Sleep(2000);
            //Browser TearDown handled by TearDown()
            //driver.Quit();
        }
        //[Test,Order(5)]//,Ignore("Test under development, no point running")]
        public void Test3()
        {
            
        }

    }
}
