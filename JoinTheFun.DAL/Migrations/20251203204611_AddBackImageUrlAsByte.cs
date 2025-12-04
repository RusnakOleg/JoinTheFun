using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoinTheFun.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddBackImageUrlAsByte : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ImageUrl",
                table: "Posts",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Posts");
        }
    }
}
