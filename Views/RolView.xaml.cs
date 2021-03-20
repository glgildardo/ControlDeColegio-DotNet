using System.Windows;
using ControlDeColegio.ModelView;
using MahApps.Metro.Controls;

namespace ControlDeColegio.Views
{
    public partial class RolView : MetroWindow
    {
        public RolView()
        {
            InitializeComponent();
            RolViewModel ModelDatos = new RolViewModel();
            this.DataContext = ModelDatos;
        }
    }
}