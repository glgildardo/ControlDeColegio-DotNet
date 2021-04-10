using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using ControlDeColegio.Models;

namespace ControlDeColegio.ModelView
{
    public class DetalleActividadFormViewModel : INotifyPropertyChanged, ICommand
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        public DetalleActividadFormViewModel Instancia {get; set;}
        public DetalleActividadViewModel DetalleActividadViewModel {get; set;}
        public DetalleActividad DetalleActividadForm {get; set;}
        public string SeminarioId {get; set;}
        public string NombreActividad {get; set;}
        public int NotaActividad {get; set;}
        public DateTime FechaCreacion {get; set;}
        public DateTime FechaEntrega {get; set;}
        public DateTime FechaPostergacion {get; set;}
        public char Estado {get; set;}

        public DetalleActividadFormViewModel(DetalleActividadViewModel DetalleActividadViewModel)
        {
            this.Instancia = this;
            this.DetalleActividadViewModel = DetalleActividadViewModel;
            if(this.DetalleActividadViewModel.Seleccionado != null)
            {
                this.DetalleActividadForm = new DetalleActividad();
                this.SeminarioId = DetalleActividadViewModel.Seleccionado.SeminarioId;
                this.NombreActividad = DetalleActividadViewModel.Seleccionado.NombreActividad;
                this.NotaActividad = DetalleActividadViewModel.Seleccionado.NotaActividad;
                this.FechaCreacion = DetalleActividadViewModel.Seleccionado.FechaCreacion;
                this.FechaEntrega = DetalleActividadViewModel.Seleccionado.FechaEntrega;
                this.FechaPostergacion = DetalleActividadViewModel.Seleccionado.FechaPostergacion;
                this.Estado = DetalleActividadViewModel.Seleccionado.Estado;
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
                if(this.DetalleActividadViewModel.Seleccionado == null)
                {
                    DetalleActividad nuevo = new DetalleActividad("4", SeminarioId, NombreActividad, NotaActividad, FechaCreacion, FechaEntrega, FechaPostergacion, Estado);
                    this.DetalleActividadViewModel.agregarElemento(nuevo);
                }
                else
                {
                    DetalleActividadForm.SeminarioId = this.SeminarioId;
                    DetalleActividadForm.NombreActividad = this.NombreActividad;
                    DetalleActividadForm.NotaActividad = this.NotaActividad;
                    DetalleActividadForm.FechaCreacion = this.FechaCreacion;
                    DetalleActividadForm.FechaEntrega = this.FechaEntrega;
                    DetalleActividadForm.FechaPostergacion = this.FechaPostergacion;
                    DetalleActividadForm.Estado = this.Estado;
                    int posicion = DetalleActividadViewModel.DetalleActividad.IndexOf(this.DetalleActividadViewModel.Seleccionado);
                    this.DetalleActividadViewModel.DetalleActividad.RemoveAt(posicion);
                    this.DetalleActividadViewModel.DetalleActividad.Insert(posicion, DetalleActividadForm);
                }
                ((Window)parameter).Close();
            }
        }
    }
}