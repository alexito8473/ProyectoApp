using ProyectoApp.ConexionFirebase;
namespace ProyectoApp.MVVM.View;
/// <summary> Clase vista donde tenemos la lógica del registro </summary>
/// <remarks> Clase donde tenemos todos los controles para realizar el registro en la app.</remarks>
public partial class Registrarse : ContentPage{
    /// <summary> Atributo de la clase Registrarse</summary>
    /// <remarks> El atributo que se realiza para almacenar la imagenSelecionada.</remarks>
    private FileResult imagenSubida=null;
    /// <summary> Atributo de la clase Registrarse</summary>
    /// <remarks> El atributo que se utiliza para almacenar la conexion al servidor.</remarks>
    private Conexión conexion = new Conexión();
    /// <summary> Constructor de la clase Registrarse</summary>
    /// <remarks> Se instancia los componentes que tiene el programa</remarks>
    public Registrarse(){
		InitializeComponent();
	}
    /// <summary> Botón asyncrono de la clase Registrarse</summary>
    /// <remarks> Se selecciona una imagen</remarks>
    /// <param name="e">Evento del método</param>
    /// <param name="sender"> Objecto del método</param>
    private async void subirBoton_Clicked(object sender, EventArgs e) {
        var foto = await MediaPicker.PickPhotoAsync();
		if (foto != null ) {
			imageUrl.Source = foto.FullPath;
            imagenSubida = foto;
        }
    }
    /// <summary> Botón asyncrono de la clase Registrarse</summary>
    /// <remarks> Controla que los campos esten con contenido, además que registra el usuario en la firebases</remarks>
    /// <param name="e">Evento del método</param>
    /// <param name="sender"> Objecto del método</param>
    private async void butRegistrarse_Clicked(object sender, EventArgs e) {
        if (string.IsNullOrEmpty(miNombre.Text) || string.IsNullOrEmpty(miNombre.Text) || string.IsNullOrEmpty(miGmail.Text) || string.IsNullOrEmpty(miContraseña.Text)
            || imagenSubida==null || string.IsNullOrEmpty(miCentroDocente.Text) || string.IsNullOrEmpty(miprofesorResponable.Text) || string.IsNullOrEmpty(miCentroDeTrabajo.Text)
            || string.IsNullOrEmpty(miTutorDeTrabajo.Text) || string.IsNullOrEmpty(miGrado.Text) || string.IsNullOrEmpty(miCiclo.Text)) {
            await DisplayAlert("Advertencia","Uno o varios campos estan vacios","Ok");
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