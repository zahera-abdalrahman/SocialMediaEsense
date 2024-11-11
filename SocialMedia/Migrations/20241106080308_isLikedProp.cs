using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMedia.Migrations
{
    public partial class isLikedProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isLiked",
                table: "likes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isLiked",
                table: "likes");
        }
    }
}
