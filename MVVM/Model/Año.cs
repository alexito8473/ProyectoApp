namespace ProyectoApp.MVVM.Model {
    /// <summary> Clase modelo sobre los detalles de un año </summary>
    /// <remarks>
    /// Clase donde vamos a usar como molde para poder obtener los meses que tiene un año.
    /// </remarks>
    public class Año {
        /// <summary> Atributo de la clase Año </summary>
        /// <remarks>
        /// El atributo tine almacenado el año específico que pertenece
        /// </remarks>
        public string fecha { get; set; }
        /// <summary> Atributo de la clase Año </summary>
        /// <remarks>
        /// El atributo tine almacenado una lista de sus meses
        /// </remarks>
        public List<Mes> Meses { get; set; }
        /// <summary> Constructor de la clase Año </summary>
        /// <remarks>
        /// Se instancia los el año que es es como la lista de meses que tiene.
        /// </remarks>
        /// <param name="fecha">La fecha que se trata del año en cuestión</param>
        public Año(string fecha) {
            this.fecha = fecha;
            Meses = new List<Mes>();
            for (int i = 1; i <= 12; i++) {
                Meses.Add(new Mes(Mes.ObtenerNombreMes(i)));
            }
        }
        /// <summary> Constructor de la clase Año </summary>
        /// <remarks>
        /// Se instancia los el año con el año actual.
        /// </remarks>
        public Año() {
            this.fecha = DateTime.Now.ToString("yyyy");
            Meses = new List<Mes>();
        }
    }
}
