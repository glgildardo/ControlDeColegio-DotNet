using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using ControlDeColegio.DataContext;
using ControlDeColegio.Models;
using ControlDeColegio.Views;
using MahApps.Metro.Controls.Dialogs;

namespace ControlDeColegio.ModelView
{
    public class SalonViewModel : INotifyPropertyChanged, ICommand
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        private IDialogCoordinator dialogCoordinator;
        public KalumDBContext dBContext = new KalumDBContext();
        public ObservableCollection<Salon> _Salon {get; set;}        
        public ObservableCollection<Salon> Salon 
        {
            get
            {
                if(this._Salon == null)
                {
                    this._Salon = new ObservableCollection<Salon>(dBContext.Salones.ToList());
                }
                return this._Salon;    
            } 
            set
            {
                this.Salon = value;
            }
        }        
        public SalonViewModel Instancia {get; set;}
        public Salon _Seleccionado {get; set;}
        public Salon Seleccionado 
        {
            get
            {
                return _Seleccionado;
            } 
            set
            {
                this._Seleccionado = value;
                NotificarCambio("Seleccionado");
            }
        }

        public SalonViewModel(IDialogCoordinator instance)
        {
            this.Instancia = this;
            this.dialogCoordinator = instance;
        }

        public void NotificarCambio(string property)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        public void agregarElemento(Salon nuevo)
        {
            this.Salon.Add(nuevo);
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
                SalonFormView nuevoSalon = new SalonFormView(Instancia);
                nuevoSalon.Show();
            }
            else if(parameter.Equals("Eliminar"))
            {
                if(this.Seleccionado == null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this,
                        "Salon", "Debe seleccionar un Salon",
                         MessageDialogStyle.Affirmative);
                }
                else 
                {
                    MessageDialogResult respuesta = await this.dialogCoordinator.ShowMessageAsync(this,
                        "Eliminar Salon", "¿Esta seguro de eliminar el salon?",
                        MessageDialogStyle.AffirmativeAndNegative);
                    if(respuesta == MessageDialogResult.Affirmative)
                    {
                        this.dBContext.Remove(this.Seleccionado);
                        this.dBContext.SaveChanges();
                        this.Salon.Remove(Seleccionado);
                    }
                }
            }
            else if(parameter.Equals("Modificar"))
            {
                if(this.Seleccionado == null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this,
                    "Seleccionar Salon", "Debe seleccionar un salon");
                }
                else
                {
                    SalonFormView modificarSalon = new SalonFormView(Instancia);
                    modificarSalon.ShowDialog();
                }
            }
        }
    }
}