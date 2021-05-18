using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using ControlDeColegio.DataContext;
using ControlDeColegio.Models;
using ControlDeColegio.Views;
using MahApps.Metro.Controls.Dialogs;


namespace ControlDeColegio.ModelView
{
    public class AlumnoViewModel : INotifyPropertyChanged, ICommand
    {
        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;
        public IDialogCoordinator dialogCoordinator;

        public KalumDBContext dBContext = new KalumDBContext();

        public ObservableCollection<Alumno> _Alumno;
        public ObservableCollection<Alumno> Alumno 
        {
            get
            {
                if(this._Alumno == null)
                {
                    this._Alumno = new ObservableCollection<Alumno>(dBContext.Alumnos.ToList());
                }
                return this._Alumno;
            } 
            set
            {
                this.Alumno = value;
            }
        }
        public AlumnoViewModel Instancia {get; set;}
        public Alumno Seleccionado {get; set;}

        public AlumnoViewModel(IDialogCoordinator instance)
        {
            this.Instancia = this;
            this.dialogCoordinator = instance;
        }

        public void NotificarCambio(string property)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public void agregarElemento(Alumno nuevo)
        {
            this.Alumno.Add(nuevo);
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
                AlumnoFormView nuevoAlumno = new AlumnoFormView(Instancia);
                nuevoAlumno.Show();
            }
            else if(parameter.Equals("Eliminar"))
            {
                if(this.Seleccionado == null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this, 
                        "Alumno", "Debe seleccionar un Alumno",
                         MessageDialogStyle.Affirmative);
                }
                else 
                {
                    MessageDialogResult respuesta = await this.dialogCoordinator.ShowMessageAsync(this,
                        "Eliminar Alumno", "¿Esta seguro de eliminar este Alumno?",
                        MessageDialogStyle.AffirmativeAndNegative);
                    if(respuesta == MessageDialogResult.Affirmative)
                    {
                        this.dBContext.Remove(this.Seleccionado);
                        this.dBContext.SaveChanges();
                        this.Alumno.Remove(Seleccionado);
                    }
                }
            }
            else if(parameter.Equals("Modificar"))
            {
                if(this.Seleccionado == null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this,
                    "Seleccionar Alumno", "Debe seleccionar un Alumno");
                }
                else
                {
                    AlumnoFormView modificarAlumno = new AlumnoFormView(Instancia);
                    modificarAlumno.ShowDialog();
                }
            }
        }
    }
}