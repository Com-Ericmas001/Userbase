/****** Object:  Table [UserAuthentications]    Script Date: 2015-12-12 09:54:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [UserAuthentications](
	[IdUser] [int] UNIQUE NOT NULL,
	[Password] [nchar](60) NOT NULL,
	[RecoveryEmail] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_UserAuthentications] PRIMARY KEY CLUSTERED 
(
	[IdUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [UserProfiles]    Script Date: 2015-12-12 09:54:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [UserProfiles](
	[IdUser] [int] UNIQUE NOT NULL,
	[DisplayName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_UserProfiles] PRIMARY KEY CLUSTERED 
(
	[IdUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Relations]    Script Date: 2015-12-12 09:54:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [UserRelations](
	[IdUserRelation] [int] IDENTITY(1,1) NOT NULL,
	[IdUserOwner] [int] NOT NULL,
	[IdUserLinked] [int] NOT NULL,
	[IdUserRelationType] [int] NOT NULL,
 CONSTRAINT [PK_UserRelations] PRIMARY KEY CLUSTERED 
(
	[IdUserRelation] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [RelationTypes]    Script Date: 2015-12-12 09:54:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [UserRelationTypes](
	[IdUserRelationType] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_UserRelationTypes] PRIMARY KEY CLUSTERED 
(
	[IdUserRelationType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Users]    Script Date: 2015-12-12 09:54:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Users](
	[IdUser] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Active] [bit] NOT NULL CONSTRAINT [DF_Users_Active]  DEFAULT ((1)),
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[IdUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [UserTokens]    Script Date: 2015-12-12 09:54:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [UserTokens](
	[IdUserToken] [int] IDENTITY(1,1) NOT NULL,
	[IdUser] [int] NOT NULL,
	[Token] [uniqueidentifier] NOT NULL,
	[Expiration] [datetime] NOT NULL,
 CONSTRAINT [PK_UserTokens] PRIMARY KEY CLUSTERED 
(
	[IdUserToken] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [UserRecoveryTokens]    Script Date: 2015-12-12 09:54:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [UserRecoveryTokens](
	[IdUserRecoveryToken] [int] IDENTITY(1,1) NOT NULL,
	[IdUser] [int] NOT NULL,
	[Token] [uniqueidentifier] NOT NULL,
	[Expiration] [datetime] NOT NULL,
 CONSTRAINT [PK_UserRecoveryTokens] PRIMARY KEY CLUSTERED 
(
	[IdUserRecoveryToken] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [UserRelationTypes] ON 

GO
INSERT [UserRelationTypes] ([IdUserRelationType], [Name]) VALUES (1, N'Friend')
GO
INSERT [UserRelationTypes] ([IdUserRelationType], [Name]) VALUES (2, N'Blocked')
GO
SET IDENTITY_INSERT [UserRelationTypes] OFF
GO
ALTER TABLE [UserAuthentications]  WITH CHECK ADD  CONSTRAINT [FK_UserAuthentications_Users] FOREIGN KEY([IdUser])
REFERENCES [Users] ([IdUser])
GO
ALTER TABLE [UserAuthentications] CHECK CONSTRAINT [FK_UserAuthentications_Users]
GO
ALTER TABLE [UserProfiles]  WITH CHECK ADD  CONSTRAINT [FK_UserProfiles_Users] FOREIGN KEY([IdUser])
REFERENCES [Users] ([IdUser])
GO
ALTER TABLE [UserProfiles] CHECK CONSTRAINT [FK_UserProfiles_Users]
GO
ALTER TABLE [UserRelations]  WITH CHECK ADD  CONSTRAINT [FK_UserRelations_UserRelationTypes] FOREIGN KEY([IdUserRelationType])
REFERENCES [UserRelationTypes] ([IdUserRelationType])
GO
ALTER TABLE [UserRelations] CHECK CONSTRAINT [FK_UserRelations_UserRelationTypes]
GO
ALTER TABLE [UserRelations]  WITH CHECK ADD  CONSTRAINT [FK_UserRelations_Users] FOREIGN KEY([IdUserOwner])
REFERENCES [Users] ([IdUser])
GO
ALTER TABLE [UserRelations] CHECK CONSTRAINT [FK_UserRelations_Users]
GO
ALTER TABLE [UserRelations]  WITH CHECK ADD  CONSTRAINT [FK_UserRelations_Users1] FOREIGN KEY([IdUserLinked])
REFERENCES [Users] ([IdUser])
GO
ALTER TABLE [UserRelations] CHECK CONSTRAINT [FK_UserRelations_Users1]
GO
ALTER TABLE [UserTokens]  WITH CHECK ADD  CONSTRAINT [FK_UserTokens_Users] FOREIGN KEY([IdUser])
REFERENCES [Users] ([IdUser])
GO
ALTER TABLE [UserTokens] CHECK CONSTRAINT [FK_UserTokens_Users]
GO
ALTER TABLE [UserRecoveryTokens]  WITH CHECK ADD  CONSTRAINT [FK_UserRecoveryTokens_Users] FOREIGN KEY([IdUser])
REFERENCES [Users] ([IdUser])
GO
ALTER TABLE [UserRecoveryTokens] CHECK CONSTRAINT [FK_UserRecoveryTokens_Users]
GO
