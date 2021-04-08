using ControlDeColegio.ModelView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ControlDeColegio.Views
{
    public partial class ModuloView : MetroWindow
    {
        public ModuloView()
        {
            InitializeComponent();
            ModuloViewModel model = new ModuloViewModel(DialogCoordinator.Instance);
            this.DataContext = model;
        }
    }
}