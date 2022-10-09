using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShowScoreCompare.Migrations
{
    public partial class Added_show_type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "ShowsDB",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "ShowsDB");
        }
    }
}
