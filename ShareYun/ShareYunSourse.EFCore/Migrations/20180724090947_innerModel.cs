using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ShareYunSourse.EFCore.Migrations
{
    public partial class innerModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_YunSourse_Role_RoleId",
                table: "YunSourse");

            migrationBuilder.DropColumn(
                name: "SourseID",
                table: "Role");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "YunSourse",
                newName: "YunSourseId");

            migrationBuilder.RenameIndex(
                name: "IX_YunSourse_RoleId",
                table: "YunSourse",
                newName: "IX_YunSourse_YunSourseId");

            migrationBuilder.AddColumn<int>(
                name: "yunSourseId",
                table: "Role",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Role_yunSourseId",
                table: "Role",
                column: "yunSourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_YunSourse_yunSourseId",
                table: "Role",
                column: "yunSourseId",
                principalTable: "YunSourse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_YunSourse_YunSourse_YunSourseId",
                table: "YunSourse",
                column: "YunSourseId",
                principalTable: "YunSourse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Role_YunSourse_yunSourseId",
                table: "Role");

            migrationBuilder.DropForeignKey(
                name: "FK_YunSourse_YunSourse_YunSourseId",
                table: "YunSourse");

            migrationBuilder.DropIndex(
                name: "IX_Role_yunSourseId",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "yunSourseId",
                table: "Role");

            migrationBuilder.RenameColumn(
                name: "YunSourseId",
                table: "YunSourse",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_YunSourse_YunSourseId",
                table: "YunSourse",
                newName: "IX_YunSourse_RoleId");

            migrationBuilder.AddColumn<int>(
                name: "SourseID",
                table: "Role",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_YunSourse_Role_RoleId",
                table: "YunSourse",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
