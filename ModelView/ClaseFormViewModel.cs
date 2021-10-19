using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using ControlDeColegio.Models;
using ControlDeColegio.DataContext;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.EntityFrameworkCore;

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
        
        public Carrera CarreraTecnicaSeleccionado {get; set;}
        public Instructor InstructorSeleccionado {get; set;}
        public Salon SalonSeleccionado {get; set;}
        public Horario HorarioSeleccionado{get; set;}
        public ObservableCollection<Carrera> Carreras {get; set;}
        public ObservableCollection<Salon> Salones {get; set;}
        public ObservableCollection<Horario> Horarios {get; set;}
        public ObservableCollection<Instructor> Instructores {get; set;}
        public IDialogCoordinator dialogCoordinator;
        public string ValorDescripcion {get; set;}
        public string ValorCiclo {get; set;}
        public string ValorCupoMaximo {get; set;}
        public string ValorCupoMinimo {get; set;}
        public string Titulo {get; set;}


        //VALORES PREDETERMINADOS DE LOS COMBO BOX
        public string NombreDefinido {get; set;} = "Seleccionar";
        public string HorarioDefinido {get; set;} = "Seleccionar";
        public string SalondDefinido {get; set;} = "Seleccionar";
        public string CarreraDefinida {get; set;} = "Seleccionar";
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

        public ClaseFormViewModel(ClaseViewModel ClaseViewModel, IDialogCoordinator instance)
        {
            this.Instancia = this;
            this.dialogCoordinator = instance;
            this.ClaseViewModel = ClaseViewModel;
            this.Carreras = new ObservableCollection<Carrera>(this.dBContext.Carreras.ToList());
            this.Instructores = new ObservableCollection<Instructor>(this.dBContext.Instructores.ToList());
            this.Salones = new ObservableCollection<Salon>(this.dBContext.Salones.ToList());
            this.Horarios = new ObservableCollection<Horario>(this.dBContext.Horarios.ToList());
        
            if(this.ClaseViewModel.Seleccionado != null)
            {
                Titulo = "Modificar Registro";
                this.ClaseForm = new Clase();
                this.ValorDescripcion = this.ClaseViewModel.Seleccionado.Descripcion;
                this.ValorCiclo = this.ClaseViewModel.Seleccionado.Ciclo.ToString();
                this.ValorCupoMinimo = this.ClaseViewModel.Seleccionado.CupoMinimo.ToString();
                this.ValorCupoMaximo = this.ClaseViewModel.Seleccionado.CupoMaximo.ToString();
                this.CarreraTecnicaSeleccionado = this.ClaseViewModel.Seleccionado.Carrera;
                this.InstructorSeleccionado = this.ClaseViewModel.Seleccionado.Instructor;
                this.HorarioSeleccionado = this.ClaseViewModel.Seleccionado.Horario;
                this.SalonSeleccionado = this.ClaseViewModel.Seleccionado.Salon;
                this.NombreDefinido = this.ClaseViewModel.Seleccionado.Instructor.Apellidos + " " + this.ClaseViewModel.Seleccionado.Instructor.Nombres;
                this.HorarioDefinido = $"{this.ClaseViewModel.Seleccionado.Horario.HorarioInicio.ToString(@"hh\:mm")} - {this.ClaseViewModel.Seleccionado.Horario.HorarioFinal:hh\\:mm}";
                this.SalondDefinido = this.ClaseViewModel.Seleccionado.Salon.NombreSalon;
                this.CarreraDefinida = this.ClaseViewModel.Seleccionado.Carrera.Nombre;
            }
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            if(parameter is Window)
            {                
                if(this.ClaseViewModel.Seleccionado == null)
                {
                    // Clase nuevo = new Clase("4", Ciclo, CupoMaximo, CupoMinimo, Descripcion, CarreraId, HorarioId, InstructorId, SalonId);
                    // this.ClaseViewModel.agregarElemento(nuevo);
                    Clase nuevaClase = new Clase();
                    nuevaClase.ClaseId = Guid.NewGuid().ToString();
                    nuevaClase.Ciclo = Convert.ToInt16(ValorCiclo);
                    nuevaClase.CupoMaximo = Convert.ToInt16(ValorCupoMaximo);
                    nuevaClase.CupoMinimo = Convert.ToInt16(ValorCupoMinimo);
                    nuevaClase.Descripcion = ValorDescripcion;
                    nuevaClase.Carrera = CarreraTecnicaSeleccionado;
                    nuevaClase.Horario =  HorarioSeleccionado;
                    nuevaClase.Instructor = InstructorSeleccionado;
                    nuevaClase.Salon = SalonSeleccionado;
                    dBContext.Clases.Add(nuevaClase);
                    dBContext.SaveChanges();
                    this.ClaseViewModel.Clase.Add(nuevaClase);
                    await this.dialogCoordinator.ShowMessageAsync(this, "Clase", "Registro Almacenado");
                }
                else
                {
                    int posicion = this.ClaseViewModel.Clase.IndexOf(this.ClaseViewModel.Seleccionado);
                    Clase temporal = new Clase();
                    temporal.ClaseId = this.ClaseViewModel.Seleccionado.ClaseId;
                    temporal.Descripcion = this.ValorDescripcion;
                    temporal.Ciclo = Convert.ToInt16(this.ValorCiclo);
                    temporal.CupoMaximo = Convert.ToInt16(this.ValorCupoMaximo);
                    temporal.CupoMinimo = Convert.ToInt16(this.ValorCupoMinimo);
                    temporal.CarreraId = this.CarreraTecnicaSeleccionado.CarreraId;
                    temporal.InstructorId = this.InstructorSeleccionado.InstructorId;
                    temporal.SalonId = this.SalonSeleccionado.SalonId;
                    temporal.HorarioId = this.HorarioSeleccionado.HorarioId;
                    this.dBContext.Entry(temporal).State = EntityState.Modified;
                    this.dBContext.SaveChanges();
                    this.ClaseViewModel.Clase.RemoveAt(posicion);
                    this.ClaseViewModel.Clase.Insert(posicion, temporal);
                    await this.dialogCoordinator.ShowMessageAsync(this,
                         "Clases", "Registro actualizado");
                }
                ((Window)parameter).Close();
            }
        }
    }
}