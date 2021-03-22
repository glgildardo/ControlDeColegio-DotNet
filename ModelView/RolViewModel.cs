using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using ControlDeColegio.Models;
using ControlDeColegio.Views;

namespace ControlDeColegio.ModelView
{
    public class RolViewModel : INotifyPropertyChanged, ICommand
    {
        public ObservableCollection<Rol> Roles {get; set;}

        public RolViewModel Instancia {get; set;}
        
        public Rol Seleccionado {get;set;}
        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler CanExecuteChanged;
        public RolViewModel()
        {
            this.Instancia = this;
            this.Roles = new ObservableCollection<Rol>();
         this.Roles.Add(new Rol(1, "ROLE_ADMIN"));
         this.Roles.Add(new Rol(2, "ROLE_USER"));
         this.Roles.Add(new Rol(3, "ROLE_SUPERV"));
        } 

        public void NotificarCambio(string property)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        public void agregarElemento(Rol nuevo){
            this.Roles.Add(nuevo);
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(parameter.Equals("Nuevo"))
            {
                RolFormView nuevoRol = new RolFormView(Instancia);
                nuevoRol.Show();
            }
            else if (parameter.Equals("Eliminar"))
            {
                if(this.Seleccionado == null){
                    MessageBox.Show("Debe seleccionar un elemento");
                } else {
                    this.Roles.Remove(Seleccionado);
                }
            }
            else if(parameter.Equals("Modificar"))
            {
                if(this.Seleccionado == null)
                {
                    MessageBox.Show("Debe seleccionar un elemento");
                }
                else
                {
                    RolFormView modificarRol = new RolFormView(Instancia);
                    modificarRol.ShowDialog();
                }
            }
        }
    }
}