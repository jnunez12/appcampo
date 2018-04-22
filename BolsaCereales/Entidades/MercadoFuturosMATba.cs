using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BolsaCereales.Entidades
{
    public class MercadoFuturosMATba
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public List<Entidades.ItemFuturoMATba> items { get; set; }

        public MercadoFuturosMATba()
        {
            items = new List<ItemFuturoMATba>();
        }
    }
}
