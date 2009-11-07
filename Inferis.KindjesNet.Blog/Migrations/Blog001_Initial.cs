using Migrator.Framework;

namespace Inferis.KindjesNet.Blog.Migrations
{
    [Migration(BlogMigrationsProvider.Context, 001)]
    public class Blog001_Initial : Migration
    {
        public override void Up()
        {
            Database.ExecuteNonQuery(@"
                /****** Object:  Table [dbo].[Post]    Script Date: 11/07/2009 14:18:15 ******/
                SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO
                IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Post]') AND type in (N'U'))
                BEGIN
                CREATE TABLE [dbo].[Post](
	                [PostId] [uniqueidentifier] NOT NULL,
	                [Slug] [nvarchar](250) NULL,
	                [Title] [nvarchar](100) NULL,
	                [Body] [ntext] NULL,
	                [PostDate] [datetime] NULL,
	                [AuthorId] [uniqueidentifier] NULL,
	                [LegacyId] [int] NULL,
                 CONSTRAINT [PK_Post] PRIMARY KEY CLUSTERED 
                (
	                [PostId] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
                END
                GO
                /****** Object:  Table [dbo].[PostKid]    Script Date: 11/07/2009 14:18:15 ******/
                SET ANSI_NULLS ON
                GO
                SET QUOTED_IDENTIFIER ON
                GO
                IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PostKid]') AND type in (N'U'))
                BEGIN
                CREATE TABLE [dbo].[PostKid](
	                [PostId] [uniqueidentifier] NOT NULL,
	                [KidId] [uniqueidentifier] NOT NULL,
                 CONSTRAINT [PK_PostKid] PRIMARY KEY CLUSTERED 
                (
	                [PostId] ASC,
	                [KidId] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY]
                END
                GO
                /****** Object:  ForeignKey [FK_PostKid_Kid]    Script Date: 11/07/2009 14:18:15 ******/
                IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PostKid_Kid]') AND parent_object_id = OBJECT_ID(N'[dbo].[PostKid]'))
                ALTER TABLE [dbo].[PostKid]  WITH CHECK ADD  CONSTRAINT [FK_PostKid_Kid] FOREIGN KEY([KidId])
                REFERENCES [dbo].[Kid] ([KidId])
                GO
                IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PostKid_Kid]') AND parent_object_id = OBJECT_ID(N'[dbo].[PostKid]'))
                ALTER TABLE [dbo].[PostKid] CHECK CONSTRAINT [FK_PostKid_Kid]
                GO
                /****** Object:  ForeignKey [FK_PostKid_Post]    Script Date: 11/07/2009 14:18:15 ******/
                IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PostKid_Post]') AND parent_object_id = OBJECT_ID(N'[dbo].[PostKid]'))
                ALTER TABLE [dbo].[PostKid]  WITH CHECK ADD  CONSTRAINT [FK_PostKid_Post] FOREIGN KEY([PostId])
                REFERENCES [dbo].[Post] ([PostId])
                GO
                IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PostKid_Post]') AND parent_object_id = OBJECT_ID(N'[dbo].[PostKid]'))
                ALTER TABLE [dbo].[PostKid] CHECK CONSTRAINT [FK_PostKid_Post]
                GO
            ");
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
