using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoApp.MVVM.Model {
    /// <summary> Clase modelo sobre los detalles de un Usuario </summary>
    /// <remarks> Clase donde vamos a usar como molde para almacenar los datos de un Usuario.</remarks>
    public class Usuario {
        /// <summary> Atributo de la clase Usario</summary>
        /// <remarks> El atributo tine alamecenado el nombre completo del usuario.</remarks>
        public required string NombreCompleto { get; set; }
        /// <summary> Atributo de la clase Usario</summary>
        /// <remarks> El atributo tine almacenado el gmail del usuario.</remarks>
        public required string Gmail { get; set; }
        /// <summary> Atributo de la clase Uusario</summary>
        /// <remarks> El atributo tine almacenado la contraseña del usuario. </remarks>
        public required string Contraseña { get; set; }
        /// <summary> Atributo de la clase Usario</summary>
        /// <remarks> El atributo tine almacenado la url del imagen el usuario. </remarks>
        public required string Imagen { get; set; }
        /// <summary> Atributo de la clase Usario</summary>
        /// <remarks>El atributo tine almacenado el grado del usuario.</remarks>
        public required string Grado { get; set; }
        /// <summary> Atributo de la clase Usario</summary>
        /// <remarks> El atributo tine almacenado el grado del usuario </remarks>
        public required string CentroDocente { get; set; }
        /// <summary> Atributo de la clase Usario</summary>
        /// <remarks> El atributo tine almacenado el centro docente específico. </remarks>
        public required string ProfesorResponsable { get; set; }
        /// <summary> Atributo de la clase Usario</summary>
        /// <remarks> El atributo tine almacenado el profesor responsable.</remarks>
        public required string CentroTrabajo { get; set; }
        /// <summary> Atributo de la clase Usario</summary>
        /// <remarks> El atributo tine almacenado el centro de trabajo.</remarks>
        public required string TutorTrabajo { get; set; }
        /// <summary> Atributo de la clase Usario</summary>
        /// <remarks> El atributo tine almacenado su tutor de trabajo.</remarks>
        public required string CicloFormativo { get; set; }
        /// <summary> Atributo de la clase Usario</summary>
        /// <remarks> El atributo almacenada un listado de los años que tiene el usuario.</remarks>
        public required List<Año> Años { get; set; }
    }
}
