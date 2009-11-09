/****** Object:  Table [dbo].[User]    Script Date: 11/07/2009 14:00:32 ******/
SET ANSI_NULLS ON
/*GO*/
SET QUOTED_IDENTIFIER ON
/*GO*/
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[User](
    [UserId] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
    [Nick] [nvarchar](100) NULL,
    [Name] [nvarchar](100) NULL,
    [LegacyId] [int] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
    [UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/*GO*/
/****** Object:  Table [dbo].[Kid]    Script Date: 11/07/2009 14:00:32 ******/
SET ANSI_NULLS ON
/*GO*/
SET QUOTED_IDENTIFIER ON
/*GO*/
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Kid]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Kid](
    [KidId] [uniqueidentifier] NOT NULL,
    [Name] [nvarchar](50) NULL,
    [Tag] [nvarchar](50) NULL,
    [BirthDate] [datetime] NULL,
 CONSTRAINT [PK_Kid] PRIMARY KEY CLUSTERED 
(
    [KidId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
/*GO*/
/****** Object:  Default [DF_Kid_KidId]    Script Date: 11/07/2009 14:00:32 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Kid_KidId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Kid]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Kid_KidId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Kid] ADD  CONSTRAINT [DF_Kid_KidId]  DEFAULT (newid()) FOR [KidId]
END


End
/*GO*/
/****** Object:  Default [DF_User_UserId]    Script Date: 11/07/2009 14:00:32 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_User_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[User]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_User_UserId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_UserId]  DEFAULT (newid()) FOR [UserId]
END


End
/*GO*/
