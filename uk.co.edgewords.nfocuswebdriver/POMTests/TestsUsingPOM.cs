using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uk.co.edgewords.nfocuswebdriver.POMClasses;

namespace uk.co.edgewords.nfocuswebdriver.POMTests
{
    internal class TestsUsingPOM : Utils.BaseTest
    {
        [Test]
        public void LoginLogoutWithPOM()
        {
            driver.Url = "http://www.edgewordstraining.co.uk/webdriver2";
            HomePagePOM homePage = new HomePagePOM(driver);
            homePage.GoLogin();

            LoginPagePOM login = new LoginPagePOM(driver);
            //login.SetUsername("edgewords");//.SetPassword("edgewords123"); //Only works if SetUserneame returns "this" class instance
            
            //login.passwordField.SendKeys("edgewords123");
            //login.SetPassword("edgewords123");
            //login.SubmitForm();

            //Tests ask POM classes to return values
            bool didWeLogIn = login.LoginExpectSuccess("edgewords", "edgewords123");
            //It is up to the test to assert if the value is good or bad. Do not assert in the POM classes!
            Assert.That(didWeLogIn, Is.True, "Not logged in - but we should be!");
    
            Thread.Sleep(2000);

        }

    }
}
