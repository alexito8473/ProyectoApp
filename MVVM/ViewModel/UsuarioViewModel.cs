using ProyectoApp.ConexionFirebase;
using ProyectoApp.MVVM.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ProyectoApp.MVVM.ViewModel
{
    /// <summary> Clase vistaModelo sobre los detalles del usuario </summary>
    /// <remarks>
    /// Clase donde vamos a ser intermediario entre el modelo y su vista.
    /// </remarks>
    public class UsuarioViewModel{
        /// <summary> Atributo de la clase UsuarioViewModel </summary>
        /// <remarks>
        /// El atributo tine almacenado el gmail del usuario.
        /// </remarks>
        private string gmailUsuario;
        /// <summary> Atributo de la clase UsuarioViewModel, que realiza la conexión con el servidor. </summary>
        /// <remarks>
        /// El atributo instancia la calse Conexión para poder actualizar los datos del usuario.
        /// </remarks>
        private Conexión conexion = new Conexión();
        /// <summary> Atributo de la clase UsuarioViewModel. </summary>
        /// <remarks>
        /// El atributo instancia el usuario que vamos a tratar.n Se contruye sus propios get y set.
        /// </remarks>
        public Usuario usuario { get; set; }
        /// <summary> Comando de la clase UsuarioViewModel. </summary>
        /// <remarks>
        /// El comando trate de poner obtener el id que tiene cada jornada.
        /// </remarks>
        public ICommand obtenerId { get; set; }
        /// <summary> Atributo de la clase UsuarioViewModel. </summary>
        /// <remarks>
        /// El atributo almacena el id de la jornada en su instancia.
        /// </remarks>
        public int id { get; set; }
        /// <summary> Atributo de la clase UsuarioViewModel. </summary>
        /// <remarks>
        /// El atributo almacena una lista de jornadas el usuario específico.   
        /// </remarks>
        public ObservableCollection<Jornada> ListaJornada { get; set; } = new ObservableCollection<Jornada>();
        /// <summary> Constructor de la clase UsuarioViewModel </summary>
        /// <remarks>
        /// Se instancia el atributo gmailUsuario, además se construyen los comandos del sistema, 
        /// además de obtener el alumno que queremos.
        /// </remarks>
        /// <param name="gmailUsuario">Gmail del usuario</param>
        public UsuarioViewModel(string gmailUsuario) {
            this.gmailUsuario = gmailUsuario.ToLower();
            CargarUsuario();
            ConstruirMetodoObtenerId();
        }
        /// <summary> Método para construir el comando de obtener el id de la jornada</summary>
        /// <remarks> 
        /// Con el método creamos un comando para  obtener el id de la jornada que hemos seleccionado.
        /// </remarks>
        public void ConstruirMetodoObtenerId() {
            obtenerId = new Command(miId => {
                id = (int)miId;
            });
        }
        /// <summary> Método para obtener el usuario de la base de datos</summary>
        /// <remarks> 
        /// Con el método buscaremos en la firebase el usuario que vayamos a trabajar con el.
        /// </remarks>
        public void CargarUsuario() {
            conexion.GetCliente().Child("Usuario").AsObservable<Usuario>()
                        .Subscribe((user) => {
                            if (user.Object != null) {
                                if ( user.Object.Gmail.ToLower().Equals(gmailUsuario.ToLower())) {
                                    usuario=user.Object ;
                                    añadirMeses();
                                }
                            }
                        });
        }
        /// <summary> Método para añadir los meses al usuario</summary>
        /// <remarks> 
        /// Si el usuario no tiene instancio los meses que su año, se le añade de inmediato.
        /// </remarks>
        public async Task añadirMeses() {
            bool cambio = false;
            for (int i=0;i< usuario.Años.Count;i++) {
                if (usuario.Años[i].Meses.Count == 0) {
                    for (int j = 1; j <= 12; j++) {
                        usuario.Años[i].Meses.Add(new Mes(Mes.ObtenerNombreMes(j)));
                        cambio = true;
                    }      
                }
            }
            if (cambio) { 
            await conexion.ActualizarGuardarDatosAsync(usuario.NombreCompleto, usuario.Gmail, usuario.Contraseña, usuario.Imagen, usuario.CentroDocente, usuario.ProfesorResponsable, usuario.CentroTrabajo, usuario.TutorTrabajo, usuario.Años, usuario.Grado, usuario.CicloFormativo);
            }
        }
        /// <summary> Método para borrar una jornada del usuario</summary>
        /// <remarks> 
        /// Con el id que hemos tenido que obtener anteriormente por el comando, podemos borrar el 
        /// </remarks>
        /// <param name="dia">Dia de la actividad</param>
        /// <param name="año">Año de la actividad</param>
        /// <param name="mes">Mes de la actividad</param>
        public async Task borrarJornada(int dia, int mes, int año) {
            bool cambio = false;
            for (int i = 0; i < usuario.Años.Count; i++) {
                if (usuario.Años[i].fecha.Equals("" + año)) {
                    for (int j = 0; j < usuario.Años[i].Meses.Count; j++) {
                        if (usuario.Años[i].Meses[j].Nombre.Equals(Mes.ObtenerNombreMes(mes))) {
                            for (int k = 0; k < usuario.Años[i].Meses[j].Dias.Count; k++) {
                                if (usuario.Años[i].Meses[j].Dias[k].DiaActual == dia) {
                                    for (int l = 0; l < usuario.Años[i].Meses[j].Dias[k].Jornadas.Count; l++) {
                                        if (usuario.Años[i].Meses[j].Dias[k].Jornadas[l].id == id) {
                                            usuario.Años[i].Meses[j].Dias[k].Jornadas.RemoveAt(l);
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
                await conexion.ActualizarGuardarDatosAsync(usuario.NombreCompleto, usuario.Gmail, usuario.Contraseña, usuario.Imagen, usuario.CentroDocente, usuario.ProfesorResponsable, usuario.CentroTrabajo, usuario.TutorTrabajo, usuario.Años, usuario.Grado,usuario.CicloFormativo);
            }
            cargarJornada(dia, mes, año);
        }
        /// <summary> Método para mostrar las jornada del usuario, con un id concreto</summary>
        /// <remarks> 
        /// Se muestran las jornadas de un año, mes y dia concretos, con un id concreto.
        /// </remarks>
        /// <param name="dia">Dia de la actividad</param>
        /// <param name="año">Año de la actividad</param>
        /// <param name="mes">Mes de la actividad</param>
        public Jornada GetJornada(int dia, int mes, int año) {
            for (int i = 0; i < usuario.Años.Count; i++) {
                if (usuario.Años[i].fecha.Equals("" + año)) {
                    for (int j = 0; j < usuario.Años[i].Meses.Count; j++) {
                        if (usuario.Años[i].Meses[j].Nombre.Equals(Mes.ObtenerNombreMes(mes))) {
                            for (int k = 0; k < usuario.Años[i].Meses[j].Dias.Count; k++) {
                                if (usuario.Años[i].Meses[j].Dias[k].DiaActual == dia) {
                                    for (int l = 0; l < usuario.Años[i].Meses[j].Dias[k].Jornadas.Count; l++) {
                                        if (usuario.Años[i].Meses[j].Dias[k].Jornadas[l].id == id) {
                                            return usuario.Años[i].Meses[j].Dias[k].Jornadas[l];
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
        /// <summary> Método para cargar las lista de jornadas de un usuario</summary>
        /// <remarks> 
        /// Se cargan en la lista de las jornadas  de un dia, mes y año concreto
        /// </remarks>
        /// <param name="dia">Dia de la actividad</param>
        /// <param name="año">Año de la actividad</param>
        /// <param name="mes">Mes de la actividad</param>
        public void cargarJornada(int dia, int mes, int año) {
            ListaJornada.Clear();
                for (int i = 0; i < usuario.Años.Count; i++) {
                    if (usuario.Años[i].fecha.Equals("" + año)) {
                        for (int j = 0; j < usuario.Años[i].Meses.Count; j++) {
                            if (usuario.Años[i].Meses[j].Nombre.Equals(Mes.ObtenerNombreMes(mes))) {
                                for (int k = 0; k < usuario.Años[i].Meses[j].Dias.Count; k++) {
                                    if (usuario.Años[i].Meses[j].Dias[k].DiaActual == dia) {
                                        for (int l = 0; l < usuario.Años[i].Meses[j].Dias[k].Jornadas.Count; l++) {
                                            ListaJornada.Add(usuario.Años[i].Meses[j].Dias[k].Jornadas[l]);
                                        }
                                    }
                                }
                            }
                        }
                    }
            }   
        }

        /// <summary> Método para añadir jornadas al usuario</summary>
        /// <remarks> 
        /// En un dia concreto se el añade un jornada.
        /// </remarks>
        /// <param name="actividad">La actividad que ha hecho ese dia</param>
        /// <param name="dia">Dia de la actividad</param>
        /// <param name="año">Año de la actividad</param>
        /// <param name="mes">Mes de la actividad</param>
        /// <param name="observaciones">Las observaciones que ha tenido</param>
        /// <param name="tiempo">El tiempo que ha tardado</param>
        public async Task añadirJornadaAsync(int dia, int mes, int año,string actividad,double tiempo,string observaciones) {
            ListaJornada.Clear();
            int maxId=-1;
            for (int i = 0; i < usuario.Años.Count; i++) {
                if (usuario.Años[i].fecha.Equals("" + año)) {
                    for (int j = 0; j < usuario.Años[i].Meses.Count; j++) {
                        if (usuario.Años[i].Meses[j].Nombre.Equals(Mes.ObtenerNombreMes(mes))) {
                            for (int k = 0; k < usuario.Años[i].Meses[j].Dias.Count; k++) {
                                if (usuario.Años[i].Meses[j].Dias[k].DiaActual == dia) {
                                    for (int l = 0; l < usuario.Años[i].Meses[j].Dias[k].Jornadas.Count; l++) {
                                        if (usuario.Años[i].Meses[j].Dias[k].Jornadas[l].id > maxId) {
                                            maxId = usuario.Años[i].Meses[j].Dias[k].Jornadas[l].id;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            // Se repite porque es necesario para poder buscar el id más grande
            for (int i = 0; i < usuario.Años.Count; i++) {
                if (usuario.Años[i].fecha.Equals("" + año)) {
                    for (int j = 0; j < usuario.Años[i].Meses.Count; j++) {
                        if (usuario.Años[i].Meses[j].Nombre.Equals(Mes.ObtenerNombreMes(mes))) {
                            for (int k = 0; k < usuario.Años[i].Meses[j].Dias.Count; k++) {
                                if (usuario.Años[i].Meses[j].Dias[k].DiaActual == dia) {
                                    usuario.Años[i].Meses[j].Dias[k].Jornadas.Add(new Jornada(maxId+1, actividad, tiempo, observaciones));
                                }
                            }
                        }
                    }
                }
            }
            await conexion.ActualizarGuardarDatosAsync(usuario.NombreCompleto, usuario.Gmail, usuario.Contraseña, usuario.Imagen, usuario.CentroDocente, usuario.ProfesorResponsable, usuario.CentroTrabajo, usuario.TutorTrabajo, usuario.Años, usuario.Grado, usuario.CicloFormativo);
            cargarJornada(dia, mes, año);
        }

        /// <summary> Método para actualizar una jornada del usuario</summary>
        /// <remarks> 
        /// Se actualiza una jornada en concreto.   
        /// </remarks>
        /// <param name="actividad">La actividad que ha hecho ese dia</param>
        /// <param name="dia">Dia de la actividad</param>
        /// <param name="año">Año de la actividad</param>
        /// <param name="mes">Mes de la actividad</param>
        /// <param name="observaciones">Las observaciones que ha tenido</param>
        /// <param name="tiempo">El tiempo que ha tardado</param>
        public async Task actualizarteJornadaAsync(int dia, int mes, int año, string actividad, double tiempo, string observaciones) {
            ListaJornada.Clear();
            int maxId = -1;
            for (int i = 0; i < usuario.Años.Count; i++) {
                if (usuario.Años[i].fecha.Equals("" + año)) {
                    for (int j = 0; j < usuario.Años[i].Meses.Count; j++) {
                        if (usuario.Años[i].Meses[j].Nombre.Equals(Mes.ObtenerNombreMes(mes))) {
                            for (int k = 0; k < usuario.Años[i].Meses[j].Dias.Count; k++) {
                                if (usuario.Años[i].Meses[j].Dias[k].DiaActual == dia) {
                                    for (int l = 0; l < usuario.Años[i].Meses[j].Dias[k].Jornadas.Count; l++) {
                                        if (usuario.Años[i].Meses[j].Dias[k].Jornadas[l].id == id) {
                                            usuario.Años[i].Meses[j].Dias[k].Jornadas[l].ActividadDesarrollada= actividad;
                                            usuario.Años[i].Meses[j].Dias[k].Jornadas[l].TiempoEmpleado = tiempo;
                                            usuario.Años[i].Meses[j].Dias[k].Jornadas[l].Observaciones = observaciones;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            await conexion.ActualizarGuardarDatosAsync(usuario.NombreCompleto, usuario.Gmail, usuario.Contraseña, usuario.Imagen, usuario.CentroDocente, usuario.ProfesorResponsable, usuario.CentroTrabajo, usuario.TutorTrabajo, usuario.Años, usuario.Grado, usuario.CicloFormativo); 
            cargarJornada(dia, mes, año);
        }
    }
}
