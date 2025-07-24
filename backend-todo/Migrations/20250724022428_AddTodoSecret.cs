using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_todo.Migrations
{
    /// <inheritdoc />
    public partial class AddTodoSecret : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Secret",
                table: "Todos",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Secret",
                table: "Todos");
        }
    }
}
