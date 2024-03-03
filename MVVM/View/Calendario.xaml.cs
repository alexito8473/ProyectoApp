
using ProyectoApp.ConexionFirebase;
using ProyectoApp.MVVM.Model;
using ProyectoApp.MVVM.ViewModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ProyectoApp.MVVM.View;

public partial class Calendario : ContentPage
{
    private string gmailUsuario;
    private string contraseñaUsuario;
    UsuarioViewModel usuario;
    public Calendario(string gmailUsuario, string contraseñaUsuario, UsuarioViewModel usuario) {
		InitializeComponent();
        this.gmailUsuario = gmailUsuario;
        this.contraseñaUsuario = contraseñaUsuario;
        this.usuario = usuario;
        BindingContext = usuario;
      
    }

    private void Button_Clicked(object sender, EventArgs e) {
        if (usuario.año!=null) {
            if (usuario.ListaMesesFaltantes.Count > 0) {
                Navigation.PushAsync(new AñadirMes(usuario));
            } else {
                DisplayAlert("Información", "No puedes tener más meses", "ok");
            }
        } else {
            DisplayAlert("Información", "Debes seleccionar antes un año", "ok");
        }
    }

    private void Button_Clicked_1(object sender, EventArgs e) {
        Navigation.PushAsync(new InformacionMes(usuario));
    }

    private void Button_Clicked_2(object sender, EventArgs e) {
        butAño.IsVisible = true;
        butMes.IsVisible = true;
    }
}