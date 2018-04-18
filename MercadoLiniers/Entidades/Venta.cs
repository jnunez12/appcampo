using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercadoLiniers.Entidades
{
    public class Venta
    {
        public int id { get; set; }
        public DateTime fecha { get; set; }
        public string especie { get; set; }
        public string tipo { get; set; }
        public Entidades.Consignatario consignatario { get; set; }
        public List<Entidades.ItemVenta> items { get; set; }

        public Venta()
        {
            consignatario = new Consignatario();
            items = new List<ItemVenta>();
        }
    }
}
