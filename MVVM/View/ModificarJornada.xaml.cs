using ProyectoApp.MVVM.Model;
using ProyectoApp.MVVM.ViewModel;
using System.Security.Cryptography;

namespace ProyectoApp.MVVM.View;
/// <summary> Clase vista donde podemos crear un actividad en un dia concreto </summary>
/// <remarks> Clase donde vamos a usar los métodos necesario para poder crear una actividad para un alumno.</remarks>
public partial class ModificarJornada : ContentPage{
    /// <summary> Atributo de la clase crearActividad</summary>
    /// <remarks> El atributo es usado para almacenar el ViewModel del usuario.</remarks>
    private UsuarioViewModel usuario;
    /// <summary> Atributo de la clase crearActividad</summary>
    /// <remarks> El atributo del dia de la actividad.</remarks>
    private int dia;
    /// <summary> Atributo de la clase crearActividad</summary>
    /// <remarks> El atributo del mes de la actividad.</remarks>
    private int mes;
    /// <summary> Atributo de la clase crearActividad</summary>
    /// <remarks> El atributo del año de la actividad.</remarks>
    private int año;
    /// <summary> Constructor de la clase ModificarJornada</summary>
    /// <remarks> Se instancia los componentes que tiene el programa, como los datos del usuario</remarks>
    /// <param name="usuario">Nos pasamos el el videmodel que controla al usuario que hemos escogido</param><
    /// <param name="año">Año de la actividad</param>
    /// <param name="dia">Dia de la actividad</param>
    /// <param name="mes">Mes de la actividad</param>
    public ModificarJornada(UsuarioViewModel usuario, int dia, int mes, int año){
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
    /// <summary> Botón de la clase crearActividad</summary>
    /// <remarks> Actualiza los datos de la actividad </remarks>
    /// <param name="e">Evento del método</param>
    /// <param name="sender"> Objecto del método</param>
    private void Button_Clicked(object sender, EventArgs e) {
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
            usuario.actualizarteJornadaAsync(dia, mes, año, miActividad.Text, double.Parse(miTiempoEmpleado.Text.Replace("hora", "").Trim()), miObservaciones.Text);
            Navigation.PopAsync();
        } else {
             DisplayAlert("Advertencia", mensaje, "Ok");
        }
    }
    /// <summary> Botón de la clase crearActividad</summary>
    /// <remarks> Disminuyes la hora  </remarks>
    /// <param name="e">Evento del método</param>
    /// <param name="sender"> Objecto del método</param>
    private void Button_Clicked2(object sender, EventArgs e) {
        if (miTiempoEmpleado.Text == "1 hora") {
            DisplayAlert("Advertencia", "No puede ser menor que 1 hora", "Ok");
        } else {
            miTiempoEmpleado.Text = (int.Parse(miTiempoEmpleado.Text.Replace("hora", "").Trim()) - 1).ToString() + " hora";
        }
    }
    /// <summary> Botón de la clase crearActividad</summary>
    /// <remarks> Aumenta la hora  </remarks>
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