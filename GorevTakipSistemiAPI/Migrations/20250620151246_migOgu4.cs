using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GorevTakipSistemiAPI.Migrations
{
    /// <inheritdoc />
    public partial class migOgu4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "kullaniciId",
                table: "Gorevler",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Gorevler_kullaniciId",
                table: "Gorevler",
                column: "kullaniciId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gorevler_AspNetUsers_kullaniciId",
                table: "Gorevler",
                column: "kullaniciId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gorevler_AspNetUsers_kullaniciId",
                table: "Gorevler");

            migrationBuilder.DropIndex(
                name: "IX_Gorevler_kullaniciId",
                table: "Gorevler");

            migrationBuilder.DropColumn(
                name: "kullaniciId",
                table: "Gorevler");
        }
    }
}
