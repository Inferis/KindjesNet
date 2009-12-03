using System;
using System.Data;
using Migrator.Framework;

namespace Inferis.KindjesNet.Core.Migrations
{
    [Migration(006)]
    public class Core006_AddExternalUserReferencesTable : Migration
    {
        public override void Up()
        {
            Database.AddTable("ExternalUserReference",
                new Column("ExternalUserReferenceId", DbType.Guid, ColumnProperty.PrimaryKey),
                new Column("UserId", DbType.Guid, ColumnProperty.NotNull),
                new Column("[Type]", DbType.String, 50, ColumnProperty.NotNull),
                new Column("[Value]", DbType.String, 100, ColumnProperty.NotNull)
            );

            Database.AddForeignKey("FK_ExternalUserReference_UserId", "ExternalUserReference", "UserId", "[User]", "UserId");
        }

        public override void Down()
        {
            Database.RemoveForeignKey("FK_ExternalUserReference_UserId", "ExternalUserReference");
            Database.RemoveTable("ExternalUserReference");
        }
    }
}