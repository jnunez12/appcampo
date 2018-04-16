using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using General;


namespace Monedas
{
    public class Program
    {
        #region Variables Globales"
        static bool success = false;
        static int retry = 5;
        static string procesos = "MONEDA";
        #endregion


        public static void Main(string[] args)
        {

        }
        public static void Monedas()
        {
            #region Iniciar driver
            IWebDriver driver = null;
            while (!success & retry > 0)
            {
                try
                {
                    driver = General.SeleniumUtility.iniciarDriver(driver, "firefox");
                    driver.Navigate().GoToUrl("http://www.dolarhoy.com");
                    success = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("MONEDA: Error al iniciar el driver");
                    retry--;
                }
            }
            #endregion

            driver.FindElement(By.XPath("//*[@id='table2']/tbody/tr[2]/td[1]/font/span/a")).Click();
            Dolar.scrapDolar(driver);
            string a = "a";

        }
    }
}
