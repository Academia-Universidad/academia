using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProgramacionV.Migrations
{
    /// <inheritdoc />
    public partial class firstmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProgramasAcademicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Codigo = table.Column<string>(type: "TEXT", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramasAcademicos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estudiantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Documento = table.Column<string>(type: "TEXT", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Correo = table.Column<string>(type: "TEXT", nullable: false),
                    ProgramaAcademicoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudiantes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estudiantes_ProgramasAcademicos_ProgramaAcademicoId",
                        column: x => x.ProgramaAcademicoId,
                        principalTable: "ProgramasAcademicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ProgramasAcademicos",
                columns: new[] { "Id", "Codigo", "Nombre" },
                values: new object[,]
                {
                    { 1, "SIS", "Ingeniería de Sistemas" },
                    { 2, "ADM", "Administración de Empresas" }
                });

            migrationBuilder.InsertData(
                table: "Estudiantes",
                columns: new[] { "Id", "Correo", "Documento", "Nombre", "ProgramaAcademicoId" },
                values: new object[,]
                {
                    { 1, "ana.torres@universidad.edu.co", "1001001001", "Ana Torres", 1 },
                    { 2, "carlos.gomez@universidad.edu.co", "1001001002", "Carlos Gómez", 1 },
                    { 3, "laura.perez@universidad.edu.co", "1001001003", "Laura Pérez", 1 },
                    { 4, "miguel.ramirez@universidad.edu.co", "1001001004", "Miguel Ramírez", 2 },
                    { 5, "sofia.martinez@universidad.edu.co", "1001001005", "Sofía Martínez", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Estudiantes_ProgramaAcademicoId",
                table: "Estudiantes",
                column: "ProgramaAcademicoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estudiantes");

            migrationBuilder.DropTable(
                name: "ProgramasAcademicos");
        }
    }
}
