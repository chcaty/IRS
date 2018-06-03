using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace IRS.Model.Migrations
{
    public partial class updatedb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RoleDecs",
                table: "Roles",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProcessingPeople",
                table: "ProcessingRecords",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "EndFlag",
                table: "CategoryInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StartFlag",
                table: "CategoryInfos",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleDecs",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "EndFlag",
                table: "CategoryInfos");

            migrationBuilder.DropColumn(
                name: "StartFlag",
                table: "CategoryInfos");

            migrationBuilder.AlterColumn<int>(
                name: "ProcessingPeople",
                table: "ProcessingRecords",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
