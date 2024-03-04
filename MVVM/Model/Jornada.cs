using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoApp.MVVM.Model {
    public class Jornada {
        public int id { get; set; }
        public string ActividadDesarrollada { get; set; }
        public double TiempoEmpleado { get; set; }
        public string Observaciones { get; set; }
        public Jornada() {
            ActividadDesarrollada = "ejmeplo";
            Observaciones = "ejemplo";
        }
        public Jornada(int id) {
            this.id = id;
        }
    }
}
