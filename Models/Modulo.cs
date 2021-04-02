namespace ControlDeColegio.Models
{
    public class Modulo
    {
        public string ModuloId {get; set;}
        public string CarreraId {get; set;}
        public string NombreModulo {get; set;}
        public int NumeroDeSeminarios {get; set;}

        public Modulo()
        {
            
        }

        public Modulo(string ModuloId, string CarreraId, string NombreModulo, int NumeroDeSeminarios)
        {
            this.ModuloId = ModuloId;
            this.CarreraId = CarreraId;
            this.NombreModulo = NombreModulo;
            this.NumeroDeSeminarios = NumeroDeSeminarios;
        }
    }
}