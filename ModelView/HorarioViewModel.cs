using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using ControlDeColegio.Models;

namespace ControlDeColegio.ModelView
{
    public class HorarioViewModel : INotifyPropertyChanged, ICommand
    {
        public ObservableCollection<Horario> Horario {get; set;}

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        public HorarioViewModel Instancia {get; set;}
        public Horario Seleccionado {get; set;}

        public HorarioViewModel()
        {
            this.Horario = new ObservableCollection<Horario>();
            this.Horario.Add(new Horario("1", new DateTime(2020, 5, 1, 8, 30, 52), new DateTime(2019, 5, 1, 8, 30, 52)));
            this.Horario.Add(new Horario("2", new DateTime(2020, 5, 1, 8, 30, 52), new DateTime(2019, 5, 1, 8, 30, 52)));
            this.Horario.Add(new Horario("3", new DateTime(2020, 5, 1, 8, 30, 52), new DateTime(2019, 5, 1, 8, 30, 52)));
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

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }

        
    }
}