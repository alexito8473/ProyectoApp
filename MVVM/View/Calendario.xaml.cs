using ProyectoApp.MVVM.ViewModel;


namespace ProyectoApp.MVVM.View;
/// <summary> Clase vista donde podemos ver el calendario de un usuario </summary>
/// <remarks> Clase donde vamos a usar los métodos necesario para el manejo del usuario y su calendario.</remarks>
public partial class Calendario : ContentPage{
    /// <summary> Atributo de la clase Calendario</summary>
    /// <remarks> El atributo es usado para poder almacenar los método que vamos a usar en el programa.</remarks>
    private UsuarioViewModel usuario=null;
    /// <summary> Constructor de la clase Calendario</summary>
    /// <remarks> Se instancia los componentes que tiene el programa. </remarks>
    /// <param name="usuario">Nos pasamos el el videmodel que controla al usuario que hemos escogido</param>
    public Calendario(UsuarioViewModel usuario) {
		InitializeComponent();
        this.usuario = usuario;
        establecer();
        BindingContext = this.usuario;
    }
    /// <summary> Método de la clase Calendario</summary>
    /// <remarks> Se instancia los componentes que tiene el programa. </remarks>
    /// <param name="sender">Nos pasamos el el videmodel que controla al usuario que hemos escogido</param>
    /// <param name="e">Nos pasamos el el videmodel que controla al usuario que hemos escogido</param>
    private void miFecha_DateSelected(object sender, DateChangedEventArgs e) {
        establecer();
    }
    /// <summary> Método de la clase Calendario</summary>
    /// <remarks> Se establece las jornadas de una fecha concreta. </remarks>
    private void establecer() {
        try {
            if (usuario.usuario.Años==null) {
                DisplayAlert("Advertencia","Aun no se han recopilado todos los datos","Ok");
            }
            usuario.cargarJornada(miFecha.Date.Day, miFecha.Date.Month, miFecha.Date.Year);
            mensaje.IsVisible = usuario.ListaJornada.Count == 0 ? true : false;
        }catch {
            DisplayAlert("Advertencia", "Aun no se han recopilado todos los datos", "Ok");
        }
    }
    /// <summary> Botón de la clase Calendario</summary>
    /// <remarks> Se establece las jornadas de una fecha concreta. </remarks>
    /// <param name="e">Evento del método</param>
    /// <param name="sender"> Objecto del método</param>
    private void butAñadir_Clicked(object sender, EventArgs e) {
        if (usuario.usuario != null) {
            Navigation.PushAsync(new crearActividad(usuario, miFecha));
            establecer();
        } else {
            DisplayAlert("Advertencia", "Aun no se han recopilado todos los datos", "Ok");
        }
    }

    /// <summary> Botón de la clase Calendario </summary>
    /// <remarks> Nos lleva al la pantalla de informacion el usaurio</remarks>
    /// <param name="e">Evento del método</param>
    /// <param name="sender"> Objecto del método</param>
    private void Button_Clicked(object sender, EventArgs e) {
        if (usuario.usuario != null) {
            Navigation.PushAsync(new InfromacionUsuario(usuario.usuario));
            mensaje.IsVisible = usuario.ListaJornada.Count == 0 ? true : false;
        } else {
            DisplayAlert("Advertencia", "Aun no se han recopilado todos los datos", "Ok");
        }
    }

    /// <summary> Botón de la clase Calendario</summary>
    /// <remarks> Nos lleva a la vista para poder modificar una actividad</remarks>
    /// <param name="e">Evento del método</param>
    /// <param name="sender"> Objecto del método</param>
    private void Button_ClickedModificar(object sender, EventArgs e) {
        Navigation.PushAsync(new ModificarJornada(usuario, miFecha.Date.Day, miFecha.Date.Month, miFecha.Date.Year));
        mensaje.IsVisible = usuario.ListaJornada.Count == 0 ? true : false;
    }
    
    /// <summary> Botón de la clase Calendario</summary>
    /// <remarks> El método nos borra un ajornada específica</remarks>
    /// <param name="e">Evento del método</param>
    /// <param name="sender"> Objecto del método</param>
    private void Button_ClickedBorrar(object sender, EventArgs e) {
        usuario.borrarJornada(miFecha.Date.Day, miFecha.Date.Month, miFecha.Date.Year);
        DisplayAlert("Información","Se ha borrado correctamente de la base de datos","ok");
        establecer();
    }
}