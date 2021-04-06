using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using ControlDeColegio.Models;
using ControlDeColegio.Views;
using MahApps.Metro.Controls.Dialogs;

namespace ControlDeColegio.ModelView
{
    public class SalonViewModel : INotifyPropertyChanged, ICommand
    {
        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;
        private IDialogCoordinator dialogCoordinator;
        public ObservableCollection<Salon> Salon {get; set;}        
        public SalonViewModel Instancia {get; set;}
        public Salon Seleccionado {get; set;}

        public SalonViewModel(IDialogCoordinator instance)
        {
            this.Instancia = this;
            this.dialogCoordinator = instance;
            this.Salon = new ObservableCollection<Salon>();
            this.Salon.Add(new Salon("1", 5, "Salon de 5to primaria", "5to A"));
            this.Salon.Add(new Salon("1", 6, "Salon de 5to primaria", "5to B"));
            this.Salon.Add(new Salon("1", 8, "Salon de 5to primaria", "5to C"));
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
                    await this.dialogCoordinator.ShowMessageAsync(this, "Salon", "Sebe seleccionar un Salon", MessageDialogStyle.Affirmative);
                }
                else 
                {
                    MessageDialogResult respuesta = await this.dialogCoordinator.ShowMessageAsync(this,
                        "Eliminar Salon", "¿Esta seguro de eliminar el salon?",
                        MessageDialogStyle.AffirmativeAndNegative);
                    if(respuesta == MessageDialogResult.Affirmative)
                    {
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