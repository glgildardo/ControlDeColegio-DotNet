using ControlDeColegio.ModelView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ControlDeColegio.Views
{
    public partial class DetalleNotaView : MetroWindow
    {
        public DetalleNotaView()
        {
            InitializeComponent();
            DetalleNotaViewModel model = new DetalleNotaViewModel(DialogCoordinator.Instance);
            this.DataContext = model;
        }
    }
}