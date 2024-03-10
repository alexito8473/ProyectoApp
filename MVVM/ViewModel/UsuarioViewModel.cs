using ProyectoApp.ConexionFirebase;
using ProyectoApp.MVVM.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ProyectoApp.MVVM.ViewModel
{
    public class UsuarioViewModel{
        private string gmailUsuario;
        private Conexión conexion = new Conexión();
        public ObservableCollection<Usuario> usuario { get; set; } = new ObservableCollection<Usuario>();
        public ICommand obtenerId { get; set; }
        public int id { get; set; }
        public ObservableCollection<Jornada> ListaJornada { get; set; } = new ObservableCollection<Jornada>();
        public UsuarioViewModel(string gmailUsuario) {
            this.gmailUsuario = gmailUsuario.ToLower();
            CargarUsuario();
            ConstruirMetodoObtenerId();
        }

        public void ConstruirMetodoObtenerId() {
            obtenerId = new Command(miId => {
                id = (int)miId;
            });
        }

        public void CargarUsuario() {
            conexion.GetCliente().Child("Usuario").AsObservable<Usuario>()
                        .Subscribe((user) => {
                            if (user.Object != null) {
                                if ( user.Object.Gmail.ToLower().Equals(gmailUsuario.ToLower())) {
                                    usuario.Add(user.Object) ;
                                    añadirMeses();
                                }
                            }
                        });
        }
        public async Task añadirMeses() {
            bool cambio = false;
            for (int i=0;i< usuario.First().Años.Count;i++) {
                if (usuario.First().Años[i].Meses.Count == 0) {
                    for (int j = 1; j <= 12; j++) {
                        usuario.First().Años[i].Meses.Add(new Mes(Mes.ObtenerNombreMes(j)));
                        cambio = true;
                    }      
                }
            }
            if (cambio) { 
            await conexion.ActualizarGuardarDatosAsync(usuario.First().NombreCompleto, usuario.First().Gmail, usuario.First().Contraseña, usuario.First().Imagen, usuario.First().CentroDocente, usuario.First().ProfesorResponsable, usuario.First().CentroTrabajo, usuario.First().TutorTrabajo, usuario.First().Años, usuario.First().Grado);
            }
        }
        public async Task borrarJornada(int dia, int mes, int año) {
            bool cambio = false;
            for (int i = 0; i < usuario.First().Años.Count; i++) {
                if (usuario.First().Años[i].fecha.Equals("" + año)) {
                    for (int j = 0; j < usuario.First().Años[i].Meses.Count; j++) {
                        if (usuario.First().Años[i].Meses[j].Nombre.Equals(Mes.ObtenerNombreMes(mes))) {
                            for (int k = 0; k < usuario.First().Años[i].Meses[j].Dias.Count; k++) {
                                if (usuario.First().Años[i].Meses[j].Dias[k].DiaActual == dia) {
                                    for (int l = 0; l < usuario.First().Años[i].Meses[j].Dias[k].Jornadas.Count; l++) {
                                        if (usuario.First().Años[i].Meses[j].Dias[k].Jornadas[l].id == id) {
                                            usuario.First().Años[i].Meses[j].Dias[k].Jornadas.RemoveAt(l);
                                            cambio = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (cambio) {
                await conexion.ActualizarGuardarDatosAsync(usuario.First().NombreCompleto, usuario.First().Gmail, usuario.First().Contraseña, usuario.First().Imagen, usuario.First().CentroDocente, usuario.First().ProfesorResponsable, usuario.First().CentroTrabajo, usuario.First().TutorTrabajo, usuario.First().Años, usuario.First().Grado);
            }
            cargarJornada(dia, mes, año);
        }
        public Jornada GetJornada(int dia, int mes, int año) {
            for (int i = 0; i < usuario.First().Años.Count; i++) {
                if (usuario.First().Años[i].fecha.Equals("" + año)) {
                    for (int j = 0; j < usuario.First().Años[i].Meses.Count; j++) {
                        if (usuario.First().Años[i].Meses[j].Nombre.Equals(Mes.ObtenerNombreMes(mes))) {
                            for (int k = 0; k < usuario.First().Años[i].Meses[j].Dias.Count; k++) {
                                if (usuario.First().Años[i].Meses[j].Dias[k].DiaActual == dia) {
                                    for (int l = 0; l < usuario.First().Años[i].Meses[j].Dias[k].Jornadas.Count; l++) {
                                        if (usuario.First().Años[i].Meses[j].Dias[k].Jornadas[l].id == id) {
                                            return usuario.First().Años[i].Meses[j].Dias[k].Jornadas[l];
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }
        public void cargarJornada(int dia, int mes, int año) {
            ListaJornada.Clear();
                for (int i = 0; i < usuario.First().Años.Count; i++) {
                    if (usuario.First().Años[i].fecha.Equals("" + año)) {
                        for (int j = 0; j < usuario.First().Años[i].Meses.Count; j++) {
                            if (usuario.First().Años[i].Meses[j].Nombre.Equals(Mes.ObtenerNombreMes(mes))) {
                                for (int k = 0; k < usuario.First().Años[i].Meses[j].Dias.Count; k++) {
                                    if (usuario.First().Años[i].Meses[j].Dias[k].DiaActual == dia) {
                                        for (int l = 0; l < usuario.First().Años[i].Meses[j].Dias[k].Jornadas.Count; l++) {
                                            ListaJornada.Add(usuario.First().Años[i].Meses[j].Dias[k].Jornadas[l]);
                                        }
                                    }
                                }
                            }
                        }
                    }
               
            }
          
        }
       
        public async Task añadirJornadaAsync(int dia, int mes, int año,string actividad,double tiempo,string observaciones) {
            ListaJornada.Clear();
            int maxId=-1;
            for (int i = 0; i < usuario.First().Años.Count; i++) {
                if (usuario.First().Años[i].fecha.Equals("" + año)) {
                    for (int j = 0; j < usuario.First().Años[i].Meses.Count; j++) {
                        if (usuario.First().Años[i].Meses[j].Nombre.Equals(Mes.ObtenerNombreMes(mes))) {
                            for (int k = 0; k < usuario.First().Años[i].Meses[j].Dias.Count; k++) {
                                if (usuario.First().Años[i].Meses[j].Dias[k].DiaActual == dia) {
                                    for (int l = 0; l < usuario.First().Años[i].Meses[j].Dias[k].Jornadas.Count; l++) {
                                        if (usuario.First().Años[i].Meses[j].Dias[k].Jornadas[l].id > maxId) {
                                            maxId = usuario.First().Años[i].Meses[j].Dias[k].Jornadas[l].id;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < usuario.First().Años.Count; i++) {
                if (usuario.First().Años[i].fecha.Equals("" + año)) {
                    for (int j = 0; j < usuario.First().Años[i].Meses.Count; j++) {
                        if (usuario.First().Años[i].Meses[j].Nombre.Equals(Mes.ObtenerNombreMes(mes))) {
                            for (int k = 0; k < usuario.First().Años[i].Meses[j].Dias.Count; k++) {
                                if (usuario.First().Años[i].Meses[j].Dias[k].DiaActual == dia) {
                                    usuario.First().Años[i].Meses[j].Dias[k].Jornadas.Add(new Jornada(maxId+1, actividad, tiempo, observaciones));
                                }
                            }
                        }
                    }
                }
            }
            await conexion.ActualizarGuardarDatosAsync(usuario.First().NombreCompleto, usuario.First().Gmail, usuario.First().Contraseña, usuario.First().Imagen, usuario.First().CentroDocente, usuario.First().ProfesorResponsable, usuario.First().CentroTrabajo, usuario.First().TutorTrabajo, usuario.First().Años, usuario.First().Grado);
            cargarJornada(dia, mes, año);
        }

        public async Task actualizarteJornadaAsync(int dia, int mes, int año, string actividad, double tiempo, string observaciones) {
            ListaJornada.Clear();
            int maxId = -1;
            for (int i = 0; i < usuario.First().Años.Count; i++) {
                if (usuario.First().Años[i].fecha.Equals("" + año)) {
                    for (int j = 0; j < usuario.First().Años[i].Meses.Count; j++) {
                        if (usuario.First().Años[i].Meses[j].Nombre.Equals(Mes.ObtenerNombreMes(mes))) {
                            for (int k = 0; k < usuario.First().Años[i].Meses[j].Dias.Count; k++) {
                                if (usuario.First().Años[i].Meses[j].Dias[k].DiaActual == dia) {
                                    for (int l = 0; l < usuario.First().Años[i].Meses[j].Dias[k].Jornadas.Count; l++) {
                                        if (usuario.First().Años[i].Meses[j].Dias[k].Jornadas[l].id == id) {
                                            usuario.First().Años[i].Meses[j].Dias[k].Jornadas[l].ActividadDesarrollada= actividad;
                                            usuario.First().Años[i].Meses[j].Dias[k].Jornadas[l].TiempoEmpleado = tiempo;
                                            usuario.First().Años[i].Meses[j].Dias[k].Jornadas[l].Observaciones = observaciones;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            await conexion.ActualizarGuardarDatosAsync(usuario.First().NombreCompleto, usuario.First().Gmail, usuario.First().Contraseña, usuario.First().Imagen, usuario.First().CentroDocente, usuario.First().ProfesorResponsable, usuario.First().CentroTrabajo, usuario.First().TutorTrabajo, usuario.First().Años, usuario.First().Grado);
            cargarJornada(dia, mes, año);
        }
    }
}
