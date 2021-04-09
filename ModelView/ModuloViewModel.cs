using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using ControlDeColegio.Models;
using ControlDeColegio.Views;
using MahApps.Metro.Controls.Dialogs;


namespace ControlDeColegio.ModelView
{
    public class ModuloViewModel : INotifyPropertyChanged, ICommand
    {
        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;
        public IDialogCoordinator dialogCoordinator;
        public ObservableCollection<Modulo> Modulo {get; set;}
        public ModuloViewModel Instancia {get; set;}
        public Modulo Seleccionado {get; set;}

        public ModuloViewModel(IDialogCoordinator instance)
        {
            this.Instancia = this;
            this.dialogCoordinator = instance;
            this.Modulo = new ObservableCollection<Modulo>();
            this.Modulo.Add(new Modulo("1", "1", "De usuarios", 1));
            this.Modulo.Add(new Modulo("2", "2", "De maestros", 2));
            this.Modulo.Add(new Modulo("3", "3", "De papas", 3));
        }

        public void NotificarCambio(string property)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public void agregarElemento(Modulo nuevo)
        {
            this.Modulo.Add(nuevo);
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            if(parameter.Equals("Nuevo"))
            {
                this.Seleccionado = null;
                ModuloFormView nuevoModulo = new ModuloFormView(Instancia);
                nuevoModulo.Show();
            }
            else if(parameter.Equals("Eliminar"))
            {
                if(this.Seleccionado == null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this, 
                        "Modulo", "Debe seleccionar una Modulo",
                         MessageDialogStyle.Affirmative);
                }
                else 
                {
                    MessageDialogResult respuesta = await this.dialogCoordinator.ShowMessageAsync(this,
                        "Eliminar Modulo", "¿Esta seguro de eliminar esta Modulo?",
                        MessageDialogStyle.AffirmativeAndNegative);
                    if(respuesta == MessageDialogResult.Affirmative)
                    {
                        this.Modulo.Remove(Seleccionado);
                    }
                }
            }
            else if(parameter.Equals("Modificar"))
            {
                if(this.Seleccionado == null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this,
                    "Seleccionar Modulo", "Debe seleccionar un Modulo");
                }
                else
                {
                    ModuloFormView modificarModulo = new ModuloFormView(Instancia);
                    modificarModulo.ShowDialog();
                }
            }
        }
    }
}