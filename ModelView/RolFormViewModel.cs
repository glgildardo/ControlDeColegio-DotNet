using System;
using System.ComponentModel;
using System.Windows;
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
        
        public Rol RolForm {get; set;}
        public RolFormViewModel(RolViewModel RolViewModel)
        {
            this.Instancia = this;
            this.RolViewModel = RolViewModel;
            if(this.RolViewModel.Seleccionado != null)
            {
                this.RolForm = new Rol();
                this.Nombre = RolViewModel.Seleccionado.Nombre;
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
                if(this.RolViewModel.Seleccionado == null)
                {
                    Rol nuevo = new Rol(4, Nombre);
                    this.RolViewModel.agregarElemento(nuevo);                
                }
                else
                {
                    RolForm.Nombre = this.Nombre;
                    int posicion = RolViewModel.Roles.IndexOf(this.RolViewModel.Seleccionado);
                    this.RolViewModel.Roles.RemoveAt(posicion);
                    this.RolViewModel.Roles.Insert(posicion, RolForm);
                }
                ((Window)parameter).Close();
            }
        }
        


    }
}