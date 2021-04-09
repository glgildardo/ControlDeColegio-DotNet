using ControlDeColegio.ModelView;
using MahApps.Metro.Controls;

namespace ControlDeColegio.Views
{
    public partial class DetalleActividadFormView : MetroWindow
    {
        public DetalleActividadFormView(DetalleActividadViewModel DetalleActividadViewModel)
        {
            InitializeComponent();
            DetalleActividadFormViewModel model = new DetalleActividadFormViewModel(DetalleActividadViewModel);
            this.DataContext = model;
        }
    }
}