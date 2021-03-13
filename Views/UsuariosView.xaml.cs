using System.Windows;
using ControlDeColegio.ModelView;

namespace ControlDeColegio.Views
{
    public partial class UsuariosView : Window
    {
        public UsuariosView()
        {
            InitializeComponent();
            UsuariosViewModel ModeloDatos = new UsuariosViewModel();
            this.DataContext = ModeloDatos; 
        }
    }
}