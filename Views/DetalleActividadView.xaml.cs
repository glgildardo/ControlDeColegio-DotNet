using ControlDeColegio.ModelView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ControlDeColegio.Views
{
    public partial class DetalleActividadView : MetroWindow
    {
        public DetalleActividadView()
        {
            InitializeComponent();
            DetalleActividadViewModel model = new DetalleActividadViewModel(DialogCoordinator.Instance);
            this.DataContext = model;
        }
    }
}