namespace ProyectoApp.MVVM.View;

public partial class Registrarse : ContentPage
{
	public Registrarse()
	{
		InitializeComponent();
	}

    private async void subirBoton_Clicked(object sender, EventArgs e) {
        var foto = await MediaPicker.PickPhotoAsync();
		if (foto != null ) {
			imageUrl.Source = foto.FullPath;

        }
    }
    private void butRegistrarse_Clicked(object sender, EventArgs e) {

    }
}