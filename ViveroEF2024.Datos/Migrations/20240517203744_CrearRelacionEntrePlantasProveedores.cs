using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ViveroEF2024.Datos.Migrations
{
    /// <inheritdoc />
    public partial class CrearRelacionEntrePlantasProveedores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProveedoresPlantas",
                columns: table => new
                {
                    ProveedorId = table.Column<int>(type: "int", nullable: false),
                    PlantaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProveedoresPlantas", x => new { x.ProveedorId, x.PlantaId });
                    table.ForeignKey(
                        name: "FK_ProveedoresPlantas_Plantas_PlantaId",
                        column: x => x.PlantaId,
                        principalTable: "Plantas",
                        principalColumn: "PlantaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProveedoresPlantas_Proveedores_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "Proveedores",
                        principalColumn: "ProveedorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProveedoresPlantas_PlantaId",
                table: "ProveedoresPlantas",
                column: "PlantaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProveedoresPlantas");
        }
    }
}
