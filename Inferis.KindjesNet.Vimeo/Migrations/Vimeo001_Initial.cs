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
            Database.ExecuteNonQuery(@"
                /****** Object:  Table [dbo].[VimeoVideo]    Script Date: 11/07/2009 14:18:15 ******/
                SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO
                IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VimeoVideo]') AND type in (N'U'))
                BEGIN
                CREATE TABLE [dbo].[VimeoVideo](
	                [VimeoVideoId] [uniqueidentifier] NOT NULL,
	                [Slug] [nvarchar](250) NULL,
	                [Title] [nvarchar](250) NULL,
	                [Caption] [ntext] NULL,
	                [UploadDate] [datetime] NULL,
	                [Duration] [int] NULL,
	                [Width] [int] NULL,
	                [Height] [int] NULL,
	                [VimeoId] [nvarchar](50) NULL,
	                [VimeoOwnerId] [nvarchar](50) NULL,
	                [Privacy] [nvarchar](50) NULL,
	                [IsHighDefinition] [bit] NULL,
	                [VimeoPageUrl] [nvarchar](250) NULL,
                 CONSTRAINT [PK_VimeoVideo] PRIMARY KEY CLUSTERED 
                (
	                [VimeoVideoId] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
                END
                GO
            ");
        }

        public override void Down()
        {
            Database.RemoveTable("VimeoVideo");
        }
    }
}