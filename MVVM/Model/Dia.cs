using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoApp.MVVM.Model {
    public class Dia {

        public int DiaActual { get; set; }
        public List<Jornada> Jornadas { get; set; }
       
        public Dia(int dia) {
            DiaActual = dia;
            Jornadas = new List<Jornada>();
        }

    }
}
