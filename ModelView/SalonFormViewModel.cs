using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ControlDeColegio.DataContext;
using ControlDeColegio.Models;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ControlDeColegio.ModelView
{
    public class SalonFormViewModel : INotifyPropertyChanged, ICommand
    {
        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;
        public SalonFormViewModel Instancia {get; set;}
        public SalonViewModel SalonViewModel {get; set;}
        public Salon SalonForm {get; set;}
        private IDialogCoordinator DialogCoordinator;
        private KalumDBContext dbContext;
        public string Titulo {get; set;}
        public string SalonId {get; set;}
        public int Capacidad {get; set;}
        public string Descripcion {get; set;}
        public string NombreSalon {get; set;}

        public SalonFormViewModel(SalonViewModel SalonViewModel, IDialogCoordinator instance)
        {
            this.Instancia = this;
            this.SalonViewModel = SalonViewModel;
            this.DialogCoordinator = instance;
            this.dbContext = new KalumDBContext();
            if(this.SalonViewModel.Seleccionado != null)
            {
                this.SalonForm = new Salon();
                this.Titulo = "Modificar salón";
                this.Capacidad = SalonViewModel.Seleccionado.Capacidad;
                this.Descripcion = SalonViewModel.Seleccionado.Descripcion;
                this.NombreSalon = SalonViewModel.Seleccionado.NombreSalon;
            } 
            else if (this.SalonViewModel.Seleccionado == null)
            {
                this.Titulo = "Nuevo salón";
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
                try
                {
                    if(this.SalonViewModel.Seleccionado == null)
                    {
                        var CapacidadParameter = new SqlParameter("@Capacidad", this.Capacidad);
                        var DescripcionParameter = new SqlParameter("@Descripcion", this.Descripcion);
                        var NombreSalonParameter = new SqlParameter("@NombreSalon", this.NombreSalon);
                        var Resultado = this.dbContext.Salones
                            .FromSqlRaw("sp_registrar_salon @Capacidad, @Descripcion, @NombreSalon", CapacidadParameter, DescripcionParameter, NombreSalonParameter)
                            .ToListAsync();
                        foreach(Object registro in await Resultado)
                        {
                            this.SalonViewModel.Salon.Add((Salon)registro);
                        }
                        await DialogCoordinator.ShowMessageAsync(this, "Salon", "Registro guardado");
                    }
                    else
                    {
                        int posicion = SalonViewModel.Salon.IndexOf(this.SalonViewModel.Seleccionado);
                        Salon temporal = new Salon();
                        temporal.SalonId = this.SalonViewModel.Seleccionado.SalonId;
                        temporal.Capacidad = this.Capacidad;
                        temporal.Descripcion = this.Descripcion;
                        temporal.NombreSalon = this.NombreSalon;
                        this.dbContext.Entry(temporal).State = EntityState.Modified;
                        this.dbContext.SaveChanges();
                        this.SalonViewModel.Salon.RemoveAt(posicion);
                        this.SalonViewModel.Salon.Insert(posicion, temporal);
                        await this.DialogCoordinator.ShowMessageAsync(this,
                            "Salon", "Registro actualizado");
                    
                    }
                } catch(Exception e) {
                    await this.DialogCoordinator.ShowMessageAsync(this, "Error", e.Message);
                }
                ((Window)parameter).Close();
            }
        }
    }
}