using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Interactions;
using System.Threading;


namespace Selenium_Zaliczenie
{
    class LoginTests
    {
        public IWebDriver driver;
        private const string ChromeDriverDirectory = "sciezka do projektu";
        private string passwordCorrect;
        private const string websiteForTest = "https://www.abra-meble.pl/";
        private const string userEmail = "podac swoj email";
        

        [SetUp]
        public void startBrowser()
        {
            driver = new ChromeDriver(ChromeDriverDirectory);
            driver.Manage().Window.Maximize();
            passwordCorrect = "podac haslo do strony";
            driver.Url = websiteForTest;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(6);
        }

        [Test]
        public void beforeLogin()
        {
            Thread.Sleep(6000);
            IWebElement pipe1 = driver.FindElement(By.XPath("/ html / body / div[4] / div[1] / div[2] / div[2] / div[1] / div"));
            IWebElement pipa1 = driver.FindElement(By.XPath("/ html / body / div[3] / div / a"));
            pipe1.Click();
            pipa1.Click();
            driver.Manage().Window.Maximize();
            IWebElement DivUserBeforeLogin = driver.FindElement(By.XPath("//*[@id=\"header\"]/div[1]/div/div"));
            Console.WriteLine(DivUserBeforeLogin.Text);
            bool isLogged = DivUserBeforeLogin.Text != "Zaloguj lub zarejestruj";

            Assert.IsFalse(isLogged, "Użytkownik zalogowany przed logowaniem");
            Thread.Sleep(3000);
        }

        [Test]
        public void EnterLoginPage()
        {
            Thread.Sleep(6000);
            IWebElement pipe1 = driver.FindElement(By.XPath("/ html / body / div[4] / div[1] / div[2] / div[2] / div[1] / div"));
            IWebElement pipa1 = driver.FindElement(By.XPath("/ html / body / div[3] / div / a"));
            pipe1.Click();
            pipa1.Click();
            driver.Manage().Window.Maximize();
            IWebElement ButtonZalogujSie = driver.FindElement(By.XPath("//*[@id=\"header\"]/div[1]/div/div/a[1]"));
            IWebElement DivLogowanie = driver.FindElement(By.Id("loginPopup"));
          
            bool isVisible = DivLogowanie.Displayed;
            Assert.IsFalse(isVisible);
            ButtonZalogujSie.Click();

            isVisible = DivLogowanie.Displayed;

            Assert.IsTrue(isVisible);
            Thread.Sleep(3000);
        }

        
        [Test]
        public void loginCorrect()
        {
           // Thread.Sleep(6000);
           // IWebElement pipe1 = driver.FindElement(By.XPath("/ html / body / div[4] / div[1] / div[2] / div[2] / div[1] / div"));
           // IWebElement pipa1 = driver.FindElement(By.XPath("/ html / body / div[3] / div / a"));
           // pipe1.Click();
            //pipa1.Click();
            driver.Manage().Window.Maximize();
            EnterLoginPage();
            IWebElement InputTextFieldUserEmail = driver.FindElement(By.Id("email2"));
            IWebElement InputTextFieldUserPassword = driver.FindElement(By.Id("pass2"));
            IWebElement ButtonLogin = driver.FindElement(By.Id("send2"));

            InputTextFieldUserEmail.SendKeys(userEmail);
            InputTextFieldUserPassword.SendKeys(passwordCorrect);
            ButtonLogin.Click();

            Thread.Sleep(7000);

            IWebElement DivLogged = driver.FindElement(By.XPath("//*[@id=\"header\"]/div[1]/div/div"));
            Assert.IsTrue(DivLogged.Text.Contains("Moje konto"),$"Nie zalogowano porawnie: {DivLogged.Text}");

        }

        [Test]
        public void loginWrong()
        {
            //Thread.Sleep(6000);
           // IWebElement pipe1 = driver.FindElement(By.XPath("/ html / body / div[4] / div[1] / div[2] / div[2] / div[1] / div"));
           // IWebElement pipa1 = driver.FindElement(By.XPath("/ html / body / div[3] / div / a"));
           // pipe1.Click();
          //  pipa1.Click();
            driver.Manage().Window.Maximize();
            EnterLoginPage();

            string passwordWrong = "wrongPasswd";
            IWebElement InputTextFieldUserEmail = driver.FindElement(By.Id("email2"));
            IWebElement InputTextFieldUserPassword = driver.FindElement(By.Id("pass2"));
            IWebElement ButtonLogin = driver.FindElement(By.Id("send2"));
            IWebElement DivLogowanie = driver.FindElement(By.Id("loginPopup"));

            try { 
            InputTextFieldUserEmail.SendKeys(userEmail);
            InputTextFieldUserPassword.SendKeys(passwordWrong);
            ButtonLogin.Submit();
            Thread.Sleep(3000);
            driver.SwitchTo().Alert().Accept();
            }
            catch(NoSuchElementException e) {
                
            }

            Thread.Sleep(3000);

        }


