using System.Windows;
using ControlDeColegio.ModelView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ControlDeColegio.Views
{
    public partial class SalonView : MetroWindow
    {
        public SalonView()
        {
            InitializeComponent();
            SalonViewModel ModelDatos = new SalonViewModel(DialogCoordinator.Instance);
            this.DataContext = ModelDatos;
        }
    }
}