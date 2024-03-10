using ProyectoApp.MVVM.ViewModel;

namespace ProyectoApp.MVVM.View;

public partial class InfromacionUsuario : ContentPage
{
	public InfromacionUsuario(UsuarioViewModel model)
	{
		InitializeComponent();
		UsuarioViewModel usuario= model;
        miImagen.Source = usuario.usuario.First().Imagen;
		miNombre.Text= usuario.usuario.First().NombreCompleto;
		miGrado.Text = usuario.usuario.First().Grado;
        miCentroDo.Text = usuario.usuario.First().CentroDocente;
		miCentroTra.Text = usuario.usuario.First().CentroTrabajo;
		miTutor.Text = usuario.usuario.First().TutorTrabajo;
		miProfesor.Text = usuario.usuario.First().ProfesorResponsable;
		miGmail.Text = usuario.usuario.First().Gmail;
    }
}