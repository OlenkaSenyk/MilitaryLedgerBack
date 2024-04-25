﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    MaritalStatus = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Education = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublicSpecialty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Workplace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TRSSC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DischargeDate = table.Column<DateOnly>(type: "date", nullable: true),
                    DischargeReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Signature = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    NeedMMC = table.Column<bool>(type: "bit", nullable: false),
                    LastMMC = table.Column<DateOnly>(type: "date", nullable: true),
                    Fine = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Region = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    House = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Entrance = table.Column<int>(type: "int", maxLength: 10, nullable: true),
                    Apartment = table.Column<int>(type: "int", maxLength: 10, nullable: true),
                    ResidenceOrRegistration = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_PersonId",
                table: "Addresses",
                column: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
