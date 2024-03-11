using ProyectoApp.MVVM.Model;
using ProyectoApp.MVVM.ViewModel;
using System.Text.RegularExpressions;

namespace ProyectoApp.MVVM.View;
/// <summary> Clase vista donde podemos crear un actividad en un dia concreto </summary>
/// <remarks> Clase donde vamos a usar los métodos necesario para poder crear una actividad para un alumno.</remarks>
public partial class crearActividad : ContentPage{
    /// <summary> Atributo de la clase crearActividad</summary>
    /// <remarks> El atributo es usado para almacenar el ViewModel del usuario.</remarks>
    private UsuarioViewModel usuario;
    /// <summary> Atributo de la clase crearActividad</summary>
    /// <remarks> El atributo es usado para almacenar la fecha de la actividad.</remarks>
    private DatePicker fecha;
    /// <summary> Constructor de la clase crearActividad</summary>
    /// <remarks> Se instancia los componentes que tiene el programa. Como la fecha que vamos a mostrar </remarks>
    /// <param name="usuario">Nos pasamos el el videmodel que controla al usuario que hemos escogido</param>
    /// <param name="fecha">La fecha que vamos a crear una actividad</param>
    public crearActividad(UsuarioViewModel usuario,DatePicker fecha){
		InitializeComponent();
		this.usuario = usuario;
        this.fecha = fecha;
        miAño.Text = fecha.Date.Year.ToString();
        miMes.Text = Mes.ObtenerNombreMes(fecha.Date.Month);
        miDia.Text = fecha.Date.Day.ToString();
        miTiempoEmpleado.Text = "1 hora";
        BindingContext = this;
	}
    /// <summary> Botón de la clase crearActividad</summary>
    /// <remarks> Realiza las comprobaciones para poder crear una actividad. </remarks>
    /// <param name="e">Evento del método</param>
    /// <param name="sender"> Objecto del método</param>
    private async void Button_ClickedAsync(object sender, EventArgs e) {
        string mensaje = "";
        bool control = true;
        if (string.IsNullOrEmpty(miActividad.Text)) {
            mensaje = mensaje + "La actividad no puede estar vacia ";
                control = false;
        }
        if (string.IsNullOrEmpty(miObservaciones.Text)) {
            mensaje = mensaje + "Las observaviones no puede estar vacia ";
            control = false;
        }
        if (control) {
            await usuario.añadirJornadaAsync(fecha.Date.Day, fecha.Date.Month, fecha.Date.Year, miActividad.Text, double.Parse(miTiempoEmpleado.Text.Replace("hora", "").Trim()), miObservaciones.Text);
            await Navigation.PopAsync();
        } else {
            await DisplayAlert("Advertencia", mensaje, "Ok");
        }
       
    }

    /// <summary> Botón de la clase crearActividad</summary>
    /// <remarks> Realiza las comprobaciones para añadir una quitar 1 hora del total de la actividad. </remarks>
    /// <param name="e">Evento del método</param>
    /// <param name="sender"> Objecto del método</param>
    private void Button_Clicked(object sender, EventArgs e) {
        if (miTiempoEmpleado.Text=="1 hora") {
            DisplayAlert("Advertencia", "No puede ser menor que 1 hora", "Ok");
        } else {
            miTiempoEmpleado.Text = (int.Parse(miTiempoEmpleado.Text.Replace("hora","").Trim()) - 1).ToString() + " hora";
        }
    }
    /// <summary> Botón de la clase crearActividad</summary>
    /// <remarks> Realiza las comprobaciones para añadir una añadir 1 hora del total de la actividad. </remarks>
    /// <param name="e">Evento del método</param>
    /// <param name="sender"> Objecto del método</param>
    private void Button_Clicked_1(object sender, EventArgs e) {
        if (miTiempoEmpleado.Text == "8 hora") {
            DisplayAlert("Advertencia", "No puede ser menor que 8 hora", "Ok");
        } else {
            miTiempoEmpleado.Text = (int.Parse(miTiempoEmpleado.Text.Replace("hora", "").Trim()) + 1).ToString() + " hora";
        }
    }
}