using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowDesk.API.Migrations
{
    /// <inheritdoc />
    public partial class AddOwnerIdToProjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "created_by_user_id",
                table: "projects",
                newName: "owner_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "owner_id",
                table: "projects",
                newName: "created_by_user_id");
        }
    }
}
