using System.Windows;
using ControlDeColegio.ModelView;

namespace ControlDeColegio.Views
{
    public partial class RolView : Window
    {
        public RolView()
        {
            InitializeComponent();
            RolViewModel ModelDatos = new RolViewModel();
            this.DataContext = ModelDatos;
        }
    }
}