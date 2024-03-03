using Microsoft.Maui.Controls;
using ProyectoApp.ConexionFirebase;
using ProyectoApp.MVVM.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<Año> ListaAño { get; set; } = new ObservableCollection<Año>();
        public ObservableCollection<Mes> ListaMes { get; set; } = new ObservableCollection<Mes>();
        public ObservableCollection<Dia> ListaDia { get; set; } = new ObservableCollection<Dia>();
        public ICommand ActualizarListaMeses { get; set; }
        public ICommand AñadirMes { get; set; }
        public ICommand CargarAño { get; set; }
        public ICommand MesesFaltantes { get; set; }
        public ICommand ObtenerMes { get; set; }
        public List<string> ListaMesesFaltantes { get; set; }=new List<string>();
        public string año { get; set; } = null;
        public string mes { get; set; } = null;
        public UsuarioViewModel(string gmailUsuario, string contraseñaUsuario) {
            this.gmailUsuario = gmailUsuario.ToLower();
            this.contraseñaUsuario = contraseñaUsuario.ToLower();
            CargarUsuario();
            ConstruirComandoActualizarListaMeses();
            ConstruirComandoAñadirMes();
            ConstruirComandoCargarAño();
            ConstruirComandoActualizarMesesFaltantes();
            ConstruirComandoCargarMes();
        }
        public void CargarUsuario() {
            ListaDia.Clear();
            ListaMes.Clear();
            ListaAño.Clear();
            conexion.GetCliente().Child("Usuario").AsObservable<Usuario>()
                    .Subscribe((user) => {
                        if (user.Object != null) {
                            if (user.Object.Contraseña.ToLower().Equals(contraseñaUsuario.ToLower())  && user.Object.Gmail.ToLower().Equals(gmailUsuario.ToLower()) ) {
                                    usuario = user.Object;
                                    añadirPrimerMesAsync();
                            }
                        }
                    });
        }
        public async Task añadirPrimerUnMesAsync(string mes) {
            for (int i = 0; i < usuario.Años.Count; i++) {
                if (usuario.Años[i].Meses.Count == 0) {
                    usuario.Años[i].Meses.Add(new Mes(mes));
                }
            }
            await conexion.ActualizarGuardarDatosAsync(usuario.NombreCompleto, usuario.Gmail, usuario.Contraseña, usuario.Imagen, usuario.CentroDocente, usuario.ProfesorResponsable, usuario.CentroTrabajo, usuario.TutorTrabajo, usuario.Años);
        }
        public async Task añadirPrimerMesAsync() {
            bool cambio = false;
            for (int i = 0; i < usuario.Años.Count; i++) {
                if (usuario.Años[i].Meses.Count == 0) {
                    usuario.Años[i].Meses.Add(new Mes());
                    cambio = true;
                }
            }
            if (cambio) {
                await conexion.ActualizarGuardarDatosAsync(usuario.NombreCompleto, usuario.Gmail, usuario.Contraseña, usuario.Imagen, usuario.CentroDocente, usuario.ProfesorResponsable, usuario.CentroTrabajo, usuario.TutorTrabajo, usuario.Años);
            }
        }
        public void ConstruirComandoCargarMes() {
            ObtenerMes = new Command(parametro => {
                ListaDia.Clear();
                mes = (string)parametro;
                for (int i = 0; i < usuario.Años.Count; i++) {
                    if (usuario.Años[i].fecha.Equals(año)) {
                        for (int j = 0; j < usuario.Años[i].Meses.Count; j++) {
                            if (usuario.Años[i].Meses[j].Nombre.Equals(mes)) {
                                for (int k=0;k< usuario.Años[i].Meses[j].Dias.Count;k++) {
                                    ListaDia.Add(usuario.Años[i].Meses[j].Dias[k]);
                                }
                            }
                        }
                    }
                 
                }
            });
        }
        public void ConstruirComandoCargarAño() {
            CargarAño = new Command(parametro => {
                ListaAño.Clear();
                for (int i = 0; i < usuario.Años.Count; i++) {
                    ListaAño.Add(usuario.Años[i]);
                }
            });
        }
        private void ConstruirComandoActualizarListaMeses() {
            ActualizarListaMeses = new Command(parametro => {
                año = (string)parametro;
                ListaMes.Clear();
                for (int i = 0; i < usuario.Años.Count; i++) {
                    if (usuario.Años[i].fecha.Equals(año)) {
                        for (int j=0;j< usuario.Años[i].Meses.Count;j++) {
                            ListaMes.Add(usuario.Años[i].Meses[j]);
                        }
                    }
                }
            });
          
        }
        private void ConstruirComandoActualizarMesesFaltantes() {
            MesesFaltantes = new Command(() => {
                List<string> listaMeses = new List<string>();
                List<string> listaRemover = new List<string>();
                listaRemover.AddRange(ListaMes.Select(x => x.Nombre).ToList());
                listaMeses.AddRange(Mes.ListaNombreMeses());
                ListaMesesFaltantes.Clear();
                ListaMesesFaltantes.AddRange(listaMeses.Where(i => !listaRemover.Contains(i)).ToList());
            });

        }

    
        private void ConstruirComandoAñadirMes() {
            AñadirMes = new Command(() => {
                if (año!=null) {
                    for (int i = 0; i < usuario.Años.Count; i++) {
                        if (usuario.Años[i].fecha.Equals(año)) {
                            usuario.Años[i].Meses.Add(new Mes());
                        }
                    }
                    conexion.ActualizarGuardarDatosAsync(usuario.NombreCompleto, usuario.Gmail, usuario.Contraseña, usuario.Imagen, usuario.CentroDocente, usuario.ProfesorResponsable, usuario.CentroTrabajo, usuario.TutorTrabajo, usuario.Años);
                    ListaAño.Clear();
                    ListaDia.Clear();
                    ListaMes.Clear();
                    for (int i = 0; i < usuario.Años.Count; i++) {
                        ListaAño.Add(usuario.Años[i]);
                    }
                    for (int i = 0; i < usuario.Años.Count; i++) {
                        if (usuario.Años[i].fecha.Equals(año)) {
                            for (int j = 0; j < usuario.Años[i].Meses.Count; j++) {
                                ListaMes.Add(usuario.Años[i].Meses[j]);
                            }
                        }
                    }
                }
                
            });
        }
    }
}
