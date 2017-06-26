/****** Object:  Table [dbo].[UserAccessTypes]    Script Date: 2016-05-29 09:34:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAccessTypes](
	[IdUserAccessType] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Value] [int] NOT NULL,
 CONSTRAINT [PK_UserAccessTypes] PRIMARY KEY CLUSTERED 
(
	[IdUserAccessType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[UserAuthentications]    Script Date: 2016-05-29 09:34:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAuthentications](
	[IdUser] [int] UNIQUE NOT NULL,
	[Password] [nchar](60) NOT NULL,
	[RecoveryEmail] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_UserAuthentications] PRIMARY KEY CLUSTERED 
(
	[IdUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserGroups]    Script Date: 2016-05-29 09:34:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserGroups](
	[IdUserGroup] [int] IDENTITY(1,1) NOT NULL,
	[IdUser] [int] NOT NULL,
	[IdUserGroupType] [int] NOT NULL,
 CONSTRAINT [PK_UserGroup] PRIMARY KEY CLUSTERED 
(
	[IdUserGroup] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[UserGroupTypes]    Script Date: 2016-05-29 09:34:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].UserGroupTypes(
	[IdUserGroupType] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL
 CONSTRAINT [PK_UserGroupTypes] PRIMARY KEY CLUSTERED 
(
	[IdUserGroupType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[UserProfiles]    Script Date: 2016-05-29 09:34:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProfiles](
	[IdUser] [int] UNIQUE NOT NULL,
	[DisplayName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_UserProfiles] PRIMARY KEY CLUSTERED 
(
	[IdUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserRecoveryTokens]    Script Date: 2016-05-29 09:34:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRecoveryTokens](
	[IdUserRecoveryToken] [int] IDENTITY(1,1) NOT NULL,
	[IdUser] [int] NOT NULL,
	[Token] [uniqueidentifier] NOT NULL,
	[Expiration] [datetime] NOT NULL,
 CONSTRAINT [PK_UserRecoveryTokens] PRIMARY KEY CLUSTERED 
(
	[IdUserRecoveryToken] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[UserRelations]    Script Date: 2016-05-29 09:34:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRelations](
	[IdUserRelation] [int] IDENTITY(1,1) NOT NULL,
	[IdUserOwner] [int] NOT NULL,
	[IdUserLinked] [int] NOT NULL,
	[IdUserRelationType] [int] NOT NULL,
 CONSTRAINT [PK_UserRelations] PRIMARY KEY CLUSTERED 
(
	[IdUserRelation] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[UserRelationTypes]    Script Date: 2016-05-29 09:34:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRelationTypes](
	[IdUserRelationType] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_UserRelationTypes] PRIMARY KEY CLUSTERED 
(
	[IdUserRelationType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[Users]    Script Date: 2016-05-29 09:34:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[IdUser] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[IdUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[UserSettings]    Script Date: 2016-05-29 09:34:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserSettings](
	[IdUser] [int] UNIQUE NOT NULL,
	[IdUserAccessTypeListFriends] [int] NOT NULL,
 CONSTRAINT [PK_UserSettings] PRIMARY KEY CLUSTERED 
(
	[IdUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserTokens]    Script Date: 2016-05-29 09:34:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTokens](
	[IdUserToken] [int] IDENTITY(1,1) NOT NULL,
	[IdUser] [int] NOT NULL,
	[Token] [uniqueidentifier] NOT NULL,
	[Expiration] [datetime] NOT NULL,
 CONSTRAINT [PK_UserTokens] PRIMARY KEY CLUSTERED 
(
	[IdUserToken] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET IDENTITY_INSERT [dbo].[UserAccessTypes] ON 

GO
INSERT [dbo].[UserAccessTypes] ([IdUserAccessType], [Name], [Value]) VALUES (1, N'Everybody', 10)
GO
INSERT [dbo].[UserAccessTypes] ([IdUserAccessType], [Name], [Value]) VALUES (2, N'EverybodyNotBlocked', 20)
GO
INSERT [dbo].[UserAccessTypes] ([IdUserAccessType], [Name], [Value]) VALUES (3, N'Friends', 30)
GO
INSERT [dbo].[UserAccessTypes] ([IdUserAccessType], [Name], [Value]) VALUES (4, N'JustMe', 40)
GO
SET IDENTITY_INSERT [dbo].[UserAccessTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[UserRelationTypes] ON 

GO
INSERT [dbo].[UserRelationTypes] ([IdUserRelationType], [Name]) VALUES (1, N'Friend')
GO
INSERT [dbo].[UserRelationTypes] ([IdUserRelationType], [Name]) VALUES (2, N'Blocked')
GO
SET IDENTITY_INSERT [dbo].[UserRelationTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[UserGroupTypes] ON 

GO
INSERT [dbo].[UserGroupTypes] ([IdUserGroupType], [Name]) VALUES (1, N'Admin')
GO
SET IDENTITY_INSERT [dbo].[UserGroupTypes] OFF
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[UserAuthentications]  WITH CHECK ADD  CONSTRAINT [FK_UserAuthentications_Users] FOREIGN KEY([IdUser])
REFERENCES [dbo].[Users] ([IdUser])
GO
ALTER TABLE [dbo].[UserAuthentications] CHECK CONSTRAINT [FK_UserAuthentications_Users]
GO
ALTER TABLE [dbo].[UserGroups]  WITH CHECK ADD  CONSTRAINT [FK_UserGroups_UserGroupTypes] FOREIGN KEY([IdUserGroupType])
REFERENCES [dbo].[UserGroupTypes] ([IdUserGroupType])
GO
ALTER TABLE [dbo].[UserGroups] CHECK CONSTRAINT [FK_UserGroups_UserGroupTypes]
GO
ALTER TABLE [dbo].[UserGroups]  WITH CHECK ADD  CONSTRAINT [FK_UserGroups_Users] FOREIGN KEY([IdUser])
REFERENCES [dbo].[Users] ([IdUser])
GO
ALTER TABLE [dbo].[UserGroups] CHECK CONSTRAINT [FK_UserGroups_Users]
GO
ALTER TABLE [dbo].[UserProfiles]  WITH CHECK ADD  CONSTRAINT [FK_UserProfiles_Users] FOREIGN KEY([IdUser])
REFERENCES [dbo].[Users] ([IdUser])
GO
ALTER TABLE [dbo].[UserProfiles] CHECK CONSTRAINT [FK_UserProfiles_Users]
GO
ALTER TABLE [dbo].[UserRecoveryTokens]  WITH CHECK ADD  CONSTRAINT [FK_UserRecoveryTokens_Users] FOREIGN KEY([IdUser])
REFERENCES [dbo].[Users] ([IdUser])
GO
ALTER TABLE [dbo].[UserRecoveryTokens] CHECK CONSTRAINT [FK_UserRecoveryTokens_Users]
GO
ALTER TABLE [dbo].[UserRelations]  WITH CHECK ADD  CONSTRAINT [FK_UserRelations_UserRelationTypes] FOREIGN KEY([IdUserRelationType])
REFERENCES [dbo].[UserRelationTypes] ([IdUserRelationType])
GO
ALTER TABLE [dbo].[UserRelations] CHECK CONSTRAINT [FK_UserRelations_UserRelationTypes]
GO
ALTER TABLE [dbo].[UserRelations]  WITH CHECK ADD  CONSTRAINT [FK_UserRelations_Users] FOREIGN KEY([IdUserOwner])
REFERENCES [dbo].[Users] ([IdUser])
GO
ALTER TABLE [dbo].[UserRelations] CHECK CONSTRAINT [FK_UserRelations_Users]
GO
ALTER TABLE [dbo].[UserRelations]  WITH CHECK ADD  CONSTRAINT [FK_UserRelations_Users1] FOREIGN KEY([IdUserLinked])
REFERENCES [dbo].[Users] ([IdUser])
GO
ALTER TABLE [dbo].[UserRelations] CHECK CONSTRAINT [FK_UserRelations_Users1]
GO
ALTER TABLE [dbo].[UserSettings]  WITH CHECK ADD  CONSTRAINT [FK_UserSettings_UserAccessTypes] FOREIGN KEY([IdUserAccessTypeListFriends])
REFERENCES [dbo].[UserAccessTypes] ([IdUserAccessType])
GO
ALTER TABLE [dbo].[UserSettings] CHECK CONSTRAINT [FK_UserSettings_UserAccessTypes]
GO
ALTER TABLE [dbo].[UserSettings]  WITH CHECK ADD  CONSTRAINT [FK_UserSettings_Users] FOREIGN KEY([IdUser])
REFERENCES [dbo].[Users] ([IdUser])
GO
ALTER TABLE [dbo].[UserSettings] CHECK CONSTRAINT [FK_UserSettings_Users]
GO
ALTER TABLE [dbo].[UserTokens]  WITH CHECK ADD  CONSTRAINT [FK_UserTokens_Users] FOREIGN KEY([IdUser])
REFERENCES [dbo].[Users] ([IdUser])
GO
ALTER TABLE [dbo].[UserTokens] CHECK CONSTRAINT [FK_UserTokens_Users]
GO
