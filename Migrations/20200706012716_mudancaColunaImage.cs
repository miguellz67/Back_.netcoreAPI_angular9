using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LojaAPI.Migrations
{
    public partial class mudancaColunaImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUpload",
                table: "Products");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Image",
                table: "Products",
                type: "mediumblob",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Products",
                type: "text",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "mediumblob");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageUpload",
                table: "Products",
                type: "mediumblob",
                nullable: false,
                defaultValue: new byte[] {  });
        }
    }
}
