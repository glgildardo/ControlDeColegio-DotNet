namespace ControlDeColegio.Models
{
    public class Rol
    {
        public int Id {get; set;}
        public string Nombre {get; set;}

        public Rol()
        {
            
        }
        public Rol(int Id, string Nombre)
        {
            this.Id = Id;
            this.Nombre = Nombre;     
        }
    }
}