using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace IRS.Model.Migrations
{
    public partial class UpdatePermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PermissDecs",
                table: "Actions",
                newName: "PermissionDecs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PermissionDecs",
                table: "Actions",
                newName: "PermissDecs");
        }
    }
}
