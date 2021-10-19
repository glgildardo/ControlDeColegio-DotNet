using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using ControlDeColegio.DataContext;
using ControlDeColegio.Models;
using ControlDeColegio.Views;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ControlDeColegio.ModelView
{
    public class ClaseViewModel : INotifyPropertyChanged, ICommand

    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        public IDialogCoordinator dialogCoordinator;
        public KalumDBContext dBContext = new KalumDBContext();
        
        private Clase _Seleccionado;
        public Clase Seleccionado {
            get
            {
                return this._Seleccionado;
            } 
            set
            {
                this._Seleccionado = value;
                NotificarCambio("Seleccionado");
            }
        }

        public ClaseViewModel Instancia {get; set;}

        public ObservableCollection<Clase> _Clase {get; set;}
        public ObservableCollection<Clase> Clase 
        {
            get{
                if(this._Clase == null) {
                    this._Clase = new ObservableCollection<Clase>(dBContext.Clases.Include(c => c.Instructor).Include(c => c.Carrera).Include(c => c.Salon).Include(c => c.Horario).ToList());
                }
                return this._Clase;    
            } 
            set{
                this.Clase = value;
            }
        }
        
        public ClaseViewModel(IDialogCoordinator instance)
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
        public void agregarElemento(Clase nuevo)
        {
            this.Clase.Add(nuevo);
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            if(parameter.Equals("Nuevo"))
            {
                if(this.Seleccionado != null) 
                {
                    this.Seleccionado = null;
                }
                this.Seleccionado = null;
                ClaseFormView nuevoClase = new ClaseFormView(Instancia);
                nuevoClase.Show();
            }
            else if(parameter.Equals("Eliminar")) 
            {
                if(this.Seleccionado == null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this,
                        "Clase", "Debe seleccionar la Clase que desea eliminar",
                        MessageDialogStyle.Affirmative);
                }
                else
                {
                    MessageDialogResult respuesta = await this.dialogCoordinator.ShowMessageAsync(this,
                        "Eliminar la Clase", "¿Esta seguro de eliminar la Clase?",
                        MessageDialogStyle.AffirmativeAndNegative);
                    if(respuesta == MessageDialogResult.Affirmative)
                    {
                        try
                        {
                            this.dBContext.Remove(this.Seleccionado);
                            this.dBContext.SaveChanges();
                            this.Clase.Remove(Seleccionado);
                            await this.dialogCoordinator.ShowMessageAsync(this, "Clases", "Registro eliminado");
                        }catch(Exception e) {
                            await this.dialogCoordinator.ShowMessageAsync(this, "Clases", "Error al eliminar el registro");
                        }
                        
                    }
                }
            }
            else if (parameter.Equals("Modificar"))
            {
                if(this.Seleccionado == null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this,
                    "Seleccionar la  Clase", "Debe seleccionar la Clase");
                }
                else
                {
                    ClaseFormView modificarClase = new ClaseFormView(Instancia);
                    modificarClase.ShowDialog();
                }
            }
        }
    }
}