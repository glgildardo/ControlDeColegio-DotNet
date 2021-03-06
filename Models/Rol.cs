namespace ControlDeColegio.Models
{
    public class Rol
    {
        public int Id {get; set;}
        public string Name {get; set;}

        public Rol()
        {
            
        }
        public Rol(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;     
        }
    }
}