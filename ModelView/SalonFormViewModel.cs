using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using ControlDeColegio.Models;

namespace ControlDeColegio.ModelView
{
    public class SalonFormViewModel : INotifyPropertyChanged, ICommand
    {
        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;
        public SalonFormViewModel Instancia {get; set;}
        public SalonViewModel SalonViewModel {get; set;}
        public Salon SalonForm {get; set;}
        public int Capacidad {get; set;}
        public string Descripcion {get; set;}
        public string NombreSalon {get; set;}

        public SalonFormViewModel(SalonViewModel SalonViewModel)
        {
            this.Instancia = this;
            this.SalonViewModel = SalonViewModel;
            if(this.SalonViewModel.Seleccionado != null)
            {
                this.SalonForm = new Salon();
                this.Capacidad = SalonViewModel.Seleccionado.Capacidad;
                this.Descripcion = SalonViewModel.Seleccionado.Descripcion;
                this.NombreSalon = SalonViewModel.Seleccionado.NombreSalon;
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
                if(this.SalonViewModel.Seleccionado == null)
                {
                    Salon nuevo = new Salon("4", Capacidad, Descripcion, NombreSalon);
                    this.SalonViewModel.agregarElemento(nuevo);
                }
                else
                {
                    SalonForm.Capacidad = this.Capacidad;
                    SalonForm.Descripcion = this.Descripcion;
                    SalonForm.NombreSalon = this.NombreSalon;
                    int posicion = SalonViewModel.Salon.IndexOf(this.SalonViewModel.Seleccionado);
                    this.SalonViewModel.Salon.RemoveAt(posicion);
                    this.SalonViewModel.Salon.Insert(posicion, SalonForm);
                }
                ((Window)parameter).Close();
            }
        }
    }
}