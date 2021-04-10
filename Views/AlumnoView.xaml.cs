using ControlDeColegio.ModelView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ControlDeColegio.Views
{
    public partial class AlumnoView : MetroWindow
    {
        public AlumnoView()
        {
            InitializeComponent();
            AlumnoViewModel model = new AlumnoViewModel(DialogCoordinator.Instance);
            this.DataContext = model;
        }
    }
}