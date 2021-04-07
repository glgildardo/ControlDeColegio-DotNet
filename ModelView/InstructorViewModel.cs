using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using ControlDeColegio.Models;
using ControlDeColegio.Views;
using MahApps.Metro.Controls.Dialogs;

namespace ControlDeColegio.ModelView
{
    public class InstructorViewModel : INotifyPropertyChanged, ICommand
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        private IDialogCoordinator dialogCoordinator;
        public ObservableCollection<Instructor> Instructor {get; set;}
        public InstructorViewModel Instancia {get; set;}
        public Instructor Seleccionado {get; set;}
        public InstructorViewModel(IDialogCoordinator instance)
        {
            this.Instancia = this;
            this.dialogCoordinator = instance;
            this.Instructor = new ObservableCollection<Instructor>();
            this.Instructor.Add(new Instructor("1", "Pelope Lopez", "Instructor que hace trabaja bien", "6ta. calle A", "Activo", "Foto", "Filomeno Chente", "52541312"));
            this.Instructor.Add(new Instructor("2", "Caceres Mazariegos", "Le falta capacitacion", "5ta. calle B", "Activo", "Foto", "Daniel Diego", "54531413"));
            this.Instructor.Add(new Instructor("3", "Velasquez Vasquez", "Buen trabajo", "4ta. calle C", "Activo", "Foto", "Filomeno Chente", "52541312"));
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void NotificarCambio(string property)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        public void agregarElemento(Instructor nuevo)
        {
            this.Instructor.Add(nuevo);
        }
        public async void Execute(object parameter)
        {
            if(parameter.Equals("Nuevo"))
            {
                this.Seleccionado = null;
                InstructorFormView nuevoInstructor = new InstructorFormView(Instancia);
                nuevoInstructor.Show();
            }
            else if(parameter.Equals("Eliminar"))
            {
                if(this.Seleccionado == null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this, "Instructor", "Sebe seleccionar un Instructor", MessageDialogStyle.Affirmative);
                }
                else 
                {
                    MessageDialogResult respuesta = await this.dialogCoordinator.ShowMessageAsync(this,
                        "Eliminar Instructor", "¿Esta seguro de eliminar el Instructor?",
                        MessageDialogStyle.AffirmativeAndNegative);
                    if(respuesta == MessageDialogResult.Affirmative)
                    {
                        this.Instructor.Remove(Seleccionado);
                    }
                }
            }
            else if(parameter.Equals("Modificar"))
            {
                if(this.Seleccionado == null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this,
                    "Seleccionar Instructor", "Debe seleccionar un Instructor");
                }
                else
                {
                    InstructorFormView modificarInstructor = new InstructorFormView(Instancia);
                    modificarInstructor.ShowDialog();
                }
            }
        }
    }
}