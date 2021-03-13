using System;
using System.ComponentModel;
using System.Windows.Input;

namespace ControlDeColegio.ModelView
{
    public class UsuarioViewModel : INotifyPropertyChanged, ICommand
    {
        public UsuarioViewModel Instancia {get; set;}
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;

        public UsuarioViewModel()
        {
            this.Instancia = this;
        }
        public bool CanExecute(object parametros)
        {
            return true;
        }

        public void Execute(object parametros)
        {
            throw new NotImplementedException();
        }
    }
}