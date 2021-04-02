using System;

namespace ControlDeColegio.Models
{
    public class DetalleActividad
    {
        public string DetalleActividadId {get; set;}
        public string SeminarioId {get; set;}
        public string NombreActividad {get; set;}
        public int NotaActividad {get; set;}
        public DateTime FechaCreacion {get; set;}
        public DateTime FechaEntrega {get; set;}
        public DateTime FechaPostergacion {get; set;}
        public char Estado {get; set;}

        
        public DetalleActividad()
        {
            
        }
        public DetalleActividad(string DetalleActividadId, string SeminarioId, string NombreActividad, int NotaActividad, DateTime FechaCreacion, DateTime FechaEntrega, DateTime FechaPostergacion, char Estado)
        {
            this.DetalleActividadId = DetalleActividadId;
            this.SeminarioId = SeminarioId;
            this.NombreActividad = NombreActividad;
            this.NotaActividad = NotaActividad;
            this.FechaCreacion = FechaCreacion;
            this.FechaEntrega = FechaEntrega;
            this.FechaPostergacion = FechaPostergacion;
            this.Estado = Estado;
        }
    }
}