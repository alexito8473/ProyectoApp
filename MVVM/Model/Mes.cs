using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoApp.MVVM.Model {
    public class Mes {
        public string Nombre { get; set; }
        public List<Dia> Dias { get; set; }

        public Mes(string nombre) {
            int dias= diastotales(nombre);
            Nombre=nombre;
            Dias =new List<Dia>();
            for (int i=1;i<=dias;i++) {
                Dias.Add(new Dia(dias));
            }
        }
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
        public static List<string> ListaNombreMeses() {
            var list = new List<string>();
            for (int i=0;i<12;i++) {
                list.Add(ObtenerNombreMes(i + 1));
            }
            return list;
        }
    }
}
