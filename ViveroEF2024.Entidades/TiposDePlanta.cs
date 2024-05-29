namespace ViveroEF2024.Entidades
{
    public class TipoDePlanta
    {
        public int TipoDePlantaId { get; set; }

        public string Descripcion { get; set; } = null!;

        public virtual ICollection<Planta> Plantas { get; set; } = new List<Planta>();
    }
}
