using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomerApp.Infrastructure.Migrations
{
    public partial class LoyaltyDbv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Address_Street = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Address_City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address_PostalCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address_Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AuditCreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AuditCreatedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditCreatedByUsername = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AuditModifiedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditModifiedByUsername = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Details_FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Details_LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Details_Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Details_Phone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contact_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contact_CustomerId",
                table: "Contact",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
