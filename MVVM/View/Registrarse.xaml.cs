using ProyectoApp.ConexionFirebase;
namespace ProyectoApp.MVVM.View;

public partial class Registrarse : ContentPage
{
    private FileResult imagenSubida;
    private Conexión conexion = new Conexión();
    public Registrarse()
	{
		InitializeComponent();
	}

    private async void subirBoton_Clicked(object sender, EventArgs e) {
        var foto = await MediaPicker.PickPhotoAsync();
		if (foto != null ) {
			imageUrl.Source = foto.FullPath;
            imagenSubida = foto;
        }
    }
    private async void butRegistrarse_Clicked(object sender, EventArgs e) {
        try {
            await conexion.registrarse(miGmail.Text, miContraseña.Text);
            await conexion.RegistrarGuardarDatosAsync(nombre: miNombre.Text,email:miGmail.Text,password:miContraseña.Text,imagen: imagenSubida, centroDocente:miCentroDocente.Text, profesorResponsable:miprofesorResponable.Text, centroTrabajo:miCentroDeTrabajo.Text, tutorTrabajo:miTutorDeTrabajo.Text,grado:miGrado.Text);
            await DisplayAlert("Datos corecto", "Todos los datos han sido subido a la nube", "Vale");
            await Navigation.PopAsync(true);
        } catch
        {
            await DisplayAlert("Fallo en la autentificación", "Datos incorrectos", "Vale");
        }
    }
}