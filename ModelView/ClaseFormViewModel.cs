using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using ControlDeColegio.Models;

namespace ControlDeColegio.ModelView
{
    public class ClaseFormViewModel : INotifyPropertyChanged, ICommand
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        public ClaseFormViewModel Instancia {get; set;}
        public ClaseViewModel ClaseViewModel {get; set;}
        public Clase ClaseForm {get; set;}
        public int Ciclo {get; set;}
        public int CupoMaximo {get; set;}
        public int CupoMinimo {get; set;}
        public string Descripcion {get; set;}
        public string CarreraId {get; set;}
        public string HorarioId {get; set;}
        public string InstructorId {get; set;}
        public string SalonId {get; set;}


        public ClaseFormViewModel(ClaseViewModel ClaseViewModel)
        {
            this.Instancia = this;
            this.ClaseViewModel = ClaseViewModel;
            if(this.ClaseViewModel.Seleccionado != null)
            {
                this.ClaseForm = new Clase();
                this.Ciclo = ClaseViewModel.Seleccionado.Ciclo;
                this.CupoMaximo = ClaseViewModel.Seleccionado.CupoMaximo;
                this.CupoMinimo = ClaseViewModel.Seleccionado.CupoMinimo;   
                this.Descripcion = ClaseViewModel.Seleccionado.Descripcion;
                this.CarreraId = ClaseViewModel.Seleccionado.CarreraId;
                this.HorarioId = ClaseViewModel.Seleccionado.HorarioId;
                this.InstructorId = ClaseViewModel.Seleccionado.InstructorId;
                this.SalonId = ClaseViewModel.Seleccionado.SalonId;                
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
                if(this.ClaseViewModel.Seleccionado == null)
                {
                    Clase nuevo = new Clase("4", Ciclo, CupoMaximo, CupoMinimo, Descripcion, CarreraId, HorarioId, InstructorId, SalonId);
                    this.ClaseViewModel.agregarElemento(nuevo);
                }
                else
                {
                    ClaseForm.Ciclo = this.Ciclo;
                    ClaseForm.CupoMaximo = this.CupoMaximo;
                    ClaseForm.CupoMinimo = this.CupoMinimo;
                    ClaseForm.Descripcion = this.Descripcion;
                    ClaseForm.CarreraId = this.CarreraId;
                    ClaseForm.HorarioId = this.HorarioId;
                    ClaseForm.InstructorId = this.InstructorId;
                    ClaseForm.SalonId = this.SalonId;;
                    int posicion = ClaseViewModel.Clase.IndexOf(this.ClaseViewModel.Seleccionado);
                    this.ClaseViewModel.Clase.RemoveAt(posicion);
                    this.ClaseViewModel.Clase.Insert(posicion, ClaseForm);
                }
                ((Window)parameter).Close();
            }
        }
    }
}