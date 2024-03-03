using Firebase.Auth;
using ProyectoApp.ConexionFirebase;

namespace ProyectoApp.MVVM.View;

public partial class Principal : ContentPage
{
    private Conexión conexion= new Conexión();
	public Principal()
	{
		InitializeComponent();
     
    }

    private async void butRegistrarse_Clicked(object sender, EventArgs e) {
        await Shell.Current.GoToAsync(nameof(Registrarse));
    }

    private async void butInicioSesion_ClickedAsync(object sender, EventArgs e) {
        try {
            await conexion.iniciar_sesion(miEmail.Text, miContraseña.Text);
            await DisplayAlert("Fallo en la autentificación", "Has iniciado sesión", "Vale");
            await Navigation.PushAsync(new Calendario(miEmail.Text, miContraseña.Text,new ViewModel.UsuarioViewModel(miEmail.Text, miContraseña.Text)));
        } catch {
            await DisplayAlert("Fallo en la autentificación", "El usuario o contraseña son incorrectos", "Vale");
        }
       
    }
}