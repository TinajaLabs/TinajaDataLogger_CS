USE [master]
GO

/****** Object:  Database [tinaja]    Script Date: 03/16/2014 21:11:10 ******/
CREATE DATABASE [tinaja] ON  PRIMARY 
( NAME = N'tinaja', FILENAME = N'E:\_Data\MSSQL\MSSQL10.MSSQLSERVER\MSSQL\DATA\tinaja.mdf' , SIZE = 20736KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'tinaja_log', FILENAME = N'E:\_Data\MSSQL\MSSQL10.MSSQLSERVER\MSSQL\DATA\tinaja_log.LDF' , SIZE = 18240KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [tinaja] SET COMPATIBILITY_LEVEL = 100
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [tinaja].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [tinaja] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [tinaja] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [tinaja] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [tinaja] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [tinaja] SET ARITHABORT OFF 
GO

ALTER DATABASE [tinaja] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [tinaja] SET AUTO_CREATE_STATISTICS ON 
GO

ALTER DATABASE [tinaja] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [tinaja] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [tinaja] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [tinaja] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [tinaja] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [tinaja] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [tinaja] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [tinaja] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [tinaja] SET  ENABLE_BROKER 
GO

ALTER DATABASE [tinaja] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [tinaja] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [tinaja] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [tinaja] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [tinaja] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [tinaja] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [tinaja] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [tinaja] SET  READ_WRITE 
GO

ALTER DATABASE [tinaja] SET RECOVERY FULL 
GO

ALTER DATABASE [tinaja] SET  MULTI_USER 
GO

ALTER DATABASE [tinaja] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [tinaja] SET DB_CHAINING OFF 
GO




USE [tinaja]
GO

/****** Object:  Table [dbo].[datalog]    Script Date: 03/16/2014 23:11:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[datalog](
    [id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
    [logtime] [datetime2](7) NULL,
    [logvalue] [decimal](10, 4) NULL,
    [apikey] [nvarchar](50) NULL,
 CONSTRAINT [PK_datalog] PRIMARY KEY CLUSTERED 
(
    [id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The time of the log entry' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'datalog', @level2type=N'COLUMN',@level2name=N'logtime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The value of the log entry' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'datalog', @level2type=N'COLUMN',@level2name=N'logvalue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The apikey associated with the record' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'datalog', @level2type=N'COLUMN',@level2name=N'apikey'
GO
/****** Object:  Default [DF_datalog_id]    Script Date: 03/16/2014 23:11:44 ******/
ALTER TABLE [dbo].[datalog] ADD  CONSTRAINT [DF_datalog_id]  DEFAULT (newid()) FOR [id]
GO
/****** Object:  Default [DF_datalog_logtime]    Script Date: 03/16/2014 23:11:44 ******/
ALTER TABLE [dbo].[datalog] ADD  CONSTRAINT [DF_datalog_logtime]  DEFAULT (getdate()) FOR [logtime]
GO





