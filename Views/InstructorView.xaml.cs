using ControlDeColegio.ModelView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ControlDeColegio.Views
{
    public partial class InstructorView : MetroWindow
    {
        public InstructorView()
        {
            InitializeComponent();
            InstructorViewModel modeloDatos = new InstructorViewModel(DialogCoordinator.Instance);
            this.DataContext = modeloDatos;
        }
    }
}