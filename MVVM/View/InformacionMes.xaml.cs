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

}