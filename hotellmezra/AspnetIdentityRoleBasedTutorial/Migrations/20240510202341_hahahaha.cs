using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspnetIdentityRoleBasedTutorial.Migrations
{
    /// <inheritdoc />
    public partial class hahahaha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ketersediaan",
                table: "Kamar",
                newName: "Ketersediaa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ketersediaa",
                table: "Kamar",
                newName: "Ketersediaan");
        }
    }
}
