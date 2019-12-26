using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaPlannerCore.Data.Migrations
{
    public partial class InitialTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    ClientId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    CountryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "MediaChannel",
                columns: table => new
                {
                    MediaChannelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MediaChannelName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaChannel", x => x.MediaChannelId);
                });

            migrationBuilder.CreateTable(
                name: "Campaign",
                columns: table => new
                {
                    CampaignId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CampaignName = table.Column<string>(nullable: true),
                    ClientId = table.Column<int>(nullable: true),
                    CountryId = table.Column<int>(nullable: true),
                    StartDateTime = table.Column<DateTime>(nullable: true),
                    EndDateTime = table.Column<DateTime>(nullable: true),
                    Budget = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaign", x => x.CampaignId);
                    table.ForeignKey(
                        name: "FK_Campaign_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Campaign_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    SupplierId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierName = table.Column<string>(nullable: true),
                    MediaChannelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.SupplierId);
                    table.ForeignKey(
                        name: "FK_Supplier_MediaChannel_MediaChannelId",
                        column: x => x.MediaChannelId,
                        principalTable: "MediaChannel",
                        principalColumn: "MediaChannelId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdSet",
                columns: table => new
                {
                    AdSetId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CampaignId = table.Column<int>(nullable: true),
                    MediaChannelId = table.Column<int>(nullable: true),
                    SupplierId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdSet", x => x.AdSetId);
                    table.ForeignKey(
                        name: "FK_AdSet_Campaign_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaign",
                        principalColumn: "CampaignId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdSet_MediaChannel_MediaChannelId",
                        column: x => x.MediaChannelId,
                        principalTable: "MediaChannel",
                        principalColumn: "MediaChannelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdSet_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ad",
                columns: table => new
                {
                    AdId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdTitle = table.Column<string>(nullable: true),
                    CampaignId = table.Column<int>(nullable: true),
                    AdStartDateTime = table.Column<DateTime>(nullable: true),
                    AdEndDateTime = table.Column<DateTime>(nullable: true),
                    RedirectUrl = table.Column<string>(nullable: true),
                    AdImage = table.Column<string>(nullable: true),
                    AdSetId = table.Column<int>(nullable: true),
                    AdContent = table.Column<string>(nullable: true),
                    AdBudget = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ad", x => x.AdId);
                    table.ForeignKey(
                        name: "FK_Ad_AdSet_AdSetId",
                        column: x => x.AdSetId,
                        principalTable: "AdSet",
                        principalColumn: "AdSetId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ad_Campaign_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaign",
                        principalColumn: "CampaignId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ad_AdSetId",
                table: "Ad",
                column: "AdSetId",
                unique: true,
                filter: "[AdSetId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Ad_CampaignId",
                table: "Ad",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_AdSet_CampaignId",
                table: "AdSet",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_AdSet_MediaChannelId",
                table: "AdSet",
                column: "MediaChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_AdSet_SupplierId",
                table: "AdSet",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Campaign_ClientId",
                table: "Campaign",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Campaign_CountryId",
                table: "Campaign",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_MediaChannelId",
                table: "Supplier",
                column: "MediaChannelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ad");

            migrationBuilder.DropTable(
                name: "AdSet");

            migrationBuilder.DropTable(
                name: "Campaign");

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "MediaChannel");
        }
    }
}
