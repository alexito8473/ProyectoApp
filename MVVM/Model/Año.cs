namespace ProyectoApp.MVVM.Model {
    public class Año {

        public string fecha { get; set; }
        public List<Mes> Meses { get; set; }

        public Año(string fecha) {
            this.fecha = fecha;
            Meses = new List<Mes>();
            for (int i = 1; i <= 12; i++) {
                Meses.Add(new Mes(Mes.ObtenerNombreMes(i)));
            }
        }
        public Año() {
            this.fecha = DateTime.Now.ToString("yyyy");
            Meses = new List<Mes>();
        }

    }
}
