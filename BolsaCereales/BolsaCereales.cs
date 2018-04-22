using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BolsaCereales
{
    public class BolsaCereales
    {
        static void Main(string[] args)
        {

        }

        public static List<Entidades.CerealComun> scrapCereales()
        {
            List<Entidades.CerealComun> cereales = new List<Entidades.CerealComun>();

            cereales = Scrap.scrapCereales.loopCereales(cereales);

            return cereales;
        }
    }
}
