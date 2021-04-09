using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using ControlDeColegio.Models;

namespace ControlDeColegio.ModelView
{
    public class AsignacionAlumnoFormViewModel : INotifyPropertyChanged, ICommand
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        public AsignacionAlumnoFormViewModel Instancia {get; set;}
        public AsignacionAlumnoViewModel AsignacionAlumnoViewModel {get; set;}
        public AsignacionAlumno AsignacionAlumnoForm {get; set;}
        public string Carne {get; set;}
        public string ClaseId {get; set;}
        public DateTime FechaAsignacion {get; set;}
        
        public AsignacionAlumnoFormViewModel(AsignacionAlumnoViewModel AsignacionAlumnoViewModel)
        {
            this.Instancia = this;
            this.AsignacionAlumnoViewModel = AsignacionAlumnoViewModel;
            if(this.AsignacionAlumnoViewModel.Seleccionado != null)
            {
                this.AsignacionAlumnoForm = new AsignacionAlumno();
                this.Carne = AsignacionAlumnoViewModel.Seleccionado.Carne;
                this.ClaseId = AsignacionAlumnoViewModel.Seleccionado.ClaseId;
                this.FechaAsignacion = AsignacionAlumnoViewModel.Seleccionado.FechaAsignacion;
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
                if(this.AsignacionAlumnoViewModel.Seleccionado == null)
                {
                    AsignacionAlumno nuevo = new AsignacionAlumno("4", Carne, ClaseId, FechaAsignacion);
                    this.AsignacionAlumnoViewModel.agregarElemento(nuevo);
                }
                else
                {
                    AsignacionAlumnoForm.Carne = this.Carne;
                    AsignacionAlumnoForm.ClaseId = this.ClaseId;
                    AsignacionAlumnoForm.FechaAsignacion = this.FechaAsignacion;
                    int posicion = AsignacionAlumnoViewModel.AsignacionAlumno.IndexOf(this.AsignacionAlumnoViewModel.Seleccionado);
                    this.AsignacionAlumnoViewModel.AsignacionAlumno.RemoveAt(posicion);
                    this.AsignacionAlumnoViewModel.AsignacionAlumno.Insert(posicion, AsignacionAlumnoForm);
                }
                ((Window)parameter).Close();
            }
        }
    }
}