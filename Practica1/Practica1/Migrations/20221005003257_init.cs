using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Practica1.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "autores",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    biografia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    foto = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_autores", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "generos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_generos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "libros",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    resumen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fechapubli = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fotoportada = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    generoId = table.Column<int>(type: "int", nullable: false),
                    autorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_libros", x => x.ID);
                    table.ForeignKey(
                        name: "FK_libros_autores_autorId",
                        column: x => x.autorId,
                        principalTable: "autores",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_libros_generos_generoId",
                        column: x => x.generoId,
                        principalTable: "generos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_libros_autorId",
                table: "libros",
                column: "autorId");

            migrationBuilder.CreateIndex(
                name: "IX_libros_generoId",
                table: "libros",
                column: "generoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "libros");

            migrationBuilder.DropTable(
                name: "autores");

            migrationBuilder.DropTable(
                name: "generos");
        }
    }
}
