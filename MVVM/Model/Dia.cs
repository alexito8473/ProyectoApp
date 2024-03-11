using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoApp.MVVM.Model {
    /// <summary> Clase modelo sobre el contenido de un dia</summary>
    /// <remarks>
    /// Clase donde vamos a usar como molde para poder almacenar las jornadas que tiene en un dia, como el dia que se trata.
    /// </remarks>
    public class Dia {
        /// <summary> Atributo de la clase Dia</summary>
        /// <remarks>
        /// El atributo almacena el dia que se trata la clase.
        /// </remarks>
        public int DiaActual { get; set; }
        /// <summary> Atributo de la clase Dia</summary>
        /// <remarks>
        /// El atributo almacena la lista de jornadas que tiene el dia.
        /// </remarks>
        public List<Jornada> Jornadas { get; set; }
        /// <summary> Constructor de la clase Dia </summary>
        /// <remarks>
        /// Se instancia el dia y se instancia la lista de jornadas.
        /// </remarks>
        /// <param name="dia">La dia en cuestión</param>
        public Dia(int dia) {
            DiaActual = dia;
            Jornadas = new List<Jornada>();
        }
    }
}
