// Generated by Selenium IDE
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using NUnit.Framework;


namespace uk.co.edgewords.nfocuswebdriver.TestCases
{
    //[TestFixture] //Optional annotation on a class that contains tests. NUnit does not need this.
    public class SelIDEDemoTest : Utils.BaseTest //Have this test class inherit setup, teardown and driver from a base class
    {
        //SelIDE [SetUp] and [TearDown] removed
        //They include some unncessary code, like setting up things for
        //javascript injection in to the browser
        //Why might you want to do that? Maybe change page styles to highlight an element before screenshots.


        [Test]
        public void selIDEDemo()
        {
            
            // Test name: SelIDEDemo
            // Step # | name | target | value
            // 1 | open | /webdriver2/ | 
            driver.Navigate().GoToUrl("https://www.edgewordstraining.co.uk/webdriver2/");
            // 2 | setWindowSize | 974x1032 | 
            //sets a particular window size - probably something you want to do especially if executing headless (see base class)
            driver.Manage().Window.Size = new System.Drawing.Size(974, 1032);

            //driver.Manage().Window.Maximize(); //Or just maximise the window
            
            //Remember many pages will have alternative rendering styles depending on screen size.
            //This might go as far as also changing /how/ to interact with a page
            //e.g. instead of a full navbar across the top, maybe navigation gets collapsed in to a hamburger menu structure on mobile
            //In that case you would probably need seperate tests
            //and might also consider Appium - mobile testing based on WebDriver - https://appium.io/

            // 3 | click | css=.last span | 
            driver.FindElement(By.CssSelector(".last span")).Click();
            // 4 | click | linkText=Forms | 
            driver.FindElement(By.LinkText("Forms")).Click();
            // 5 | click | id=textInput | 
            driver.FindElement(By.Id("textInput")).Click();
            // 6 | type | id=textInput | Steve
            driver.FindElement(By.Id("textInput")).SendKeys("Steve");
            // 7 | click | id=checkbox | 
            driver.FindElement(By.Id("checkbox")).Click();
            // 8 | click | id=select | 
            driver.FindElement(By.Id("select")).Click();
            // 9 | select | id=select | label=Selection Two
            {
                var dropdown = driver.FindElement(By.Id("select"));
                dropdown.FindElement(By.XPath("//option[. = 'Selection Two']")).Click();
            }
            // 10 | click | linkText=Submit | 
            driver.FindElement(By.LinkText("Submit")).Click();
            // 11 | click | css=tr:nth-child(2) > td:nth-child(3) | 
            driver.FindElement(By.CssSelector("tr:nth-child(2) > td:nth-child(3)")).Click();
            // 12 | assertText | id=textInputValue | Steve
            Assert.That(driver.FindElement(By.Id("textInputValue")).Text, Is.EqualTo("Steve"));
            // 13 | click | css=tr:nth-child(4) > td:nth-child(3) | 
            driver.FindElement(By.CssSelector("tr:nth-child(4) > td:nth-child(3)")).Click();
            // 14 | verifyText | id=selectValue | Selection One

            //Dont stop the test if assertion fails - but still report a fail ultimately
            try
            {
                Assert.That(driver.FindElement(By.Id("selectValue")).Text, Is.EqualTo("Selection Two"));
            }
            catch (Exception)
            {
                Console.WriteLine("Got wrong selection");
                //throw; //if you throw test will stop
            }


            // 15 | click | linkText=CSS/XPath | 
            driver.FindElement(By.LinkText("CSS/XPath")).Click();
            
            
            //Unfortunately SelIDE isn't perfect. These drag and drop actions don't work as exported
            //and would have to be rewritten.
            // 16 | mouseDownAt | css=.ui-slider-handle | 8.512481689453125,6.86248779296875
            {
                var element = driver.FindElement(By.CssSelector(".ui-slider-handle"));
                Actions builder = new Actions(driver);
                builder.MoveToElement(element).ClickAndHold().Perform();
            }
            // 17 | mouseMoveAt | css=.ui-slider-handle | 8.512481689453125,6.86248779296875
            {
                var element = driver.FindElement(By.CssSelector(".ui-slider-handle"));
                Actions builder = new Actions(driver);
                builder.MoveToElement(element).Perform();
            }
            // 18 | mouseUpAt | css=.ui-slider-handle | 8.512481689453125,6.86248779296875
            {
                var element = driver.FindElement(By.CssSelector(".ui-slider-handle"));
                Actions builder = new Actions(driver);
                builder.MoveToElement(element).Release().Perform();
            }
            // 19 | click | css=.ui-slider-handle | 
            driver.FindElement(By.CssSelector(".ui-slider-handle")).Click();


        }
    }
}