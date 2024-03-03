using ProyectoApp.MVVM.Model;
using ProyectoApp.MVVM.ViewModel;

namespace ProyectoApp.MVVM.View;

public partial class AñadirMes : ContentPage
{
	UsuarioViewModel usuarioMio;
    public AñadirMes(UsuarioViewModel usuarioMio)
	{
		InitializeComponent();
		this.usuarioMio = usuarioMio;
        BindingContext = this.usuarioMio;
    }

    public async void Button_ClickedAsync(object sender, EventArgs e) {
        if (miMes.SelectedItem!=null) {
            await usuarioMio.añadirPrimerMesAsync();
            await Navigation.PopAsync(true);
        } else {
            await  DisplayAlert("Error","No has seleccionado un més","Ok");
        }
        
    }
}