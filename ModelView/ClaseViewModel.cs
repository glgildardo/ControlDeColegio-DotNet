using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using ControlDeColegio.Models;
using ControlDeColegio.Views;
using MahApps.Metro.Controls.Dialogs;

namespace ControlDeColegio.ModelView
{
    public class ClaseViewModel : INotifyPropertyChanged, ICommand

    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        public IDialogCoordinator dialogCoordinator;
        public ObservableCollection<Clase> Clase {get; set;}
        public Clase Seleccionado {get; set;}
        public ClaseViewModel Instancia {get; set;}
        
        public ClaseViewModel(IDialogCoordinator instance)
        {
            this.Instancia = this;
            this.dialogCoordinator = instance;
            this.Clase = new ObservableCollection<Clase>();
            this.Clase.Add(new Clase("1", 1, 10, 5, "Clase de primaria", "1", "Matutino", "1", "1"));
            this.Clase.Add(new Clase("2", 2, 15, 8, "Clase de segundaria", "2", "Vespertino", "2", "2"));
            this.Clase.Add(new Clase("3", 3, 10, 6, "Clase de Kinder", "3", "Matutino", "3", "3"));
        }

        public void agregarElemento(Clase nuevo)
        {
            this.Clase.Add(nuevo);
        }

        public void NotificarCambio(string property)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
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
                ClaseFormView nuevoClase = new ClaseFormView(Instancia);
                nuevoClase.Show();
            }
            else if(parameter.Equals("Eliminar")) 
            {
                if(this.Seleccionado == null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this,
                        "Clase", "Debe seleccionar la Clase que desea eliminar",
                        MessageDialogStyle.Affirmative);
                }
                else
                {
                    MessageDialogResult respuesta = await this.dialogCoordinator.ShowMessageAsync(this,
                        "Eliminar la Clase", "¿Esta seguro de eliminar la Clase?",
                        MessageDialogStyle.AffirmativeAndNegative);
                    if(respuesta == MessageDialogResult.Affirmative)
                    {
                        this.Clase.Remove(Seleccionado);
                    }
                }
            }
            else if (parameter.Equals("Modificar"))
            {
                if(this.Seleccionado == null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this,
                    "Seleccionar la  Clase", "Debe seleccionar la Clase");
                }
                else
                {
                    ClaseFormView modificarClase = new ClaseFormView(Instancia);
                    modificarClase.ShowDialog();
                }
            }
        }
    }
}