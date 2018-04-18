using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BolsaCereales.Entidades
{
    public class ItemDisponible
    {
        public int id { get; set; }
        public string mercado { get; set; }
        public double precio { get; set; }
        public string var { get; set; }
        public DateTime fecha { get; set; }
    }
}
