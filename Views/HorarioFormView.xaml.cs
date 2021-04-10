using System.Windows;
using ControlDeColegio.ModelView;
using MahApps.Metro.Controls;

namespace ControlDeColegio.Views
{
    public partial class HorarioFormView : MetroWindow
    {
        public HorarioFormView(HorarioViewModel HorarioViewModel)
        {
            InitializeComponent();
            HorarioFormViewModel model = new HorarioFormViewModel(HorarioViewModel);
            this.DataContext = model;
        }
    }
}