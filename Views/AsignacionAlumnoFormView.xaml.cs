using ControlDeColegio.ModelView;
using MahApps.Metro.Controls;

namespace ControlDeColegio.Views
{
    public partial class AsignacionAlumnoFormView : MetroWindow
    {
        public AsignacionAlumnoFormView(AsignacionAlumnoViewModel AsignacionAlumnoViewModel)
        {
            InitializeComponent();
            AsignacionAlumnoFormViewModel model = new AsignacionAlumnoFormViewModel(AsignacionAlumnoViewModel);
            this.DataContext = model;
        }
    }
}