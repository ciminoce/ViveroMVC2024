using Microsoft.EntityFrameworkCore;
using ViveroEF2024.Entidades;
namespace ViveroEF2024.Datos;

public partial class ViveroDbContext : DbContext
{
    public ViveroDbContext()
    {
    }

    public ViveroDbContext(DbContextOptions<ViveroDbContext> options)
        : base(options)
    {
    }

    public DbSet<Planta> Plantas { get; set; }
    public DbSet<Proveedor> Proveedores { get; set; }
    public DbSet<TipoDePlanta> TiposDePlantas { get; set; }
    public DbSet<TipoDeEnvase> TiposDeEnvases { get; set; }
    public DbSet<ProveedorPlanta> ProveedoresPlantas { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder.UseSqlServer(@"Data Source=.; 
                        Initial Catalog=ViveroEF2024; 
                        Trusted_Connection=true; 
                        TrustServerCertificate=true;");

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TipoDeEnvase>(entity =>
        {
            entity.ToTable("TiposDeEnvases");
            entity.Property(e => e.Descripcion)
            .HasMaxLength(50);
            entity.HasIndex(e => e.Descripcion).IsUnique();
        });
        modelBuilder.Entity<Planta>(entity =>
        {
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PrecioCosto).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.TipoDePlanta).WithMany(p => p.Plantas)
                .HasForeignKey(d => d.TipoDePlantaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Plantas_TiposDePlantas");
            entity.HasIndex(e => e.Descripcion).IsUnique();
        });

        modelBuilder.Entity<TipoDePlanta>(entity =>
        {
            entity.HasKey(e => e.TipoDePlantaId);

            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.HasIndex(e => e.Descripcion).IsUnique();

        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.ToTable("Proveedores");
            entity.Property(e => e.Nombre).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Direccion).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Telefono).HasMaxLength(20).IsRequired();
            entity.Property(e => e.Email).HasMaxLength(100).IsRequired();
            entity.HasIndex(e=>e.Nombre).IsUnique();

            entity.HasData(new Proveedor
            {
                ProveedorId = 1,
                Nombre = "Proveedor 1",
                Direccion = "Direccion 1",
                Telefono = "422111",
                Email = "proveedor1@gmail.com"
            },
             new Proveedor
             {
                 ProveedorId = 2,
                 Nombre = "Proveedor 2",
                 Direccion = "Direccion 2",
                 Telefono = "422222",
                 Email = "proveedor2@gmail.com"
             });


        });
        modelBuilder.Entity<ProveedorPlanta>(entity =>
        {
            entity.HasKey(pp => new { pp.ProveedorId, 
                pp.PlantaId });

            entity.HasOne(pp => pp.Proveedor)
                .WithMany(p => p.ProveedoresPlantas)
                .HasForeignKey(pp => pp.ProveedorId);

            entity.HasOne(pp => pp.Planta)
                .WithMany(p => p.ProveedoresPlantas)
                .HasForeignKey(pp => pp.PlantaId);

        });



        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
