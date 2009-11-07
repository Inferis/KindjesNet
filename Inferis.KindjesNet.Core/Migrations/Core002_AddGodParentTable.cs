using System;
using System.Data;
using Migrator.Framework;

namespace Inferis.KindjesNet.Core.Migrations
{
    [Migration(002)]
    public class Core002_AddGodParentTable : Migration
    {
        public override void Up()
        {
            Database.AddTable("GodParent",
                new Column("GodParentId", DbType.Guid, ColumnProperty.PrimaryKey, Guid.NewGuid()),
                new Column("Name", DbType.String, 100, ColumnProperty.NotNull),
                new Column("Info", DbType.String, -1, ColumnProperty.Null),
                new Column("PictureUrl", DbType.String, 250, ColumnProperty.Null),
                new Column("UserId", DbType.Guid, ColumnProperty.ForeignKey | ColumnProperty.Null)
                );
            Database.AddForeignKey("FK_GodParent_User_UserId", "GodParent", "UserId", "User", "UserId");

            Database.AddTable("KidGodParent",
                new Column("KidId", DbType.Guid, ColumnProperty.PrimaryKey | ColumnProperty.ForeignKey),
                new Column("GodParentId", DbType.Guid, ColumnProperty.PrimaryKey | ColumnProperty.ForeignKey)
            );
            Database.AddForeignKey("FK_KidGodParent_Kid_KidId", "KidGodParent", "KidId", "Kid", "KidId");
            Database.AddForeignKey("FK_KidGodParent_GodParent_GodParentId", "KidGodParent", "GodParentId", "GodParent", "GodParentId");
        }

        public override void Down()
        {
            Database.RemoveForeignKey("KidGodParent", "FK_KidGodParent_Kid_KidId");
            Database.RemoveForeignKey("KidGodParent", "FK_KidGodParent_GodParent_GodParentId");
            Database.RemoveTable("KidGodParent");

            Database.RemoveForeignKey("KidGodParent", "FK_GodParent_User_UserId");
            Database.RemoveTable("GodParent");
        }
    }
}