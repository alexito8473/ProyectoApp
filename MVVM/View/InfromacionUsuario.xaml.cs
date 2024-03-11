using ProyectoApp.MVVM.ViewModel;

namespace ProyectoApp.MVVM.View;
/// <summary> Clase vista donde podemos ver la informacion de un usuario </summary>
/// <remarks> Clase donde vamos a usar para ver todos los datos de un usuario.</remarks>
public partial class InfromacionUsuario : ContentPage{
    /// <summary> Constructor de la clase InfromacionUsuario</summary>
    /// <remarks> Se instancia los componentes que tiene el programa, como los datos del usuario</remarks>
    /// <param name="model">Nos pasamos el el videmodel que controla al usuario que hemos escogido</param>
    public InfromacionUsuario(UsuarioViewModel model){
		InitializeComponent();
		UsuarioViewModel usuario= model;
        miImagen.Source = usuario.usuario.Imagen;
		miNombre.Text= usuario.usuario.NombreCompleto;
		miGrado.Text = usuario.usuario.Grado;
        miCentroDo.Text = usuario.usuario.CentroDocente;
		miCentroTra.Text = usuario.usuario.CentroTrabajo;
		miTutor.Text = usuario.usuario.TutorTrabajo;
		miProfesor.Text = usuario.usuario.ProfesorResponsable;
		miGmail.Text = usuario.usuario.Gmail;
    }
}