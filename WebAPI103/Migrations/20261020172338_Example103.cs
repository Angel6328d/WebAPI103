using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI103.Migrations
{
    /// <inheritdoc />
    public partial class Example103 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "rols",
                columns: table => new
                {
                    PkRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rols", x => x.PkRol);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    PkUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FkRol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.PkUsuario);
                    table.ForeignKey(
                        name: "FK_usuarios_rols_FkRol",
                        column: x => x.FkRol,
                        principalTable: "rols",
                        principalColumn: "PkRol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "rols",
                columns: new[] { "PkRol", "Estatus", "Nombre" },
                values: new object[,]
                {
                    { 1, true, "SA" },
                    { 2, false, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "usuarios",
                columns: new[] { "PkUsuario", "FkRol", "Nombre", "Password", "Username" },
                values: new object[] { 1, 1, "Angel", "123456", "Chocorroll" });

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_FkRol",
                table: "usuarios",
                column: "FkRol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "rols");
        }
    }
}
