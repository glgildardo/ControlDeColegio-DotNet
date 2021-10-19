using System.Windows;
using ControlDeColegio.ModelView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ControlDeColegio.Views
{
    public partial class HorarioFormView : MetroWindow
    {
        public HorarioFormView(HorarioViewModel HorarioViewModel)
        {
            InitializeComponent();
            HorarioFormViewModel model = new HorarioFormViewModel(HorarioViewModel, DialogCoordinator.Instance);
            this.DataContext = model;
        }
    }
}