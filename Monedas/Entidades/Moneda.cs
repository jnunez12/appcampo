using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monedas.Entidades
{
    public class Moneda
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string simbolo { get; set; }
        public string cod_iso { get; set; }
        public DateTime dateUpdate { get; set; }
        public TimeSpan timeUpdate { get; set; }
        public List<Precio> listaPrecios { get; set; }

        /// <summary>
        /// Constructor con iniziacilizaciones
        /// </summary>
        public Moneda()
        {
            id = 0;
            nombre = "";
            cod_iso = "";
            listaPrecios = new List<Precio>();
        } 

    }
}
