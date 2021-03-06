using System.Windows;
using ControlDeColegio.ModelView;

namespace ControlDeColegio.Views
{
    public partial class UsuariosView : Window
    {
        public UsuariosView()
        {
            InitializeComponent();
            UsuarioViewModel ModeloDatos = new UsuarioViewModel();
            this.DataContext = ModeloDatos; 
        }
    }
}