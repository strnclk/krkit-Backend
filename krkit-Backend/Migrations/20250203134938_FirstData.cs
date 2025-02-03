using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace krkit_Backend.Migrations
{
    /// <inheritdoc />
    public partial class FirstData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
table: "Users",
columns: new[] {  "Username", "PasswordHash" },
values: new object[,]
{
                {  "Sito", "123"},
             
});
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
