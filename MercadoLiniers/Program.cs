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

        public static List<Entidades.Categoria> scrapCategorias()
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

            List<Entidades.Categoria> lista = new List<Entidades.Categoria>();

            lista = Scrap.scrapTablaHacienda.scrapTabla(driver, lista);

            return lista;
        }
    }
}
