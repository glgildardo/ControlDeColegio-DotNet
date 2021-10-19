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
    public class HorarioViewModel : INotifyPropertyChanged, ICommand
    {
        private IDialogCoordinator dialogCoordinator;
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        public KalumDBContext dbContext = new KalumDBContext();
        public HorarioViewModel Instancia {get; set;}
        public ObservableCollection<Horario> _Horario {get; set;}        
        public ObservableCollection<Horario> Horario
        {
            get
            {
                if(this._Horario == null)
                {
                    this._Horario = new ObservableCollection<Horario>(dbContext.Horarios.ToList());
                }
                return this._Horario;
            } 
            set
            {
                this.Horario = value;
            }
        }

        public Horario _Seleccionado {get; set;}
        public Horario Seleccionado 
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

        public HorarioViewModel(IDialogCoordinator instance)
        {
            this.Instancia = this;
            this.dialogCoordinator = instance;
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
                    await this.dialogCoordinator.ShowMessageAsync(this,
                        "Horario", "Debe seleccionar un elemento",
                        MessageDialogStyle.Affirmative);
                }
                else
                {
                    MessageDialogResult respuesta = await this.dialogCoordinator.ShowMessageAsync(this,
                        "Eliminar Horario", "¿Esta seguro de eliminar este Horario?",
                        MessageDialogStyle.AffirmativeAndNegative);
                    if(respuesta == MessageDialogResult.Affirmative)
                    {
                        this.dbContext.Remove(this.Seleccionado);
                        this.dbContext.SaveChanges();
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