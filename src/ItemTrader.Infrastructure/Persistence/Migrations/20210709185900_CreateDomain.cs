using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ItemTrader.Infrastructure.Persistence.Migrations
{
    public partial class CreateDomain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TradeItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TradeItems_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Proposals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    ProposedToId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    OfferedItemId = table.Column<int>(type: "int", nullable: false),
                    RequestedItemId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proposals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proposals_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Proposals_AspNetUsers_ProposedToId",
                        column: x => x.ProposedToId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Proposals_TradeItems_OfferedItemId",
                        column: x => x.OfferedItemId,
                        principalTable: "TradeItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Proposals_TradeItems_RequestedItemId",
                        column: x => x.RequestedItemId,
                        principalTable: "TradeItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_OfferedItemId",
                table: "Proposals",
                column: "OfferedItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_OwnerId",
                table: "Proposals",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_ProposedToId",
                table: "Proposals",
                column: "ProposedToId");

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_RequestedItemId",
                table: "Proposals",
                column: "RequestedItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeItems_OwnerId",
                table: "TradeItems",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Proposals");

            migrationBuilder.DropTable(
                name: "TradeItems");
        }
    }
}
