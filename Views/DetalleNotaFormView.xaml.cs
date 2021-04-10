using ControlDeColegio.ModelView;
using MahApps.Metro.Controls;

namespace ControlDeColegio.Views
{
    public partial class DetalleNotaFormView : MetroWindow
    {
        public DetalleNotaFormView(DetalleNotaViewModel DetalleNotaViewModel)
        {
            InitializeComponent();
            DetalleNotaFormViewModel model = new DetalleNotaFormViewModel(DetalleNotaViewModel);
            this.DataContext = model;
        }
    }
}