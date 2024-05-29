﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ViveroEF2024.Datos;

#nullable disable

namespace ViveroEF2024.Datos.Migrations
{
    [DbContext(typeof(ViveroDbContext))]
    [Migration("20240514134130_PopularTablaProveedores")]
    partial class PopularTablaProveedores
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ViveroEF2024.Datos.Planta", b =>
                {
                    b.Property<int>("PlantaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlantaId"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("PrecioCosto")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal>("PrecioVenta")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TipoDeEnvaseId")
                        .HasColumnType("int");

                    b.Property<int>("TipoDePlantaId")
                        .HasColumnType("int");

                    b.HasKey("PlantaId");

                    b.HasIndex("Descripcion")
                        .IsUnique();

                    b.HasIndex("TipoDeEnvaseId");

                    b.HasIndex("TipoDePlantaId");

                    b.ToTable("Plantas");
                });

            modelBuilder.Entity("ViveroEF2024.Datos.TipoDePlanta", b =>
                {
                    b.Property<int>("TipoDePlantaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TipoDePlantaId"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("TipoDePlantaId");

                    b.HasIndex("Descripcion")
                        .IsUnique();

                    b.ToTable("TiposDePlantas");
                });

            modelBuilder.Entity("ViveroEF2024.Entidades.Proveedor", b =>
                {
                    b.Property<int>("ProveedorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProveedorId"));

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("ProveedorId");

                    b.HasIndex("Nombre")
                        .IsUnique();

                    b.ToTable("Proveedores", (string)null);

                    b.HasData(
                        new
                        {
                            ProveedorId = 1,
                            Direccion = "Direccion 1",
                            Email = "proveedor1@gmail.com",
                            Nombre = "Proveedor 1",
                            Telefono = "422111"
                        },
                        new
                        {
                            ProveedorId = 2,
                            Direccion = "Direccion 2",
                            Email = "proveedor2@gmail.com",
                            Nombre = "Proveedor 2",
                            Telefono = "422222"
                        });
                });

            modelBuilder.Entity("ViveroEF2024.Entidades.TipoDeEnvase", b =>
                {
                    b.Property<int>("TipoDeEnvaseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TipoDeEnvaseId"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("TipoDeEnvaseId");

                    b.HasIndex("Descripcion")
                        .IsUnique();

                    b.ToTable("TiposDeEnvases", (string)null);
                });

            modelBuilder.Entity("ViveroEF2024.Datos.Planta", b =>
                {
                    b.HasOne("ViveroEF2024.Entidades.TipoDeEnvase", "TipoDeEnvase")
                        .WithMany("Plantas")
                        .HasForeignKey("TipoDeEnvaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ViveroEF2024.Datos.TipoDePlanta", "TipoDePlanta")
                        .WithMany("Plantas")
                        .HasForeignKey("TipoDePlantaId")
                        .IsRequired()
                        .HasConstraintName("FK_Plantas_TiposDePlantas");

                    b.Navigation("TipoDeEnvase");

                    b.Navigation("TipoDePlanta");
                });

            modelBuilder.Entity("ViveroEF2024.Datos.TipoDePlanta", b =>
                {
                    b.Navigation("Plantas");
                });

            modelBuilder.Entity("ViveroEF2024.Entidades.TipoDeEnvase", b =>
                {
                    b.Navigation("Plantas");
                });
#pragma warning restore 612, 618
        }
    }
}
