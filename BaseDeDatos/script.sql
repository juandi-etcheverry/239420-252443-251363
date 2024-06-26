USE [master]
GO
/****** Object:  Database [DA2DB]    Script Date: 10/5/2023 6:38:27 PM ******/
CREATE DATABASE [DA2DB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DA2DB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\DA2DB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DA2DB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\DA2DB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [DA2DB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DA2DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DA2DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DA2DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DA2DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DA2DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DA2DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [DA2DB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [DA2DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DA2DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DA2DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DA2DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DA2DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DA2DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DA2DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DA2DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DA2DB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DA2DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DA2DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DA2DB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DA2DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DA2DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DA2DB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [DA2DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DA2DB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DA2DB] SET  MULTI_USER 
GO
ALTER DATABASE [DA2DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DA2DB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DA2DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DA2DB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DA2DB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DA2DB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [DA2DB] SET QUERY_STORE = ON
GO
ALTER DATABASE [DA2DB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [DA2DB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 10/5/2023 6:38:27 PM ******/
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
/****** Object:  Table [dbo].[Brands]    Script Date: 10/5/2023 6:38:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brands](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Brands] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Carts]    Script Date: 10/5/2023 6:38:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[TotalPrice] [real] NOT NULL,
	[FinalPrice] [real] NOT NULL,
	[PromotionName] [nvarchar](max) NULL,
 CONSTRAINT [PK_Carts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 10/5/2023 6:38:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Colors]    Script Date: 10/5/2023 6:38:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Colors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Colors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductColor]    Script Date: 10/5/2023 6:38:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductColor](
	[ColorsId] [int] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ProductColor] PRIMARY KEY CLUSTERED 
(
	[ColorsId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 10/5/2023 6:38:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Price] [real] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[BrandId] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseProduct]    Script Date: 10/5/2023 6:38:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseProduct](
	[ProductsId] [uniqueidentifier] NOT NULL,
	[PurchaseId] [int] NOT NULL,
 CONSTRAINT [PK_PurchaseProduct] PRIMARY KEY CLUSTERED 
(
	[ProductsId] ASC,
	[PurchaseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SessionTokens]    Script Date: 10/5/2023 6:38:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SessionTokens](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NULL,
	[CartId] [int] NULL,
 CONSTRAINT [PK_SessionTokens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 10/5/2023 6:38:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[Role] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231004224653_initial', N'7.0.11')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231005030744_setup', N'7.0.11')
GO
SET IDENTITY_INSERT [dbo].[Brands] ON 

INSERT [dbo].[Brands] ([Id], [Name]) VALUES (1, N'brand1')
INSERT [dbo].[Brands] ([Id], [Name]) VALUES (2, N'brand2')
INSERT [dbo].[Brands] ([Id], [Name]) VALUES (3, N'brand3')
SET IDENTITY_INSERT [dbo].[Brands] OFF
GO
SET IDENTITY_INSERT [dbo].[Carts] ON 

INSERT [dbo].[Carts] ([Id], [UserId], [TotalPrice], [FinalPrice], [PromotionName]) VALUES (1, N'9fd20edc-7771-4cb1-c1f2-08dbc5cfb1a6', 500, 500, NULL)
INSERT [dbo].[Carts] ([Id], [UserId], [TotalPrice], [FinalPrice], [PromotionName]) VALUES (2, N'9fd20edc-7771-4cb1-c1f2-08dbc5cfb1a6', 500, 500, N'No Promotion')
INSERT [dbo].[Carts] ([Id], [UserId], [TotalPrice], [FinalPrice], [PromotionName]) VALUES (3, N'9fd20edc-7771-4cb1-c1f2-08dbc5cfb1a6', 1200, 950, N'Total Look Promotion')
INSERT [dbo].[Carts] ([Id], [UserId], [TotalPrice], [FinalPrice], [PromotionName]) VALUES (4, N'9fd20edc-7771-4cb1-c1f2-08dbc5cfb1a6', 1000, 900, N'20% Promotion')
INSERT [dbo].[Carts] ([Id], [UserId], [TotalPrice], [FinalPrice], [PromotionName]) VALUES (5, N'9fd20edc-7771-4cb1-c1f2-08dbc5cfb1a6', 800, 700, N'20% Promotion')
INSERT [dbo].[Carts] ([Id], [UserId], [TotalPrice], [FinalPrice], [PromotionName]) VALUES (6, N'9fd20edc-7771-4cb1-c1f2-08dbc5cfb1a6', 2000, 1500, N'3x2 Category Promotion')
INSERT [dbo].[Carts] ([Id], [UserId], [TotalPrice], [FinalPrice], [PromotionName]) VALUES (7, N'9fd20edc-7771-4cb1-c1f2-08dbc5cfb1a6', 1500, 500, N'Fidelity Promotion')
SET IDENTITY_INSERT [dbo].[Carts] OFF
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Id], [Name]) VALUES (1, N'category1')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (2, N'category2')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (3, N'category3')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Colors] ON 

INSERT [dbo].[Colors] ([Id], [Name]) VALUES (1, N'color1')
INSERT [dbo].[Colors] ([Id], [Name]) VALUES (2, N'color2')
INSERT [dbo].[Colors] ([Id], [Name]) VALUES (3, N'color3')
SET IDENTITY_INSERT [dbo].[Colors] OFF
GO
INSERT [dbo].[ProductColor] ([ColorsId], [ProductId]) VALUES (3, N'1718a315-e95f-48d1-6595-08dbc55226b7')
INSERT [dbo].[ProductColor] ([ColorsId], [ProductId]) VALUES (1, N'0968ba15-c673-46a8-6596-08dbc55226b7')
INSERT [dbo].[ProductColor] ([ColorsId], [ProductId]) VALUES (3, N'ad5085c3-92c8-4899-43d5-08dbc5e12bef')
INSERT [dbo].[ProductColor] ([ColorsId], [ProductId]) VALUES (1, N'd2f73f41-c2a5-46c4-43d6-08dbc5e12bef')
INSERT [dbo].[ProductColor] ([ColorsId], [ProductId]) VALUES (1, N'996c844e-d826-4bad-43d7-08dbc5e12bef')
INSERT [dbo].[ProductColor] ([ColorsId], [ProductId]) VALUES (3, N'f7ed149b-1716-4205-43d8-08dbc5e12bef')
GO
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [BrandId], [IsDeleted]) VALUES (N'1718a315-e95f-48d1-6595-08dbc55226b7', N'Product 3', N'Description of product 3', 500, 1, 3, 0)
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [BrandId], [IsDeleted]) VALUES (N'0968ba15-c673-46a8-6596-08dbc55226b7', N'Product 1', N'Description of product 1', 500, 3, 3, 0)
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [BrandId], [IsDeleted]) VALUES (N'ad5085c3-92c8-4899-43d5-08dbc5e12bef', N'Product 4', N'Description of product 4', 500, 3, 3, 0)
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [BrandId], [IsDeleted]) VALUES (N'd2f73f41-c2a5-46c4-43d6-08dbc5e12bef', N'Product 5', N'Description of product 5', 500, 3, 3, 0)
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [BrandId], [IsDeleted]) VALUES (N'996c844e-d826-4bad-43d7-08dbc5e12bef', N'Product 6', N'Description of product 6', 1000, 3, 3, 0)
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [CategoryId], [BrandId], [IsDeleted]) VALUES (N'f7ed149b-1716-4205-43d8-08dbc5e12bef', N'Product 7', N'Description of product 7', 500, 3, 1, 0)
GO
INSERT [dbo].[PurchaseProduct] ([ProductsId], [PurchaseId]) VALUES (N'0968ba15-c673-46a8-6596-08dbc55226b7', 1)
INSERT [dbo].[PurchaseProduct] ([ProductsId], [PurchaseId]) VALUES (N'0968ba15-c673-46a8-6596-08dbc55226b7', 2)
INSERT [dbo].[PurchaseProduct] ([ProductsId], [PurchaseId]) VALUES (N'1718a315-e95f-48d1-6595-08dbc55226b7', 3)
INSERT [dbo].[PurchaseProduct] ([ProductsId], [PurchaseId]) VALUES (N'ad5085c3-92c8-4899-43d5-08dbc5e12bef', 3)
INSERT [dbo].[PurchaseProduct] ([ProductsId], [PurchaseId]) VALUES (N'f7ed149b-1716-4205-43d8-08dbc5e12bef', 3)
INSERT [dbo].[PurchaseProduct] ([ProductsId], [PurchaseId]) VALUES (N'1718a315-e95f-48d1-6595-08dbc55226b7', 4)
INSERT [dbo].[PurchaseProduct] ([ProductsId], [PurchaseId]) VALUES (N'0968ba15-c673-46a8-6596-08dbc55226b7', 4)
INSERT [dbo].[PurchaseProduct] ([ProductsId], [PurchaseId]) VALUES (N'd2f73f41-c2a5-46c4-43d6-08dbc5e12bef', 5)
INSERT [dbo].[PurchaseProduct] ([ProductsId], [PurchaseId]) VALUES (N'996c844e-d826-4bad-43d7-08dbc5e12bef', 5)
INSERT [dbo].[PurchaseProduct] ([ProductsId], [PurchaseId]) VALUES (N'f7ed149b-1716-4205-43d8-08dbc5e12bef', 5)
INSERT [dbo].[PurchaseProduct] ([ProductsId], [PurchaseId]) VALUES (N'd2f73f41-c2a5-46c4-43d6-08dbc5e12bef', 6)
INSERT [dbo].[PurchaseProduct] ([ProductsId], [PurchaseId]) VALUES (N'996c844e-d826-4bad-43d7-08dbc5e12bef', 6)
INSERT [dbo].[PurchaseProduct] ([ProductsId], [PurchaseId]) VALUES (N'f7ed149b-1716-4205-43d8-08dbc5e12bef', 6)
INSERT [dbo].[PurchaseProduct] ([ProductsId], [PurchaseId]) VALUES (N'1718a315-e95f-48d1-6595-08dbc55226b7', 7)
INSERT [dbo].[PurchaseProduct] ([ProductsId], [PurchaseId]) VALUES (N'0968ba15-c673-46a8-6596-08dbc55226b7', 7)
INSERT [dbo].[PurchaseProduct] ([ProductsId], [PurchaseId]) VALUES (N'ad5085c3-92c8-4899-43d5-08dbc5e12bef', 7)
GO
INSERT [dbo].[SessionTokens] ([Id], [UserId], [CartId]) VALUES (N'c6d42b43-af67-44b0-e928-08dbc5cfb1fc', N'fd0d0274-6cf0-4cef-c1dc-08dbc5cfb1a6', NULL)
INSERT [dbo].[SessionTokens] ([Id], [UserId], [CartId]) VALUES (N'f056f563-112c-4047-e929-08dbc5cfb1fc', N'df292558-7894-4e4e-c1dd-08dbc5cfb1a6', NULL)
INSERT [dbo].[SessionTokens] ([Id], [UserId], [CartId]) VALUES (N'6188753b-01ad-49b7-e92a-08dbc5cfb1fc', N'aa9ce9ce-8fa1-4ca2-c1de-08dbc5cfb1a6', NULL)
INSERT [dbo].[SessionTokens] ([Id], [UserId], [CartId]) VALUES (N'23ff6e29-0352-4c29-e92b-08dbc5cfb1fc', N'e3aa1ffc-3687-4af3-c1df-08dbc5cfb1a6', NULL)
INSERT [dbo].[SessionTokens] ([Id], [UserId], [CartId]) VALUES (N'a007133f-4622-4623-e92c-08dbc5cfb1fc', N'01701bf3-73bf-4d9d-c1e1-08dbc5cfb1a6', NULL)
INSERT [dbo].[SessionTokens] ([Id], [UserId], [CartId]) VALUES (N'8a5f6371-1325-459a-e93c-08dbc5cfb1fc', N'0aef233f-77df-4845-c1ed-08dbc5cfb1a6', NULL)
INSERT [dbo].[SessionTokens] ([Id], [UserId], [CartId]) VALUES (N'84cc3028-9e26-4d54-e940-08dbc5cfb1fc', N'089d504f-2a76-45ae-c1f1-08dbc5cfb1a6', NULL)
INSERT [dbo].[SessionTokens] ([Id], [UserId], [CartId]) VALUES (N'9fb81b7b-f2f3-4214-e942-08dbc5cfb1fc', N'ac95d3d2-7ed5-4a34-c1f3-08dbc5cfb1a6', NULL)
INSERT [dbo].[SessionTokens] ([Id], [UserId], [CartId]) VALUES (N'beb27128-93d1-4471-e949-08dbc5cfb1fc', N'c69e5ffc-c3a5-4768-c1fa-08dbc5cfb1a6', NULL)
INSERT [dbo].[SessionTokens] ([Id], [UserId], [CartId]) VALUES (N'75089ba0-ca6e-44bb-e94b-08dbc5cfb1fc', N'cc9420bb-00cd-4ecb-c1fc-08dbc5cfb1a6', NULL)
INSERT [dbo].[SessionTokens] ([Id], [UserId], [CartId]) VALUES (N'9d7fc4be-0ac2-40da-e94f-08dbc5cfb1fc', N'32dd65e8-e51e-4468-c911-08dbc59f5fda', NULL)
INSERT [dbo].[SessionTokens] ([Id], [UserId], [CartId]) VALUES (N'2f3a50d4-c958-4df8-e951-08dbc5cfb1fc', N'9fd20edc-7771-4cb1-c1f2-08dbc5cfb1a6', NULL)
INSERT [dbo].[SessionTokens] ([Id], [UserId], [CartId]) VALUES (N'382f31fa-3409-4c7e-4037-08dbc5e843ee', N'08f68b18-f5ab-490b-c1f4-08dbc5cfb1a6', NULL)
GO
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Role], [IsDeleted]) VALUES (N'141e28d8-e33b-4d96-a291-08dbc5562d3f', N'test@test.com', N'Password123', N'Address 123', 1, 0)
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Role], [IsDeleted]) VALUES (N'32dd65e8-e51e-4468-c911-08dbc59f5fda', N'Email@Email.com', N'Password', N'Address', 1, 0)
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Role], [IsDeleted]) VALUES (N'fd0d0274-6cf0-4cef-c1dc-08dbc5cfb1a6', N'Email@Email.comasd', N'Password', N'Address', 1, 0)
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Role], [IsDeleted]) VALUES (N'df292558-7894-4e4e-c1dd-08dbc5cfb1a6', N'Email@Email.com31055', N'Password', N'Address', 1, 0)
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Role], [IsDeleted]) VALUES (N'aa9ce9ce-8fa1-4ca2-c1de-08dbc5cfb1a6', N'Email@Email.com685748', N'Password', N'Address', 1, 0)
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Role], [IsDeleted]) VALUES (N'e3aa1ffc-3687-4af3-c1df-08dbc5cfb1a6', N'Email@Email.com609099', N'Password', N'Address', 1, 0)
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Role], [IsDeleted]) VALUES (N'40481d65-e4f2-4149-c1e0-08dbc5cfb1a6', N'Email@Email.com33253', N'Password', N'Address', 1, 0)
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Role], [IsDeleted]) VALUES (N'01701bf3-73bf-4d9d-c1e1-08dbc5cfb1a6', N'Email@Email.com33253', N'Password', N'Address', 1, 0)
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Role], [IsDeleted]) VALUES (N'7df58cef-91f8-4c80-c1e2-08dbc5cfb1a6', N'Email@Email.com182551', N'Password', N'Address', 1, 0)
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Role], [IsDeleted]) VALUES (N'405a6cf4-82bb-41e9-c1e3-08dbc5cfb1a6', N'Email@Email.com470042', N'Password', N'Address', 1, 0)
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Role], [IsDeleted]) VALUES (N'20ce1edf-e588-4f4f-c1e4-08dbc5cfb1a6', N'Email@Email.com499003', N'Password', N'Address', 1, 0)
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Role], [IsDeleted]) VALUES (N'867b7663-b7c9-42b7-c1e5-08dbc5cfb1a6', N'Email@Email.com288974', N'Password', N'Address', 1, 0)
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Role], [IsDeleted]) VALUES (N'39c5b25d-1ec8-48bb-c1e6-08dbc5cfb1a6', N'Email@Email.com424833', N'Password', N'Address', 1, 0)
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Role], [IsDeleted]) VALUES (N'148e7ff6-a7c7-4948-c1e7-08dbc5cfb1a6', N'Email@Email.com9651', N'Password', N'Address', 1, 0)
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Role], [IsDeleted]) VALUES (N'b1bdea19-b619-4ff4-c1e8-08dbc5cfb1a6', N'Email@Email.com722776', N'Password', N'Address', 1, 0)
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Role], [IsDeleted]) VALUES (N'83bfb1a5-a14b-4944-c1e9-08dbc5cfb1a6', N'Email@Email.com39001', N'Password', N'Address', 1, 0)
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Role], [IsDeleted]) VALUES (N'85225ded-1510-44dc-c1ea-08dbc5cfb1a6', N'Email@Email.com45388', N'Password', N'Address', 1, 0)
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Role], [IsDeleted]) VALUES (N'cef16577-2365-44d7-c1eb-08dbc5cfb1a6', N'Email@Email.com433701', N'Password', N'Address', 1, 0)
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Role], [IsDeleted]) VALUES (N'6e276887-1eb8-4ba8-c1ec-08dbc5cfb1a6', N'Email@Email.com674260', N'Password', N'Address', 1, 0)
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Role], [IsDeleted]) VALUES (N'0aef233f-77df-4845-c1ed-08dbc5cfb1a6', N'Email@Email.com793241', N'Password', N'Address', 1, 0)
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Role], [IsDeleted]) VALUES (N'3bdf88e6-4ad8-4c04-c1ee-08dbc5cfb1a6', N'Email@Email.com882165', N'Password', N'Address', 1, 0)
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Role], [IsDeleted]) VALUES (N'17e868cd-07b3-4b77-c1ef-08dbc5cfb1a6', N'Email@Email.com80488', N'Password', N'Address', 1, 0)
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Role], [IsDeleted]) VALUES (N'ecbd13b7-436e-42c1-c1f0-08dbc5cfb1a6', N'Email@Email.com137888', N'Password', N'Address', 1, 0)
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Role], [IsDeleted]) VALUES (N'089d504f-2a76-45ae-c1f1-08dbc5cfb1a6', N'Email@Email.com750216', N'Password', N'Address', 1, 0)
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Role], [IsDeleted]) VALUES (N'9fd20edc-7771-4cb1-c1f2-08dbc5cfb1a6', N'UpdateUser@test.com', N'Password', N'New Address 123', 1, 0)
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Role], [IsDeleted]) VALUES (N'ac95d3d2-7ed5-4a34-c1f3-08dbc5cfb1a6', N'Email@Email.com449903', N'Password', N'Address', 1, 0)
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Role], [IsDeleted]) VALUES (N'08f68b18-f5ab-490b-c1f4-08dbc5cfb1a6', N'Admin@Admin.com', N'Password', N'Address', 2, 0)
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Role], [IsDeleted]) VALUES (N'5b411557-bf9a-45da-c1f5-08dbc5cfb1a6', N'Email@Email.com786122', N'Password', N'Address', 1, 0)
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Role], [IsDeleted]) VALUES (N'2502c148-2cd1-4ae1-c1f6-08dbc5cfb1a6', N'Email@Email.com421214', N'Password', N'Address', 1, 0)
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Role], [IsDeleted]) VALUES (N'8defbdc3-7073-4d0f-c1f7-08dbc5cfb1a6', N'Email@Email.com616519', N'Password', N'Address', 1, 0)
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Role], [IsDeleted]) VALUES (N'bd991314-70f5-49a7-c1f8-08dbc5cfb1a6', N'Email@Email.com241208', N'Password', N'Address', 1, 0)
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Role], [IsDeleted]) VALUES (N'557520b1-c54c-4897-c1f9-08dbc5cfb1a6', N'Email@Email.com314910', N'Password', N'Address', 1, 0)
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Role], [IsDeleted]) VALUES (N'c69e5ffc-c3a5-4768-c1fa-08dbc5cfb1a6', N'Email@Email.com334551', N'Password', N'Address', 1, 0)
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Role], [IsDeleted]) VALUES (N'e41998b4-f0bb-4478-c1fb-08dbc5cfb1a6', N'Email@Email.com113765', N'Password', N'Address', 1, 0)
INSERT [dbo].[Users] ([Id], [Email], [Password], [Address], [Role], [IsDeleted]) VALUES (N'cc9420bb-00cd-4ecb-c1fc-08dbc5cfb1a6', N'UpdateUser2@test.com', N'Password', N'New Address 12345', 2, 0)
GO
/****** Object:  Index [IX_Carts_UserId]    Script Date: 10/5/2023 6:38:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_Carts_UserId] ON [dbo].[Carts]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductColor_ProductId]    Script Date: 10/5/2023 6:38:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_ProductColor_ProductId] ON [dbo].[ProductColor]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Products_BrandId]    Script Date: 10/5/2023 6:38:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_Products_BrandId] ON [dbo].[Products]
(
	[BrandId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Products_CategoryId]    Script Date: 10/5/2023 6:38:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_Products_CategoryId] ON [dbo].[Products]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PurchaseProduct_PurchaseId]    Script Date: 10/5/2023 6:38:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_PurchaseProduct_PurchaseId] ON [dbo].[PurchaseProduct]
(
	[PurchaseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_SessionTokens_CartId]    Script Date: 10/5/2023 6:38:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_SessionTokens_CartId] ON [dbo].[SessionTokens]
(
	[CartId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_SessionTokens_UserId]    Script Date: 10/5/2023 6:38:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_SessionTokens_UserId] ON [dbo].[SessionTokens]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Carts]  WITH CHECK ADD  CONSTRAINT [FK_Carts_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Carts] CHECK CONSTRAINT [FK_Carts_Users_UserId]
GO
ALTER TABLE [dbo].[ProductColor]  WITH CHECK ADD  CONSTRAINT [FK_ProductColor_Colors_ColorsId] FOREIGN KEY([ColorsId])
REFERENCES [dbo].[Colors] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductColor] CHECK CONSTRAINT [FK_ProductColor_Colors_ColorsId]
GO
ALTER TABLE [dbo].[ProductColor]  WITH CHECK ADD  CONSTRAINT [FK_ProductColor_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductColor] CHECK CONSTRAINT [FK_ProductColor_Products_ProductId]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Brands_BrandId] FOREIGN KEY([BrandId])
REFERENCES [dbo].[Brands] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Brands_BrandId]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Categories_CategoryId]
GO
ALTER TABLE [dbo].[PurchaseProduct]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseProduct_Carts_PurchaseId] FOREIGN KEY([PurchaseId])
REFERENCES [dbo].[Carts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PurchaseProduct] CHECK CONSTRAINT [FK_PurchaseProduct_Carts_PurchaseId]
GO
ALTER TABLE [dbo].[PurchaseProduct]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseProduct_Products_ProductsId] FOREIGN KEY([ProductsId])
REFERENCES [dbo].[Products] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PurchaseProduct] CHECK CONSTRAINT [FK_PurchaseProduct_Products_ProductsId]
GO
ALTER TABLE [dbo].[SessionTokens]  WITH CHECK ADD  CONSTRAINT [FK_SessionTokens_Carts_CartId] FOREIGN KEY([CartId])
REFERENCES [dbo].[Carts] ([Id])
GO
ALTER TABLE [dbo].[SessionTokens] CHECK CONSTRAINT [FK_SessionTokens_Carts_CartId]
GO
ALTER TABLE [dbo].[SessionTokens]  WITH CHECK ADD  CONSTRAINT [FK_SessionTokens_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[SessionTokens] CHECK CONSTRAINT [FK_SessionTokens_Users_UserId]
GO
USE [master]
GO
ALTER DATABASE [DA2DB] SET  READ_WRITE 
GO
