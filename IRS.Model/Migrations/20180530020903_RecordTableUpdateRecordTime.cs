using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace IRS.Model.Migrations
{
    public partial class RecordTableUpdateRecordTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FaultCondition",
                table: "Records");

            migrationBuilder.AddColumn<string>(
                name: "RecordTime",
                table: "Records",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecordTime",
                table: "Records");

            migrationBuilder.AddColumn<int>(
                name: "FaultCondition",
                table: "Records",
                nullable: false,
                defaultValue: 0);
        }
    }
}
