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
        public string HeightlblPassword {get; set;} = "Auto";
        public string HeightTxtPassword {get; set;} = "Auto";
        
        public RolFormViewModel(RolViewModel RolViewModel)
        {
            this.Instancia = this;
            this.RolViewModel = RolViewModel;
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
        }
        


    }
}