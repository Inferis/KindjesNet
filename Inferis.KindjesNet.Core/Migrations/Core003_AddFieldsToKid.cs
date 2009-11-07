using System;
using System.Data;
using Migrator.Framework;

namespace Inferis.KindjesNet.Core.Migrations
{
    [Migration(003)]
    public class Core003_AddInfoToKid : Migration
    {
        public override void Up()
        {
            Database.AddColumn("Kid", "Info", DbType.String, -1, ColumnProperty.Null);
        }

        public override void Down()
        {
            Database.RemoveColumn("Kid", "Info");
        }
    }
}