using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ControlDeColegio.Models;
using MahApps.Metro.Controls.Dialogs;

namespace ControlDeColegio.ModelView
{
    public class UsuarioViewModel : INotifyPropertyChanged, ICommand
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
     
        private IDialogCoordinator dialogCoordinator;

        public UsuarioViewModel Instancia {get; set;}
        public UsuariosViewModel UsuariosViewModel {get; set;}

        public string Apellidos {get; set;}
        public string Nombres {get; set;}
        public string Email {get; set;}
        public string Username {get; set;}
        public string Password {get; set;}
        public string HeightlblPassword {get; set;} = "Auto";
        public string HeightTxtPassword {get; set;} = "Auto";
        
        public Usuarios Usuario {get; set;}

        public UsuarioViewModel(UsuariosViewModel UsuariosViewModel, IDialogCoordinator instance)
        {
            this.Instancia = this;
            this.dialogCoordinator = instance;
            this.UsuariosViewModel = UsuariosViewModel;
            if(this.UsuariosViewModel.Seleccionado != null)
            {
                this.Usuario = new Usuarios();
                this.Usuario.Enable = UsuariosViewModel.Seleccionado.Enable;
                this.Usuario.Id = UsuariosViewModel.Seleccionado.Id;
                this.Apellidos = this.UsuariosViewModel.Seleccionado.Apellidos;
                this.Nombres = this.UsuariosViewModel.Seleccionado.Nombres;
                this.Email = this.UsuariosViewModel.Seleccionado.Email;
                this.Username = this.UsuariosViewModel.Seleccionado.Username;
                this.HeightlblPassword = "0";
                this.HeightTxtPassword = "0";
            }
        }
        public bool CanExecute(object parametros)
        {
            return true;
        }

        public async void Execute(object parametro)
        {
            if(parametro is Window)
            {
                if(this.UsuariosViewModel.Seleccionado == null)
                {
                    Usuarios nuevo = new Usuarios(100, Username, true, Nombres, Apellidos, Email);
                    nuevo.Password = ((PasswordBox)((Window)parametro).FindName("TxtPassword")).Password;
                    this.UsuariosViewModel.agregarElemento(nuevo);        
                    await dialogCoordinator.ShowMessageAsync(this, "Usuario agregado", "Usuario guardado exitosamente", MessageDialogStyle.Affirmative);
                }
                else
                {
                    Usuario.Apellidos = this.Apellidos;
                    Usuario.Nombres = this.Nombres;
                    Usuario.Email = this.Email;
                    Usuario.Username = this.Username;
                    int posicion = UsuariosViewModel.Usuarios.IndexOf(this.UsuariosViewModel.Seleccionado);
                    this.UsuariosViewModel.Usuarios.RemoveAt(posicion);
                    this.UsuariosViewModel.Usuarios.Insert(posicion, Usuario);
                    await dialogCoordinator.ShowMessageAsync(this, "Usuario Actualizado", "Usuario actualizado exitosamente", MessageDialogStyle.Affirmative);
                }
                ((Window)parametro).Close();
            }
        }
    }
}