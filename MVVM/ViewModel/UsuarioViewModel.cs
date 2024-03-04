using Microsoft.Maui.Controls;
using ProyectoApp.ConexionFirebase;
using ProyectoApp.MVVM.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProyectoApp.MVVM.ViewModel
{
    public class UsuarioViewModel{
        private string gmailUsuario;
        private string contraseñaUsuario;
        private Conexión conexion = new Conexión();
        public Usuario usuario { get; set; }
        public ObservableCollection<Jornada> ListaJornada { get; set; } = new ObservableCollection<Jornada>();
        public UsuarioViewModel(string gmailUsuario, string contraseñaUsuario) {
            this.gmailUsuario = gmailUsuario.ToLower();
            this.contraseñaUsuario = contraseñaUsuario.ToLower();
            CargarUsuario();
        }
        public void CargarUsuario() {
            conexion.GetCliente().Child("Usuario").AsObservable<Usuario>()
                    .Subscribe((user) => {
                        if (user.Object != null) {
                            if (user.Object.Contraseña.ToLower().Equals(contraseñaUsuario.ToLower())  && user.Object.Gmail.ToLower().Equals(gmailUsuario.ToLower()) ) {
                                 usuario = user.Object;
                                 añadirMeses();
                            }
                        }
                    });
        }
        public async Task añadirMeses() {
            bool cambio = false;
            for (int i=0;i< usuario.Años.Count;i++) {
                if (usuario.Años[i].Meses.Count == 0) {
                    for (int j = 1; j <= 12; j++) {
                        usuario.Años[i].Meses.Add(new Mes(Mes.ObtenerNombreMes(i)));
                        cambio = true;
                    }      
                }
            }
            if (cambio) { 
            await conexion.ActualizarGuardarDatosAsync(usuario.NombreCompleto, usuario.Gmail, usuario.Contraseña, usuario.Imagen, usuario.CentroDocente, usuario.ProfesorResponsable, usuario.CentroTrabajo, usuario.TutorTrabajo, usuario.Años,usuario.Grado);
            }
        }
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
            };
        }}
    }
}
