using System;
using System.ComponentModel;
using System.Windows.Input;
using ControlDeColegio.Views;

namespace ControlDeColegio.ModelView
{
    public class MainViewModel : INotifyPropertyChanged, ICommand
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;

        public MainViewModel Instancia {get; set;}
        public string Fondo {get; set;} = $"{Environment.CurrentDirectory}\\Images\\fondo.gif";
        
        public MainViewModel()
        {
            this.Instancia = this;    
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parametro)
        {
            if(parametro.Equals("Usuarios"))
            {
                UsuariosView ventanaUsuarios = new UsuariosView();
                ventanaUsuarios.ShowDialog();
            }
            else if(parametro.Equals("Roles"))
            {
                RolView ventanaRoles = new RolView();
                ventanaRoles.ShowDialog();
            }
        }
    }
}