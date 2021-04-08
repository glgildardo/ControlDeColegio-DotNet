using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using ControlDeColegio.Models;

namespace ControlDeColegio.ModelView
{
    public class SeminarioFormViewModel : INotifyPropertyChanged, ICommand
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        public SeminarioFormViewModel Instancia {get; set;}
        public SeminarioViewModel SeminarioViewModel {get; set;}
        public Seminario SeminarioForm {get; set;}
        public string ModuloId {get; set;}
        public string ModuloSeminario {get; set;}
        public DateTime FechaInicio {get; set;}
        public DateTime FechaFin {get; set;}

        public SeminarioFormViewModel(SeminarioViewModel SeminarioViewModel)
        {
            this.Instancia = this;
            this.SeminarioViewModel = SeminarioViewModel;
            if(this.SeminarioViewModel.Seleccionado != null)
            {
                this.SeminarioForm = new Seminario();
                this.ModuloId = SeminarioViewModel.Seleccionado.ModuloId;
                this.ModuloSeminario = SeminarioViewModel.Seleccionado.ModuloSeminario;
                this.FechaInicio = SeminarioViewModel.Seleccionado.FechaInicio;
                this.FechaFin = SeminarioViewModel.Seleccionado.FechaFin;
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
                if(this.SeminarioViewModel.Seleccionado == null)
                {
                    Seminario nuevo = new Seminario("4", ModuloId, ModuloSeminario, FechaInicio, FechaFin);
                    this.SeminarioViewModel.agregarElemento(nuevo);
                }
                else
                {
                    SeminarioForm.ModuloId = this.ModuloId;
                    SeminarioForm.ModuloSeminario = this.ModuloSeminario;
                    SeminarioForm.FechaInicio = this.FechaInicio;
                    SeminarioForm.FechaFin = this.FechaFin;
                    int posicion = SeminarioViewModel.Seminario.IndexOf(this.SeminarioViewModel.Seleccionado);
                    this.SeminarioViewModel.Seminario.RemoveAt(posicion);
                    this.SeminarioViewModel.Seminario.Insert(posicion, SeminarioForm);
                }
                ((Window)parameter).Close();
            }
        }
    }
}