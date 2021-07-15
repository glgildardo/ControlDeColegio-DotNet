using ControlDeColegio.ModelView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ControlDeColegio.Views
{
    public partial class CarreraFormView : MetroWindow
    {
        public CarreraFormView(CarreraViewModel CarreraViewMOdel)
        {
            InitializeComponent();
            CarreraFormViewModel model = new CarreraFormViewModel(CarreraViewMOdel, DialogCoordinator.Instance);
            this.DataContext = model;            
        }
    }
}