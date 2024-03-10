using ProyectoApp.MVVM.Model;
using ProyectoApp.MVVM.ViewModel;

namespace ProyectoApp.MVVM.View;

public partial class crearActividad : ContentPage
{
	private UsuarioViewModel usuario;
    private DatePicker fecha;
    public crearActividad(UsuarioViewModel usuario,DatePicker fecha)
	{
		InitializeComponent();
		this.usuario = usuario;
        this.fecha = fecha;
        miAño.Text = fecha.Date.Year.ToString();
        miMes.Text = Mes.ObtenerNombreMes(fecha.Date.Month);
        miDia.Text = fecha.Date.Day.ToString();
        BindingContext = this;
	}

    private async void Button_ClickedAsync(object sender, EventArgs e) {
        await usuario.añadirJornadaAsync(fecha.Date.Day,fecha.Date.Month,fecha.Date.Year,miActividad.Text,double.Parse(miTiempoEmpleado.Text),miObservaciones.Text );
        await Navigation.PopAsync();
    }
}