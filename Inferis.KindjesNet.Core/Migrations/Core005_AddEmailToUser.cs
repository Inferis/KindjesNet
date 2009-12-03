using System;
using System.Data;
using Migrator.Framework;

namespace Inferis.KindjesNet.Core.Migrations
{
    [Migration(005)]
    public class Core005_AddEmailToUser : Migration
    {
        public override void Up()
        {
            Database.AddColumn("[User]", "Email", DbType.String, 200, ColumnProperty.NotNull, "'invalid@example.com'");
        }

        public override void Down()
        {
            Database.RemoveColumn("[User]", "Email");
        }
    }
}