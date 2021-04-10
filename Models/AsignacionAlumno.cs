using System;

namespace ControlDeColegio.Models
{
    public class AsignacionAlumno
    {
        public string AsignacionId {get; set;}
        public string Carne {get; set;}
        public string ClaseId {get; set;}
        public DateTime FechaAsignacion {get; set;}

        
        public AsignacionAlumno()
        {
            
        }

        public AsignacionAlumno(string AsignacionId, string Carne, string ClaseId, DateTime FechaAsignacion)
        {
            this.AsignacionId = AsignacionId;
            this.Carne = Carne;
            this.ClaseId = ClaseId;
            this.FechaAsignacion = FechaAsignacion;
        }
    }
}