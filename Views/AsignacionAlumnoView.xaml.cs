using ControlDeColegio.ModelView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ControlDeColegio.Views
{
    public partial class AsignacionAlumnoView : MetroWindow
    {
        public AsignacionAlumnoView()
        {
            InitializeComponent();
            AsignacionAlumnoViewModel model = new AsignacionAlumnoViewModel(DialogCoordinator.Instance);
            this.DataContext = model;
        }
    }
}