ALTER TABLE [UserRecoveryTokens] DROP CONSTRAINT [FK_UserRecoveryTokens_Users]
GO
ALTER TABLE [UserTokens] DROP CONSTRAINT [FK_UserTokens_Users]
GO
ALTER TABLE [UserRelations] DROP CONSTRAINT [FK_UserRelations_Users1]
GO
ALTER TABLE [UserRelations] DROP CONSTRAINT [FK_UserRelations_Users]
GO
ALTER TABLE [UserRelations] DROP CONSTRAINT [FK_UserRelations_UserRelationTypes]
GO
ALTER TABLE [UserProfiles] DROP CONSTRAINT [FK_UserProfiles_Users]
GO
ALTER TABLE [UserAuthentications] DROP CONSTRAINT [FK_UserAuthentications_Users]
GO
/****** Object:  Table [UserRecoveryTokens]    Script Date: 2015-12-12 09:53:44 ******/
DROP TABLE UserRecoveryTokens
GO
/****** Object:  Table [UserTokens]    Script Date: 2015-12-12 09:53:44 ******/
DROP TABLE [UserTokens]
GO
/****** Object:  Table [Users]    Script Date: 2015-12-12 09:53:44 ******/
DROP TABLE [Users]
GO
/****** Object:  Table [RelationTypes]    Script Date: 2015-12-12 09:53:45 ******/
DROP TABLE [UserRelationTypes]
GO
/****** Object:  Table [Relations]    Script Date: 2015-12-12 09:53:45 ******/
DROP TABLE [UserRelations]
GO
/****** Object:  Table [UserProfiles]    Script Date: 2015-12-12 09:53:44 ******/
DROP TABLE [UserProfiles]
GO
/****** Object:  Table [UserAuthentications]    Script Date: 2015-12-12 09:53:45 ******/
DROP TABLE [UserAuthentications]
GO