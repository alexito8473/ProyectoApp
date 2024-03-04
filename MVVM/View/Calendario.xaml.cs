using ProyectoApp.MVVM.ViewModel;


namespace ProyectoApp.MVVM.View;

public partial class Calendario : ContentPage
{

    UsuarioViewModel usuario;
    public Calendario(UsuarioViewModel usuario) {
		InitializeComponent();
        this.usuario = usuario;
        BindingContext = usuario;
    }
    private void miFecha_DateSelected(object sender, DateChangedEventArgs e) {
        comprobacion();
    }
    private void comprobacion() {
        bool controlled = usuario.ListaJornada.Count == 0 ? true : false;
            usuario.cargarJornada(miFecha.Date.Day, miFecha.Date.Month, miFecha.Date.Year);
            mensaje.IsVisible = controlled;
            butAñadir.IsVisible = controlled;
        DisplayAlert("asd", miFecha.Date.Day.ToString(), "ok");
    }

    private void butAñadir_Clicked(object sender, EventArgs e) {
          Navigation.PushAsync(new crearActividad(usuario, miFecha));
    }
}