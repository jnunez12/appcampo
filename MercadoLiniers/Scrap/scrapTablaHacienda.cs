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

        public static List<Entidades.Categoria> scrapTabla(IWebDriver driver, List<Entidades.Categoria> lista)
        {

            IList<IWebElement> listatr = driver.FindElements(By.XPath("/html/body/table/tbody/tr[1]/td/table[2]/tbody/tr/td/table/tbody/tr[2]/td/table/tbody/tr"));

            foreach (IWebElement tr in listatr)
            {
                Entidades.Categoria categoria = new Entidades.Categoria();

                try
                {
                    categoria.nombre = tr.FindElement(By.XPath("./td[1]")).Text;
                }
                catch (Exception e)
                {
                    continue;
                }

                if(categoria.nombre == "")
                {
                    continue;
                }

                //DANJO... obtener items
                lista.Add(categoria);

                
            }

            return lista;
        }
    }
}
