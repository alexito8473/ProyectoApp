using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoApp.MVVM.Model {
    public class Usuario {
        public required string NombreCompleto { get; set; }
        public required string Gmail { get; set; }
        public required string Contraseña { get; set; }
        public required string Imagen { get; set; }
        public required string Grado { get; set; }
        public required string CentroDocente { get; set; }
        public required string ProfesorResponsable { get; set; }
        public required string CentroTrabajo { get; set; }
        public required string TutorTrabajo { get; set; }
        public required List<Año> Años { get; set; }
    }
}
