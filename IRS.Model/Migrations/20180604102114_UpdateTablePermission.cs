using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace IRS.Model.Migrations
{
    public partial class UpdateTablePermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PermissionCode",
                table: "Actions",
                newName: "PermissionRoute");

            migrationBuilder.AddColumn<string>(
                name: "PermissDecs",
                table: "Actions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermissionApi",
                table: "Actions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PermissDecs",
                table: "Actions");

            migrationBuilder.DropColumn(
                name: "PermissionApi",
                table: "Actions");

            migrationBuilder.RenameColumn(
                name: "PermissionRoute",
                table: "Actions",
                newName: "PermissionCode");
        }
    }
}
