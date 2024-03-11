using ProyectoApp.ConexionFirebase;
namespace ProyectoApp.MVVM.View;

public partial class Registrarse : ContentPage
{
    private FileResult imagenSubida=null;
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
        if (string.IsNullOrEmpty(miNombre.Text) || string.IsNullOrEmpty(miNombre.Text) || string.IsNullOrEmpty(miGmail.Text) || string.IsNullOrEmpty(miContraseña.Text)
            || imagenSubida==null || string.IsNullOrEmpty(miCentroDocente.Text) || string.IsNullOrEmpty(miprofesorResponable.Text) || string.IsNullOrEmpty(miCentroDeTrabajo.Text)
            || string.IsNullOrEmpty(miTutorDeTrabajo.Text) || string.IsNullOrEmpty(miGrado.Text) || string.IsNullOrEmpty(miCiclo.Text)) {
            DisplayAlert("Advertencia","Uno o varios campos estan vacios","Ok");
        } else {
            try {
                await conexion.registrarse(miGmail.Text, miContraseña.Text);
                await conexion.RegistrarGuardarDatosAsync(nombre: miNombre.Text, email: miGmail.Text, password: miContraseña.Text, imagen: imagenSubida, centroDocente: miCentroDocente.Text, profesorResponsable: miprofesorResponable.Text, centroTrabajo: miCentroDeTrabajo.Text, tutorTrabajo: miTutorDeTrabajo.Text, grado: miGrado.Text, cicloFormativo: miCiclo.Text);
                await DisplayAlert("Datos corecto", "Todos los datos han sido subido a la nube", "Ok");
                await Navigation.PopAsync(true);
            } catch {
                await DisplayAlert("Fallo en la autentificación", "Gmail o contraseñas invalidos", "Ok");
            }
        }
       
    }
}