using System;
using System.Collections.Generic;
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
    public class HorarioFormViewModel : INotifyPropertyChanged, ICommand
    {
        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;
        public HorarioFormViewModel Instancia {get; set;}
        public HorarioViewModel HorarioViewModel {get; set;} 
        private IDialogCoordinator DialogCoordinator;
        private KalumDBContext dbContext;
        public string Titulo {get; set;}
        public string HorarioInicialDefinido {get; set;} = "Seleccionar";
        public string HorarioFinalDefinido {get; set;} = "Seleccionar";
        public Horario HorarioForm {get; set;}
        public List<TimeSpan> _Horarios {get;set;}
        public List<TimeSpan> Horarios {
            get
            {
                this._Horarios = new List<TimeSpan>();
                for(double i = 0.5; i <= 23.5; i = i + 0.5)
                {
                    this._Horarios.Add(TimeSpan.FromHours(i));
                }
                return this._Horarios;
            }
            set
            {
                this.Horarios = value;
            }
        }
        public TimeSpan  HorarioInicio {get; set;}
        public TimeSpan HorarioFinal {get; set;}

        public HorarioFormViewModel(HorarioViewModel HorarioViewModel, IDialogCoordinator instance)
        {
            this.Instancia = this;
            this.HorarioViewModel = HorarioViewModel;
            this.DialogCoordinator = instance;
            this.dbContext = new KalumDBContext();
            if(this.HorarioViewModel.Seleccionado != null)
            {
                this.Titulo = "Modificar Horario";
                this.HorarioForm = new Horario();
                this.HorarioInicio = HorarioViewModel.Seleccionado.HorarioInicio;
                this.HorarioFinal = HorarioViewModel.Seleccionado.HorarioFinal;
            }
            else if(this.HorarioViewModel.Seleccionado == null)
            {
                this.Titulo = "Nuevo Horario";
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
                    if(this.HorarioViewModel.Seleccionado == null)
                    {
                        var HorarioInicioParameter = new SqlParameter("@HorarioInicio", this.HorarioInicialDefinido);
                        var HorarioFinalParameter = new SqlParameter("@HorarioFinal", this.HorarioFinalDefinido);
                        var Resultado = await this.dbContext.Horarios
                            .FromSqlRaw("sp_registrar_horario @HorarioInicio, @HorarioFinal", HorarioInicioParameter, HorarioFinalParameter)
                            .ToListAsync();
                        foreach(Object registro in Resultado)
                        {
                            this.HorarioViewModel.Horario.Add((Horario)registro);
                        }
                        await DialogCoordinator.ShowMessageAsync(this, "Horario", "Registro guardado");
                    }
                    else
                    {
                        // Conversion de horas tipo string a TimeSpan
                        int[] partesHorarioInicial = HorarioInicialDefinido.Split(new char[] { ':' }).Select(x => Convert.ToInt32(x)).ToArray();
                        TimeSpan tiempoInicial = new TimeSpan(partesHorarioInicial[0], partesHorarioInicial[1], partesHorarioInicial[2]);
                        int[] partesHorarioFinal = HorarioFinalDefinido.Split(new char[] { ':' }).Select(x => Convert.ToInt32(x)).ToArray();
                        TimeSpan tiempoFinal = new TimeSpan(partesHorarioFinal[0], partesHorarioFinal[1], partesHorarioFinal[2]);

                        // Setteo de datos
                        int posicion = HorarioViewModel.Horario.IndexOf(this.HorarioViewModel.Seleccionado);
                        Horario temporal = new Horario();
                        temporal.HorarioId = this.HorarioViewModel.Seleccionado.HorarioId;
                        temporal.HorarioInicio = tiempoInicial;
                        temporal.HorarioFinal = tiempoFinal;
                        this.dbContext.Entry(temporal).State = EntityState.Modified;
                        this.dbContext.SaveChanges();
                        await this.DialogCoordinator.ShowMessageAsync(this,
                            "Horario", "Registro actualizado");
                        this.HorarioViewModel.Horario.RemoveAt(posicion);
                        this.HorarioViewModel.Horario.Insert(posicion, temporal);
                    }
                } catch (Exception e){
                    await this.DialogCoordinator.ShowMessageAsync(this, "Error", e.Message);
                }
                ((Window)parameter).Close();
            }
        }

    }
}