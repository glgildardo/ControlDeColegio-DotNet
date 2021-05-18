using System.Windows;
using ControlDeColegio.ModelView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ControlDeColegio.Views
{
    public partial class UsuarioView : MetroWindow
    {
        public UsuarioView(UsuariosViewModel UsuariosViewModel)
        {
            InitializeComponent();
            UsuarioViewModel model =  new UsuarioViewModel(UsuariosViewModel, DialogCoordinator.Instance);
            this.DataContext = model;
            
        }
    }
}