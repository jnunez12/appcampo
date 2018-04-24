using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clima.Entidades
{
    public class Clima
    {
        public int id { get; set; }
        public DateTime dateUpdate { get; set; }
        public string tipo { get; set; }
        public Temperatura temperaturaActual { get; set; }
        public SensacionTermica sensacionTermica { get; set; }
        public Viento viento { get; set; }
        public Humedad humedad { get; set; }
        public Condensacion condensacion { get; set; }
        public Presion presion { get; set; }
        public Visibilidad visibilidad { get; set; }
        public FaseLunar luna { get; set; }
        public Sol sol { get; set; }
    }
}
