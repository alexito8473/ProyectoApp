using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoApp.MVVM.Model {
    public class Año {

        public string fecha { get; set; }
        public List<Mes> Meses { get; set; }

        public Año() {
            this.fecha = DateTime.Now.ToString("yyyy");
            Meses = new List<Mes>();
           // Meses.Add(new Mes(Mes.ObtenerNombreMes(DateTime.Now.Month)));
        }
        public Año(string fecha) {
            this.fecha = fecha;
            Meses = new List<Mes>();
            Meses.Add(new Mes(Mes.ObtenerNombreMes(DateTime.Now.Month)));
        }

    }
}
