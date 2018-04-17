using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;

namespace Monedas
{
    public static class General
    {

        /// <summary>
        /// Scrapea el resto de las monedas que no es dólar
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="lista"></param>
        /// <returns></returns>
        public static List<Entidades.Moneda> scrapMonedas(IWebDriver driver, List<Entidades.Moneda> lista)
        {
            int id = 2;
            IList<IWebElement> monedasweb = driver.FindElements(By.XPath("//html//div/table/tbody"));

            foreach(IWebElement monedaweb in monedasweb)
            {
                Entidades.Moneda moneda = new Entidades.Moneda();
                moneda.id = id;
                string textomoneda = "";
                try
                {
                    textomoneda = monedaweb.FindElement(By.XPath(".//tr/td/font/b")).Text.Trim();
                }
                catch (Exception)
                {
                    // si no es una moneda y es algun otro cuadro que ande por ahi lo salteo
                    continue;
                }

                
                moneda.nombre = parseNombre(textomoneda);
                if(moneda.nombre == "")
                {
                    continue;
                }

                List<string> codigos = getSimbolo(moneda.nombre);
                moneda.simbolo = codigos[0];
                moneda.cod_iso = codigos[1];

                moneda.dateUpdate = parseDate(textomoneda);

                IList<IWebElement> preciosweb = monedaweb.FindElements(By.XPath(".//tr"));
                for(int z = 4; z <= preciosweb.Count; z++)
                {
                    IWebElement precioweb = preciosweb[z-1];
                    Entidades.Precio precio = new Entidades.Precio();
                    precio.id = 1;
                    try
                    {
                        precio.tipo = precioweb.FindElement(By.XPath(".//td[1]")).Text;
                    }
                    catch (Exception)
                    {
                        try
                        {
                            precio.tipo = precioweb.FindElement(By.XPath(".//td[1]/font")).Text;
                        }
                        catch (Exception)
                        {
                            precio.tipo = "";
                        }
                        
                    }

                    if (precio.tipo == "" || precio.tipo == " ")
                    {
                        continue;
                    }else if (precio.tipo.ToUpper().Contains("PROMEDIO") || precio.tipo.ToUpper().Contains("PROM."))
                    {
                        precio.tipo = "promedio";
                    }else if (precio.tipo.ToUpper().Contains("MEJORES"))
                    {
                        precio.tipo = "mejores";
                    }

                    try
                    {
                        precio.compra = Convert.ToDouble(precioweb.FindElement(By.XPath(".//td[2]/b/font")).Text.Replace(".", ","));
                        precio.venta = Convert.ToDouble(precioweb.FindElement(By.XPath(".//td[3]/b/font")).Text.Replace(".", ","));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                    
                    moneda.listaPrecios.Add(precio);
                }

                id++;
                lista.Add(moneda);
            }

            return lista;
            
        }

        /// <summary>
        /// Agarra el texto principal de la tabla y obtiene el nombre de la moneda
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static string parseNombre(string texto)
        {
            string nombre = "";
            try
            {
                nombre = texto.Substring(0, texto.IndexOf(":") - 3).Trim();
            }
            catch (Exception)
            {
                nombre = "";
            }               
            return nombre;
        }

        /// <summary>
        /// Según el nombre de la moneda obtiene su síbmbolo
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public static List<string> getSimbolo(string nombre)
        {
            string simbolo = "";
            string iso = "";
            switch (nombre.ToUpper())
            {
                case "EURO":
                    simbolo = "€";
                    iso = "EUR";
                    break;
                case "REAL":
                    simbolo = "R$";
                    iso = "BRL";
                    break;
                case "PESO URUGUAYO":
                    simbolo = "$";
                    iso = "URU";
                    break;
                case "PESO CHILENO":
                    simbolo = "$";
                    iso = "CLP";
                    break;
                case "GUARANÍ":
                    simbolo = "";
                    iso = "PYG";
                    break;
                case "FRANCO SUIZO":
                    simbolo = "Fr";
                    iso = "CHF";
                    break;
                case "LIBRA ESTERLINA":
                    simbolo = "£";
                    iso = "GBP";
                    break;
                case "YEN":
                    simbolo = "¥";
                    iso = "JPY";
                    break;
                case "DÓLAR CANADIENSE":
                    simbolo = "C$";
                    iso = "CAD";
                    break;
                case "PESO MEXICANO":
                    simbolo = "$";
                    iso = "MXN";
                    break;
                case "DÓLAR AUSTRALIANO":
                    simbolo = "A$";
                    iso = "AUD";
                    break;
                default:
                    simbolo = "";
                    iso = "";
                    break;
            }

            List<string> lista = new List<string>();
            lista.Add(simbolo);
            lista.Add(iso);
            return lista;
        }

        /// <summary>
        /// Agarra el texto principal de la tabla y obtiene la fecha de actualización del dato
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static DateTime parseDate(string texto)
        {
            string fecha = texto.Substring(texto.IndexOf("/") - 3);
            string hora = texto.Substring(texto.IndexOf(":")-3, texto.Length - texto.IndexOf("HS.") -6);
            DateTime result = Convert.ToDateTime(fecha);
            result.Add(TimeSpan.Parse(hora));

            return result;
        }
    }
}
