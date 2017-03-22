USE [master]
GO
/****** Object:  Database [C:\USERS\ADMIN\DESKTOP\UPLOAD\SYMPHONY\SYMPHONY\APP_DATA\DATABASE.MDF]    Script Date: 3/23/2017 1:08:39 AM ******/
CREATE DATABASE [C:\USERS\ADMIN\DESKTOP\UPLOAD\SYMPHONY\SYMPHONY\APP_DATA\DATABASE.MDF]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Alvand-Orchestra', FILENAME = N'C:\Users\admin\Desktop\Upload\Symphony\Symphony\App_Data\Database.mdf' , SIZE = 204800KB , MAXSIZE = UNLIMITED, FILEGROWTH = 50%)
 LOG ON 
( NAME = N'Alvand-Orchestra_log', FILENAME = N'C:\Users\admin\Desktop\Upload\Symphony\Symphony\App_Data\Database_log.ldf' , SIZE = 768KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)
GO
ALTER DATABASE [C:\USERS\ADMIN\DESKTOP\UPLOAD\SYMPHONY\SYMPHONY\APP_DATA\DATABASE.MDF] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [C:\USERS\ADMIN\DESKTOP\UPLOAD\SYMPHONY\SYMPHONY\APP_DATA\DATABASE.MDF].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [C:\USERS\ADMIN\DESKTOP\UPLOAD\SYMPHONY\SYMPHONY\APP_DATA\DATABASE.MDF] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [C:\USERS\ADMIN\DESKTOP\UPLOAD\SYMPHONY\SYMPHONY\APP_DATA\DATABASE.MDF] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [C:\USERS\ADMIN\DESKTOP\UPLOAD\SYMPHONY\SYMPHONY\APP_DATA\DATABASE.MDF] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [C:\USERS\ADMIN\DESKTOP\UPLOAD\SYMPHONY\SYMPHONY\APP_DATA\DATABASE.MDF] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [C:\USERS\ADMIN\DESKTOP\UPLOAD\SYMPHONY\SYMPHONY\APP_DATA\DATABASE.MDF] SET ARITHABORT OFF 
GO
ALTER DATABASE [C:\USERS\ADMIN\DESKTOP\UPLOAD\SYMPHONY\SYMPHONY\APP_DATA\DATABASE.MDF] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [C:\USERS\ADMIN\DESKTOP\UPLOAD\SYMPHONY\SYMPHONY\APP_DATA\DATABASE.MDF] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [C:\USERS\ADMIN\DESKTOP\UPLOAD\SYMPHONY\SYMPHONY\APP_DATA\DATABASE.MDF] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [C:\USERS\ADMIN\DESKTOP\UPLOAD\SYMPHONY\SYMPHONY\APP_DATA\DATABASE.MDF] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [C:\USERS\ADMIN\DESKTOP\UPLOAD\SYMPHONY\SYMPHONY\APP_DATA\DATABASE.MDF] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [C:\USERS\ADMIN\DESKTOP\UPLOAD\SYMPHONY\SYMPHONY\APP_DATA\DATABASE.MDF] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [C:\USERS\ADMIN\DESKTOP\UPLOAD\SYMPHONY\SYMPHONY\APP_DATA\DATABASE.MDF] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [C:\USERS\ADMIN\DESKTOP\UPLOAD\SYMPHONY\SYMPHONY\APP_DATA\DATABASE.MDF] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [C:\USERS\ADMIN\DESKTOP\UPLOAD\SYMPHONY\SYMPHONY\APP_DATA\DATABASE.MDF] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [C:\USERS\ADMIN\DESKTOP\UPLOAD\SYMPHONY\SYMPHONY\APP_DATA\DATABASE.MDF] SET  DISABLE_BROKER 
GO
ALTER DATABASE [C:\USERS\ADMIN\DESKTOP\UPLOAD\SYMPHONY\SYMPHONY\APP_DATA\DATABASE.MDF] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [C:\USERS\ADMIN\DESKTOP\UPLOAD\SYMPHONY\SYMPHONY\APP_DATA\DATABASE.MDF] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [C:\USERS\ADMIN\DESKTOP\UPLOAD\SYMPHONY\SYMPHONY\APP_DATA\DATABASE.MDF] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [C:\USERS\ADMIN\DESKTOP\UPLOAD\SYMPHONY\SYMPHONY\APP_DATA\DATABASE.MDF] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [C:\USERS\ADMIN\DESKTOP\UPLOAD\SYMPHONY\SYMPHONY\APP_DATA\DATABASE.MDF] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [C:\USERS\ADMIN\DESKTOP\UPLOAD\SYMPHONY\SYMPHONY\APP_DATA\DATABASE.MDF] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [C:\USERS\ADMIN\DESKTOP\UPLOAD\SYMPHONY\SYMPHONY\APP_DATA\DATABASE.MDF] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [C:\USERS\ADMIN\DESKTOP\UPLOAD\SYMPHONY\SYMPHONY\APP_DATA\DATABASE.MDF] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [C:\USERS\ADMIN\DESKTOP\UPLOAD\SYMPHONY\SYMPHONY\APP_DATA\DATABASE.MDF] SET  MULTI_USER 
GO
ALTER DATABASE [C:\USERS\ADMIN\DESKTOP\UPLOAD\SYMPHONY\SYMPHONY\APP_DATA\DATABASE.MDF] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [C:\USERS\ADMIN\DESKTOP\UPLOAD\SYMPHONY\SYMPHONY\APP_DATA\DATABASE.MDF] SET DB_CHAINING OFF 
GO
ALTER DATABASE [C:\USERS\ADMIN\DESKTOP\UPLOAD\SYMPHONY\SYMPHONY\APP_DATA\DATABASE.MDF] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [C:\USERS\ADMIN\DESKTOP\UPLOAD\SYMPHONY\SYMPHONY\APP_DATA\DATABASE.MDF] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [C:\USERS\ADMIN\DESKTOP\UPLOAD\SYMPHONY\SYMPHONY\APP_DATA\DATABASE.MDF] SET DELAYED_DURABILITY = DISABLED 
GO
USE [C:\USERS\ADMIN\DESKTOP\UPLOAD\SYMPHONY\SYMPHONY\APP_DATA\DATABASE.MDF]
GO
/****** Object:  Table [dbo].[A1]    Script Date: 3/23/2017 1:08:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[A1](
	[Id] [int] NOT NULL,
	[InstrumentId] [uniqueidentifier] NOT NULL,
	[StringerId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[A2]    Script Date: 3/23/2017 1:08:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[A2](
	[Id] [int] NOT NULL,
	[A1Id] [int] NOT NULL,
	[TrackId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[A3]    Script Date: 3/23/2017 1:08:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[A3](
	[Id] [int] NOT NULL,
	[TrackId] [uniqueidentifier] NOT NULL,
	[ConcertId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[A4]    Script Date: 3/23/2017 1:08:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[A4](
	[Id] [int] NOT NULL,
	[ConcertId] [uniqueidentifier] NOT NULL,
	[LeaderId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Admins]    Script Date: 3/23/2017 1:08:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admins](
	[Username] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Adverties]    Script Date: 3/23/2017 1:08:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Adverties](
	[Id] [int] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Image] [varbinary](max) NOT NULL,
	[Url] [nvarchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Composers]    Script Date: 3/23/2017 1:08:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Composers](
	[Id] [uniqueidentifier] NOT NULL,
	[OrderId] [int] NOT NULL,
	[Fullname] [nvarchar](max) NOT NULL,
	[Text] [nvarchar](max) NULL,
	[Thumbnail] [varbinary](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Concerts]    Script Date: 3/23/2017 1:08:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Concerts](
	[Id] [uniqueidentifier] NOT NULL,
	[OrderId] [int] NOT NULL,
	[Date] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Thumbnail] [varbinary](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Files]    Script Date: 3/23/2017 1:08:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Files](
	[Name] [uniqueidentifier] NOT NULL,
	[Type] [nvarchar](max) NOT NULL,
	[Lenght] [int] NOT NULL,
	[Bytes] [varbinary](max) NOT NULL,
	[FolderId] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Folders]    Script Date: 3/23/2017 1:08:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Folders](
	[Id] [int] NOT NULL,
	[Parent] [int] NULL,
	[Name] [nvarchar](50) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Genus]    Script Date: 3/23/2017 1:08:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genus](
	[Id] [uniqueidentifier] NOT NULL,
	[OrderId] [int] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Text] [nvarchar](max) NULL,
	[Thumbnail] [varbinary](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Instruments]    Script Date: 3/23/2017 1:08:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Instruments](
	[Id] [uniqueidentifier] NOT NULL,
	[OrderId] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Text] [nvarchar](max) NULL,
	[Thumbnail] [varbinary](max) NULL,
	[GenusId] [uniqueidentifier] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Leaders]    Script Date: 3/23/2017 1:08:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Leaders](
	[Id] [uniqueidentifier] NOT NULL,
	[OrderId] [int] NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[BirthYear] [nvarchar](max) NOT NULL,
	[Text] [nvarchar](max) NULL,
	[Thumbnail] [varbinary](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[News]    Script Date: 3/23/2017 1:08:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[News](
	[Id] [int] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Date] [nvarchar](max) NOT NULL,
	[Abstract] [nvarchar](max) NULL,
	[Text] [nvarchar](max) NOT NULL,
	[Thumbnail] [varbinary](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Pics]    Script Date: 3/23/2017 1:08:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pics](
	[Id] [uniqueidentifier] NOT NULL,
	[OrderId] [int] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Thumb] [varbinary](max) NOT NULL,
	[Bytes] [varbinary](max) NULL,
	[ConcertId] [uniqueidentifier] NULL,
	[StringerId] [uniqueidentifier] NULL,
	[TrackId] [uniqueidentifier] NULL,
	[LeaderId] [uniqueidentifier] NULL,
	[InstrumentId] [uniqueidentifier] NULL,
	[GenusId] [uniqueidentifier] NULL,
	[ComposerId] [uniqueidentifier] NULL,
	[NewsId] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Stringers]    Script Date: 3/23/2017 1:08:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stringers](
	[Id] [uniqueidentifier] NOT NULL,
	[OrderId] [int] NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[BirthYear] [nvarchar](max) NOT NULL,
	[Text] [nvarchar](max) NULL,
	[Thumbnail] [varbinary](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tracks]    Script Date: 3/23/2017 1:08:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tracks](
	[Id] [uniqueidentifier] NOT NULL,
	[OrderId] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Text] [nvarchar](max) NULL,
	[Thumbnail] [varbinary](max) NULL,
	[ComposerId] [uniqueidentifier] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
USE [master]
GO
ALTER DATABASE [C:\USERS\ADMIN\DESKTOP\UPLOAD\SYMPHONY\SYMPHONY\APP_DATA\DATABASE.MDF] SET  READ_WRITE 
GO
