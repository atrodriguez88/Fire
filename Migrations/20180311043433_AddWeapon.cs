using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Fire.Migrations
{
    public partial class AddWeapon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Weapons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContactEmail = table.Column<string>(maxLength: 255, nullable: true),
                    ContactName = table.Column<string>(maxLength: 255, nullable: false),
                    ContactPhone = table.Column<string>(maxLength: 255, nullable: false),
                    IsRegistered = table.Column<bool>(nullable: false),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    ModelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weapons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Weapons_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeaponFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FeatureId = table.Column<int>(nullable: false),
                    WeaponId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeaponFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeaponFeatures_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WeaponFeatures_Weapons_WeaponId",
                        column: x => x.WeaponId,
                        principalTable: "Weapons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeaponFeatures_FeatureId",
                table: "WeaponFeatures",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_WeaponFeatures_WeaponId",
                table: "WeaponFeatures",
                column: "WeaponId");

            migrationBuilder.CreateIndex(
                name: "IX_Weapons_ModelId",
                table: "Weapons",
                column: "ModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeaponFeatures");

            migrationBuilder.DropTable(
                name: "Weapons");
        }
    }
}
