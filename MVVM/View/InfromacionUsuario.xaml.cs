using ProyectoApp.MVVM.ViewModel;

namespace ProyectoApp.MVVM.View;

public partial class InfromacionUsuario : ContentPage
{
	public InfromacionUsuario(UsuarioViewModel model)
	{
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