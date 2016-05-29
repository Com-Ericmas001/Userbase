ALTER TABLE [dbo].[UserTokens] DROP CONSTRAINT [FK_UserTokens_Users]
GO
ALTER TABLE [dbo].[UserSettings] DROP CONSTRAINT [FK_UserSettings_Users]
GO
ALTER TABLE [dbo].[UserSettings] DROP CONSTRAINT [FK_UserSettings_UserAccessTypes]
GO
ALTER TABLE [dbo].[UserRelations] DROP CONSTRAINT [FK_UserRelations_Users1]
GO
ALTER TABLE [dbo].[UserRelations] DROP CONSTRAINT [FK_UserRelations_Users]
GO
ALTER TABLE [dbo].[UserRelations] DROP CONSTRAINT [FK_UserRelations_UserRelationTypes]
GO
ALTER TABLE [dbo].[UserRecoveryTokens] DROP CONSTRAINT [FK_UserRecoveryTokens_Users]
GO
ALTER TABLE [dbo].[UserProfiles] DROP CONSTRAINT [FK_UserProfiles_Users]
GO
ALTER TABLE [dbo].[UserAuthentications] DROP CONSTRAINT [FK_UserAuthentications_Users]
GO
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [DF_Users_Active]
GO
/****** Object:  Table [dbo].[UserTokens]    Script Date: 2016-05-29 09:34:44 ******/
DROP TABLE [dbo].[UserTokens]
GO
/****** Object:  Table [dbo].[UserSettings]    Script Date: 2016-05-29 09:34:44 ******/
DROP TABLE [dbo].[UserSettings]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2016-05-29 09:34:44 ******/
DROP TABLE [dbo].[Users]
GO
/****** Object:  Table [dbo].[UserRelationTypes]    Script Date: 2016-05-29 09:34:44 ******/
DROP TABLE [dbo].[UserRelationTypes]
GO
/****** Object:  Table [dbo].[UserRelations]    Script Date: 2016-05-29 09:34:44 ******/
DROP TABLE [dbo].[UserRelations]
GO
/****** Object:  Table [dbo].[UserRecoveryTokens]    Script Date: 2016-05-29 09:34:45 ******/
DROP TABLE [dbo].[UserRecoveryTokens]
GO
/****** Object:  Table [dbo].[UserProfiles]    Script Date: 2016-05-29 09:34:45 ******/
DROP TABLE [dbo].[UserProfiles]
GO
/****** Object:  Table [dbo].[UserAuthentications]    Script Date: 2016-05-29 09:34:45 ******/
DROP TABLE [dbo].[UserAuthentications]
GO
/****** Object:  Table [dbo].[UserAccessTypes]    Script Date: 2016-05-29 09:34:45 ******/
DROP TABLE [dbo].[UserAccessTypes]
GO