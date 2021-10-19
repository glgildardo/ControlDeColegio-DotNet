using ControlDeColegio.ModelView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ControlDeColegio.Views
{
    public partial class ClaseFormView : MetroWindow
    {
        public ClaseFormView(ClaseViewModel ClaseViewModel)
        {
            InitializeComponent();
            ClaseFormViewModel model = new ClaseFormViewModel(ClaseViewModel, DialogCoordinator.Instance);
            this.DataContext = model;
        }
    }
}