using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.Web.Script.Serialization;

namespace Monedas
{
    public static class Dolar
    {

        /// <summary>
        /// Scrapea los datos de Dólar
        /// </summary>
        /// <param name="driver"></param>
        public static void scrapDolar(IWebDriver driver)
        {
            Entidades.Moneda moneda = new Entidades.Moneda();
            moneda.id = 1; // DANJO...
            moneda.nombre = "dolar";
            moneda.simbolo = "U$S";

            string fecha_actualizacion = driver.FindElement(By.XPath("/html/body/div[12]/center/table/tbody/tr[1]/td[2]/table/tbody/tr[1]/td/font/b")).Text;
            moneda.dateUpdate = Convert.ToDateTime(fecha_actualizacion.Substring(fecha_actualizacion.IndexOf("/") - 3));

            moneda.timeUpdate = TimeSpan.Parse(fecha_actualizacion.Substring(fecha_actualizacion.IndexOf("$") + 1, fecha_actualizacion.Length - fecha_actualizacion.IndexOf("HS.") - 6));
            moneda.dateUpdate.Add(moneda.timeUpdate);

            // precios
            int cantidadPrecios = driver.FindElements(By.XPath("//*[@id='table2']/tbody/tr")).Count;
            int id = 1;
            for(int i = 1; i<=cantidadPrecios; i++)
            {
                Entidades.Precio precio = new Entidades.Precio();
                precio = scrapPrecio(driver, precio, i);
                if(precio != null)
                {
                    precio.id = id;
                    id++;
                    moneda.listaPrecios.Add(precio);
                }
            }

            var json = new JavaScriptSerializer().Serialize(moneda);
            System.IO.File.WriteAllText(@"C:\Users\jonat\Desktop\PROYECTOS\scrapCampo\scrapCampo\dolar.json", json);
            string b = "";
        }

        /// <summary>
        /// Scrapea las distintas cotizaciones de dólar
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="precio"></param>
        /// <param name="tr"></param>
        /// <returns></returns>
        public static Entidades.Precio scrapPrecio(IWebDriver driver, Entidades.Precio precio, int tr)
        {
            try
            {
                precio.tipo = driver.FindElement(By.XPath("//*[@id='table2']/tbody/tr[" + tr + "]/td[1]")).Text;
            }
            catch (Exception)
            {
                return null;
            }
            if(precio.tipo.Trim() == "")
            {
                return null;
            }
            precio.compra = Convert.ToDouble(driver.FindElement(By.XPath("//*[@id='table2']/tbody/tr[" + tr + "]/td[2]/b")).Text.Replace(".",","));
            precio.venta = Convert.ToDouble(driver.FindElement(By.XPath("//*[@id='table2']/tbody/tr[" + tr + "]/td[3]/b")).Text.Replace(".", ","));

            return precio;
        }

        

    }
}
