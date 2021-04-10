using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using ControlDeColegio.Models;

namespace ControlDeColegio.ModelView
{
    public partial class CarreraFormViewModel : INotifyPropertyChanged, ICommand
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        public CarreraFormViewModel Instancia {get; set;}
        public CarreraViewModel CarreraViewModel {get; set;}
        public Carrera CarreraForm {get; set;}
        public string Nombre {get; set;}

        public CarreraFormViewModel(CarreraViewModel CarreraViewModel)
        {
            this.Instancia = this;
            this.CarreraViewModel = CarreraViewModel;
            if(this.CarreraViewModel.Seleccionado != null)
            {
                this.CarreraForm = new Carrera();
                this.Nombre = CarreraViewModel.Seleccionado.Nombre;
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
                if(this.CarreraViewModel.Seleccionado == null)
                {
                    Carrera nuevo = new Carrera("4", Nombre);
                    this.CarreraViewModel.agregarElemento(nuevo);
                }
                else
                {
                    CarreraForm.Nombre = this.Nombre;
                    int posicion = CarreraViewModel.Carrera.IndexOf(this.CarreraViewModel.Seleccionado);
                    this.CarreraViewModel.Carrera.RemoveAt(posicion);
                    this.CarreraViewModel.Carrera.Insert(posicion, CarreraForm);
                }
                ((Window)parameter).Close();
            }
        }
    }
}