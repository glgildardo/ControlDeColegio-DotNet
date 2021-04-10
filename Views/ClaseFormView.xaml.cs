using ControlDeColegio.ModelView;
using MahApps.Metro.Controls;

namespace ControlDeColegio.Views
{
    public partial class ClaseFormView : MetroWindow
    {
        public ClaseFormView(ClaseViewModel ClaseViewModel)
        {
            InitializeComponent();
            ClaseFormViewModel model = new ClaseFormViewModel(ClaseViewModel);
            this.DataContext = model;
        }
    }
}