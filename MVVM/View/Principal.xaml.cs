using ProyectoApp.ConexionFirebase;
using ProyectoApp.MVVM.Model;
using ProyectoApp.MVVM.ViewModel;

namespace ProyectoApp.MVVM.View;

public partial class Principal : ContentPage
{
    private Conexión conexion= new Conexión();
    private UsuarioViewModel usuario;
    public Principal(){
		InitializeComponent();
    }

    private async void butRegistrarse_Clicked(object sender, EventArgs e) {
        await Shell.Current.GoToAsync(nameof(Registrarse));
    }

    private async void butInicioSesion_ClickedAsync(object sender, EventArgs e) {
        string mensaje = "";
        bool isEmpty = false;
        if (string.IsNullOrEmpty(miEmail.Text)) {
            mensaje = "Introduce un gmail.";
            isEmpty = true;
        }
        if (string.IsNullOrEmpty(miContraseña.Text)) {
            mensaje = mensaje+"Introduce una contraseña.";
            isEmpty = true;
        }
        if (isEmpty) {
            await DisplayAlert("Advertencia", mensaje, "OK");
        } else {
            try {
                await conexion.iniciar_sesion(miEmail.Text, miContraseña.Text);
                usuario = new UsuarioViewModel(miEmail.Text,conexion);
                await Navigation.PushAsync(new Calendario(usuario));
            } catch {
                await DisplayAlert("Fallo en la autentificación", "El usuario o contraseña son incorrectos", "Ok");
            }
        } 
       
    }
}