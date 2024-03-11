using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoApp.MVVM.Model {
    /// <summary> Clase modelo sobre los detalles de un Jornada </summary>
    /// <remarks> Clase donde vamos a usar como molde para poder almacenar las jornadas que tiene en un dia, como el dia que se trata.</remarks>
    public class Jornada {
        /// <summary> Atributo de la clase Jornada</summary>
        /// <remarks> El atributo almacena un identificador unico para distinguirse entre los distintas jornadas.</remarks>
        public int id { get; set; }
        /// <summary> Atributo de la clase Jornada</summary>
        /// <remarks> El atributo almacena un texto que sera la actividad desarrollada en la jornada.</remarks>
        public string ActividadDesarrollada { get; set; }
        /// <summary> Atributo de la clase Jornada</summary>
        /// <remarks> El atributo almacena el número de horas que ha impartido la jornada.</remarks>
        public double TiempoEmpleado { get; set; }
        /// <summary> Atributo de la clase Jornada</summary>
        /// <remarks> El atributo almacena las observaciones obtenidas de la jornada. </remarks>
        public string Observaciones { get; set; }
        /// <summary> Constructor de la clase Jornada </summary>
        /// <remarks> Se instancia una jornada concreta. </remarks>
        /// <param name="actividad">La actividad que se ha desarrolado</param>
        /// <param name="id">EL id unico del la actividad</param>
        /// <param name="observaciones">La observación que ha obtenido</param>
        /// <param name="tiempo">El tiempo estipulado</param>
        public Jornada(int id,string actividad,double tiempo,string observaciones) {
            this.id = id;
            ActividadDesarrollada= actividad;
            TiempoEmpleado = tiempo;
            Observaciones = observaciones;
        }
    }
}
