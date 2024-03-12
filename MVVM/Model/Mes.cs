using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoApp.MVVM.Model {
    /// <summary> Clase modelo sobre los detalles de un Jornada </summary>
    /// <remarks>
    /// Clase donde vamos a usar como molde para poder almacenar las jornadas que tiene en un dia, como el dia que se trata.
    /// </remarks>
    public class Mes {
        /// <summary> Atributo de la clase Mes</summary>
        /// <remarks> El atributo tine almacenado el mes específico. </remarks>
        public string Nombre { get; set; }
        /// <summary> Atributo de la clase Mes</summary>
        /// <remarks> El atributo tine almacenado una lista de los dias específicos de ese mes. </remarks>
        public List<Dia> Dias { get; set; }
        /// <summary> Constructor de la clase Mes</summary>
        /// <remarks> Se instancia el més en cuestión con su nombre concreto. </remarks>
        /// <param name="nombre">El nombre del mes</param>
        public Mes(string nombre) {
            int dias= diastotales(nombre);
            Nombre=nombre;
            Dias =new List<Dia>();
            for (int i=1;i<=dias;i++) {
                Dias.Add(new Dia(i));
            }
        }
        /// <summary>Método de la clase Mes</summary>
        /// <remarks> Se trata un método que nos devuelve la cantidad de dias que tiene un més en concreto. </remarks>
        /// <param name="nombre"> El nombre del mes</param>
        /// <returns> Numero de dias</returns>
        private int diastotales(string nombre) {
            int dias=31;
            switch (nombre) {
                case "Febrero":
                dias = 29;
                break;
                case "Abril":
                case "Junio":
                case "Septiembre":
                case "Noviembre":  
                dias = 30;
                break;
            }
            return dias;
        }
        /// <summary>Método de la clase Mes</summary>
        /// <remarks> Se trata de un numero que nos devuelve el nombre del més, dependiendo del numero que hayamos introducido. </remarks>
        /// <param name="numeroMes">El numero del mes</param>
        /// <returns> Numero del mes</returns>
        public static string ObtenerNombreMes(int numeroMes) {
            switch (numeroMes) {
                case 1: return "Enero";
                case 2: return "Febrero";
                case 3: return "Marzo";
                case 4: return "Abril";
                case 5: return "Mayo";
                case 6: return "Junio";
                case 7: return "Julio";
                case 8: return "Agosto";
                case 9: return "Septiembre";
                case 10: return "Octubre";
                case 11: return "Noviembre";
                case 12: return "Diciembre";
                default: return "mes inválido";
            }
        }
        /// <summary>Método de la clase Mes</summary>
        /// <remarks> Se trata un método nos crea una lista de todos los meses de un año. </remarks>
        /// <returns> Lista con los nombre de los meses</returns>
        public static List<string> ListaNombreMeses() {
            var list = new List<string>();
            for (int i=0;i<12;i++) {
                list.Add(ObtenerNombreMes(i + 1));
            }
            return list;
        }
    }
}
