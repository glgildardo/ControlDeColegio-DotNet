using ControlDeColegio.ModelView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ControlDeColegio.Views
{
    public partial class ClaseView : MetroWindow
    {
        public ClaseView()
        {
            InitializeComponent();
            ClaseViewModel model = new ClaseViewModel(DialogCoordinator.Instance);
            this.DataContext = model;
        }
    }
}