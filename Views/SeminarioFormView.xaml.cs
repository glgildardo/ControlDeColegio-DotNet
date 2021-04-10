using ControlDeColegio.ModelView;
using MahApps.Metro.Controls;

namespace ControlDeColegio.Views
{
    public partial class SeminarioFormView : MetroWindow
    {
        public SeminarioFormView(SeminarioViewModel SeminarioViewModel)
        {
            InitializeComponent();
            SeminarioFormViewModel model = new SeminarioFormViewModel(SeminarioViewModel);
            this.DataContext = model;
        }
    }
}