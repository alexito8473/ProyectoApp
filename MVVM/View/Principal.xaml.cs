namespace ProyectoApp.MVVM.View;

public partial class Principal : ContentPage
{
	public Principal()
	{
		InitializeComponent();
     
    }

    private async void butRegistrarse_Clicked(object sender, EventArgs e) {
        await Shell.Current.GoToAsync(nameof(Registrarse));
    }
}