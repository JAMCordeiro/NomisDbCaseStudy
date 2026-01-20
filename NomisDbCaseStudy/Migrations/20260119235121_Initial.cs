using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NomisDbCaseStudy.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Body",
                columns: table => new
                {
                    IDBody = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BodyGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Body", x => x.IDBody);
                    table.UniqueConstraint("AK_Body_BodyGUID", x => x.BodyGUID);
                });

            migrationBuilder.CreateTable(
                name: "BodyTable",
                columns: table => new
                {
                    IDBodyTable = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TableName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyTable", x => x.IDBodyTable);
                    table.UniqueConstraint("AK_BodyTable_TableGUID", x => x.TableGUID);
                });

            migrationBuilder.CreateTable(
                name: "Element",
                columns: table => new
                {
                    IDElement = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TableName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Element", x => x.IDElement);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    IDPerson = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.IDPerson);
                    table.UniqueConstraint("AK_Person_PersonGUID", x => x.PersonGUID);
                });

            migrationBuilder.CreateTable(
                name: "PersonInformation_11111111111111111111111111111111",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BodyGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonInformation_11111111111111111111111111111111", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BodyBodyTable",
                columns: table => new
                {
                    IDBodyBodyTable = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BodyGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TableGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyBodyTable", x => x.IDBodyBodyTable);
                    table.ForeignKey(
                        name: "FK_BodyBodyTable_BodyTable_TableGUID",
                        column: x => x.TableGUID,
                        principalTable: "BodyTable",
                        principalColumn: "TableGUID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BodyBodyTable_Body_BodyGUID",
                        column: x => x.BodyGUID,
                        principalTable: "Body",
                        principalColumn: "BodyGUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonBody",
                columns: table => new
                {
                    IDPersonBody = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BodyGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonBody", x => x.IDPersonBody);
                    table.ForeignKey(
                        name: "FK_PersonBody_Body_BodyGUID",
                        column: x => x.BodyGUID,
                        principalTable: "Body",
                        principalColumn: "BodyGUID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonBody_Person_PersonGUID",
                        column: x => x.PersonGUID,
                        principalTable: "Person",
                        principalColumn: "PersonGUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BodyBodyTable_BodyGUID",
                table: "BodyBodyTable",
                column: "BodyGUID");

            migrationBuilder.CreateIndex(
                name: "IX_BodyBodyTable_TableGUID",
                table: "BodyBodyTable",
                column: "TableGUID");

            migrationBuilder.CreateIndex(
                name: "IX_Element_TableGUID",
                table: "Element",
                column: "TableGUID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonBody_BodyGUID",
                table: "PersonBody",
                column: "BodyGUID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonBody_PersonGUID",
                table: "PersonBody",
                column: "PersonGUID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BodyBodyTable");

            migrationBuilder.DropTable(
                name: "Element");

            migrationBuilder.DropTable(
                name: "PersonBody");

            migrationBuilder.DropTable(
                name: "PersonInformation_11111111111111111111111111111111");

            migrationBuilder.DropTable(
                name: "BodyTable");

            migrationBuilder.DropTable(
                name: "Body");

            migrationBuilder.DropTable(
                name: "Person");
        }
    }
}