        [TearDown]
        public void closeBrowser()
        {
            driver.Close();
        }
    }
    
    class SearchTests
    {
       
        IWebDriver driver;

        private const string ChromeDriverDirectory = "C:/Users/Mateusz Dec/Desktop/zaliczenieMDTestowanie/zaliczenieMDTestowanie";
        private const string websiteForTest = "https://www.abra-meble.pl/";
        private const int V = 10;

        [SetUp]
        public void startBrowserAndLogIn()
        {
            driver = new ChromeDriver(ChromeDriverDirectory);
            driver.Manage().Window.Maximize();
            driver.Url = websiteForTest;
        }
        
        [Test]
        public void Search()
        {
            Thread.Sleep(6000);
            IWebElement pipe1 = driver.FindElement(By.XPath("/ html / body / div[4] / div[1] / div[2] / div[2] / div[1] / div"));
            IWebElement pipa1 = driver.FindElement(By.XPath("/ html / body / div[3] / div / a"));
            pipe1.Click();
            pipa1.Click();
            driver.Manage().Window.Maximize();
            Thread.Sleep(5000);
            IWebElement Search1 = driver.FindElement(By.Id("search"));

            Search1.Click();

            Search1.SendKeys("krzesło");

            IWebElement lupa = driver.FindElement(By.XPath("//*[@id=\"search_mini_form\"]/div/button"));

            lupa.Click();
            Thread.Sleep(5000);
            IJavaScriptExecutor jsee = (IJavaScriptExecutor)driver;

            jsee.ExecuteScript("window.scrollBy(0,250)");
            Thread.Sleep(9000);

            IWebElement Search3 = driver.FindElement(By.XPath("/html/body/div[8]/div[2]/div[1]/div/div/div/div/div/div/div/select/option[2]"));

            Search3.Click();

          //  IWebElement Search2 = driver.FindElement(By.XPath("/html/body/div[8]/div[2]/div[1]/div/div/div/div/div/div/div/select"));

           // Search2.Click();

            Thread.Sleep(7000);

        }
        
        [Test]
        public void CheckList()
        {
            Thread.Sleep(6000);
            IWebElement pipe1 = driver.FindElement(By.XPath("/ html / body / div[4] / div[1] / div[2] / div[2] / div[1] / div"));
            IWebElement pipa1 = driver.FindElement(By.XPath("/ html / body / div[3] / div / a"));
            pipe1.Click();
            pipa1.Click();
            IWebElement Search1 = driver.FindElement(By.XPath("/html/body/header/div[3]/div/ul/li[3]/a"));
            IWebElement Search2 = driver.FindElement(By.XPath("/html/body/header/div[3]/div/ul/li[3]/ul/li[6]/a"));
            
            Thread.Sleep(3000);
            Search1.Click();
            Search2.Click();
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            Thread.Sleep(2000);
            jse.ExecuteScript("window.scrollBy(0,1700)");
            Thread.Sleep(2000);

            IWebElement Search4 = driver.FindElement(By.XPath(" /html/body/div[9]/div[2]/div[3]/div[2]/a"));
            Search4.Click();
            Thread.Sleep(2000);
            IJavaScriptExecutor jsee = (IJavaScriptExecutor)driver;
           
            jsee.ExecuteScript("window.scrollBy(0,250)");
            Thread.Sleep(2000);

            jse.ExecuteScript("window.scrollBy(0,-1950)");
            Thread.Sleep(3000);

        }

        
        [Test]
        public void NextChoice()
        {
            Thread.Sleep(6000);
            IWebElement pipe1 = driver.FindElement(By.XPath("/ html / body / div[4] / div[1] / div[2] / div[2] / div[1] / div"));
            IWebElement pipa1 = driver.FindElement(By.XPath("/ html / body / div[3] / div / a"));
            pipe1.Click();
            pipa1.Click();
            IWebElement NextPage = driver.FindElement(By.XPath("  //*[@id=\"main\"]/div/section/div/div/section/div/div/div/div/div/div[2]/div[1]/a[2]"));
            Thread.Sleep(3000);
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("window.scrollBy(0,250)");
            Thread.Sleep(3000);
            NextPage.Click();
            Thread.Sleep(1000);
            NextPage.Click();
            Thread.Sleep(1000);
            NextPage.Click();
            Thread.Sleep(1000);
            NextPage.Click();
            Thread.Sleep(1000);
            NextPage.Click();

            Thread.Sleep(3000);
            IJavaScriptExecutor jsee = (IJavaScriptExecutor)driver;
            jsee.ExecuteScript("window.scrollBy(0,1230)");
            Thread.Sleep(4000);
            IWebElement upup = driver.FindElement(By.XPath("/ html / body / footer / div[1]"));

            upup.Click();


            Thread.Sleep(3000);


        }
        
