using System.Windows;
using ControlDeColegio.ModelView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ControlDeColegio.Views
{
    public partial class SalonFormView : MetroWindow
    {
        public SalonFormView(SalonViewModel SalonViewModel)
        {
            InitializeComponent();
            SalonFormViewModel model = new SalonFormViewModel(SalonViewModel, DialogCoordinator.Instance);
            this.DataContext = model;
        }  
    }
}