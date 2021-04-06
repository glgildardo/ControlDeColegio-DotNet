using System.Windows;
using ControlDeColegio.ModelView;
using MahApps.Metro.Controls;

namespace ControlDeColegio.Views
{
    public partial class SalonFormView : MetroWindow
    {
        public SalonFormView(SalonViewModel SalonViewModel)
        {
            InitializeComponent();
            SalonFormViewModel model = new SalonFormViewModel(SalonViewModel);
            this.DataContext = model;
        }  
    }
}