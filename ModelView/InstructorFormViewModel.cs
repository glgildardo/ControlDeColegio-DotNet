using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using ControlDeColegio.Models;
using ControlDeColegio.ModelView;
using MahApps.Metro.Controls;

namespace ControlDeColegio.ModelView
{
    public partial class InstructorFormViewModel : INotifyPropertyChanged, ICommand
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        public InstructorFormViewModel Instancia {get; set;}
        public InstructorViewModel InstructorViewModel {get; set;}
        public Instructor InstructorForm {get; set;}
        public string Apellidos {get; set;}
        public string Comentario {get; set;}
        public string Direccion {get; set;}
        public string Estatus {get; set;}
        public string Foto {get; set;}
        public string Nombres {get; set;}
        public string Telefono {get; set;} 

        public InstructorFormViewModel(InstructorViewModel InstructorViewModel)
        {
            this.Instancia = this;
            this.InstructorViewModel = InstructorViewModel;
            if(this.InstructorViewModel.Seleccionado != null)
            {
                this.InstructorForm = new Instructor();
                this.Apellidos = InstructorViewModel.Seleccionado.Apellidos;
                this.Comentario = InstructorViewModel.Seleccionado.Comentario;
                this.Direccion = InstructorViewModel.Seleccionado.Direccion;
                this.Estatus = InstructorViewModel.Seleccionado.Estatus;
                this.Foto = InstructorViewModel.Seleccionado.Foto;
                this.Nombres = InstructorViewModel.Seleccionado.Nombres;
                this.Telefono = InstructorViewModel.Seleccionado.Telefono;
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
                if(this.InstructorViewModel.Seleccionado == null)
                {
                    Instructor nuevo = new Instructor("4", Apellidos, Comentario, Direccion, Estatus, Foto, Nombres, Telefono);
                    this.InstructorViewModel.agregarElemento(nuevo);
                }
                else
                {
                    InstructorForm.Apellidos = this.Apellidos;
                    InstructorForm.Comentario = this.Comentario;
                    InstructorForm.Direccion = this.Direccion;
                    InstructorForm.Estatus = this.Estatus;
                    InstructorForm.Foto = this.Foto;
                    InstructorForm.Nombres = this.Nombres;
                    InstructorForm.Telefono = this.Telefono;
                    int posicion = InstructorViewModel.Instructor.IndexOf(this.InstructorViewModel.Seleccionado);
                    this.InstructorViewModel.Instructor.RemoveAt(posicion);
                    this.InstructorViewModel.Instructor.Insert(posicion, InstructorForm);
                }
                ((Window)parameter).Close();
            }
        }
    }
}