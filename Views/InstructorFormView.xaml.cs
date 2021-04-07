using System.Windows;
using ControlDeColegio.ModelView;
using MahApps.Metro.Controls;

namespace ControlDeColegio.Views
{
    public partial class InstructorFormView : MetroWindow
    {
        public InstructorFormView(InstructorViewModel InstructorViewModel)
        {
            InitializeComponent();
            InstructorFormViewModel model = new InstructorFormViewModel(InstructorViewModel);
            this.DataContext = model;
        }
    }
}