using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using ControlDeColegio.DataContext;
using ControlDeColegio.Models;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Diagnostics;

namespace ControlDeColegio.ModelView
{
    public class AlumnoFormViewModel : INotifyPropertyChanged, ICommand
    {
        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public AlumnoFormViewModel Instancia { get; set; }
        public AlumnoViewModel AlumnoViewModel { get; set; }
        public Alumno AlumnoForm { get; set; }
        private IDialogCoordinator DialogCoordinator;
        private KalumDBContext dbContext;
        public string Carne { get; set; }
        public string NoExpediente { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public string Email { get; set; }
        public string Titulo {get; set;}
        
        public AlumnoFormViewModel(AlumnoViewModel AlumnoViewModel, IDialogCoordinator instance)
        {
            this.Instancia = this;
            this.AlumnoViewModel = AlumnoViewModel;
            this.DialogCoordinator = instance;
            this.dbContext = new KalumDBContext();
            if (this.AlumnoViewModel.Seleccionado != null)
            {
                this.AlumnoForm = new Alumno();
                this.Titulo = "Modificar alumno";
                this.Carne = AlumnoViewModel.Seleccionado.Carne;
                this.NoExpediente = AlumnoViewModel.Seleccionado.NoExpediente;
                this.Apellidos = AlumnoViewModel.Seleccionado.Apellidos;
                this.Nombres = AlumnoViewModel.Seleccionado.Nombres;
                this.Email = AlumnoViewModel.Seleccionado.Email;
            } 
            else if (this.AlumnoViewModel.Seleccionado == null)
            {
                this.Titulo = "Nuevo alumno";
            }
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            if (parameter is Window)
            {
                try
                {
                    if (this.AlumnoViewModel.Seleccionado == null)
                    {
                        var ApellidosParameter = new SqlParameter("@Apellidos", this.Apellidos);
                        var NombresParameter = new SqlParameter("@Nombres", this.Nombres);
                        var EmailParameter = new SqlParameter("@Email", this.Email);
                        var Resultado = this.dbContext.Alumnos
                                                        .FromSqlRaw("sp_registrar_alumno @Apellidos, @Nombres, @Email", ApellidosParameter, NombresParameter, EmailParameter)
                                                        .ToList();
                        foreach(Object registro in Resultado){
                            this.AlumnoViewModel.Alumno.Add((Alumno)registro);
                        }
                        await DialogCoordinator.ShowMessageAsync(this, "Alumno", "Registro Actualizado");
                        // Alumno nuevo = new Alumno(Carne, NoExpediente, Apellidos, Nombres, Email);
                        // this.AlumnoViewModel.agregarElemento(nuevo);
                        
                        // this.dbContext.Alumnos.Add(nuevo);
                    }
                    else
                    {
                        int posicion = AlumnoViewModel.Alumno.IndexOf(this.AlumnoViewModel.Seleccionado);
                        AlumnoForm.Carne = this.Carne;
                        AlumnoForm.NoExpediente = this.NoExpediente;
                        AlumnoForm.Apellidos = this.Apellidos;
                        AlumnoForm.Nombres = this.Nombres;
                        AlumnoForm.Email = this.Email;
                        Console.WriteLine(AlumnoForm);

                        this.dbContext.Entry(AlumnoForm).State  = EntityState.Modified;
                        this.dbContext.SaveChanges();
                        this.AlumnoViewModel.Alumno.RemoveAt(posicion);
                        this.AlumnoViewModel.Alumno.Insert(posicion, AlumnoForm);
                        await this.DialogCoordinator.ShowMessageAsync(this,
                            "Alumno", "Registro actualizado");
                    }
                    // this.dbContext.SaveChanges();
                    ((Window)parameter).Close();
                } catch (Exception e) {
                    await this.DialogCoordinator.ShowMessageAsync(this, "Error", e.Message);
                }
            }
        }
    }
}