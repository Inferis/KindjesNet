﻿using System;
using System.Data;
using Migrator.Framework;

namespace Inferis.KindjesNet.Core.Migrations
{
    [Migration(004)]
    public class Core004_AddSettingsTable : Migration
    {
        public override void Up()
        {
            Database.AddTable("Settings",
                new Column("Key", DbType.String, 50, ColumnProperty.PrimaryKey),
                new Column("Value", DbType.String, 2000, ColumnProperty.Null)
            );
        }

        public override void Down()
        {
            Database.RemoveTable("Settings");
        }
    }
}