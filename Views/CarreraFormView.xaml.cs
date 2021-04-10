using ControlDeColegio.ModelView;
using MahApps.Metro.Controls;

namespace ControlDeColegio.Views
{
    public partial class CarreraFormView : MetroWindow
    {
        public CarreraFormView(CarreraViewModel CarreraViewMOdel)
        {
            InitializeComponent();
            CarreraFormViewModel model = new CarreraFormViewModel(CarreraViewMOdel);
            this.DataContext = model;            
        }
    }
}