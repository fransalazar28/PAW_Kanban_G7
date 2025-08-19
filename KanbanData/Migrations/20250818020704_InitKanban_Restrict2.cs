using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace K.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitKanban_Restrict2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreUsuario = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "Tableros",
                columns: table => new
                {
                    TableroId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tableros", x => x.TableroId);
                    table.ForeignKey(
                        name: "FK_Tableros_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Columnas",
                columns: table => new
                {
                    ColumnaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    TableroId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Columnas", x => x.ColumnaId);
                    table.ForeignKey(
                        name: "FK_Columnas_Tableros_TableroId",
                        column: x => x.TableroId,
                        principalTable: "Tableros",
                        principalColumn: "TableroId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Etiquetas",
                columns: table => new
                {
                    EtiquetaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TableroId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etiquetas", x => x.EtiquetaId);
                    table.ForeignKey(
                        name: "FK_Etiquetas_Tableros_TableroId",
                        column: x => x.TableroId,
                        principalTable: "Tableros",
                        principalColumn: "TableroId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HistoriasUsuario",
                columns: table => new
                {
                    HistoriaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    ColumnaId = table.Column<int>(type: "int", nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false, defaultValue: 2147483647),
                    ResponsableId = table.Column<int>(type: "int", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    FechaVencimiento = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoriasUsuario", x => x.HistoriaId);
                    table.ForeignKey(
                        name: "FK_HistoriasUsuario_Columnas_ColumnaId",
                        column: x => x.ColumnaId,
                        principalTable: "Columnas",
                        principalColumn: "ColumnaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistoriasUsuario_Usuarios_ResponsableId",
                        column: x => x.ResponsableId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Comentarios",
                columns: table => new
                {
                    ComentarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoriaId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Texto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentarios", x => x.ComentarioId);
                    table.ForeignKey(
                        name: "FK_Comentarios_HistoriasUsuario_HistoriaId",
                        column: x => x.HistoriaId,
                        principalTable: "HistoriasUsuario",
                        principalColumn: "HistoriaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comentarios_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoriasEtiquetas",
                columns: table => new
                {
                    HistoriaId = table.Column<int>(type: "int", nullable: false),
                    EtiquetaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoriasEtiquetas", x => new { x.HistoriaId, x.EtiquetaId });
                    table.ForeignKey(
                        name: "FK_HistoriasEtiquetas_Etiquetas_EtiquetaId",
                        column: x => x.EtiquetaId,
                        principalTable: "Etiquetas",
                        principalColumn: "EtiquetaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistoriasEtiquetas_HistoriasUsuario_HistoriaId",
                        column: x => x.HistoriaId,
                        principalTable: "HistoriasUsuario",
                        principalColumn: "HistoriaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Columnas_TableroId",
                table: "Columnas",
                column: "TableroId");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_HistoriaId",
                table: "Comentarios",
                column: "HistoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_UsuarioId",
                table: "Comentarios",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Etiquetas_TableroId_Nombre",
                table: "Etiquetas",
                columns: new[] { "TableroId", "Nombre" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistoriasEtiquetas_EtiquetaId",
                table: "HistoriasEtiquetas",
                column: "EtiquetaId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoriasUsuario_ColumnaId_Orden",
                table: "HistoriasUsuario",
                columns: new[] { "ColumnaId", "Orden" });

            migrationBuilder.CreateIndex(
                name: "IX_HistoriasUsuario_ResponsableId",
                table: "HistoriasUsuario",
                column: "ResponsableId");

            migrationBuilder.CreateIndex(
                name: "IX_Tableros_UsuarioId",
                table: "Tableros",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comentarios");

            migrationBuilder.DropTable(
                name: "HistoriasEtiquetas");

            migrationBuilder.DropTable(
                name: "Etiquetas");

            migrationBuilder.DropTable(
                name: "HistoriasUsuario");

            migrationBuilder.DropTable(
                name: "Columnas");

            migrationBuilder.DropTable(
                name: "Tableros");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
