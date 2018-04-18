using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace MercadoLiniers
{
    public class MercadoLiniers
    {
        #region Variables Globales"
        static bool success = false;
        static int retry = 5;
        static string procesos = "MERCADO DE LINIERS";
        #endregion

        static void Main(string[] args)
        {
        }

        public static List<Entidades.Venta> scrapMercado()
        {

            Entidades.TablaMercado tabla = new Entidades.TablaMercado();

            //tabla = Scrap.scrapTablaHacienda.scrapTablaMercados(tabla);

            List<Entidades.Venta> ventas = new List<Entidades.Venta>();

            ventas = Scrap.scrapVentas.srapTablaVentas(ventas);
            

            return ventas;
        }
    }
}
