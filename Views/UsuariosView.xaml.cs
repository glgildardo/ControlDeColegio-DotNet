using System.Windows;
using ControlDeColegio.ModelView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ControlDeColegio.Views
{
    public partial class UsuariosView : MetroWindow
    {
        public UsuariosView()
        {
            InitializeComponent();
            UsuariosViewModel ModeloDatos = new UsuariosViewModel(DialogCoordinator.Instance);
            this.DataContext = ModeloDatos; 
        }
    }
}