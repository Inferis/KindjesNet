using Migrator.Framework;
using Inferis.Core.Migrations;

namespace Inferis.KindjesNet.Blog.Migrations
{
    [Migration(BlogMigrationsProvider.Context, 001)]
    public class Blog001_Initial : Migration
    {
        public override void Up()
        {
            Database.ExecuteScript(@"Migrations\Scripts", @"Blog001_Initial.sql");
        }

        public override void Down()
        {
            Database.RemoveForeignKey("PostKid", "FK_PostKid_Post");
            Database.RemoveForeignKey("PostKid", "FK_PostKid_Kid");
            Database.RemoveTable("Post");
            Database.RemoveTable("Post");
            Database.RemoveTable("PostKid");
        }
    }
}
