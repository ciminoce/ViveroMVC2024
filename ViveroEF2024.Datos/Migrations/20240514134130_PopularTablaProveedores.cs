using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ViveroEF2024.Datos.Migrations
{
    /// <inheritdoc />
    public partial class PopularTablaProveedores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Proveedores",
                columns: new[] { "ProveedorId", "Direccion", "Email", "Nombre", "Telefono" },
                values: new object[,]
                {
                    { 1, "Direccion 1", "proveedor1@gmail.com", "Proveedor 1", "422111" },
                    { 2, "Direccion 2", "proveedor2@gmail.com", "Proveedor 2", "422222" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Proveedores",
                keyColumn: "ProveedorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Proveedores",
                keyColumn: "ProveedorId",
                keyValue: 2);
        }
    }
}
