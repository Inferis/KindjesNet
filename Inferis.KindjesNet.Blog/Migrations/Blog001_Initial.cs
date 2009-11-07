using System.Data;
using Migrator.Framework;

namespace Inferis.KindjesNet.Blog.Migrations
{
    [Migration(BlogMigrationsProvider.Context, 001)]
    public class Blog001_Initial : Migration
    {
        public override void Up()
        {
            Database.AddTable("TableauTest",
                              new Column("TestId", DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity));
        }

        public override void Down()
        {
            Database.RemoveTable("TableauTest");
        }
    }
}
