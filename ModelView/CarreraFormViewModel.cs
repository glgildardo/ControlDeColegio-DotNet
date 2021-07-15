using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using ControlDeColegio.DataContext;
using ControlDeColegio.Models;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ControlDeColegio.ModelView
{
    public partial class CarreraFormViewModel : INotifyPropertyChanged, ICommand
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        private IDialogCoordinator DialogCoordinator;
        private KalumDBContext dbContext;
        public CarreraFormViewModel Instancia {get; set;}
        public CarreraViewModel CarreraViewModel {get; set;}
        public Carrera CarreraForm {get; set;}
        public string Titulo {get; set;}
        public string CarreraId {get; set;}
        public string Nombre {get; set;}

        public CarreraFormViewModel(CarreraViewModel CarreraViewModel, IDialogCoordinator instance)
        {
            this.Instancia = this;
            this.CarreraViewModel = CarreraViewModel;
            this.DialogCoordinator = instance;
            this.dbContext = new KalumDBContext();
            if(this.CarreraViewModel.Seleccionado != null)
            {
                this.CarreraForm = new Carrera();
                this.Titulo = "Modificar salón";
                this.Nombre = CarreraViewModel.Seleccionado.Nombre;
            }
            else if (this.CarreraViewModel.Seleccionado == null)
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
                    if(this.CarreraViewModel.Seleccionado == null)
                    {
                        var NombreParameter = new SqlParameter("@Nombre", this.Nombre);
                        var Resultado = await this.dbContext.Carreras
                            .FromSqlRaw("sp_registrar_carrera @Nombre", NombreParameter)
                            .ToListAsync();
                        foreach(Object registro in Resultado)
                        {
                            this.CarreraViewModel.Carrera.Add((Carrera) registro);
                        }
                        await DialogCoordinator.ShowMessageAsync(this,
                            "Salon", "Registro guardado");
                    }
                    else
                    {
                        
                        int posicion = CarreraViewModel.Carrera.IndexOf(this.CarreraViewModel.Seleccionado);
                        Carrera temporal = new Carrera();
                        temporal.CarreraId = this.CarreraViewModel.Seleccionado.CarreraId;
                        temporal.Nombre = this.Nombre;
                        this.dbContext.Entry(temporal).State = EntityState.Modified;
                        this.dbContext.SaveChanges();
                        await this.DialogCoordinator.ShowMessageAsync(this,
                            "Carrera", "Registro actualizado");
                        this.CarreraViewModel.Carrera.RemoveAt(posicion);
                        this.CarreraViewModel.Carrera.Insert(posicion, temporal);
                    }
                } catch (Exception e) {
                    await this.DialogCoordinator.ShowMessageAsync(this, "Error", e.Message);
                }
                ((Window)parameter).Close();
            }
        }
    }
}