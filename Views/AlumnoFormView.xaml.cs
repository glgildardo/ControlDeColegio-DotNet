using ControlDeColegio.ModelView;
using MahApps.Metro.Controls;

namespace ControlDeColegio.Views
{
    public partial class AlumnoFormView : MetroWindow
    {
        public AlumnoFormView(AlumnoViewModel AlumnoViewModel)
        {
            InitializeComponent();
            AlumnoFormViewModel model = new AlumnoFormViewModel(AlumnoViewModel);
            this.DataContext = model;
        }
    }
}