using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uk.co.edgewords.nfocuswebdriver.Utils;

namespace uk.co.edgewords.nfocuswebdriver.TestCases
{
    internal class CustomWaitExample : BaseTest
    {
        [Test]
        public void TryOutSomeWaits()
        {
            driver.Url = "https://www.edgewordstraining.co.uk/webdriver2/docs/dynamicContent.html";
            driver.FindElement(By.CssSelector("#delay")).Clear();
            driver.FindElement(By.CssSelector("#delay")).SendKeys("5");
            driver.FindElement(By.LinkText("Load Content")).Click(); //Load apple

            //No wait - will die if not in try catch
            try
            {
                IWebElement apple = driver.FindElement(By.CssSelector("#image-holder > img"));
            }
            catch (Exception e) //Will catch any exception type
            {
                Console.WriteLine("Throws NoSuchElementException");
                Console.WriteLine(e.ToString());
            }

            //Not enough waiting time
            try
            {
                //WebDriverWait
                var customPollingWait = new WebDriverWait(driver, TimeSpan.FromSeconds(1))
                {
                    Message = "Did not find apple in only 1 sec",
                    PollingInterval = TimeSpan.FromMilliseconds(100) //default is 500 
                };

                customPollingWait.Until(drv => drv.FindElement(By.CssSelector("#image-holder > img")));

            }
            catch (Exception e)
            {
                Console.WriteLine("Throws WebDriverTimeoutException");
                Console.WriteLine(e.ToString());
            }

            //Now we'll wait long enough to get the apple
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(6));
            //Waits will retry until the passed function no longer throws an exception, returns false or null
            //So if the function returns a web element we can store that for use

            IWebElement appleFound = wait.Until(drv => drv.FindElement(By.CssSelector("#image-holder > img")));
            appleFound.Click();
            Console.WriteLine("Found the apple!");

            //Stale elements
            driver.FindElement(By.LinkText("Clear Content")).Click(); //Get rid of the apple
            driver.FindElement(By.LinkText("Load Content")).Click(); //Load apple
            Thread.Sleep(6); //uncoditional wait to allow plenty of time

            try
            {
                appleFound.Click();
            }
            catch (Exception e)
            {
                Console.WriteLine("That just failed with a StaleELementException");
                Console.WriteLine(e.ToString());
                //One 'fix' could be to now refind the element now it's been reloaded
                appleFound = wait.Until(drv => drv.FindElement(By.CssSelector("#image-holder > img")));
            }
            appleFound.Click(); //Exception caught earlier, apple refound above. This is now ok here

            //Imagine that apple keeps being reloaded for a period of time
            //We might wait for it to appear(fine), but by the time we try to use it, it has gone stale
            //Can we make a wait that's more resiliant
            //The apple not only needs to be found, but it needs to be stable i.e. not dissapear after a short period of time

            //Lets reset the apple delay time to 1s, then inject some js that will call the load function 5 times (making the apple go stale 5 times in just over 5 seconds)

            driver.FindElement(By.CssSelector("#delay")).Clear();
            driver.FindElement(By.CssSelector("#delay")).SendKeys("1");

            var js = driver as IJavaScriptExecutor;

            string makeAppleGoStale = @"(function(){
    for (let i = 0; i < 5; i++) {
        setTimeout(function(){loadAjax();console.log('That apple looks off to me')},i*1000)
    }
})();";

            js.ExecuteScript(makeAppleGoStale);

            js.ExecuteScript("console.log('hi')"); //Note that this executes straight away and doesnt wait for the previous js to return
            
            
            //As such lets see if we can make a wait that deals with stale elements
            //The end of this test attempts to click the apple a number of times. This will fail if the wait doesnt wait for the element to become stable

            WebDriverWait staleWaiter = new WebDriverWait(driver, TimeSpan.FromSeconds(10)); //Being generous on the total time

            //1st attempt - Goes stale (as expected)
            //IWebElement maybeStaleApple = staleWaiter.Until(drv => drv.FindElement(By.CssSelector("#image-holder > img")));

            //2nd attempt - Nope
            //staleWaiter.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            //IWebElement maybeStaleApple = staleWaiter.Until(drv => drv.FindElement(By.CssSelector("#image-holder > img")));
            //But I didn't think that would work - at find time the element would not be stale, so ignoring a StaleElementReferenceException is a bit silly

            //3rd attempt seems to work

            //Set a polling period (retry wait after x time)
            //This is both the wait retry period and the time the element must remain stable for within the total wait
            staleWaiter.PollingInterval = TimeSpan.FromSeconds(2);
            
            var polltime = staleWaiter.PollingInterval.TotalMilliseconds;
            Console.WriteLine("Polltime is: " + polltime.ToString());

            //Example of a custom wait using WebDriverWait
            //The function will be retried until it does not return null or false, or throw an exception that is not ignored
            //NoSuchElement is ignored automatically
            var maybeStaleApple = staleWaiter.Until((drv) =>
            {
                IWebElement apple = drv.FindElement(By.CssSelector("#image-holder > img")); //If no such el, exception forces return to retry wait

                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                bool pollexpired = false;
                try
                {
                    while(!pollexpired)
                    {
                        bool appleIsThere = apple.Enabled; //Initially (presumably) true, we found it earlier - but it may go stale. If it does this will throw.
                        pollexpired = stopWatch.ElapsedMilliseconds > staleWaiter.PollingInterval.TotalMilliseconds;
                    }
                    //To get here the poll must have expired AND apple.Enabled has not caused a StaleElementException
                    Console.WriteLine("Poll expired and I still have the apple! Assuming safe to return...");
                    return apple; //if poll expires and we still have a good apple return it - this ends the wait(s)
                }
                catch (StaleElementReferenceException)
                {
                    Console.WriteLine("Element not stable - retry wait");
                    return null; //Retry wait if it goes stale during polling period
                }
                finally
                {
                    Console.WriteLine("Stopwatch finished at: " + stopWatch.ElapsedMilliseconds.ToString());
                    stopWatch.Stop();
                }
            });



            //If the apple goes stale this will fail
            for (int i = 0; i < 30; i++) //30 (Arbitary number of) clicks just to see if the apple is there
            {
                Console.WriteLine("Clicky apple");
                maybeStaleApple.Click();
            }





            Thread.Sleep(3000);
        }
    }
}
