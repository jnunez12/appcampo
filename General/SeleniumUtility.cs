using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.PhantomJS;


namespace General
{
    public class SeleniumUtility
    {

        /// <summary>
        /// Inicializa el driver según el tipo pasado como parámetro, "firefox", "chrome", "phantomjs"
        /// </summary>
        /// <param name="url"></param>
        /// <param name="tipo"></param>
        /// <returns></returns>
        public static IWebDriver iniciarDriver(IWebDriver driver, string tipo)
        {
            try
            {
                switch (tipo)
                {
                    case "firefox":
                        driver = new FirefoxDriver();
                        break;
                    case "chrome":
                        driver = new ChromeDriver();
                        break;
                    case "phantonjs":
                        driver = new PhantomJSDriver();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("IniciarDriver Exception: " + e);
            }

            return driver;
        }

    }
}
