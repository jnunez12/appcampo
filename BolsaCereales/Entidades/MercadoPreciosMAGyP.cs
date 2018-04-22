using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BolsaCereales.Entidades
{
    public class MercadoPreciosMAGyP
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public List<Entidades.ItemPreciosMAGyP> items { get; set; }

        public MercadoPreciosMAGyP()
        {
            items = new List<ItemPreciosMAGyP>();
        }
    }

}
