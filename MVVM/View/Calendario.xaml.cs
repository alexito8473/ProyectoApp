using ProyectoApp.MVVM.ViewModel;


namespace ProyectoApp.MVVM.View;

public partial class Calendario : ContentPage
{

    private UsuarioViewModel usuario;
    public Calendario(UsuarioViewModel usuario) {
		InitializeComponent();
        this.usuario = usuario;
        establecer();
        BindingContext = this.usuario;
    }
    private void miFecha_DateSelected(object sender, DateChangedEventArgs e) {
        establecer();
    }
    private void establecer() {
        try {
            usuario.cargarJornada(miFecha.Date.Day, miFecha.Date.Month, miFecha.Date.Year);
            mensaje.IsVisible = usuario.ListaJornada.Count == 0 ? true : false;
        }catch(Exception ex) {
            Navigation.PopAsync();
        }
       
    }
    private void butAñadir_Clicked(object sender, EventArgs e) {
        Navigation.PushAsync(new crearActividad(usuario, miFecha));
        mensaje.IsVisible = usuario.ListaJornada.Count == 0 ? true : false;
    }

    private void Button_Clicked(object sender, EventArgs e) {
        Navigation.PushAsync(new InfromacionUsuario(usuario));
    }

    private void Button_ClickedModificar(object sender, EventArgs e) {
        Navigation.PushAsync(new ModificarJornada(usuario, miFecha.Date.Day, miFecha.Date.Month, miFecha.Date.Year));
    }

    private void Button_ClickedBorrar(object sender, EventArgs e) {
        usuario.borrarJornada(miFecha.Date.Day, miFecha.Date.Month, miFecha.Date.Year);
        DisplayAlert("Información","Se ha borrado correctamente de la base de datos","ok");
    }
}