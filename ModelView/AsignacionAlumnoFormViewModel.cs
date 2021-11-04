using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using ControlDeColegio.DataContext;
using ControlDeColegio.Models;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.EntityFrameworkCore;

namespace ControlDeColegio.ModelView
{
    public class AsignacionAlumnoFormViewModel : INotifyPropertyChanged, ICommand
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        public AsignacionAlumnoFormViewModel Instancia {get; set;}
        public AsignacionAlumnoViewModel AsignacionAlumnoViewModel {get; set;}
        public AsignacionAlumno AsignacionAlumnoForm {get; set;}
        private KalumDBContext dBContext;
        public IDialogCoordinator dialogCoordinator;
        public string Carne {get; set;}
        public string ClaseId {get; set;}
        public DateTime FechaAsignacion {get; set;}
        public string Titulo {get; set;}
        
        public AsignacionAlumnoFormViewModel(AsignacionAlumnoViewModel AsignacionAlumnoViewModel)
        {
            this.Instancia = this;
            this.AsignacionAlumnoViewModel = AsignacionAlumnoViewModel;
            if(this.AsignacionAlumnoViewModel.Seleccionado != null)
            {
                this.AsignacionAlumnoForm = new AsignacionAlumno();
                this.Titulo = "Modificar asignacion de alumno";
                this.Carne = AsignacionAlumnoViewModel.Seleccionado.Carne;
                this.ClaseId = AsignacionAlumnoViewModel.Seleccionado.ClaseId;
                this.FechaAsignacion = AsignacionAlumnoViewModel.Seleccionado.FechaAsignacion;
            }
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
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
                    this.dBContext.Entry(AsignacionAlumnoForm).State = EntityState.Modified;
                    this.dBContext.SaveChanges();
                    this.AsignacionAlumnoViewModel.AsignacionAlumno.RemoveAt(posicion);
                    this.AsignacionAlumnoViewModel.AsignacionAlumno.Insert(posicion, AsignacionAlumnoForm);
                    await this.dialogCoordinator.ShowMessageAsync(this,
                            "Alumno", "Registro actualizado");
                }
                ((Window)parameter).Close();
            }
        }
    }
}