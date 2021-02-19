using Microsoft.EntityFrameworkCore.Migrations;

namespace MyNotesProject.Migrations
{
    public partial class addDescricao_Lembrete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "descricao_Lembrete",
                table: "notas",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "descricao_Lembrete",
                table: "notas");
        }
    }
}
