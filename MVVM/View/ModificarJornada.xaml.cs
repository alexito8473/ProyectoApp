using ProyectoApp.MVVM.Model;
using ProyectoApp.MVVM.ViewModel;
using System.Security.Cryptography;

namespace ProyectoApp.MVVM.View;

public partial class ModificarJornada : ContentPage
{
    private UsuarioViewModel usuario;
    private int dia;
    private int mes;
    private int año;

    public ModificarJornada(UsuarioViewModel usuario, int dia, int mes, int año)
	{
		InitializeComponent();
        this.usuario = usuario;
        Jornada jornada = usuario.GetJornada(dia, mes, año);
        miActividad.Text = jornada.ActividadDesarrollada;
		miTiempoEmpleado.Text = jornada.TiempoEmpleado.ToString()+" hora";
		miObservaciones.Text = jornada.Observaciones;
        this.dia = dia;
        this.mes = mes;
        this.año = año;
    }

    private void Button_Clicked(object sender, EventArgs e) {
        usuario.actualizarteJornadaAsync(dia, mes, año, miActividad.Text,double.Parse(miTiempoEmpleado.Text.Replace("hora", "").Trim()), miObservaciones.Text);
        Navigation.PopAsync();
    }
    private void Button_Clicked2(object sender, EventArgs e) {
        if (miTiempoEmpleado.Text == "1 hora") {
            DisplayAlert("Advertencia", "No puede ser menor que 1 hora", "Ok");
        } else {
            miTiempoEmpleado.Text = (int.Parse(miTiempoEmpleado.Text.Replace("hora", "").Trim()) - 1).ToString() + " hora";
        }
    }

    private void Button_Clicked_1(object sender, EventArgs e) {
        if (miTiempoEmpleado.Text == "8 hora") {
            DisplayAlert("Advertencia", "No puede ser menor que 8 hora", "Ok");
        } else {
            miTiempoEmpleado.Text = (int.Parse(miTiempoEmpleado.Text.Replace("hora", "").Trim()) + 1).ToString() + " hora";
        }
    }
}