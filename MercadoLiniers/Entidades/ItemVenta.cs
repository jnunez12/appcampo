using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercadoLiniers.Entidades
{
    public class ItemVenta
    {
        public int id { get; set; }
        public string remitente { get; set; }
        public string localidad { get; set; }
        public string provincia { get; set; }
        public int cabezas { get; set; }
        public string categoria { get; set; }
        public double kgs { get; set; }
        public double promedio { get; set; }
        public double precio { get; set; }
    }
}
