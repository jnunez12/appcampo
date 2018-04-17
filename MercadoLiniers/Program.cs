using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace MercadoLiniers
{
    public class Program
    {
        #region Variables Globales"
        static bool success = false;
        static int retry = 5;
        static string procesos = "MERCADO DE LINIERS";
        #endregion

        static void Main(string[] args)
        {
        }

        public static Entidades.TablaMercado scrapMercado()
        {
            #region Iniciar driver
            IWebDriver driver = null;
            while (!success & retry > 0)
            {
                try
                {
                    driver = General.SeleniumUtility.iniciarDriver(driver, "firefox");
                    driver.Navigate().GoToUrl("http://www.mercadodeliniers.com.ar/dll/hacienda1.dll/haciinfo000002");
                    success = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("MONEDA: Error al iniciar el driver");
                    retry--;
                }
            }
            #endregion

            Entidades.TablaMercado tabla = new Entidades.TablaMercado();

            tabla = Scrap.scrapTablaHacienda.scrapTablaMercados(driver, tabla);

            driver.Quit();

            return tabla;
        }
    }
}
