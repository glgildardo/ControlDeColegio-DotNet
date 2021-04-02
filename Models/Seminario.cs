using System;

namespace ControlDeColegio.Models
{
    public class Seminario
    {
        public string SeminarioId {get; set;}
        public string ModuloId {get; set;}
        public string ModuloSeminario {get; set;}
        public DateTime FechaInicio {get; set;}
        public DateTime FechaFin {get; set;}

        public Seminario()
        {
            
        }

        public Seminario (string seminarioId, string ModuloId, string ModuloSeminario, DateTime FechaInicio, DateTime FechaFin)
        {
            this.SeminarioId = seminarioId;
            this.ModuloId = ModuloId;
            this.ModuloSeminario = ModuloSeminario;
            this.FechaInicio = FechaInicio;
            this.FechaFin = FechaFin;
        }
    }
}