using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GorevTakipSistemiAPI.Migrations
{
    /// <inheritdoc />
    public partial class migOgu1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gorevler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    baslik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    basTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    bitTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    konu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    durum = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gorevler", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gorevler");
        }
    }
}
