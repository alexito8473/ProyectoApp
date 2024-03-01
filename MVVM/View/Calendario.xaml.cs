
using ProyectoApp.ConexionFirebase;
using ProyectoApp.MVVM.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ProyectoApp.MVVM.View;

public partial class Calendario : ContentPage
{
    private string gmailUsuario;
    private string contraseñaUsuario;
    private Conexión conexion= new Conexión();
    public ObservableCollection<Usuario> Lista { get; set; } = new ObservableCollection<Usuario>();
    public ObservableCollection<Año> ListaAño { get; set; } = new ObservableCollection<Año>();
    public ObservableCollection<Año> ListaMeses { get; set; } = new ObservableCollection<Año>();
    public ICommand ActualizarLista { get; set; }
    public Calendario(string gmailUsuario, string contraseñaUsuario){
		InitializeComponent();
        this.gmailUsuario = gmailUsuario;
        this.contraseñaUsuario = contraseñaUsuario;
        CargarLista();
        BindingContext = this;
       
    }
    public void CargarLista() {
        Usuario user;
        conexion.GetCliente().Child("Usuario").AsObservable<Usuario>()
                .Subscribe((user) => {
                    if (user.Object != null) {
                        if (user.Object.Contraseña.ToLower() == contraseñaUsuario.ToLower() && user.Object.Gmail.ToLower() == gmailUsuario.ToLower()) {
                            Lista.Add(user.Object);
                            foreach (Año año in user.Object.Años) { 
                                ListaAño.Add(año);
                            }  
                        }
                    }
                });;
    }
   

}