using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatingApp.API.Migrations
{
    public partial class initroot19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "newnews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_newUsers",
                table: "newUsers");

            migrationBuilder.RenameTable(
                name: "newUsers",
                newName: "NewUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewUsers",
                table: "NewUsers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NewUsers",
                table: "NewUsers");

            migrationBuilder.RenameTable(
                name: "NewUsers",
                newName: "newUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_newUsers",
                table: "newUsers",
                column: "UserId");

            migrationBuilder.CreateTable(
                name: "newnews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_newnews", x => x.Id);
                });
        }
    }
}
