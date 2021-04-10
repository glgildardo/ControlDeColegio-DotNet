using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
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
        public ObservableCollection<Alumno> Alumno {get; set;}
        public AlumnoViewModel Instancia {get; set;}
        public Alumno Seleccionado {get; set;}

        public AlumnoViewModel(IDialogCoordinator instance)
        {
            this.Instancia = this;
            this.dialogCoordinator = instance;
            this.Alumno = new ObservableCollection<Alumno>();
            this.Alumno.Add(new Alumno("2021001", "1", "de la Cruz Cierra", "Penelope Luisa", "Luisa@gmail.com"));
            this.Alumno.Add(new Alumno("2021002", "2", "Paz Tol", "Luis Pedro", "Luis@gmail.com"));
            this.Alumno.Add(new Alumno("2021003", "3", "Noroeste Lopez", "Jose Ernesto", "Jose@gmail.com"));
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