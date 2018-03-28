using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace vega.Migrations
{
    public partial class SeedFeatures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('AC')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('ABS')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('2 airbags')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('4 airbags')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('6 o más airbags')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Climatizador')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('GPS')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('ESP')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Radio touchscreen')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.Sql("DELETE FROM Features WHERE Name IN ('Feature1', 'Feature2', 'Feature3')");
        }
    }
}
