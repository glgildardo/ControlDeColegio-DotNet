using System.Windows;
using ControlDeColegio.ModelView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ControlDeColegio.Views
{
    public partial class HorarioView : MetroWindow
    {
        public HorarioView()
        {
            InitializeComponent();
            HorarioViewModel ModelDatos = new HorarioViewModel();
            this.DataContext = ModelDatos;
        }
    }
}