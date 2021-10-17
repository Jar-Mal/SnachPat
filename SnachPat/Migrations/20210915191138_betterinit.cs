using Microsoft.EntityFrameworkCore.Migrations;

namespace SnachPat.Migrations
{
    public partial class betterinit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "photoName",
                table: "photos",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "photoName",
                table: "photos");
        }
    }
}
