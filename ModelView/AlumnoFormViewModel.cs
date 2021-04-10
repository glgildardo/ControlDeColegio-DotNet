using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using ControlDeColegio.Models;

namespace ControlDeColegio.ModelView
{
    public class AlumnoFormViewModel : INotifyPropertyChanged, ICommand
    {
        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;
        public AlumnoFormViewModel Instancia {get; set;}
        public AlumnoViewModel AlumnoViewModel {get; set;}
        public Alumno AlumnoForm {get; set;}
        public string Carne {get; set;}
        public string NoExpediente {get; set;}
        public string Apellidos {get; set;}
        public string Nombres {get; set;}
        public string Email {get; set;}

        public AlumnoFormViewModel(AlumnoViewModel AlumnoViewModel)
        {
            this.Instancia = this;
            this.AlumnoViewModel = AlumnoViewModel;
            if(this.AlumnoViewModel.Seleccionado != null)
            {
                this.AlumnoForm = new Alumno();
                this.Carne = AlumnoViewModel.Seleccionado.Carne;
                this.NoExpediente = AlumnoViewModel.Seleccionado.NoExpediente;
                this.Apellidos = AlumnoViewModel.Seleccionado.Apellidos;
                this.Nombres = AlumnoViewModel.Seleccionado.Nombres;
                this.Email = AlumnoViewModel.Seleccionado.Email;
            }
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(parameter is Window)
            {
                if(this.AlumnoViewModel.Seleccionado == null)
                {
                    Alumno nuevo = new Alumno(Carne, NoExpediente, Apellidos, Nombres, Email);
                    this.AlumnoViewModel.agregarElemento(nuevo);
                }
                else
                {
                    AlumnoForm.Carne = this.Carne;
                    AlumnoForm.NoExpediente = this.NoExpediente;
                    AlumnoForm.Apellidos = this.Apellidos;
                    AlumnoForm.Nombres = this.Nombres;
                    AlumnoForm.Email = this.Email;
                    int posicion = AlumnoViewModel.Alumno.IndexOf(this.AlumnoViewModel.Seleccionado);
                    this.AlumnoViewModel.Alumno.RemoveAt(posicion);
                    this.AlumnoViewModel.Alumno.Insert(posicion, AlumnoForm);
                }
                ((Window)parameter).Close();
            }
        }
    }
}