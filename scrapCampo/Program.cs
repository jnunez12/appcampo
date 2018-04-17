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
            List<MercadoLiniers.Entidades.Categoria> mercadoliniers = new List<MercadoLiniers.Entidades.Categoria>();
            mercadoliniers = MercadoLiniers.Program.scrapCategorias();


            var json = new JavaScriptSerializer().Serialize(mercadoliniers);
            System.IO.File.WriteAllText(@"C:\Users\jonat\Desktop\PROYECTOS\appcampo\scrapCampo\mercadoliniers.json", json);
            //Monedas.Program.Monedas();
            string a = "A";
        }
    }
}
