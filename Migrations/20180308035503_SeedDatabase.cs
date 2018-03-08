using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Fire.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES ('Make1')");
            migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES ('Make2')");
            migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES ('Make3')");

            // migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES ('Make1-ModelA',1)");
            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES ('Make1-ModelA',(SELECT Id FROM Makes WHERE Name = 'Make1'))");
            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES ('Make1-ModelB',(SELECT Id FROM Makes WHERE Name = 'Make1'))");
            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES ('Make1-ModelC',(SELECT Id FROM Makes WHERE Name = 'Make1'))");

            // migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES ('Make1-ModelA',2)");
            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES ('Make1-ModelA',(SELECT Id FROM Makes WHERE Name = 'Make2'))");
            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES ('Make1-ModelB',(SELECT Id FROM Makes WHERE Name = 'Make2'))");
            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES ('Make1-ModelC',(SELECT Id FROM Makes WHERE Name = 'Make2'))");

            // migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES ('Make1-ModelA',3)");
            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES ('Make1-ModelA',(SELECT Id FROM Makes WHERE Name = 'Make3'))");
            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES ('Make1-ModelB',(SELECT Id FROM Makes WHERE Name = 'Make3'))");
            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES ('Make1-ModelC',(SELECT Id FROM Makes WHERE Name = 'Make3'))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Makes WHERE Name IN ('Make1','Make2','Make3')");
        }
    }
}
