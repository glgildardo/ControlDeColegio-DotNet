using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using ControlDeColegio.Models;

namespace ControlDeColegio.ModelView
{
    public class DetalleNotaFormViewModel : INotifyPropertyChanged, ICommand
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        public DetalleNotaFormViewModel Instancia {get; set;}
        public DetalleNotaViewModel DetalleNotaViewModel {get; set;}
        public DetalleNota DetalleNotaForm {get; set;}
        public string DetalleActividadId {get; set;}
        public string Carne {get; set;}
        public int ValorNota {get; set;}

        public DetalleNotaFormViewModel(DetalleNotaViewModel DetalleNotaViewModel)
        {
            this.Instancia = this;
            this.DetalleNotaViewModel = DetalleNotaViewModel;
            if(this.DetalleNotaViewModel.Seleccionado != null)
            {
                this.DetalleNotaForm = new DetalleNota();
                this.DetalleActividadId = DetalleNotaViewModel.Seleccionado.DetalleActividadId;
                this.Carne = DetalleNotaViewModel.Seleccionado.Carne;
                this.ValorNota = DetalleNotaViewModel.Seleccionado.ValorNota;                
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
                if(this.DetalleNotaViewModel.Seleccionado == null)
                {
                    DetalleNota nuevo = new DetalleNota("4", DetalleActividadId, Carne, ValorNota);
                    this.DetalleNotaViewModel.agregarElemento(nuevo);
                }
                else
                {
                    DetalleNotaForm.DetalleActividadId = this.DetalleActividadId;
                    DetalleNotaForm.Carne = this.Carne;
                    DetalleNotaForm.ValorNota = this.ValorNota;
                    int posicion = DetalleNotaViewModel.DetalleNota.IndexOf(this.DetalleNotaViewModel.Seleccionado);
                    this.DetalleNotaViewModel.DetalleNota.RemoveAt(posicion);
                    this.DetalleNotaViewModel.DetalleNota.Insert(posicion, DetalleNotaForm);
                }
                ((Window)parameter).Close();
            }
        }
    }
}