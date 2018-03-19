using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace vega.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES ('Nissan')");
            migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES ('Peugeot')");
            migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES ('Toyota')");

            migrationBuilder.Sql("INSERT INTO Models (Name, MakeID) VALUES ('Qashqai',1)");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeID) VALUES ('Terrano',1)");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeID) VALUES ('March',1)");

            migrationBuilder.Sql("INSERT INTO Models (Name, MakeID) VALUES ('208',2)");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeID) VALUES ('308',2)");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeID) VALUES ('508',2)");

            migrationBuilder.Sql("INSERT INTO Models (Name, MakeID) VALUES ('Yaris',3)");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeID) VALUES ('Corolla',3)");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeID) VALUES ('Rav4',3)");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Makes WHERE Name IN ('Nissan','Peugeot','Toyota')");
        }
    }
}