        [Test]
        public void CHeckProduct()
        {
            Thread.Sleep(6000);
            IWebElement pipe1 = driver.FindElement(By.XPath("/ html / body / div[4] / div[1] / div[2] / div[2] / div[1] / div"));
            IWebElement pipa1 = driver.FindElement(By.XPath("/ html / body / div[3] / div / a"));
            pipe1.Click();
            pipa1.Click();
            IWebElement Search1 = driver.FindElement(By.XPath("//*[@id=\"header\"]/div[3]/div/ul/li[2]/a"));
            IWebElement Search2 = driver.FindElement(By.XPath("//*[@id=\"header\"]/div[3]/div/ul/li[2]/ul/li[3]/a"));
            Thread.Sleep(7000);
            Search1.Click();
            Search2.Click();
            Thread.Sleep(7000);
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("window.scrollBy(0,850)");
            Thread.Sleep(6000);
            IWebElement Search3 = driver.FindElement(By.XPath("/html/body/div[9]/div[2]/div[3]/div[1]/div/div[1]/a/div/div/div[1]/div/img"));
            Search3.Click();
            Thread.Sleep(6000);
            IJavaScriptExecutor jsee = (IJavaScriptExecutor)driver;
            jsee.ExecuteScript("window.scrollBy(0,150)");
            Thread.Sleep(5000);
            IWebElement Search7 = driver.FindElement(By.XPath("/html/body/div[10]/div/div[1]/div[1]/div/div[2]/div[1]/div/div[2]/div/a[2]"));

            Search7.Click();
            Thread.Sleep(1000);
            Search7.Click();
            Thread.Sleep(1000);
           Search7.Click();
            Thread.Sleep(1000);

            Search7.Click();
            Thread.Sleep(3000);

            IJavaScriptExecutor jseee = (IJavaScriptExecutor)driver;
            jseee.ExecuteScript("window.scrollBy(0,550)");
            Thread.Sleep(5000);
            IWebElement Search9 = driver.FindElement(By.XPath("/html/body/div[10]/div/div[2]/ul/li[2]/a"));

            Search9.Click();
            Thread.Sleep(4000);

            IWebElement Search10 = driver.FindElement(By.XPath("  /html/body/div[10]/div/div[2]/ul/li[1]/a"));

            Search10.Click();
            Thread.Sleep(4000);
            IJavaScriptExecutor jseeee = (IJavaScriptExecutor)driver;
            jseeee.ExecuteScript("window.scrollBy(0,-250)");
            Thread.Sleep(4000);

            IWebElement Search11 = driver.FindElement(By.XPath("   /html/body/div[10]/div/div[1]/div[2]/form/div[2]/div[1]/div[2]/input[2]"));
            Search11.Click();
            Thread.Sleep(4000);

            IWebElement Search12 = driver.FindElement(By.XPath("  /html/body/div[10]/div/div[1]/div[2]/form/div[2]/div[1]/div[2]/input[3]"));
            Search12.Click();
            Thread.Sleep(4000);

            IWebElement Search13 = driver.FindElement(By.XPath("/html/body/div[10]/div/div[1]/div[2]/form/div[2]/div[3]/button"));

            Search13.Click();
            Thread.Sleep(9000);

            IWebElement Search14 = driver.FindElement(By.XPath(" /html/body/header/div[2]/div/div[6]/a/div/img"));

            Search14.Click();
            Thread.Sleep(5000);
            IWebElement Search15 = driver.FindElement(By.XPath(" /html/body/div[8]/div/form/fieldset/table/tbody/tr/td[5]/a/i"));
            Search15.Click();
            Thread.Sleep(5000);



          
        }
        
