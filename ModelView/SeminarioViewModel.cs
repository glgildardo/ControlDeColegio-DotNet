using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using ControlDeColegio.Models;
using ControlDeColegio.Views;
using MahApps.Metro.Controls.Dialogs;

namespace ControlDeColegio.ModelView
{
    public class SeminarioViewModel : INotifyPropertyChanged, ICommand
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        public ObservableCollection<Seminario> Seminario {get; set;}
        public IDialogCoordinator dialogCoordinator;
        public SeminarioViewModel Instancia {get; set;}
        public Seminario Seleccionado {get; set;}

        public SeminarioViewModel(IDialogCoordinator instance)
        {
            this.Instancia = this;
            this.dialogCoordinator = instance;
            this.Seminario = new ObservableCollection<Seminario>();
            this.Seminario.Add(new Seminario("1", "1", "1", new DateTime(2020, 10, 10), new DateTime(2021, 11, 11)));
            this.Seminario.Add(new Seminario("2", "2", "2", new DateTime(2020, 10, 09), new DateTime(2021, 10, 09)));
            this.Seminario.Add(new Seminario("3", "3", "3", new DateTime(2020, 10, 08), new DateTime(2021, 09, 08)));
        }

        public void agregarElemento(Seminario nuevo)
        {
            this.Seminario.Add(nuevo);
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
                SeminarioFormView nuevoSeminario = new SeminarioFormView(Instancia);
                nuevoSeminario.Show();
            }
            else if(parameter.Equals("Eliminar"))
            {
                if(this.Seleccionado == null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this, 
                        "Seminario", "Debe seleccionar una Seminario",
                         MessageDialogStyle.Affirmative);
                }
                else 
                {
                    MessageDialogResult respuesta = await this.dialogCoordinator.ShowMessageAsync(this,
                        "Eliminar Seminario", "¿Esta seguro de eliminar esta Seminario?",
                        MessageDialogStyle.AffirmativeAndNegative);
                    if(respuesta == MessageDialogResult.Affirmative)
                    {
                        this.Seminario.Remove(Seleccionado);
                    }
                }
            }
            else if(parameter.Equals("Modificar"))
            {
                if(this.Seleccionado == null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this,
                    "Seleccionar Seminario", "Debe seleccionar un Seminario");
                }
                else
                {
                    SeminarioFormView modificarSeminario = new SeminarioFormView(Instancia);
                    modificarSeminario.ShowDialog();
                }
            }
        }
    }
}