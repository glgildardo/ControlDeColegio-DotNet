using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using ControlDeColegio.Models;

namespace ControlDeColegio.ModelView
{
    public class ModuloFormViewModel : INotifyPropertyChanged, ICommand
    {
        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;
        public ModuloFormViewModel Instancia {get; set;}
        public ModuloViewModel ModuloViewModel {get; set;}
        public Modulo ModuloForm {get; set;}
        public string CarreraId {get; set;}
        public string NombreModulo {get; set;}
        public int NumeroDeSeminarios {get; set;}

        public ModuloFormViewModel(ModuloViewModel ModuloViewModel)
        {
            this.Instancia = this;
            this.ModuloViewModel = ModuloViewModel;
            if(this.ModuloViewModel.Seleccionado != null)
            {
                this.ModuloForm = new Modulo();
                this.CarreraId = ModuloViewModel.Seleccionado.CarreraId;
                this.NombreModulo = ModuloViewModel.Seleccionado.NombreModulo;
                this.NumeroDeSeminarios = ModuloViewModel.Seleccionado.NumeroDeSeminarios;
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
                if(this.ModuloViewModel.Seleccionado == null)
                {
                    Modulo nuevo = new Modulo("4",  CarreraId, NombreModulo, NumeroDeSeminarios);
                    this.ModuloViewModel.agregarElemento(nuevo);
                }
                else
                {
                    ModuloForm.CarreraId = this.CarreraId;
                    ModuloForm.NombreModulo = this.NombreModulo;
                    ModuloForm.NumeroDeSeminarios = this.NumeroDeSeminarios;
                    int posicion = ModuloViewModel.Modulo.IndexOf(this.ModuloViewModel.Seleccionado);
                    this.ModuloViewModel.Modulo.RemoveAt(posicion);
                    this.ModuloViewModel.Modulo.Insert(posicion, ModuloForm);
                }
                ((Window)parameter).Close();
            }
        }
    }
}