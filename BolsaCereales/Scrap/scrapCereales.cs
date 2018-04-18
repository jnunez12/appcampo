using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using General;

namespace BolsaCereales.Scrap
{

    public static class scrapCereales
    {
        #region Variables globales
        static bool success = false;
        static int retry = 5;
        #endregion

        public static void loopCereales()
        {
            #region Iniciar driver
            IWebDriver driver = null;
            while (!success & retry > 0)
            {
                try
                {
                    driver = General.SeleniumUtility.iniciarDriver(driver, "firefox");
                    driver.Navigate().GoToUrl("http://www.bolsadecereales.com/");
                    success = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("CEREALES: Error al iniciar el driver");
                    retry--;
                }
            }
            #endregion

            string fechadato = driver.FindElement(By.XPath("//*[@id='flash-cotiz']/p/strong")).Text;

            IList<IWebElement> cerealesweb = driver.FindElements(By.XPath("//*[@id='flash-cotiz-tab']/ul/li"));

            foreach(IWebElement cerealweb in cerealesweb)
            {
                Entidades.Cereal cereal = new Entidades.Cereal();
                cereal.id = 1;
                cereal.fechadato = Convert.ToDateTime(fechadato);
                cereal.nombre = cerealweb.Text;

                scrapMercados(driver, cereal);

            }


        }

        public static void scrapMercados(IWebDriver driver, Entidades.Cereal cereal)
        {
            //IList<IWebElement> mercadosweb = 
        }
    }
}
