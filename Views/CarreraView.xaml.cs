using ControlDeColegio.ModelView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ControlDeColegio.Views
{
    public partial class CarreraView : MetroWindow
    {
        public CarreraView()
        {
            InitializeComponent();
            CarreraViewModel model = new CarreraViewModel(DialogCoordinator.Instance);
            this.DataContext = model;
        }
    }
}