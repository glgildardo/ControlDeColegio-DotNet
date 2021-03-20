using System;
using System.ComponentModel;
using System.Windows.Input;
using ControlDeColegio.Models;

namespace ControlDeColegio.ModelView
{
    public class RolFormViewModel : INotifyPropertyChanged, ICommand
    {
        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public RolFormViewModel Instancia {get; set;}
        public RolViewModel RolViewModel {get; set;}
        public string Nombre {get; set;}
        
        public RolFormViewModel(RolViewModel RolFormViewModel)
        {
            this.Instancia = this;
            this.RolViewModel = RolFormViewModel;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(parameter.Equals("Guardar"))
            {
                Rol nuevo = new Rol(4, Nombre);
                this.RolViewModel.agregarElemento(nuevo);        
                
            }
            else if(parameter.Equals("Cancelar"))
            {

            }
        }
        


    }
}