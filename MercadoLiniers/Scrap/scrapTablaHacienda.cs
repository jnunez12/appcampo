using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using General;

namespace MercadoLiniers.Scrap
{

    
    public static class scrapTablaHacienda
    {
        static bool success = false;
        static int retry = 5;
        /// <summary>
        /// Scrapea la tabla que tiene los precios por categorías
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="tabla"></param>
        /// <returns></returns>
        public static Entidades.TablaMercado scrapTablaMercados(Entidades.TablaMercado tabla)
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

            IWebElement dateinicial = driver.FindElement(By.XPath("//*[@id='datepicker1']"));
            tabla.fechainicial = Convert.ToDateTime(dateinicial.GetAttribute("value"));
            IWebElement datefinal = driver.FindElement(By.XPath("//*[@id='datepicker2']"));
            tabla.fechafinal = Convert.ToDateTime(datefinal.GetAttribute("value"));

            tabla.categorias = scrapCategorias(driver);

            driver.Quit();

            return tabla;
        }

        /// <summary>
        /// Scrapea cada fila de la tabla (categoría)
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static List<Entidades.Categoria> scrapCategorias(IWebDriver driver)
        {
            List<Entidades.Categoria> lista = new List<Entidades.Categoria>();

            IList<IWebElement> listatr = driver.FindElements(By.XPath("/html/body/table/tbody/tr[1]/td/table[2]/tbody/tr/td/table/tbody/tr[2]/td/table/tbody/tr"));
            int id = 1;
            foreach (IWebElement tr in listatr)
            {
                Entidades.Categoria categoria = new Entidades.Categoria();
                categoria.id = id;

                // obtenemos el nombre de la categoria
                try
                {
                    categoria.nombre = tr.FindElement(By.XPath(".//td[1]")).Text;
                }
                catch (Exception e)
                {
                    continue;
                }

                if(categoria.nombre == "" || categoria.nombre.Trim() == "Totales")
                {
                    continue;
                }

                // obtenemos los valores
                var retorno = scrapValores(tr);
                categoria.precios = retorno.Item1;
                categoria.totales = retorno.Item2;
                categoria.promedio = retorno.Item3;

                lista.Add(categoria);
                id++;
            }

            return lista;
        }


        /// <summary>
        /// Scrapea los valores de cada categoría
        /// </summary>
        /// <param name="tr"></param>
        /// <returns></returns>
        public static Tuple<List<Entidades.Item>, List<Entidades.Item>, List<Entidades.Item>> scrapValores(IWebElement tr)
        {
            List<Entidades.Item> precios = new List<Entidades.Item>();
            List<Entidades.Item> totales = new List<Entidades.Item>();
            List<Entidades.Item> promedio = new List<Entidades.Item>();

            IList<IWebElement> listaitems = tr.FindElements(By.XPath(".//td"));
            int cantidaditems = listaitems.Count;

            for(int i = 2; i<=cantidaditems; i++)
            {
                string txtvalor = tr.FindElement(By.XPath(".//td["+i+"]")).Text.Replace("$","");
                double valor = Convert.ToDouble(txtvalor);
                Entidades.Item item = new Entidades.Item();
                item.id = i-1;
                item.tipo = tipoValores(i);
                item.valor = valor;
                if (i <= 5)
                {
                    precios.Add(item);
                }else if(i > 5 & i <= 8)
                {
                    totales.Add(item);
                }
                else
                {
                    promedio.Add(item);
                }
            }

            return Tuple.Create(precios, totales, promedio);

        }

        /// <summary>
        /// Dvuelve un string con el tipo de valor según un índice
        /// </summary>
        /// <param name="indice"></param>
        /// <returns></returns>
        public static string tipoValores(int indice)
        {
            string retorno = "";
            switch (indice)
            {
                case 2:
                    retorno = "minimo";
                    break;
                case 3:
                    retorno = "maximo";
                    break;
                case 4:
                    retorno = "promedio";
                    break;
                case 5:
                    retorno = "mediana";
                    break;
                case 6:
                    retorno = "cabezas";
                    break;
                case 7:
                    retorno = "importe";
                    break;
                case 8:
                    retorno = "kgs";
                    break;
                case 9:
                    retorno = "kgs";
                    break;
                default:
                    retorno = "";
                    break;
            }
            return retorno;
        }
    }




}
