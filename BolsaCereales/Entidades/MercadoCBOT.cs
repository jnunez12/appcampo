using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BolsaCereales.Entidades
{
    public class MercadoCBOT
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public List<Entidades.ItemCBOT> items { get; set; }

        public MercadoCBOT()
        {
            items = new List<ItemCBOT>();
        }
    }
}
