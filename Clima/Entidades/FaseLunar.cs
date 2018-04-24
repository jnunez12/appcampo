using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clima.Entidades
{
    public class FaseLunar
    {
        public string fase { get; set; }
        public TimeSpan salida { get; set; }
        public TimeSpan puesta { get; set; }
    }
}
