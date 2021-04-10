using ControlDeColegio.ModelView;
using MahApps.Metro.Controls;

namespace ControlDeColegio.Views
{
    public partial class ModuloFormView : MetroWindow
    {
        public ModuloFormView(ModuloViewModel ModuloViewMOdel)
        {
            InitializeComponent();
            ModuloFormViewModel model = new ModuloFormViewModel(ModuloViewMOdel);
            this.DataContext = model;            
        }
    }
}