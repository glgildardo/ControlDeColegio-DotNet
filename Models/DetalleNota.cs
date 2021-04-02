namespace ControlDeColegio.Models
{
    public class DetalleNota
    {
        public string DetalleNotaId {get; set;}
        public string DetalleActividadId {get; set;}
        public string Carne {get; set;}
        public int ValorNota {get; set;}

        public DetalleNota()
        {
            
        }

        public DetalleNota(string DetalleNotaId, string DetalleActividadId, string Carne, int ValorNota)
        {
            this.DetalleNotaId = DetalleNotaId;
            this.DetalleActividadId = DetalleActividadId;
            this.Carne = Carne;
            this.ValorNota = ValorNota;
        }

    }
}