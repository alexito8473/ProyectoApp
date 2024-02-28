using ProyectoApp.MVVM.View;

namespace ProyectoApp {
    public partial class AppShell : Shell {
        public AppShell() {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Registrarse), typeof(Registrarse));
        }
    }
}
