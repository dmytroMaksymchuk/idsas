using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IDsas.Server.Migrations
{
    /// <inheritdoc />
    public partial class SimplifyEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentLinks_Users_AssociatedUserId",
                table: "DocumentLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Users_AuthorId",
                table: "Documents");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Documents_AuthorId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_DocumentLinks_AssociatedUserId",
                table: "DocumentLinks");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "AssociatedUserId",
                table: "DocumentLinks");

            migrationBuilder.RenameColumn(
                name: "AccessToken",
                table: "DocumentLinks",
                newName: "AssociatedUserToken");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Content",
                table: "Documents",
                type: "BLOB",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Documents",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorToken",
                table: "Documents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "DocumentId",
                table: "DocumentLinks",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "DocumentLinks",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAssociatedUserConfirmed",
                table: "DocumentLinks",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorToken",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "IsAssociatedUserConfirmed",
                table: "DocumentLinks");

            migrationBuilder.RenameColumn(
                name: "AssociatedUserToken",
                table: "DocumentLinks",
                newName: "AccessToken");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Documents",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "BLOB",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Documents",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Documents",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DocumentId",
                table: "DocumentLinks",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "DocumentLinks",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "AssociatedUserId",
                table: "DocumentLinks",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AuthorizationToken = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documents_AuthorId",
                table: "Documents",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentLinks_AssociatedUserId",
                table: "DocumentLinks",
                column: "AssociatedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentLinks_Users_AssociatedUserId",
                table: "DocumentLinks",
                column: "AssociatedUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Users_AuthorId",
                table: "Documents",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
