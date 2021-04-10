using ControlDeColegio.ModelView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ControlDeColegio.Views
{
    public partial class SeminarioView : MetroWindow
    {
        public SeminarioView()
        {
            InitializeComponent();
            SeminarioViewModel model = new SeminarioViewModel(DialogCoordinator.Instance);
            this.DataContext = model;
        }
    }
}