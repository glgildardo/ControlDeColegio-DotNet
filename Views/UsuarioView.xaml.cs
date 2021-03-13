using System.Windows;
using ControlDeColegio.ModelView;

namespace ControlDeColegio.Views
{
    public partial class UsuarioView : Window
    {
        public UsuarioView(UsuariosViewModel UsuariosViewModel)
        {
            InitializeComponent();
            UsuarioViewModel model =  new UsuarioViewModel(UsuariosViewModel);
            this.DataContext = model;
            
        }
    }
}