using Firebase.Auth.Providers;
using Firebase.Auth;
using Firebase.Database;
using ProyectoApp.MVVM.Model;
using Firebase.Database.Query;
using Firebase.Storage;


namespace ProyectoApp.ConexionFirebase {
    /// <summary> Clase donde se realiza las distinas conexines. </summary>
    /// <remarks> Esta clase esta conformada para poder realizar distintas conexiones a firebase.</remarks>
    public class Conexión {
        /// <summary> Atributo de la clase Conexión </summary>
        /// <remarks> El atributo nos sirve como conexión a al storage de firebase, que tiene almacenado todos los datos de los usuarios</remarks>
        private FirebaseClient cliente = new FirebaseClient(Constante.Constante.REALMTIME_STORAGE) ;
        /// <summary> Método para mostrar el al atributo cliente</summary>
        /// <remarks> Con el mostramos al conexion del cliente.</remarks>
        /// <returns> Devuelve la conexion el realtime database</returns>
        public FirebaseClient GetCliente() {
            return cliente ;
        }
        /// <summary> Método para mostrar el al atributo cliente</summary>
        /// <remarks> Con el mostramos al conexion del cliente.</remarks>
        /// <returns> Devuelve el token del autentificator</returns>
        private FirebaseAuthClient obtenerToken() {
            var client = new FirebaseAuthClient(new FirebaseAuthConfig() {
                ApiKey = Constante.Constante.API_WEB,
                AuthDomain = Constante.Constante.AuthDomain,
                Providers = new FirebaseAuthProvider[]
                {
                new EmailProvider()
                }
            });
            return client;
        }
        /// <summary> Métodos asyncrono para iniciar sesión</summary>
        /// <remarks> El método nos logea con un gmail y contraseña en concreto en la firebase</remarks>
        /// <param name="email">El mail del usuario</param>
        /// <param name="password">La contraseña del usuario</param>
        /// <returns> Devuelve el token del inicio de sesión</returns>
        public async Task<String> iniciar_sesion(string email, string password) {
            var client = obtenerToken();
            var authResult = await client.SignInWithEmailAndPasswordAsync(email, password);
            return await authResult.User.GetIdTokenAsync();
        }
        /// <summary> Métodos asyncrono para registrarte</summary>
        /// <remarks> El método nos registra con un gmail y contraseña en concreto en la firebase</remarks>
        /// <param name="email">El mail del usuario</param>
        /// <param name="password">La contraseña del usuario</param>
        /// <returns> Devuelve el token del registro</returns>
        public async Task<String> registrarse(string email, string password) {
            var client = obtenerToken();
            var authResult = await client.CreateUserWithEmailAndPasswordAsync(email, password);
            return await authResult.User.GetIdTokenAsync();
        }
        /// <summary> Métodos asyncrono para subir imagenes </summary>
        /// <remarks> Subimos la imagen que hayamos obtenido lo subimos al Storage de firebase </remarks>
        /// <returns> Nos devuelve la url de la imagen en la firebase</returns>
        private async Task<string> subirImagenAsync(FileResult imagen) {
            FirebaseStorageTask task = new FirebaseStorage(Constante.Constante.STORAGE,
                new FirebaseStorageOptions {
                    ThrowOnCancel = false
                }
            ).Child("Imagenes")
             .Child(imagen.FileName)
             .PutAsync(await imagen.OpenReadAsync());
            return await task;
        }
        /// <summary> Métodos asyncrono para registrarte los datos del alumno </summary>
        /// <remarks> El método nos registra el realdate time los datos del alumno en la firebase </remarks>
        /// <param name="email">El mail del usuario</param>
        /// <param name="password">La contraseña del usuario</param>
        /// <param name="nombre">El nombre del usuario</param>
        /// <param name="centroDocente">El centro docente del usuario</param>
        /// <param name="cicloFormativo">El ciclo formativo del usuario</param>
        /// <param name="centroTrabajo">El centro de trabajo del usuario</param>
        /// <param name="grado">El grado del usuario</param>
        /// <param name="imagen">La imagen del usuario</param>
        /// <param name="profesorResponsable">El profesor responsable del usuario</param>
        /// <param name="tutorTrabajo">El tutor de trabajo del usuario</param>
        /// <returns> Devuelve el task del registro</returns>
        public async Task RegistrarGuardarDatosAsync(string nombre, string email, string password, FileResult imagen,string centroDocente,string profesorResponsable,string centroTrabajo,string tutorTrabajo, string grado, string cicloFormativo) {
                await cliente.Child("Usuario/"+ email.ToLower().Split("@")[0]).PutAsync(new Usuario {
                    NombreCompleto = nombre,
                    Gmail=email.ToLower(),
                    Contraseña= password,
                    Imagen= await subirImagenAsync(imagen),
                    CentroDocente=centroDocente,
                    ProfesorResponsable=profesorResponsable,
                    CentroTrabajo=centroTrabajo,
                    TutorTrabajo=tutorTrabajo,
                    Años=new List<Año>() { new Año()},
                    CicloFormativo= cicloFormativo,
                    Grado =grado
                });
        }
        /// <summary> Métodos asyncrono para actualiza los datos del alumno </summary>
        /// <remarks> El método nos actualiza el realdate time los datos del alumno especídfico en la firebase </remarks>
        /// <param name="email">El mail del usuario</param>
        /// <param name="password">La contraseña del usuario</param>
        /// <param name="nombre">El nombre del usuario</param>
        /// <param name="centroDocente">El centro docente del usuario</param>
        /// <param name="cicloFormativo">El ciclo formativo del usuario</param>
        /// <param name="centroTrabajo">El centro de trabajo del usuario</param>
        /// <param name="grado">El grado del usuario</param>
        /// <param name="imagen">La imagen del usuario</param>
        /// <param name="profesorResponsable">El profesor responsable del usuario</param>
        /// <param name="tutorTrabajo">El tutor de trabajo del usuario</param>
        /// <returns> Devuelve el task del registro</returns>
        public async Task ActualizarGuardarDatosAsync(string nombre, string email, string password, string imagen, string centroDocente, string profesorResponsable, string centroTrabajo, string tutorTrabajo, List<Año> año, string grado, string cicloFormativo) {
            await cliente.Child("Usuario/" + email.ToLower().Split("@")[0]).PutAsync(new Usuario {
                NombreCompleto = nombre,
                Gmail = email.ToLower(),
                Contraseña = password,
                Imagen = imagen,
                CentroDocente = centroDocente,
                ProfesorResponsable = profesorResponsable,
                CentroTrabajo = centroTrabajo,
                TutorTrabajo = tutorTrabajo,
                Años = año,
                Grado = grado,
                CicloFormativo = cicloFormativo,
            }); ;
        }
    }
}
