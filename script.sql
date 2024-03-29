USE [master]
GO
/****** Object:  Database [PRN221_5]    Script Date: 3/13/2023 8:39:07 AM ******/
CREATE DATABASE [PRN221_5]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PRN221_5', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\PRN221_5.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PRN221_5_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\PRN221_5_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [PRN221_5] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PRN221_5].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PRN221_5] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PRN221_5] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PRN221_5] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PRN221_5] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PRN221_5] SET ARITHABORT OFF 
GO
ALTER DATABASE [PRN221_5] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PRN221_5] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PRN221_5] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PRN221_5] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PRN221_5] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PRN221_5] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PRN221_5] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PRN221_5] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PRN221_5] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PRN221_5] SET  ENABLE_BROKER 
GO
ALTER DATABASE [PRN221_5] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PRN221_5] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PRN221_5] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PRN221_5] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PRN221_5] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PRN221_5] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [PRN221_5] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PRN221_5] SET RECOVERY FULL 
GO
ALTER DATABASE [PRN221_5] SET  MULTI_USER 
GO
ALTER DATABASE [PRN221_5] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PRN221_5] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PRN221_5] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PRN221_5] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PRN221_5] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PRN221_5] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'PRN221_5', N'ON'
GO
ALTER DATABASE [PRN221_5] SET QUERY_STORE = OFF
GO
USE [PRN221_5]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 3/13/2023 8:39:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cart]    Script Date: 3/13/2023 8:39:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cart](
	[ID] [uniqueidentifier] NOT NULL,
	[ProductID] [bigint] NOT NULL,
	[UserId] [nvarchar](450) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_Cart] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 3/13/2023 8:39:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[ShortName] [nvarchar](max) NOT NULL,
	[isDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 3/13/2023 8:39:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[ID] [uniqueidentifier] NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[isDeleted] [bit] NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ProductId] [bigint] NOT NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 3/13/2023 8:39:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[ID] [uniqueidentifier] NOT NULL,
	[OrderID] [uniqueidentifier] NOT NULL,
	[ProductID] [bigint] NOT NULL,
	[Price] [real] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Amount] [real] NOT NULL,
	[isDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 3/13/2023 8:39:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[ID] [uniqueidentifier] NOT NULL,
	[Total] [real] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[isDeleted] [bit] NOT NULL,
	[UserId] [nvarchar](450) NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 3/13/2023 8:39:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Price] [real] NOT NULL,
	[imgPath] [nvarchar](max) NULL,
	[isAvailable] [bit] NOT NULL,
	[CategoryID] [bigint] NULL,
	[isDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 3/13/2023 8:39:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleClaim]    Script Date: 3/13/2023 8:39:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleClaim](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_RoleClaim] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transaction]    Script Date: 3/13/2023 8:39:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaction](
	[ID] [uniqueidentifier] NOT NULL,
	[Amount] [real] NOT NULL,
	[PreviousHash] [nvarchar](max) NOT NULL,
	[HashValue] [nvarchar](max) NOT NULL,
	[PreviousBalance] [decimal](18, 2) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[Status] [bit] NOT NULL,
	[isDeleted] [bit] NOT NULL,
	[OrderID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserClaim]    Script Date: 3/13/2023 8:39:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserClaim](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserClaim] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLogin]    Script Date: 3/13/2023 8:39:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLogin](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_UserLogin] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 3/13/2023 8:39:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 3/13/2023 8:39:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[DoB] [datetime2](7) NULL,
	[Address] [nvarchar](max) NULL,
	[Gender] [nvarchar](max) NULL,
	[isDeleted] [bit] NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserToken]    Script Date: 3/13/2023 8:39:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserToken](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserToken] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230311160117_2', N'7.0.3')
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([ID], [Name], [ShortName], [isDeleted]) VALUES (1, N'Boba and delicacy', N'BAD', 0)
INSERT [dbo].[Category] ([ID], [Name], [ShortName], [isDeleted]) VALUES (2, N'Beverage and liquor', N'BAL', 0)
INSERT [dbo].[Category] ([ID], [Name], [ShortName], [isDeleted]) VALUES (3, N'Meat and supply', N'MAS ', 0)
INSERT [dbo].[Category] ([ID], [Name], [ShortName], [isDeleted]) VALUES (4, N'Milk and detergent', N'MAD', 0)
INSERT [dbo].[Category] ([ID], [Name], [ShortName], [isDeleted]) VALUES (5, N'Fruit and Vegetable', N'FAV', 0)
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
INSERT [dbo].[OrderDetail] ([ID], [OrderID], [ProductID], [Price], [Quantity], [Amount], [isDeleted]) VALUES (N'826d55bc-9382-47ac-b557-08db224b0b61', N'cd2d69f3-64ef-4f17-911b-0544706dab5b', 1, 1, 2, 2, 0)
INSERT [dbo].[OrderDetail] ([ID], [OrderID], [ProductID], [Price], [Quantity], [Amount], [isDeleted]) VALUES (N'aee840e8-87b6-4db8-270e-08db224b7a1b', N'd203ee6f-d41c-4372-a965-59f434e0c7bc', 1, 1, 10, 10, 0)
INSERT [dbo].[OrderDetail] ([ID], [OrderID], [ProductID], [Price], [Quantity], [Amount], [isDeleted]) VALUES (N'599bf875-0e62-42c5-33fc-08db22d0a481', N'1ea9461b-ff67-45a6-aa9e-0b9f774356ea', 1, 1, 1, 1, 0)
INSERT [dbo].[OrderDetail] ([ID], [OrderID], [ProductID], [Price], [Quantity], [Amount], [isDeleted]) VALUES (N'bcb41709-6317-4bdc-7cd9-08db22d12094', N'97f4039b-2578-40cb-b33e-6e3d26f9aa0d', 1, 1, 200, 200, 0)
INSERT [dbo].[OrderDetail] ([ID], [OrderID], [ProductID], [Price], [Quantity], [Amount], [isDeleted]) VALUES (N'c6148e3c-d2cd-4332-561e-08db2310e19e', N'980d46ae-c72d-4dd9-a8cb-2b8f7a1f5b7a', 5, 2, 1, 2, 0)
INSERT [dbo].[OrderDetail] ([ID], [OrderID], [ProductID], [Price], [Quantity], [Amount], [isDeleted]) VALUES (N'25b286e3-3b3b-4fcb-b8a8-08db235afa95', N'0d97595e-f919-44ea-8977-6ece3c030730', 5, 2, -5, -10, 0)
GO
INSERT [dbo].[Orders] ([ID], [Total], [CreatedDate], [isDeleted], [UserId]) VALUES (N'cd2d69f3-64ef-4f17-911b-0544706dab5b', 2, CAST(N'2023-03-11T23:09:51.1231863' AS DateTime2), 0, N'ef7384bc-e743-4bd4-bc5f-eb0ce86d8ee8')
INSERT [dbo].[Orders] ([ID], [Total], [CreatedDate], [isDeleted], [UserId]) VALUES (N'1ea9461b-ff67-45a6-aa9e-0b9f774356ea', 1, CAST(N'2023-03-12T15:06:11.1472115' AS DateTime2), 0, N'ef7384bc-e743-4bd4-bc5f-eb0ce86d8ee8')
INSERT [dbo].[Orders] ([ID], [Total], [CreatedDate], [isDeleted], [UserId]) VALUES (N'980d46ae-c72d-4dd9-a8cb-2b8f7a1f5b7a', 2, CAST(N'2023-03-12T22:46:01.4721757' AS DateTime2), 0, N'ef7384bc-e743-4bd4-bc5f-eb0ce86d8ee8')
INSERT [dbo].[Orders] ([ID], [Total], [CreatedDate], [isDeleted], [UserId]) VALUES (N'd203ee6f-d41c-4372-a965-59f434e0c7bc', 10, CAST(N'2023-03-11T23:12:56.9298829' AS DateTime2), 0, N'ef7384bc-e743-4bd4-bc5f-eb0ce86d8ee8')
INSERT [dbo].[Orders] ([ID], [Total], [CreatedDate], [isDeleted], [UserId]) VALUES (N'97f4039b-2578-40cb-b33e-6e3d26f9aa0d', 200, CAST(N'2023-03-12T15:09:39.3042253' AS DateTime2), 0, N'ef7384bc-e743-4bd4-bc5f-eb0ce86d8ee8')
INSERT [dbo].[Orders] ([ID], [Total], [CreatedDate], [isDeleted], [UserId]) VALUES (N'0d97595e-f919-44ea-8977-6ece3c030730', -10, CAST(N'2023-03-13T07:36:26.0798062' AS DateTime2), 0, N'ef7384bc-e743-4bd4-bc5f-eb0ce86d8ee8')
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ID], [Name], [Price], [imgPath], [isAvailable], [CategoryID], [isDeleted]) VALUES (1, N'Avocado ', 1, N'~/client/image/cache/catalog/product/15-315x315.jpg', 1, 1, 0)
INSERT [dbo].[Product] ([ID], [Name], [Price], [imgPath], [isAvailable], [CategoryID], [isDeleted]) VALUES (2, N'Cooking Oil ', 2, N'~/client/image/cache/catalog/product/10-315x315.jpg', 1, 3, 0)
INSERT [dbo].[Product] ([ID], [Name], [Price], [imgPath], [isAvailable], [CategoryID], [isDeleted]) VALUES (3, N'Milk', 3, N'~/client/image/cache/catalog/product/1-315x315.jpg', 1, 4, 0)
INSERT [dbo].[Product] ([ID], [Name], [Price], [imgPath], [isAvailable], [CategoryID], [isDeleted]) VALUES (4, N'Liquor', 10, N'~/client/image/cache/catalog/product/cream-liqueur-pistachio.jpg', 1, 2, 0)
INSERT [dbo].[Product] ([ID], [Name], [Price], [imgPath], [isAvailable], [CategoryID], [isDeleted]) VALUES (5, N'Milk Cow', 2, N'~/client/image/cache/catalog/product/milk-central-lechera-asturiana-1-5-l_215035.jpg', 1, 4, 0)
INSERT [dbo].[Product] ([ID], [Name], [Price], [imgPath], [isAvailable], [CategoryID], [isDeleted]) VALUES (6, N'Milk Buffalo', 2, N'~/client/image/cache/catalog/product/milk.jpg', 1, 4, 0)
INSERT [dbo].[Product] ([ID], [Name], [Price], [imgPath], [isAvailable], [CategoryID], [isDeleted]) VALUES (7, N'Lettuce', 1, N'~/client/image/cache/catalog/product/17-315x315.jpg', 1, 5, 0)
INSERT [dbo].[Product] ([ID], [Name], [Price], [imgPath], [isAvailable], [CategoryID], [isDeleted]) VALUES (8, N'Whiskey', 15, N'~/client/image/cache/catalog/product/images (2).jpg', 1, 2, 0)
INSERT [dbo].[Product] ([ID], [Name], [Price], [imgPath], [isAvailable], [CategoryID], [isDeleted]) VALUES (9, N'Yogurt France', 2, N'~/client/image/cache/catalog/product/yogurt.webp', 1, 1, 0)
INSERT [dbo].[Product] ([ID], [Name], [Price], [imgPath], [isAvailable], [CategoryID], [isDeleted]) VALUES (10, N'Nuts', 4, N'~/client/image/cache/catalog/product/nuts.webp', 1, 1, 0)
INSERT [dbo].[Product] ([ID], [Name], [Price], [imgPath], [isAvailable], [CategoryID], [isDeleted]) VALUES (11, N'Cereal', 2, N'~/client/image/cache/catalog/product/milk-central-lechera-asturiana-1-5-l_215035.jpg', 1, 3, 0)
INSERT [dbo].[Product] ([ID], [Name], [Price], [imgPath], [isAvailable], [CategoryID], [isDeleted]) VALUES (12, N'Brocoli', 2, N'~/client/image/cache/catalog/product/5-315x315.jpg', 1, 5, 0)
INSERT [dbo].[Product] ([ID], [Name], [Price], [imgPath], [isAvailable], [CategoryID], [isDeleted]) VALUES (13, N'Fish', 6, N'~/client/image/cache/catalog/product/fish.jpg', 1, 3, 0)
INSERT [dbo].[Product] ([ID], [Name], [Price], [imgPath], [isAvailable], [CategoryID], [isDeleted]) VALUES (14, N'Tuna', 2, N'~/client/image/cache/catalog/product/fish3.webp', 1, 3, 0)
INSERT [dbo].[Product] ([ID], [Name], [Price], [imgPath], [isAvailable], [CategoryID], [isDeleted]) VALUES (15, N'Drinks', 2, N'~/client/image/cache/catalog/product/504341.jpg', 1, 4, 0)
INSERT [dbo].[Product] ([ID], [Name], [Price], [imgPath], [isAvailable], [CategoryID], [isDeleted]) VALUES (16, N'Cow Meat', 10, N'~/client/image/cache/catalog/product/fish3.webp', 1, 3, 0)
INSERT [dbo].[Product] ([ID], [Name], [Price], [imgPath], [isAvailable], [CategoryID], [isDeleted]) VALUES (17, N'Marshmello', 2, N'~/client/image/cache/catalog/product/fruitcandy.webp', 1, 1, 0)
INSERT [dbo].[Product] ([ID], [Name], [Price], [imgPath], [isAvailable], [CategoryID], [isDeleted]) VALUES (18, N'Cookies', 3, N'~/client/image/cache/catalog/product/images (4).jpg', 1, 4, 0)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
INSERT [dbo].[Role] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'32106257-248d-4163-85f3-4601ba185d0a', N'ShopOwner', N'SHOPOWNER', N'2a757208-fa33-46a2-a85d-6057c1e1e51a')
INSERT [dbo].[Role] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'9ae022c7-650e-405e-ba80-2ce9b1e8524b', N'Administrator', N'ADMINISTRATOR', N'7bb68646-fc42-4ffc-9e1c-e327a7ef2ec6')
INSERT [dbo].[Role] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'f6df50c0-039b-4dfd-8220-b74ed738f533', N'Customer', N'CUSTOMER', N'ff5390e9-a741-4c78-be62-52782254643c')
GO
INSERT [dbo].[Transaction] ([ID], [Amount], [PreviousHash], [HashValue], [PreviousBalance], [CreatedDate], [Status], [isDeleted], [OrderID]) VALUES (N'ab67cf01-baf1-4d99-b701-328050e52d5a', 2, N'yYyN/kIibtof/HuyIs6wIhaGowojxi5bz/Dog9pCk1g=', N'iD1MqHRzRS4P5V3bipyk4zBfMDJ911R3CtN2uYON4xY=', CAST(0.00 AS Decimal(18, 2)), CAST(N'2023-03-11T16:09:51.2332604' AS DateTime2), 1, 0, N'cd2d69f3-64ef-4f17-911b-0544706dab5b')
INSERT [dbo].[Transaction] ([ID], [Amount], [PreviousHash], [HashValue], [PreviousBalance], [CreatedDate], [Status], [isDeleted], [OrderID]) VALUES (N'6b2ab28e-0a6e-4f1a-90d6-46cf766ec2ba', 2, N'O03eh+oTS93Snngt1Jwl8Va3oA4d3RlROCFFZs0pofQ=', N'NhYNH3f7XAvYH2yLhQhcHiAqr84Ln9I5Uj/Dy5XGnuM=', CAST(213.00 AS Decimal(18, 2)), CAST(N'2023-03-12T15:46:01.4991648' AS DateTime2), 1, 0, N'980d46ae-c72d-4dd9-a8cb-2b8f7a1f5b7a')
INSERT [dbo].[Transaction] ([ID], [Amount], [PreviousHash], [HashValue], [PreviousBalance], [CreatedDate], [Status], [isDeleted], [OrderID]) VALUES (N'd3c38325-8b5d-4708-bd00-519a1534b11e', 1, N'7kC2S+HY758RORS1HKrKe6GEa8YUgc/PHXfSFYOF/7Q=', N'FTMpqPiKaGpP8HxWb06awOw6s7bGU4qrKqs2qQwo/tM=', CAST(12.00 AS Decimal(18, 2)), CAST(N'2023-03-12T08:06:11.1916537' AS DateTime2), 1, 0, N'1ea9461b-ff67-45a6-aa9e-0b9f774356ea')
INSERT [dbo].[Transaction] ([ID], [Amount], [PreviousHash], [HashValue], [PreviousBalance], [CreatedDate], [Status], [isDeleted], [OrderID]) VALUES (N'744ad804-3ce8-4b59-80bb-5d77f20f803a', 200, N'FTMpqPiKaGpP8HxWb06awOw6s7bGU4qrKqs2qQwo/tM=', N'O03eh+oTS93Snngt1Jwl8Va3oA4d3RlROCFFZs0pofQ=', CAST(13.00 AS Decimal(18, 2)), CAST(N'2023-03-12T08:09:39.3545659' AS DateTime2), 1, 0, N'97f4039b-2578-40cb-b33e-6e3d26f9aa0d')
INSERT [dbo].[Transaction] ([ID], [Amount], [PreviousHash], [HashValue], [PreviousBalance], [CreatedDate], [Status], [isDeleted], [OrderID]) VALUES (N'a787e7b3-92c4-48ce-aedc-d60cc8c80f6b', 10, N'iD1MqHRzRS4P5V3bipyk4zBfMDJ911R3CtN2uYON4xY=', N'7kC2S+HY758RORS1HKrKe6GEa8YUgc/PHXfSFYOF/7Q=', CAST(2.00 AS Decimal(18, 2)), CAST(N'2023-03-11T16:12:56.9961436' AS DateTime2), 1, 0, N'd203ee6f-d41c-4372-a965-59f434e0c7bc')
INSERT [dbo].[Transaction] ([ID], [Amount], [PreviousHash], [HashValue], [PreviousBalance], [CreatedDate], [Status], [isDeleted], [OrderID]) VALUES (N'2c002645-4504-41cb-b72e-f880049fab37', -10, N'NhYNH3f7XAvYH2yLhQhcHiAqr84Ln9I5Uj/Dy5XGnuM=', N'+PLy2VncvqUOQEYaNFy2CFlkCwIYL6bCm4a+UPDGlhs=', CAST(215.00 AS Decimal(18, 2)), CAST(N'2023-03-13T00:36:26.2013223' AS DateTime2), 1, 0, N'0d97595e-f919-44ea-8977-6ece3c030730')
GO
INSERT [dbo].[UserLogin] ([LoginProvider], [ProviderKey], [ProviderDisplayName], [UserId]) VALUES (N'Google', N'112161882286442630305', N'Google', N'ef7384bc-e743-4bd4-bc5f-eb0ce86d8ee8')
GO
INSERT [dbo].[UserRole] ([UserId], [RoleId]) VALUES (N'e979a372-8c80-4b5d-9670-fb89e6ed1ab3', N'32106257-248d-4163-85f3-4601ba185d0a')
INSERT [dbo].[UserRole] ([UserId], [RoleId]) VALUES (N'511b66b8-dcef-4c48-a112-652771e72a5f', N'9ae022c7-650e-405e-ba80-2ce9b1e8524b')
INSERT [dbo].[UserRole] ([UserId], [RoleId]) VALUES (N'ef7384bc-e743-4bd4-bc5f-eb0ce86d8ee8', N'f6df50c0-039b-4dfd-8220-b74ed738f533')
GO
INSERT [dbo].[Users] ([Id], [Name], [DoB], [Address], [Gender], [isDeleted], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'511b66b8-dcef-4c48-a112-652771e72a5f', N'erererere', CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), N'NaN', N'F', 1, N'Administrator', N'ADMINISTRATOR', N'vuvanmanh30122002@gmail.com', N'VUVANMANH30122002@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEF8AFkdEuVa7W806TypxeGoSW/z77RuhI4hao2nSxYsteh2YEMs3tVk1c0IpCr4xuQ==', N'PQ6V4BWSN2SSHNL4JESLZZKFJCSBSJOK', N'793c66c8-fc83-4d35-96c7-4d85412d3bee', N'0938143536', 0, 0, NULL, 1, 0)
INSERT [dbo].[Users] ([Id], [Name], [DoB], [Address], [Gender], [isDeleted], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'e979a372-8c80-4b5d-9670-fb89e6ed1ab3', N'erererere', CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), N'NaN', NULL, 1, N'ShopOwner', N'SHOPOWNER', N'vumanh@gmail.com', N'VUMANH@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEDQ3UbO+dOfGosfbjnHCMh/Rn+e6PTG/C+QORRjLn+BPcCQTHNujCAIJ2RLZ0cR8gQ==', N'IVX7X2LMGGZF4NY3HJQAOXLSDGL5XEW5', N'ca228000-d31b-4380-b845-a44cbd8c7d42', N'0938143536', 0, 0, NULL, 1, 0)
INSERT [dbo].[Users] ([Id], [Name], [DoB], [Address], [Gender], [isDeleted], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'ef7384bc-e743-4bd4-bc5f-eb0ce86d8ee8', N'Manh Vu van', NULL, N'30-12-2002', N'Male', 1, N'vuvanmanh3012@gmail.com', N'VUVANMANH3012@GMAIL.COM', N'vuvanmanh3012@gmail.com', N'VUVANMANH3012@GMAIL.COM', 0, NULL, N'NEOVBECIUS3T3REZF3CE3NXLWO7WXOJQ', N'6a379c72-7346-497f-8c3d-12dd2ac28a60', N'0938143536', 0, 0, NULL, 1, 0)
GO
/****** Object:  Index [IX_Cart_ProductID]    Script Date: 3/13/2023 8:39:08 AM ******/
CREATE NONCLUSTERED INDEX [IX_Cart_ProductID] ON [dbo].[Cart]
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Cart_UserId]    Script Date: 3/13/2023 8:39:08 AM ******/
CREATE NONCLUSTERED INDEX [IX_Cart_UserId] ON [dbo].[Cart]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Comment_ProductId]    Script Date: 3/13/2023 8:39:08 AM ******/
CREATE NONCLUSTERED INDEX [IX_Comment_ProductId] ON [dbo].[Comment]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Comment_UserId]    Script Date: 3/13/2023 8:39:08 AM ******/
CREATE NONCLUSTERED INDEX [IX_Comment_UserId] ON [dbo].[Comment]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrderDetail_OrderID]    Script Date: 3/13/2023 8:39:08 AM ******/
CREATE NONCLUSTERED INDEX [IX_OrderDetail_OrderID] ON [dbo].[OrderDetail]
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrderDetail_ProductID]    Script Date: 3/13/2023 8:39:08 AM ******/
CREATE NONCLUSTERED INDEX [IX_OrderDetail_ProductID] ON [dbo].[OrderDetail]
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Orders_UserId]    Script Date: 3/13/2023 8:39:08 AM ******/
CREATE NONCLUSTERED INDEX [IX_Orders_UserId] ON [dbo].[Orders]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Product_CategoryID]    Script Date: 3/13/2023 8:39:08 AM ******/
CREATE NONCLUSTERED INDEX [IX_Product_CategoryID] ON [dbo].[Product]
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 3/13/2023 8:39:08 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[Role]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_RoleClaim_RoleId]    Script Date: 3/13/2023 8:39:08 AM ******/
CREATE NONCLUSTERED INDEX [IX_RoleClaim_RoleId] ON [dbo].[RoleClaim]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Transaction_OrderID]    Script Date: 3/13/2023 8:39:08 AM ******/
CREATE NONCLUSTERED INDEX [IX_Transaction_OrderID] ON [dbo].[Transaction]
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserClaim_UserId]    Script Date: 3/13/2023 8:39:08 AM ******/
CREATE NONCLUSTERED INDEX [IX_UserClaim_UserId] ON [dbo].[UserClaim]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserLogin_UserId]    Script Date: 3/13/2023 8:39:08 AM ******/
CREATE NONCLUSTERED INDEX [IX_UserLogin_UserId] ON [dbo].[UserLogin]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserRole_RoleId]    Script Date: 3/13/2023 8:39:08 AM ******/
CREATE NONCLUSTERED INDEX [IX_UserRole_RoleId] ON [dbo].[UserRole]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 3/13/2023 8:39:08 AM ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[Users]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 3/13/2023 8:39:08 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[Users]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Cart] ADD  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[Transaction] ADD  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD  CONSTRAINT [FK_Cart_Product_ProductID] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Cart] CHECK CONSTRAINT [FK_Cart_Product_ProductID]
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD  CONSTRAINT [FK_Cart_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Cart] CHECK CONSTRAINT [FK_Cart_Users_UserId]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Product_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_Product_ProductId]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_Users_UserId]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Orders_OrderID] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Orders] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Orders_OrderID]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Product_ProductID] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Product_ProductID]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Users_UserId]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category_CategoryID] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([ID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category_CategoryID]
GO
ALTER TABLE [dbo].[RoleClaim]  WITH CHECK ADD  CONSTRAINT [FK_RoleClaim_Role_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleClaim] CHECK CONSTRAINT [FK_RoleClaim_Role_RoleId]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Orders_OrderID] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Orders] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Orders_OrderID]
GO
ALTER TABLE [dbo].[UserClaim]  WITH CHECK ADD  CONSTRAINT [FK_UserClaim_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserClaim] CHECK CONSTRAINT [FK_UserClaim_Users_UserId]
GO
ALTER TABLE [dbo].[UserLogin]  WITH CHECK ADD  CONSTRAINT [FK_UserLogin_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserLogin] CHECK CONSTRAINT [FK_UserLogin_Users_UserId]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_Role_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_Role_RoleId]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_Users_UserId]
GO
ALTER TABLE [dbo].[UserToken]  WITH CHECK ADD  CONSTRAINT [FK_UserToken_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserToken] CHECK CONSTRAINT [FK_UserToken_Users_UserId]
GO
USE [master]
GO
ALTER DATABASE [PRN221_5] SET  READ_WRITE 
GO
