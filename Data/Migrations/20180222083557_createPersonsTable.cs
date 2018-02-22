using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ContactList.Data.Migrations
{
    public partial class createPersonsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhoneNumber",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PersonId = table.Column<Guid>(nullable: false),
                    Phone = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumber", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhoneNumber_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumber_PersonId",
                table: "PhoneNumber",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhoneNumber");
        }
    }
}
