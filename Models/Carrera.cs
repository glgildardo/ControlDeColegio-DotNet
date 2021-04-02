namespace ControlDeColegio.Models
{
    public class Carrera
    {
        public string CodigoCarrera {get; set;}
        public string Nombre {get; set;}

        public Carrera()
        {
            
        }

        public Carrera(string CodigoCarrera, string Nombre)
        {
            this.CodigoCarrera = CodigoCarrera;
            this.Nombre = Nombre;
        }
    }
}