using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using ControlDeColegio.DataContext;
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
        public KalumDBContext dBContext = new KalumDBContext();
        public DetalleActividadViewModel Instancia {get; set;}
        public DetalleActividad Seleccionado {get; set;}
        public ObservableCollection<DetalleActividad> _DetalleActividad {get; set;}
        public ObservableCollection<DetalleActividad> DetalleActividad {
            get{
                if(this._DetalleActividad == null) {
                    this._DetalleActividad = new ObservableCollection<DetalleActividad>(dBContext.DetalleActividades.ToList());
                }
                return this._DetalleActividad;
            } 
            set{
                this.DetalleActividad = value;
            }
        }

        public DetalleActividadViewModel(IDialogCoordinator instance)
        {
            this.Instancia = this;
            this.dialogCoordinator = instance;
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
                        this.dBContext.Remove(this.Seleccionado);
                        this.dBContext.SaveChanges();
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