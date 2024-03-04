using ProyectoApp.MVVM.ViewModel;

namespace ProyectoApp.MVVM.View;

public partial class InformacionMes : ContentPage
{
	private UsuarioViewModel usuario;

    public InformacionMes(UsuarioViewModel usuario)
	{
		InitializeComponent();
		this.usuario = usuario;
		BindingContext= usuario;
	}

    private void Button_Clicked(object sender, EventArgs e) {
		Navigation.PushAsync(new JornadaDia());
    }
}