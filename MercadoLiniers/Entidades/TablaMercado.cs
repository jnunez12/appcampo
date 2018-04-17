using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercadoLiniers.Entidades
{
    public class TablaMercado
    {
        public DateTime fechainicial { get; set; }
        public DateTime fechafinal { get; set; }
        public List<Entidades.Categoria> categorias { get; set; }
    }
}
