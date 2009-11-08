using Inferis.Core.Migrations;
using Migrator.Framework;

namespace Inferis.KindjesNet.Core.Migrations
{
    [Migration(001)]
    public class Core001_Initial : Migration
    {
        public override void Up()
        {
            // This creates the User and Kid tables in their current form.
            Database.ExecuteScript(@"Migrations\Scripts", @"Core001_Initial.sql");
        }

        public override void Down()
        {
        }
    }
}