using ProyectoApp.MVVM.Model;
using ProyectoApp.MVVM.ViewModel;
using System.Diagnostics;

namespace ProyectoApp.MVVM.View;
/// <summary> Clase vista donde podemos ver la informacion de un usuario </summary>
/// <remarks> Clase donde vamos a usar para ver todos los datos de un usuario.</remarks>
public partial class InfromacionUsuario : ContentPage{

    /// <summary> Constructor de la clase InfromacionUsuario</summary>
    /// <remarks> Se instancia los componentes que tiene el programa, como los datos del usuario</remarks>
    /// <param name="usuario">Nos pasamos el el videmodel que controla al usuario que hemos escogido</param>
    public InfromacionUsuario(Usuario usuario){
		InitializeComponent();
        BindingContext = usuario;
        Debug.WriteLine(usuario.NombreCompleto);
    }
}