using ControlDeColegio.ModelView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ControlDeColegio.Views
{
    public partial class AlumnoFormView : MetroWindow
    {
        public AlumnoFormView(AlumnoViewModel AlumnoViewModel)
        {
            InitializeComponent();
            AlumnoFormViewModel model = new AlumnoFormViewModel(AlumnoViewModel, DialogCoordinator.Instance);
            this.DataContext = model;
        }
    }
}