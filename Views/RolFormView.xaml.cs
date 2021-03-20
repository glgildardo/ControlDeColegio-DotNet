using System.Windows;
using ControlDeColegio.ModelView;
using MahApps.Metro.Controls;


namespace ControlDeColegio.Views
{
    public partial class RolFormView : MetroWindow
    {
        public RolFormView(RolViewModel RolFormViewModel)
        {
            InitializeComponent();
            RolFormViewModel model = new RolFormViewModel(RolFormViewModel);
            this.DataContext = model;
        }
    }
}