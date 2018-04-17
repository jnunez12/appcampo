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
            //mercadoliniers = MercadoLiniers.Program.scrapMercado();


            //var json = new JavaScriptSerializer().Serialize(mercadoliniers);
            //System.IO.File.WriteAllText(@"C:\Users\jonat\Desktop\PROYECTOS\appcampo\scrapCampo\mercadoliniers.json", json);
            #endregion

            #region Monedas

            #endregion
            List<Monedas.Entidades.Moneda> monedas = new List<Monedas.Entidades.Moneda>();
            monedas = Monedas.Monedas.scrapMonedas(monedas);
            var json = new JavaScriptSerializer().Serialize(monedas);
            System.IO.File.WriteAllText(@"D:\PROYECTOS\appcampo\scrapCampo\monedas.json", json);

            string a = "A";
        }
    }
}
