using Microsoft.EntityFrameworkCore.Migrations;

namespace dotNetCoreAzure.Data.Migrations
{
    public partial class members : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "repeatpassword",
                table: "members",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "repeatpassword",
                table: "members");
        }
    }
}
