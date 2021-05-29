using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using ControlDeColegio.Models;
using ControlDeColegio.DataContext;

namespace ControlDeColegio.ModelView
{
    public class ClaseFormViewModel : INotifyPropertyChanged, ICommand
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        public ClaseFormViewModel Instancia {get; set;}
        public ClaseViewModel ClaseViewModel {get; set;}
        public Clase ClaseForm {get; set;}
        public string Descripcion {get; set;}
        
        public ObservableCollection<Carrera> Carreras {get; set;}
        public ObservableCollection<Salon> Salones {get; set;}
        public ObservableCollection<Horario> Horarios {get; set;}
        public ObservableCollection<Instructor> Instructores {get; set;}
        public KalumDBContext dBContext = new KalumDBContext();

        public List<int> _CupoMinimo {get; set;}
        public List<int> CupoMinimo {
            get{
                if(this._CupoMinimo == null)
                {
                    this._CupoMinimo = new List<int>();
                    int i = 0;
                    do
                    {
                        i++;
                        this._CupoMinimo.Add(i);
                    }while(i < 30);
                }
                return this._CupoMinimo;
            } 
            set{
                this.CupoMinimo = value;
            }
        }
        public List<int> _CupoMaximo {get; set;}
        public List<int> CupoMaximo {
            get{
                if(this._CupoMaximo == null)
                {
                    this._CupoMaximo = new List<int>();
                    int i = 0;
                    while(i < 30)
                    {
                        i++;
                        this._CupoMaximo.Add(i);
                    }
                }
                return this._CupoMaximo;
            } 
            set{
                this.CupoMaximo = value;
            }
        }
        public List<int> _Ciclos {get; set;}
        public List<int> Ciclos  {
            get{
                if(this._Ciclos == null)
                {
                    this._Ciclos = new List<int>();
                    DateTime localDate = DateTime.Now;
                    for(int i = localDate.Year; i<= localDate.Year+10; i++){
                        this._Ciclos.Add(i);
                    }
                }
                return this._Ciclos;
            } 
            set{
                this.Ciclos = value;
            }
        }

        public ClaseFormViewModel(ClaseViewModel ClaseViewModel)
        {
            this.Instancia = this;
            this.ClaseViewModel = ClaseViewModel;
            this.Carreras = new ObservableCollection<Carrera>(this.dBContext.Carreras.ToList());
            this.Instructores = new ObservableCollection<Instructor>(this.dBContext.Instructores.ToList());
            this.Salones = new ObservableCollection<Salon>(this.dBContext.Salones.ToList());
            this.Horarios = new ObservableCollection<Horario>(this.dBContext.Horarios.ToList());
        
            if(this.ClaseViewModel.Seleccionado != null)
            {
                this.ClaseForm = new Clase();
                // this.Ciclo = ClaseViewModel.Seleccionado.Ciclo;
                // this.CupoMaximo = ClaseViewModel.Seleccionado.CupoMaximo;
                // this.CupoMinimo = ClaseViewModel.Seleccionado.CupoMinimo;   
                // this.Descripcion = ClaseViewModel.Seleccionado.Descripcion;
                // this.CarreraId = ClaseViewModel.Seleccionado.CarreraId;
                // this.HorarioId = ClaseViewModel.Seleccionado.HorarioId;
                // this.InstructorId = ClaseViewModel.Seleccionado.InstructorId;
                // this.SalonId = ClaseViewModel.Seleccionado.SalonId;                
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
                    // Clase nuevo = new Clase("4", Ciclo, CupoMaximo, CupoMinimo, Descripcion, CarreraId, HorarioId, InstructorId, SalonId);
                    // this.ClaseViewModel.agregarElemento(nuevo);
                }
                else
                {
                    // ClaseForm.Ciclo = this.Ciclo;
                    // ClaseForm.CupoMaximo = this.CupoMaximo;
                    // ClaseForm.CupoMinimo = this.CupoMinimo;
                    // ClaseForm.Descripcion = this.Descripcion;
                    // ClaseForm.CarreraId = this.CarreraId;
                    // ClaseForm.HorarioId = this.HorarioId;
                    // ClaseForm.InstructorId = this.InstructorId;
                    // ClaseForm.SalonId = this.SalonId;;
                    int posicion = ClaseViewModel.Clase.IndexOf(this.ClaseViewModel.Seleccionado);
                    this.ClaseViewModel.Clase.RemoveAt(posicion);
                    this.ClaseViewModel.Clase.Insert(posicion, ClaseForm);
                }
                ((Window)parameter).Close();
            }
        }
    }
}