using Microsoft.EntityFrameworkCore.Migrations;

namespace dotNetCoreAzure.Data.Migrations
{
    public partial class meds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "vnr",
                table: "meds",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "vnr",
                table: "meds");
        }
    }
}
