ALTER TABLE [ericmas001].[Sets] DROP CONSTRAINT [FK_Sets_SetCategories]
GO
ALTER TABLE [ericmas001].[SetCategories] DROP CONSTRAINT [FK_SetCategories_CollectionTypes]
GO
ALTER TABLE [Cards] DROP CONSTRAINT [FK_Cards_Sets]
GO
ALTER TABLE [CardAlternateNames] DROP CONSTRAINT [FK_CardAlternateNames_Cards]
GO
/****** Object:  Table [StagingCardsToModify]   Script Date: 2015-12-12 09:53:45 ******/
DROP TABLE [StagingCardsToModify]
GO
/****** Object:  Table [Sets]    Script Date: 2015-12-12 09:53:45 ******/
DROP TABLE [ericmas001].[Sets]
GO
/****** Object:  Table [ericmas001].[SetCategories]    Script Date: 2015-12-12 09:53:46 ******/
DROP TABLE [ericmas001].[SetCategories]
GO
/****** Object:  Table [ericmas001].[CollectionTypes]    Script Date: 2015-12-12 09:53:45 ******/
DROP TABLE [ericmas001].[CollectionTypes]
GO
/****** Object:  Table [ericmas001].[Cards]    Script Date: 2015-12-12 09:53:46 ******/
DROP TABLE [ericmas001].[Cards]
GO
/****** Object:  Table [ericmas001].[CardAlternateNames]    Script Date: 2015-12-12 09:53:46 ******/
DROP TABLE [ericmas001].[CardAlternateNames]
GO