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
    public class CarreraViewModel : INotifyPropertyChanged, ICommand
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        public IDialogCoordinator dialogCoordinator;
        public CarreraViewModel Instancia {get; set;}
        public Carrera _Seleccionado {get; set;}
        public Carrera Seleccionado 
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
        private KalumDBContext dBContext = new KalumDBContext();
        public ObservableCollection<Carrera> _Carrera {get; set;}
        public ObservableCollection<Carrera> Carrera {
            get
            {
                if(this._Carrera == null) 
                {
                    this._Carrera = new ObservableCollection<Carrera>(dBContext.Carreras.ToList());
                }
                return this._Carrera;
            } 
            set{
                this.Carrera = value;
            }
        }
        
        public CarreraViewModel(IDialogCoordinator instance)
        {
            this.Instancia = this;
            this.dialogCoordinator = instance;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void NotificarCambio(string property)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        public void agregarElemento(Carrera nuevo)
        {
            this.Carrera.Add(nuevo);
        }
        public async void Execute(object parameter)
        {
            if(parameter.Equals("Nuevo"))
            {
                this.Seleccionado = null;
                CarreraFormView nuevoCarrera = new CarreraFormView(Instancia);
                nuevoCarrera.Show();
            }
            else if(parameter.Equals("Eliminar"))
            {
                if(this.Seleccionado == null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this, 
                        "Carrera", "Debe seleccionar una Carrera",
                         MessageDialogStyle.Affirmative);
                }
                else 
                {
                    MessageDialogResult respuesta = await this.dialogCoordinator.ShowMessageAsync(this,
                        "Eliminar Carrera", "¿Esta seguro de eliminar esta Carrera?",
                        MessageDialogStyle.AffirmativeAndNegative);
                    if(respuesta == MessageDialogResult.Affirmative)
                    {
                        this.dBContext.Remove(this.Seleccionado);
                        this.dBContext.SaveChanges();
                        this.Carrera.Remove(Seleccionado);
                    }
                }
            }
            else if(parameter.Equals("Modificar"))
            {
                if(this.Seleccionado == null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this,
                    "Seleccionar Carrera", "Debe seleccionar un Carrera");
                }
                else
                {
                    CarreraFormView modificarCarrera = new CarreraFormView(Instancia);
                    modificarCarrera.ShowDialog();
                }
            }
        }
    }
}