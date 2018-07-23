using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ShareYunSourse.EFCore.Migrations
{
    public partial class roleSourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "YunSourse",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SourseID",
                table: "Role",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_YunSourse_RoleId",
                table: "YunSourse",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_YunSourse_Role_RoleId",
                table: "YunSourse",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_YunSourse_Role_RoleId",
                table: "YunSourse");

            migrationBuilder.DropIndex(
                name: "IX_YunSourse_RoleId",
                table: "YunSourse");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "YunSourse");

            migrationBuilder.DropColumn(
                name: "SourseID",
                table: "Role");
        }
    }
}