        [Test]
        public void Checkflap()
        {
            Thread.Sleep(6000);
            IWebElement pipe1 = driver.FindElement(By.XPath("/ html / body / div[4] / div[1] / div[2] / div[2] / div[1] / div"));
            IWebElement pipa1 = driver.FindElement(By.XPath("/ html / body / div[3] / div / a"));
            pipe1.Click();
            pipa1.Click();
         

            IWebElement pip1 = driver.FindElement(By.XPath("  /html/body/header/div[1]/div/ul[1]/li/a"));
            Thread.Sleep(8000);
            pip1.Click();
            Thread.Sleep(8000);
            
            IWebElement pip2 = driver.FindElement(By.XPath("/html/body/div[7]/div/div[2]/div[1]/div/a[1]"));
            pip2.Click();
            Thread.Sleep(4000);

            IWebElement pip3 = driver.FindElement(By.XPath("/html/body/div[7]/div/div[2]/div[1]/div/a[2]"));
            pip3.Click();
            Thread.Sleep(4000);

            IWebElement pip4 = driver.FindElement(By.XPath("/html/body/footer/div[1]"));
            pip4.Click();
            Thread.Sleep(3000);

            IJavaScriptExecutor jseeee = (IJavaScriptExecutor)driver;
            jseeee.ExecuteScript("window.scrollBy(0,150)");
            Thread.Sleep(4000);

            IWebElement pip6 = driver.FindElement(By.XPath("/html/body/div[7]/div/div[2]/div[1]/div/a[3]"));
            pip6.Click();
            Thread.Sleep(3000);

            IWebElement pip25 = driver.FindElement(By.XPath("/html/body/footer/div[1]"));
            pip25.Click();
            Thread.Sleep(3000);

          
            IJavaScriptExecutor jseeeee = (IJavaScriptExecutor)driver;
            jseeeee.ExecuteScript("window.scrollBy(0,150)");
            Thread.Sleep(4000);

            IWebElement pip7 = driver.FindElement(By.XPath("/html/body/div[7]/div/div[2]/div[1]/div/a[4]"));
            pip7.Click();
            Thread.Sleep(3000);

            IWebElement pip28 = driver.FindElement(By.XPath("/html/body/footer/div[1]"));
            pip28.Click();
            Thread.Sleep(3000);

            IJavaScriptExecutor jseeeeee = (IJavaScriptExecutor)driver;
            jseeeeee.ExecuteScript("window.scrollBy(0,150)");
            Thread.Sleep(4000);


            IWebElement pip45 = driver.FindElement(By.XPath(" /html/body/div[7]/div/div[2]/div[1]/div/a[5]"));
            pip45.Click();
            Thread.Sleep(3000);


            IWebElement pip255 = driver.FindElement(By.XPath("/html/body/footer/div[1]"));
            pip255.Click();
            Thread.Sleep(3000);




        }


        [Test]
        public void CheckCity()
        {
            Thread.Sleep(6000);
            IWebElement pipe1 = driver.FindElement(By.XPath("/ html / body / div[4] / div[1] / div[2] / div[2] / div[1] / div"));
            IWebElement pipa1 = driver.FindElement(By.XPath("/ html / body / div[3] / div / a"));
            pipe1.Click();
            pipa1.Click();
            IWebElement pip1 = driver.FindElement(By.XPath("  /html/body/header/div[3]/div/ul/li[9]/a"));
            Thread.Sleep(2000);
            pip1.Click();
            Thread.Sleep(2000);
            IJavaScriptExecutor jseeeeee = (IJavaScriptExecutor)driver;
            jseeeeee.ExecuteScript("window.scrollBy(0,450)");
            Thread.Sleep(3000);


            IWebElement pip2 = driver.FindElement(By.XPath("  / html / body / section / div / div[2] / div / div[4] / div[1] / div[1] / div[2] / div / button / span[1]"));
            Thread.Sleep(4000);
            pip2.Click();

            Thread.Sleep(3000);
            IWebElement pip3 = driver.FindElement(By.XPath(" /html/body/section/div/div[2]/div/div[4]/div[1]/div[1]/div[2]/div/ul/li[6]/a"));
            
            pip3.Click();
            Thread.Sleep(4000);

            IWebElement pip4 = driver.FindElement(By.XPath("/html/body/section/div/div[2]/div/div[4]/div[1]/div[2]/div/div/button"));

            pip4.Click();
            Thread.Sleep(3000);

            IWebElement pip5 = driver.FindElement(By.XPath("/html/body/section/div/div[2]/div/div[4]/div[1]/div[2]/div/div/ul/li[4]/a"));

            pip5.Click();

            Thread.Sleep(3000);

            IWebElement pip6 = driver.FindElement(By.XPath("/html/body/section/div/div[2]/div/div[4]/div[1]/div[3]/div/div/button"));

            pip6.Click();

            Thread.Sleep(5000);




        }

        [TearDown]
        public void closeBrowser()
        {
            driver.Close();
        }

    }

}
