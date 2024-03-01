using Firebase.Auth.Providers;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using ProyectoApp.MVVM.Model;
using Firebase.Database.Query;
using Firebase.Storage;
using System.Collections;

namespace ProyectoApp.ConexionFirebase {
    public class Conexión {
        private FirebaseClient cliente = new FirebaseClient(Constante.Constante.REALMTIME_STORAGE) ;

        public FirebaseClient GetCliente() {
            return cliente ;
        }
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
        public async Task<String> iniciar_sesion(string email, string password) {
            var client = obtenerToken();
            var authResult = await client.SignInWithEmailAndPasswordAsync(email, password);
            return await authResult.User.GetIdTokenAsync();
        }
        public async Task<String> registrarse(string email, string password) {
            var client = obtenerToken();
            var authResult = await client.CreateUserWithEmailAndPasswordAsync(email, password);
            return await authResult.User.GetIdTokenAsync();
        }
        
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
        public async Task RegistrarGuardarDatosAsync(string nombre, string email, string password, FileResult imagen,string centroDocente,string profesorResponsable,string centroTrabajo,string tutorTrabajo) {
                await cliente.Child("Usuario").PostAsync(new Usuario {
                    NombreCompleto = nombre,
                    Gmail=email.ToLower(),
                    Contraseña= password,
                    Imagen= await subirImagenAsync(imagen),
                    CentroDocente=centroDocente,
                    ProfesorResponsable=profesorResponsable,
                    CentroTrabajo=centroTrabajo,
                    TutorTrabajo=tutorTrabajo,
                    Años=new List<Año>() { new Año()}
                });;;
           // await cliente.Child("Calendario").PostAsync(new CalendarioModel(email, password)); ;
        }

    }
}
