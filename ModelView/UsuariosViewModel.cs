using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using ControlDeColegio.Models;
using ControlDeColegio.Views;

namespace ControlDeColegio.ModelView
{
    public class UsuariosViewModel : INotifyPropertyChanged, ICommand
    {
        public ObservableCollection<Usuarios> Usuarios {get;set;}

        public UsuariosViewModel Instancia {get;set;}

        public Usuarios Seleccionado {get;set;}
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;

        public UsuariosViewModel()
        {
            this.Instancia = this;
            this.Usuarios = new ObservableCollection<Usuarios>();
            this.Usuarios.Add(new Usuarios(1,"etumax",true,"Edwin Rolando","Tumax Chaclan", "etumax@gmail.com"));
            this.Usuarios.Add(new Usuarios(2,"nperez",true,"Nancy Elizabeth","Perez Carcamo", "eperez@gmail.com"));
            this.Usuarios.Add(new Usuarios(3,"caquino",true,"Cesar Agusto","Aquino Gaitan", "caquino@gmail.com"));
        }

        public void NotificarCambio(string propiedad)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propiedad));
            }
        }

        public void agregarElemento(Usuarios nuevo){
            this.Usuarios.Add(nuevo);
        }

        public void actualizarElemento(Usuarios actualizar)
        {
        }

        public bool CanExecute(object parametro)
        {
            return true;
        }

        public void Execute(object parametro)
        {
            if(parametro.Equals("Nuevo")){
                this.Seleccionado = null;
                UsuarioView nuevoUsuario = new UsuarioView(Instancia);
                nuevoUsuario.Show();
            }else if (parametro.Equals("Eliminar")){
                if(this.Seleccionado == null){
                    MessageBox.Show("Debe seleccionar un elemento");
                } else {
                    this.Usuarios.Remove(Seleccionado);
                }
            }else if (parametro.Equals("Modificar"))
            {
                if(this.Seleccionado == null){
                    MessageBox.Show("Debe seleccionar un elemento");
                }else{
                    UsuarioView modificarUsuario = new UsuarioView(Instancia);
                    modificarUsuario.ShowDialog();
                }
            }
        }
    }
}