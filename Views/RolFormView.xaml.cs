using System.Windows;
using ControlDeColegio.ModelView;
using MahApps.Metro.Controls;


namespace ControlDeColegio.Views
{
    public partial class RolFormView : MetroWindow
    {
        public RolFormView(RolViewModel RolViewModel)
        {
            InitializeComponent();
            RolFormViewModel model = new RolFormViewModel(RolViewModel);
            this.DataContext = model;
        }
    }
}