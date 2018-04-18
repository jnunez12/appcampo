using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace MercadoLiniers.Scrap
{
    public class scrapVentas
    {
        static bool success = false;
        static int retry = 5;

        
        /// <summary>
        /// Loopea entre todos los consignatarios y devuelve una lista de todas las tablas de ventas
        /// </summary>
        /// <param name="lista"></param>
        /// <returns></returns>
        public static List<Entidades.Venta> srapTablaVentas(List<Entidades.Venta> lista)
        {
            #region Iniciar driver
            IWebDriver driver = null;
            while (!success & retry > 0)
            {
                try
                {
                    driver = General.SeleniumUtility.iniciarDriver(driver, "firefox");
                    driver.Navigate().GoToUrl("http://www.mercadodeliniers.com.ar/dll/hacienda1.dll/haciinfo000007");
                    success = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("MONEDA: Error al iniciar el driver");
                    retry--;
                }
            }
            #endregion

            int  cant_consignatarios = driver.FindElements(By.XPath("/html/body/table/tbody/tr[1]/td/table[2]/tbody/tr/td/table/tbody/tr[1]/td[1]/table/tbody/tr[2]/td[1]/table/tbody/tr[3]/td[2]/select/option")).Count;
            DateTime fechadato = Convert.ToDateTime(driver.FindElement(By.XPath("//*[@id='datepicker1']")).GetAttribute("value"));

            for(int i = 1; i<= cant_consignatarios; i++)
            {
                // datos generales de la venta
                Entidades.Venta venta = new Entidades.Venta();
                venta.id = 1;
                venta.fecha = fechadato;
                venta.especie = "vacunos";
                venta.tipo = "faena";

                // agregamos el consignatario
                IWebElement consignatarioweb = driver.FindElement(By.XPath("/html/body/table/tbody/tr[1]/td/table[2]/tbody/tr/td/table/tbody/tr[1]/td[1]/table/tbody/tr[2]/td[1]/table/tbody/tr[3]/td[2]/select/option[" + i + "]"));
                IWebElement ddlconsignatarios = driver.FindElement(By.XPath("/html/body/table/tbody/tr[1]/td/table[2]/tbody/tr/td/table/tbody/tr[1]/td[1]/table/tbody/tr[2]/td[1]/table/tbody/tr[3]/td[2]/select"));
                Entidades.Consignatario consignatario = new Entidades.Consignatario();
                consignatario.id = 1;
                consignatario.nombre = consignatarioweb.Text;

                venta.consignatario = consignatario;

                consignatarioweb.Click();
                driver.FindElement(By.XPath("//input[@id='Aceptar']")).Click();

               

                // agregamos los items
                venta = scrapVentasxConsignatario(driver, venta);

                lista.Add(venta);
            }

            driver.Quit();

            return lista;
        }


        /// <summary>
        /// Scrapea los valores para cada fila de la tabla de ventas para un consignatario específico
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="venta"></param>
        /// <returns></returns>
        public static Entidades.Venta scrapVentasxConsignatario(IWebDriver driver,Entidades.Venta venta)
        {
            IList<IWebElement> ventasweb = driver.FindElements(By.XPath("/html/body/table/tbody/tr[1]/td/table[2]/tbody/tr/td/table/tbody/tr[2]/td/table/tbody/tr"));
            if(ventasweb.Count == 1)
            {
                return null;
            }

            foreach(IWebElement ventaweb in ventasweb)
            {
                Entidades.ItemVenta item = new Entidades.ItemVenta();
                item.id = 1;
                try
                {
                    item.remitente = ventaweb.FindElement(By.XPath(".//td[1]")).Text;
                }
                catch (Exception)
                {
                    continue;
                }

                if(item.remitente.Trim() == "Total")
                {
                    continue;
                }

                item.localidad = ventaweb.FindElement(By.XPath(".//td[2]")).Text;
                item.provincia = ventaweb.FindElement(By.XPath(".//td[3]")).Text;
                item.cabezas = Convert.ToInt32(ventaweb.FindElement(By.XPath(".//td[4]")).Text);
                item.categoria = ventaweb.FindElement(By.XPath(".//td[5]")).Text;
                item.kgs = Convert.ToDouble(ventaweb.FindElement(By.XPath(".//td[6]")).Text);
                item.promedio = Convert.ToDouble( ventaweb.FindElement(By.XPath(".//td[7]")).Text);
                item.precio = Convert.ToDouble(ventaweb.FindElement(By.XPath(".//td[8]")).Text);

                venta.items.Add(item);
            }

            return venta;
        }
    }
}
