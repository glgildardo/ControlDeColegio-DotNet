using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using ControlDeColegio.Models;
using ControlDeColegio.Views;
using MahApps.Metro.Controls.Dialogs;

namespace ControlDeColegio.ModelView
{
    public class HorarioViewModel : INotifyPropertyChanged, ICommand
    {
        public ObservableCollection<Horario> Horario {get; set;}
        private IDialogCoordinator dialogCoordinator;

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        public HorarioViewModel Instancia {get; set;}
        public Horario Seleccionado {get; set;}

        public HorarioViewModel(IDialogCoordinator instance)
        {
            this.Instancia = this;
            this.dialogCoordinator = instance;
            this.Horario = new ObservableCollection<Horario>();
            this.Horario.Add(new Horario("1", new DateTime(2020, 5, 1, 8, 30, 52), new DateTime(2019, 5, 1, 8, 30, 52)));
            this.Horario.Add(new Horario("2", new DateTime(2020, 5, 1, 8, 30, 52), new DateTime(2019, 5, 1, 8, 30, 52)));
            this.Horario.Add(new Horario("3", new DateTime(2020, 5, 1, 8, 30, 52), new DateTime(2019, 5, 1, 8, 30, 52)));
        }

        public void agregarElemento(Horario nuevo){
            this.Horario.Add(nuevo);
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
                HorarioFormView nuevoHorario = new HorarioFormView(Instancia);
                nuevoHorario.Show();
            }
            else if(parameter.Equals("Eliminar"))
            {
                if(this.Seleccionado == null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this, "Horario", "Debe seleccionar un elemento", MessageDialogStyle.Affirmative);
                }
                else
                {
                    MessageDialogResult respuesta = await this.dialogCoordinator.ShowMessageAsync(this,
                        "Eliminar Horario", "¿Esta seguro de eliminar este Horario?",
                        MessageDialogStyle.AffirmativeAndNegative);
                    if(respuesta == MessageDialogResult.Affirmative)
                    {
                        this.Horario.Remove(Seleccionado);
                    }
                }
            }
            else if(parameter.Equals("Modificar"))
            {
                if(this.Seleccionado == null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this, 
                    "Horario", "Debe seleccionar un elemento");
                }
                else
                {
                    HorarioFormView modificarHorario = new HorarioFormView(Instancia);
                    modificarHorario.ShowDialog();
                }
            }
            
        }

        
    }
}