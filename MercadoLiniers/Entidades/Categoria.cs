using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercadoLiniers.Entidades
{
    public class Categoria
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public List<Item> precios { get; set; }
        public List<Item> totales { get; set; }
        public List<Item> promedio { get; set; }
    }
}
