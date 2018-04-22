using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BolsaCereales.Entidades
{
    public class CerealComun
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public DateTime fechadato { get; set; }
        public Entidades.MercadoDisponible disponible { get; set; }
        public Entidades.MercadoFuturosMATba futuros_matba { get; set; }
        public Entidades.MercadoFOBMAGyP fob_magyp { get; set; }
        public Entidades.MercadoCBOT cbot { get; set; }
        public Entidades.MercadoPreciosMAGyP precios_magyp { get; set; }

    }
}
