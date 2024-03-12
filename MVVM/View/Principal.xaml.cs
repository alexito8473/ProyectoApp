using Microsoft.Maui.Animations;
using ProyectoApp.ConexionFirebase;
using ProyectoApp.MVVM.Model;
using ProyectoApp.MVVM.ViewModel;

namespace ProyectoApp.MVVM.View;
/// <summary> Clase vista donde tenemos la lógica del inicio de sesión </summary>
/// <remarks> Clase donde tenemos todos los controles para realizar el inicio de sesion.</remarks>
public partial class Principal : ContentPage{
    /// <summary> Atributo de la clase Principal</summary>
    /// <remarks> El atributo que se realiza la conexion de la firebase.</remarks>
    private Conexión conexion= new Conexión();
    /// <summary> Atributo de la clase Principal</summary>
    /// <remarks> El atributo donde almacenamos el view model del usuario.</remarks>
    private UsuarioViewModel usuario;
    /// <summary> Constructor de la clase Principal</summary>
    /// <remarks> Se instancia los componentes que tiene el programa. </remarks>
    public Principal(){
		InitializeComponent();
    }
    /// <summary> Botón asyncrono de la clase Principal</summary>
    /// <remarks>  Nos redirige al registro, para poder registrarnos en la firebase </remarks>
    /// <param name="e">Evento del método</param>
    /// <param name="sender"> Objecto del método</param>
    private async void butRegistrarse_Clicked(object sender, EventArgs e) {
        await Shell.Current.GoToAsync(nameof(Registrarse));
    }
    /// <summary> Botón asyncrono de la clase Principal</summary>
    /// <remarks> Nos realiza las comprobaciones para iniciar sesión el la app </remarks>
    /// <param name="e">Evento del método</param>
    /// <param name="sender"> Objecto del método</param>
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
                Thread.Sleep(3000); // Si no le meto tiempo explota
                await DisplayAlert("Correcto", "Te has logeado correctamente", "Ok");
                await Navigation.PushAsync(new Calendario(usuario));
            } catch {
                await DisplayAlert("Fallo en la autentificación", "El usuario o contraseña son incorrectos", "Ok");
            }
        } 
       
    }
}