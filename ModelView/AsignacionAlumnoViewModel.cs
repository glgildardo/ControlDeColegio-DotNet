using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using ControlDeColegio.Models;
using ControlDeColegio.Views;
using MahApps.Metro.Controls.Dialogs;

namespace ControlDeColegio.ModelView
{
    public class AsignacionAlumnoViewModel : INotifyPropertyChanged, ICommand
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        public IDialogCoordinator dialogCoordinator;
        public ObservableCollection<AsignacionAlumno> AsignacionAlumno {get; set;}
        public AsignacionAlumnoViewModel Instancia {get; set;}
        public AsignacionAlumno Seleccionado {get; set;}
        
        public AsignacionAlumnoViewModel(IDialogCoordinator instance)
        {
            this.Instancia = this;
            this.dialogCoordinator = instance;
            this.AsignacionAlumno = new ObservableCollection<AsignacionAlumno>();
            this.AsignacionAlumno.Add(new AsignacionAlumno("1", "2021001", "1", new DateTime(2021, 04, 15)));
            this.AsignacionAlumno.Add(new AsignacionAlumno("2", "2021002", "2", new DateTime(2021, 04, 14)));
            this.AsignacionAlumno.Add(new AsignacionAlumno("3", "2021003", "3", new DateTime(2021, 04, 13)));
        }

        public void agregarElemento(AsignacionAlumno nuevo)
        {
            this.AsignacionAlumno.Add(nuevo);
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

        public async void Execute(object parameter)
        {
            if(parameter.Equals("Nuevo"))
            {
                this.Seleccionado = null;
                AsignacionAlumnoFormView nuevoAsignacionAlumno = new AsignacionAlumnoFormView(Instancia);
                nuevoAsignacionAlumno.Show();
            }
            else if(parameter.Equals("Eliminar")) 
            {
                if(this.Seleccionado == null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this,
                        "Asignacion de Alumno", "Debe seleccionar la Asignacion para el Alumno",
                        MessageDialogStyle.Affirmative);
                }
                else
                {
                    MessageDialogResult respuesta = await this.dialogCoordinator.ShowMessageAsync(this,
                        "Eliminar la asignación del alumno", "¿Esta seguro de eliminar la asignacion del alumno?",
                        MessageDialogStyle.AffirmativeAndNegative);
                    if(respuesta == MessageDialogResult.Affirmative)
                    {
                        this.AsignacionAlumno.Remove(Seleccionado);
                    }
                }
            }
            else if (parameter.Equals("Modificar"))
            {
                if(this.Seleccionado == null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this,
                    "Seleccionar el Asignacion de Alumno", "Debe seleccionar el Asignacion de Alumno");
                }
                else
                {
                    AsignacionAlumnoFormView modificarAsignacionAlumno = new AsignacionAlumnoFormView(Instancia);
                    modificarAsignacionAlumno.ShowDialog();
                }
            }
        }
    }
}