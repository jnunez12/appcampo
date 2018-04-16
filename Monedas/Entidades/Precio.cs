using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monedas.Entidades
{
    public class Precio
    {
        public int id { get; set; }
        public string tipo { get; set; }
        public double compra { get; set; }
        public double venta { get; set; }

        /// <summary>
        /// Constructor con inicializacion
        /// </summary>
        public Precio()
        {
            id = 0;
            tipo = "";
            compra = 0;
            venta = 0;
        }
    }
}
