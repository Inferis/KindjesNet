using System.Data;
using Inferis.KindjesNet.Blog.Migrations;
using Migrator.Framework;

namespace Inferis.KindjesNet.Vimeo.Migrations
{
    [Migration(VimeoMigrationsProvider.Context, 001)]
    public class Vimeo001_Initial : Migration
    {
        public override void Up()
        {
            Database.AddTable("TableauTestVimeo",
                              new Column("VimeoId", DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity));
        }

        public override void Down()
        {
            Database.RemoveTable("TableauTestVimeo");
        }
    }
}