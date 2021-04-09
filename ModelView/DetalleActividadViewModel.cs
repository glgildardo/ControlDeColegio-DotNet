using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using ControlDeColegio.Models;
using ControlDeColegio.Views;
using MahApps.Metro.Controls.Dialogs;

namespace ControlDeColegio.ModelView
{
    public class DetalleActividadViewModel : INotifyPropertyChanged, ICommand
    {
        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;
        public IDialogCoordinator dialogCoordinator;
        public ObservableCollection<DetalleActividad> DetalleActividad {get; set;}
        public DetalleActividadViewModel Instancia {get; set;}
        public DetalleActividad Seleccionado {get; set;}

        public DetalleActividadViewModel(IDialogCoordinator instance)
        {
            this.Instancia = this;
            this.dialogCoordinator = instance;
            this.DetalleActividad = new ObservableCollection<DetalleActividad>();
            this.DetalleActividad.Add(new DetalleActividad("1", "1", "Pintar una escuela", 10, new DateTime (2021, 01, 28), new DateTime(2021, 05, 28), new DateTime(2021, 06, 28), 'A'));
            this.DetalleActividad.Add(new DetalleActividad("2", "2", "Reparar un Jardin", 10, new DateTime (2021, 01, 28), new DateTime(2021, 05, 28), new DateTime(2021, 06, 28), 'B'));
            this.DetalleActividad.Add(new DetalleActividad("1", "1", "Capacitaciones de nutrición", 10, new DateTime (2021, 01, 28), new DateTime(2021, 05, 28), new DateTime(2021, 06, 28), 'A'));
        }
        
        public void agregarElemento(DetalleActividad nuevo)
        {
            this.DetalleActividad.Add(nuevo);
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
                DetalleActividadFormView nuevoDetalleActividad = new DetalleActividadFormView(Instancia);
                nuevoDetalleActividad.Show();
            }
            else if(parameter.Equals("Eliminar")) 
            {
                if(this.Seleccionado == null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this,
                        "Detalle de Actividad", "Debe seleccionar el Detalle de Actividad",
                        MessageDialogStyle.Affirmative);
                }
                else
                {
                    MessageDialogResult respuesta = await this.dialogCoordinator.ShowMessageAsync(this,
                        "Eliminar Detalle de la Actividad", "¿Esta seguro de eliminar el detalle de actividad?",
                        MessageDialogStyle.AffirmativeAndNegative);
                    if(respuesta == MessageDialogResult.Affirmative)
                    {
                        this.DetalleActividad.Remove(Seleccionado);
                    }
                }
            }
            else if (parameter.Equals("Modificar"))
            {
                if(this.Seleccionado == null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this,
                    "Seleccionar el detalle de actividad", "Debe seleccionar el Detalle de Actividad");
                }
                else
                {
                    DetalleActividadFormView modificarDetalleActividad = new DetalleActividadFormView(Instancia);
                    modificarDetalleActividad.ShowDialog();
                }
            }
        }
    }
}