namespace ViveroEF2024.Entidades
{
    public class TipoDeEnvase
    {
        public int TipoDeEnvaseId { get; set; }
        public string Descripcion { get; set; } = null!;
        public ICollection<Planta> Plantas { get; set; } = new List<Planta>();
    }
}
