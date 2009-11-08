using System.Data;
using Inferis.Core.Migrations;
using Migrator.Framework;

namespace Inferis.KindjesNet.Vimeo.Migrations
{
    [Migration(VimeoMigrationsProvider.Context, 001)]
    public class Vimeo001_Initial : Migration
    {
        public override void Up()
        {
            Database.ExecuteScript(@"Migrations\Scripts", @"Vimeo001_Initial.sql");
        }

        public override void Down()
        {
            Database.RemoveTable("VimeoVideo");
        }
    }
}