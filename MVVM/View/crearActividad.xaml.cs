using ProyectoApp.MVVM.Model;
using ProyectoApp.MVVM.ViewModel;

namespace ProyectoApp.MVVM.View;

public partial class crearActividad : ContentPage
{
	private UsuarioViewModel usuario;
	public string fechaAno;
    public string fechaMes;
    public string fechaDia;
    public crearActividad(UsuarioViewModel usuario,DatePicker fecha)
	{
		InitializeComponent();
		this.usuario = usuario;
        miAño.Text = fecha.Date.Year.ToString();
        miMes.Text = Mes.ObtenerNombreMes(fecha.Date.Month);
        miDia.Text = fecha.Date.Day.ToString();
        BindingContext = this;
	}
}