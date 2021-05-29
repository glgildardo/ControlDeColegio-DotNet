using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using ControlDeColegio.Models;
using ControlDeColegio.Views;
using MahApps.Metro.Controls.Dialogs;

namespace ControlDeColegio.ModelView
{
    public class DetalleNotaViewModel : INotifyPropertyChanged, ICommand

    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        public IDialogCoordinator dialogCoordinator;
        public DetalleNota Seleccionado {get; set;}
        public DetalleNotaViewModel Instancia {get; set;}
        public ObservableCollection<DetalleNota> _DetalleNota {get; set;}
        public ObservableCollection<DetalleNota> DetalleNota {get; set;}
        
        public DetalleNotaViewModel(IDialogCoordinator instance)
        {
            this.Instancia = this;
            this.dialogCoordinator = instance;
        }

        public void agregarElemento(DetalleNota nuevo)
        {
            this.DetalleNota.Add(nuevo);
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
                DetalleNotaFormView nuevoDetalleNota = new DetalleNotaFormView(Instancia);
                nuevoDetalleNota.Show();
            }
            else if(parameter.Equals("Eliminar")) 
            {
                if(this.Seleccionado == null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this,
                        "Detalle de Nota", "Debe seleccionar el Detalle de la Nota que desea eliminar",
                        MessageDialogStyle.Affirmative);
                }
                else
                {
                    MessageDialogResult respuesta = await this.dialogCoordinator.ShowMessageAsync(this,
                        "Eliminar el Detalle de la Nota", "¿Esta seguro de eliminar el Detalle de la Nota?",
                        MessageDialogStyle.AffirmativeAndNegative);
                    if(respuesta == MessageDialogResult.Affirmative)
                    {
                        this.DetalleNota.Remove(Seleccionado);
                    }
                }
            }
            else if (parameter.Equals("Modificar"))
            {
                if(this.Seleccionado == null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this,
                    "Seleccionar el Detalle de la Nota", "Debe seleccionar el detalle de la nota");
                }
                else
                {
                    DetalleNotaFormView modificarDetalleNota = new DetalleNotaFormView(Instancia);
                    modificarDetalleNota.ShowDialog();
                }
            }
        }
    }
}