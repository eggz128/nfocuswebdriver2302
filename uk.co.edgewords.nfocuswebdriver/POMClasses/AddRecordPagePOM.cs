using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.co.edgewords.nfocuswebdriver.POMClasses
{
    internal class AddRecordPagePOM
    {
        private IWebDriver _driver;
        public AddRecordPagePOM(IWebDriver driver)
        {
            _driver = driver;
        }

        private IWebElement _usernameField => _driver.FindElement(By.Id("username"));

        //Username can have it's text value set and read using properties
        //https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/using-properties
        //This is more C#ish - my other examples are more Javaish
        public string Username
        {
            get => _usernameField.GetAttribute("value");
            set
            {
                _usernameField.Clear();
                _usernameField.SendKeys(value);
            }
        }



    }
}
