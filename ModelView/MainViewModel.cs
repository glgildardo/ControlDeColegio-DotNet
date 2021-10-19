using System;
using System.ComponentModel;
using System.Windows.Input;
using ControlDeColegio.Views;

namespace ControlDeColegio.ModelView
{
    public class MainViewModel : INotifyPropertyChanged, ICommand
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;

        public MainViewModel Instancia {get; set;}
        public string Fondo {get; set;} = $"{Environment.CurrentDirectory}\\Images\\fondo.gif";
        
        public MainViewModel()
        {
            this.Instancia = this;    
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parametro)
        {
            if(parametro.Equals("Usuarios"))
            {
                UsuariosView ventanaUsuarios = new UsuariosView();
                ventanaUsuarios.ShowDialog();
            }
            else if(parametro.Equals("Roles"))
            {
                RolView ventanaRoles = new RolView();
                ventanaRoles.ShowDialog();
            }
            else if(parametro.Equals("Alumno"))
            {
                AlumnoView ventanaAlumno = new AlumnoView();
                ventanaAlumno.ShowDialog();
            }
            else if(parametro.Equals("Horario"))
            {
                HorarioView ventanaHorario = new HorarioView();
                ventanaHorario.ShowDialog();
            }
            else if(parametro.Equals("Salon"))
            {
                SalonView ventanaSalon = new SalonView();
                ventanaSalon.ShowDialog();
            }
            
            else if(parametro.Equals("Carrera"))
            {
                CarreraView ventanaCarrera = new CarreraView();
                ventanaCarrera.ShowDialog();
            }
            else if(parametro.Equals("AsignacionAlumno"))
            {
                AsignacionAlumnoView ventanaAsignacionAlumno = new AsignacionAlumnoView();
                ventanaAsignacionAlumno.ShowDialog();
            }
            else if(parametro.Equals("Clase"))
            {
                ClaseView ventanaClase = new ClaseView();
                ventanaClase.ShowDialog();
            }
        }
    }
}