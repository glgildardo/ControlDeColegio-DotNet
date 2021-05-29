using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using ControlDeColegio.Models;

namespace ControlDeColegio.ModelView
{
    public class HorarioFormViewModel : INotifyPropertyChanged, ICommand
    {
        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public HorarioFormViewModel Instancia {get; set;}

        public HorarioViewModel HorarioViewModel {get; set;} 

        public Horario HorarioForm {get; set;}

        public TimeSpan  HorarioInicio {get; set;}

        public TimeSpan HorarioFinal {get; set;}

        public HorarioFormViewModel(HorarioViewModel HorarioViewModel)
        {
            this.Instancia = this;
            this.HorarioViewModel = HorarioViewModel;
            if(this.HorarioViewModel.Seleccionado != null)
            {
                this.HorarioForm = new Horario();
                this.HorarioInicio = HorarioViewModel.Seleccionado.HorarioInicio;
                this.HorarioFinal = HorarioViewModel.Seleccionado.HorarioFinal;
            }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(parameter is Window)
            {
                if(this.HorarioViewModel.Seleccionado == null)
                {
                    Horario nuevo = new Horario("4", HorarioInicio, HorarioFinal);
                    this.HorarioViewModel.agregarElemento(nuevo);
                }
                else
                {
                    HorarioForm.HorarioInicio = this.HorarioInicio;
                    HorarioForm.HorarioFinal = this.HorarioFinal;
                    int posicion = HorarioViewModel.Horario.IndexOf(this.HorarioViewModel.Seleccionado);
                    this.HorarioViewModel.Horario.RemoveAt(posicion);
                    this.HorarioViewModel.Horario.Insert(posicion, HorarioForm);
                }
                ((Window)parameter).Close();
            }
        }

    }
}