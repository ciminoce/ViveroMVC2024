namespace ViveroEF2024.Entidades
{
    public class Proveedor
    {
        public int ProveedorId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Email { get; set; } = null!;
        public ICollection<ProveedorPlanta> ProveedoresPlantas { get; set; } = new List<ProveedorPlanta>();
    }
}
