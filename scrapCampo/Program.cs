using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monedas;
using MercadoLiniers;
using System.Web.Script.Serialization;

namespace scrapCampo
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Mercado de liniers
            //MercadoLiniers.Entidades.TablaMercado mercadoliniers = new MercadoLiniers.Entidades.TablaMercado();
            //mercadoliniers = MercadoLiniers.MercadoLiniers.scrapMercado();

            List<MercadoLiniers.Entidades.Venta> ventas = new List<MercadoLiniers.Entidades.Venta>();
            ventas = MercadoLiniers.MercadoLiniers.scrapMercado();

            var json = new JavaScriptSerializer().Serialize(ventas);
            System.IO.File.WriteAllText(@"D:\PROYECTOS\appcampo\scrapCampo\ventas.json", json);
            #endregion

            #region Monedas
            //List<Monedas.Entidades.Moneda> monedas = new List<Monedas.Entidades.Moneda>();
            //monedas = Monedas.Monedas.scrapMonedas(monedas);
            //var json = new JavaScriptSerializer().Serialize(monedas);
            //System.IO.File.WriteAllText(@"D:\PROYECTOS\appcampo\scrapCampo\monedas.json", json);
            #endregion


            string a = "A";
        }
    }
}
