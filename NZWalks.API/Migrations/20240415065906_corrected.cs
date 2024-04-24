using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class corrected : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Walks_Regions_RegionID",
                table: "Walks");

            migrationBuilder.DropColumn(
                name: "RegiodId",
                table: "Walks");

            migrationBuilder.RenameColumn(
                name: "RegionID",
                table: "Walks",
                newName: "RegionId");

            migrationBuilder.RenameIndex(
                name: "IX_Walks_RegionID",
                table: "Walks",
                newName: "IX_Walks_RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Walks_Regions_RegionId",
                table: "Walks",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Walks_Regions_RegionId",
                table: "Walks");

            migrationBuilder.RenameColumn(
                name: "RegionId",
                table: "Walks",
                newName: "RegionID");

            migrationBuilder.RenameIndex(
                name: "IX_Walks_RegionId",
                table: "Walks",
                newName: "IX_Walks_RegionID");

            migrationBuilder.AddColumn<Guid>(
                name: "RegiodId",
                table: "Walks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_Walks_Regions_RegionID",
                table: "Walks",
                column: "RegionID",
                principalTable: "Regions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
