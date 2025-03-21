USE [master]
GO
/****** Object:  Database [WalletApiPayPhone]    Script Date: 16/03/2025 04:58:50 a. m. ******/
CREATE DATABASE [WalletApiPayPhone]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'WalletApiPayPhone', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\WalletApiPayPhone.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'WalletApiPayPhone_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\WalletApiPayPhone_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [WalletApiPayPhone] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [WalletApiPayPhone].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [WalletApiPayPhone] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [WalletApiPayPhone] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [WalletApiPayPhone] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [WalletApiPayPhone] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [WalletApiPayPhone] SET ARITHABORT OFF 
GO
ALTER DATABASE [WalletApiPayPhone] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [WalletApiPayPhone] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [WalletApiPayPhone] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [WalletApiPayPhone] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [WalletApiPayPhone] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [WalletApiPayPhone] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [WalletApiPayPhone] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [WalletApiPayPhone] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [WalletApiPayPhone] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [WalletApiPayPhone] SET  DISABLE_BROKER 
GO
ALTER DATABASE [WalletApiPayPhone] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [WalletApiPayPhone] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [WalletApiPayPhone] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [WalletApiPayPhone] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [WalletApiPayPhone] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [WalletApiPayPhone] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [WalletApiPayPhone] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [WalletApiPayPhone] SET RECOVERY FULL 
GO
ALTER DATABASE [WalletApiPayPhone] SET  MULTI_USER 
GO
ALTER DATABASE [WalletApiPayPhone] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [WalletApiPayPhone] SET DB_CHAINING OFF 
GO
ALTER DATABASE [WalletApiPayPhone] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [WalletApiPayPhone] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [WalletApiPayPhone] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [WalletApiPayPhone] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'WalletApiPayPhone', N'ON'
GO
ALTER DATABASE [WalletApiPayPhone] SET QUERY_STORE = OFF
GO
USE [WalletApiPayPhone]
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 16/03/2025 04:58:51 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[WalletId] [int] NULL,
	[Name] [varchar](50) NULL,
	[Amount] [decimal](16, 2) NULL,
	[Type] [varchar](10) NULL,
	[CreatedAt] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 16/03/2025 04:58:51 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Wallets]    Script Date: 16/03/2025 04:58:51 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Wallets](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DocumentId] [varchar](50) NULL,
	[Name] [varchar](50) NULL,
	[Balance] [decimal](16, 2) NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
	[UserId] [int] NULL,
	[Deleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Transactions] ON 

INSERT [dbo].[Transactions] ([Id], [WalletId], [Name], [Amount], [Type], [CreatedAt]) VALUES (1, 4, NULL, CAST(100.00 AS Decimal(16, 2)), N'0', CAST(N'2025-03-16T09:36:35.460' AS DateTime))
INSERT [dbo].[Transactions] ([Id], [WalletId], [Name], [Amount], [Type], [CreatedAt]) VALUES (2, 4, NULL, CAST(100.00 AS Decimal(16, 2)), N'1', CAST(N'2025-03-16T09:37:54.597' AS DateTime))
INSERT [dbo].[Transactions] ([Id], [WalletId], [Name], [Amount], [Type], [CreatedAt]) VALUES (3, 4, NULL, CAST(50.00 AS Decimal(16, 2)), N'1', CAST(N'2025-03-16T09:56:15.890' AS DateTime))
SET IDENTITY_INSERT [dbo].[Transactions] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [UserName], [Password]) VALUES (1, N'Stefano', N'Stefano123')
INSERT [dbo].[Users] ([Id], [UserName], [Password]) VALUES (2, N'Luis Quiros', N'Luis123')
INSERT [dbo].[Users] ([Id], [UserName], [Password]) VALUES (3, N'Kevin', N'Kevin123')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[Wallets] ON 

INSERT [dbo].[Wallets] ([Id], [DocumentId], [Name], [Balance], [CreatedAt], [UpdatedAt], [UserId], [Deleted]) VALUES (4, N'115820155', N'Stefano Wallet', CAST(50.00 AS Decimal(16, 2)), CAST(N'2025-03-16T09:01:49.810' AS DateTime), CAST(N'2025-03-16T09:02:04.653' AS DateTime), 1, 0)
INSERT [dbo].[Wallets] ([Id], [DocumentId], [Name], [Balance], [CreatedAt], [UpdatedAt], [UserId], [Deleted]) VALUES (5, N'115820156', N'Luis Wallet', CAST(200.00 AS Decimal(16, 2)), CAST(N'2025-03-16T09:01:49.810' AS DateTime), NULL, 2, 0)
INSERT [dbo].[Wallets] ([Id], [DocumentId], [Name], [Balance], [CreatedAt], [UpdatedAt], [UserId], [Deleted]) VALUES (6, N'115820157', N'Kevin Wallet', CAST(50.00 AS Decimal(16, 2)), CAST(N'2025-03-16T09:01:49.810' AS DateTime), NULL, 3, 0)
INSERT [dbo].[Wallets] ([Id], [DocumentId], [Name], [Balance], [CreatedAt], [UpdatedAt], [UserId], [Deleted]) VALUES (7, N'12345678', N'Stefano Wallet 2', CAST(500.00 AS Decimal(16, 2)), CAST(N'2025-03-16T10:11:54.107' AS DateTime), NULL, 1, 0)
SET IDENTITY_INSERT [dbo].[Wallets] OFF
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK__Transactions_Wallets] FOREIGN KEY([WalletId])
REFERENCES [dbo].[Wallets] ([Id])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK__Transactions_Wallets]
GO
ALTER TABLE [dbo].[Wallets]  WITH CHECK ADD  CONSTRAINT [FK__Wallets_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Wallets] CHECK CONSTRAINT [FK__Wallets_Users]
GO
USE [master]
GO
ALTER DATABASE [WalletApiPayPhone] SET  READ_WRITE 
GO
