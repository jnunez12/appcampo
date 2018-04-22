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

        public static List<Entidades.CerealComun> loopCereales(List<Entidades.CerealComun> lista)
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
            int id = 1;
            IList<IWebElement> cerealesweb = driver.FindElements(By.XPath("//*[@id='flash-cotiz-tab']/ul/li"));

            foreach(IWebElement cerealweb in cerealesweb)
            {
                Entidades.CerealComun cereal = new Entidades.CerealComun();
                cereal.id = id;
                cereal.fechadato = Convert.ToDateTime(fechadato);
                cereal.nombre = cerealweb.FindElement(By.XPath(".//a")).Text;

                cereal = scrapMercados(driver, cerealweb, cereal);

                lista.Add(cereal);
                id++;
            }

            driver.Quit();

            return lista;
        }

        public static Entidades.CerealComun scrapMercados(IWebDriver driver, IWebElement cerealweb, Entidades.CerealComun cereal)
        {
            int countmercado = 1;
            IList<IWebElement> mercadosweb = driver.FindElements(By.XPath("//html//div[@id='flash-cotiz-tab']/div/div/div"));
            foreach(IWebElement mercadoweb in mercadosweb)
            {
                string nombremercado = mercadoweb.Text.ToUpper();
                switch (nombremercado)
                {
                    case "DISPONIBLE":
                        cereal.disponible = scrapMercadoDisponible(driver, cerealweb, countmercado);
                        cereal.disponible.nombre = "Disponible";
                        cereal.disponible.id = countmercado;
                        break;
                    case "FUTUROS MATBA":
                        cereal.futuros_matba = scrapMercadoFuturosMATba(driver, cerealweb, countmercado);
                        cereal.futuros_matba.nombre = "Futuros MATba";
                        cereal.futuros_matba.id = countmercado;
                        break;
                    case "FOB MAGYP":
                        cereal.fob_magyp = scrapMercadoFOBMAGyP(driver, cerealweb, countmercado);
                        cereal.fob_magyp.nombre = "FOB MAGyP";
                        cereal.fob_magyp.id = countmercado;
                        break;
                    case "CBOT":
                        cereal.cbot = scrapMercadoCBOT(driver, cerealweb, countmercado);
                        cereal.cbot.nombre = "CBOT";
                        cereal.cbot.id = countmercado;
                        break;
                    case "PRECIOS MAGYP":
                        cereal.precios_magyp = scrapMercadoPreciosMAGyP(driver, cerealweb, countmercado);
                        cereal.precios_magyp.nombre = "Precios MAGyP";
                        cereal.precios_magyp.id = countmercado;
                        break;
                    default:
                        break;
                }

                countmercado++;

            }
            return cereal;
        }

        public static Entidades.MercadoPreciosMAGyP scrapMercadoPreciosMAGyP(IWebDriver driver, IWebElement cerealweb, int countmercado)
        {
            int id = 1;
            Entidades.MercadoPreciosMAGyP mercado = new Entidades.MercadoPreciosMAGyP();
            IList<IWebElement> listatr = cerealweb.FindElements(By.XPath("//html//div[@id='flash-cotiz-tab']/div[1]/div/table[" + countmercado + "]/tbody/tr"));
            foreach (IWebElement tr in listatr)
            {
                Entidades.ItemPreciosMAGyP item = new Entidades.ItemPreciosMAGyP();
                item.id = id;
                try
                {
                    item.resol_42_07 = tr.FindElement(By.XPath(".//td[1]/a")).Text;
                }
                catch (Exception)
                {
                    continue;
                }
                item.precio = Convert.ToDouble(tr.FindElement(By.XPath(".//td[2]")).Text.Replace(".", ","));
                item.var = parseVar(tr.FindElement(By.XPath(".//td[3]/img")).GetAttribute("src"));
                item.fecha = Convert.ToDateTime(tr.FindElement(By.XPath(".//td[4]")).Text);
                mercado.items.Add(item);
                id++;
            }

            return mercado;
        }

        public static Entidades.MercadoCBOT scrapMercadoCBOT(IWebDriver driver, IWebElement cerealweb, int countmercado)
        {
            int id = 1;
            Entidades.MercadoCBOT mercado = new Entidades.MercadoCBOT();
            IList<IWebElement> listatr = cerealweb.FindElements(By.XPath("//html//div[@id='flash-cotiz-tab']/div[1]/div/table[" + countmercado + "]/tbody/tr"));
            foreach (IWebElement tr in listatr)
            {
                Entidades.ItemCBOT item = new Entidades.ItemCBOT();
                item.id = id;
                try
                {
                    item.posicion = tr.FindElement(By.XPath(".//td[1]/a")).Text;
                }
                catch (Exception)
                {
                    continue;
                }
                
                item.precio = Convert.ToDouble(tr.FindElement(By.XPath(".//td[2]")).Text.Replace(".", ","));
                item.var = parseVar(tr.FindElement(By.XPath(".//td[3]/img")).GetAttribute("src"));
                item.fecha = Convert.ToDateTime(tr.FindElement(By.XPath(".//td[4]")).Text);
                mercado.items.Add(item);
                id++;
            }

            return mercado;
        }

        public static Entidades.MercadoFOBMAGyP scrapMercadoFOBMAGyP(IWebDriver driver, IWebElement cerealweb, int countmercado)
        {
            int id = 1;
            Entidades.MercadoFOBMAGyP mercado = new Entidades.MercadoFOBMAGyP();
            IList<IWebElement> listatr = cerealweb.FindElements(By.XPath("//html//div[@id='flash-cotiz-tab']/div[1]/div/table[" + countmercado + "]/tbody/tr"));
            foreach (IWebElement tr in listatr)
            {
                Entidades.ItemFOBMAGyP item = new Entidades.ItemFOBMAGyP();
                item.id = id;
                try
                {
                    item.embarque = tr.FindElement(By.XPath(".//td[1]/a")).Text;
                }
                catch (Exception)
                {
                    continue;
                }
                
                item.precio = Convert.ToDouble(tr.FindElement(By.XPath(".//td[2]")).Text.Replace(".", ","));
                item.var = parseVar(tr.FindElement(By.XPath(".//td[3]/img")).GetAttribute("src"));
                item.fecha = Convert.ToDateTime(tr.FindElement(By.XPath(".//td[4]")).Text);
                mercado.items.Add(item);
                id++;
            }

            return mercado;
        }

        public static Entidades.MercadoFuturosMATba scrapMercadoFuturosMATba(IWebDriver driver, IWebElement cerealweb, int countmercado)
        {
            int id = 1;
            Entidades.MercadoFuturosMATba mercado = new Entidades.MercadoFuturosMATba();
            IList<IWebElement> listatr = cerealweb.FindElements(By.XPath("//html//div[@id='flash-cotiz-tab']/div[1]/div/table[" + countmercado + "]/tbody/tr"));
            foreach (IWebElement tr in listatr)
            {
                Entidades.ItemFuturoMATba item = new Entidades.ItemFuturoMATba();
                item.id = id;
                try
                {
                    item.posicion = tr.FindElement(By.XPath(".//td[1]/a")).Text;
                }
                catch (Exception)
                {
                    continue;
                }
                
                item.precio = Convert.ToDouble(tr.FindElement(By.XPath(".//td[2]")).Text.Replace(".", ","));
                item.var = parseVar(tr.FindElement(By.XPath(".//td[3]/img")).GetAttribute("src"));
                item.fecha = Convert.ToDateTime(tr.FindElement(By.XPath(".//td[4]")).Text);
                mercado.items.Add(item);
                id++;
            }

            return mercado;
        }

        public static Entidades.MercadoDisponible scrapMercadoDisponible(IWebDriver driver, IWebElement cerealweb, int countmercado)
        {
            int id = 1;
            Entidades.MercadoDisponible mercado = new Entidades.MercadoDisponible();
            IList<IWebElement> listatr = cerealweb.FindElements(By.XPath("//html//div[@id='flash-cotiz-tab']/div[1]/div/table[" + countmercado + "]/tbody/tr"));
            foreach(IWebElement tr in listatr)
            {
                Entidades.ItemDisponible item = new Entidades.ItemDisponible();
                item.id = id;
                try
                {
                    item.mercado = tr.FindElement(By.XPath(".//td[1]/a")).Text;
                }
                catch (Exception)
                {
                    continue;
                }
                
                item.precio =  Convert.ToDouble(tr.FindElement(By.XPath(".//td[2]")).Text.Replace(".",","));
                item.var = parseVar(tr.FindElement(By.XPath(".//td[3]/img")).GetAttribute("src"));
                item.fecha = Convert.ToDateTime(tr.FindElement(By.XPath(".//td[4]")).Text);
                mercado.items.Add(item);
                id++;
            }

            return mercado;
        }

        public static string parseVar(string var)
        {
            if (var.ToUpper().Contains("UP"))
            {
                return "up";
            }
            else
            {
                return "down";
            }
        }
    }
}
