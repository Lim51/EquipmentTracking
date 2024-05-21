USE [master]
GO
/****** Object:  Database [Equipment]    Script Date: 5/20/2024 5:12:22 PM ******/
CREATE DATABASE [Equipment]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Equipment', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Equipment.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Equipment_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Equipment_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Equipment] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Equipment].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Equipment] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Equipment] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Equipment] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Equipment] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Equipment] SET ARITHABORT OFF 
GO
ALTER DATABASE [Equipment] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Equipment] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Equipment] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Equipment] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Equipment] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Equipment] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Equipment] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Equipment] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Equipment] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Equipment] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Equipment] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Equipment] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Equipment] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Equipment] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Equipment] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Equipment] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Equipment] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Equipment] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Equipment] SET  MULTI_USER 
GO
ALTER DATABASE [Equipment] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Equipment] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Equipment] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Equipment] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Equipment] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Equipment] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Equipment] SET QUERY_STORE = ON
GO
ALTER DATABASE [Equipment] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Equipment]
GO
/****** Object:  User [CW01\uig60644]    Script Date: 5/20/2024 5:12:22 PM ******/
CREATE USER [CW01\uig60644] FOR LOGIN [CW01\uig60644] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [1222]    Script Date: 5/20/2024 5:12:22 PM ******/
CREATE USER [1222] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [CW01\uig60644]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [CW01\uig60644]
GO
ALTER ROLE [db_datareader] ADD MEMBER [1222]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [1222]
GO
/****** Object:  Table [dbo].[cable]    Script Date: 5/20/2024 5:12:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cable](
	[CableID] [int] IDENTITY(1,1) NOT NULL,
	[cables] [nvarchar](50) NOT NULL,
	[quantity] [int] NULL,
 CONSTRAINT [PK_cable_1] PRIMARY KEY CLUSTERED 
(
	[CableID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[docking_sys]    Script Date: 5/20/2024 5:12:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[docking_sys](
	[DockingID] [int] IDENTITY(1,1) NOT NULL,
	[Model] [nvarchar](50) NULL,
	[Code_SN] [nvarchar](50) NULL,
	[Received_date] [nvarchar](50) NULL,
	[Condition] [nvarchar](50) NULL,
	[Remarks] [nvarchar](50) NULL,
	[Owner] [nvarchar](50) NULL,
	[OwnerID] [int] NULL,
 CONSTRAINT [PK_docking_sys] PRIMARY KEY CLUSTERED 
(
	[DockingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[headphone]    Script Date: 5/20/2024 5:12:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[headphone](
	[hID] [int] IDENTITY(1,1) NOT NULL,
	[Model] [nvarchar](50) NULL,
	[Code_SN] [nvarchar](50) NULL,
	[Received_date] [nvarchar](50) NULL,
	[Condition] [nvarchar](50) NULL,
	[Remarks] [nvarchar](50) NULL,
	[Owner] [nvarchar](50) NULL,
	[OwnerID] [int] NULL,
 CONSTRAINT [PK_headphone] PRIMARY KEY CLUSTERED 
(
	[hID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[keyboard]    Script Date: 5/20/2024 5:12:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[keyboard](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Model] [nvarchar](50) NULL,
	[Code_SN] [nvarchar](50) NULL,
	[Received_date] [nvarchar](50) NULL,
	[Condition] [nvarchar](50) NULL,
	[Remarks] [nvarchar](50) NULL,
	[Owner] [nvarchar](50) NULL,
	[OwnerID] [int] NULL,
 CONSTRAINT [PK_keyboard] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[laptop]    Script Date: 5/20/2024 5:12:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[laptop](
	[LaptopID] [int] IDENTITY(1,1) NOT NULL,
	[Model] [nvarchar](50) NULL,
	[Code_SN] [nvarchar](50) NULL,
	[Received_date] [nvarchar](50) NULL,
	[Condition] [nvarchar](50) NULL,
	[Remarks] [nvarchar](50) NULL,
	[Owner] [nvarchar](50) NULL,
	[OwnerID] [int] NULL,
 CONSTRAINT [PK_laptop] PRIMARY KEY CLUSTERED 
(
	[LaptopID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[monitor]    Script Date: 5/20/2024 5:12:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[monitor](
	[MonitorID] [int] IDENTITY(1,1) NOT NULL,
	[Model] [nvarchar](50) NULL,
	[Code_SN] [nvarchar](50) NULL,
	[Received_date] [nvarchar](50) NULL,
	[Condition] [nvarchar](50) NULL,
	[Remarks] [nvarchar](50) NULL,
	[Owner] [nvarchar](50) NULL,
	[OwnerID] [int] NULL,
 CONSTRAINT [PK_monitor] PRIMARY KEY CLUSTERED 
(
	[MonitorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mouse]    Script Date: 5/20/2024 5:12:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mouse](
	[MouseID] [int] IDENTITY(1,1) NOT NULL,
	[Model] [nvarchar](50) NULL,
	[Code_SN] [nvarchar](50) NULL,
	[Received_date] [nvarchar](50) NULL,
	[Condition] [nvarchar](50) NULL,
	[Remarks] [nvarchar](50) NULL,
	[Owner] [nvarchar](50) NULL,
	[OwnerID] [int] NULL,
 CONSTRAINT [PK_mouse] PRIMARY KEY CLUSTERED 
(
	[MouseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[owner]    Script Date: 5/20/2024 5:12:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[owner](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Owner] [nvarchar](50) NULL,
 CONSTRAINT [PK_owner] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[try]    Script Date: 5/20/2024 5:12:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[try](
	[id] [int] NOT NULL,
 CONSTRAINT [PK_try] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 5/20/2024 5:12:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userID] [nvarchar](50) NOT NULL,
	[username] [nvarchar](50) NOT NULL,
	[password] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[cable] ON 

INSERT [dbo].[cable] ([CableID], [cables], [quantity]) VALUES (1, N'2 Pin Male Power Cable                            ', 3)
INSERT [dbo].[cable] ([CableID], [cables], [quantity]) VALUES (2, N'Audio Cable                                       ', 1)
INSERT [dbo].[cable] ([CableID], [cables], [quantity]) VALUES (3, N'C5 Power Cord                                     ', 8)
INSERT [dbo].[cable] ([CableID], [cables], [quantity]) VALUES (4, N'Display Port                                      ', 5)
INSERT [dbo].[cable] ([CableID], [cables], [quantity]) VALUES (5, N'Extension Socket                                  ', 1)
INSERT [dbo].[cable] ([CableID], [cables], [quantity]) VALUES (6, N'HDMI                                              ', 0)
INSERT [dbo].[cable] ([CableID], [cables], [quantity]) VALUES (7, N'HP Dual Head Keyed Cable Lock                     ', 3)
INSERT [dbo].[cable] ([CableID], [cables], [quantity]) VALUES (8, N'HP Sure Key Cable Lock                            ', 2)
INSERT [dbo].[cable] ([CableID], [cables], [quantity]) VALUES (9, N'HP Thunderbolt Docking Power Cable                ', 1)
INSERT [dbo].[cable] ([CableID], [cables], [quantity]) VALUES (10, N'LAN Cable                                         ', 0)
INSERT [dbo].[cable] ([CableID], [cables], [quantity]) VALUES (11, N'Laptop Battery                                    ', 8)
INSERT [dbo].[cable] ([CableID], [cables], [quantity]) VALUES (12, N'Monitor Cable                                     ', 1)
INSERT [dbo].[cable] ([CableID], [cables], [quantity]) VALUES (13, N'USB type A to B                                   ', 10)
INSERT [dbo].[cable] ([CableID], [cables], [quantity]) VALUES (14, N'USB type A to C                                   ', 4)
INSERT [dbo].[cable] ([CableID], [cables], [quantity]) VALUES (15, N'UTP (Ethernet cable)                              ', 2)
INSERT [dbo].[cable] ([CableID], [cables], [quantity]) VALUES (16, N'VGA                                               ', 6)
INSERT [dbo].[cable] ([CableID], [cables], [quantity]) VALUES (17, N'try123', 12)
SET IDENTITY_INSERT [dbo].[cable] OFF
GO
SET IDENTITY_INSERT [dbo].[docking_sys] ON 

INSERT [dbo].[docking_sys] ([DockingID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (1, N'HP 2013 UltraSlim Docking                         ', N'5CG845WB2T                                        ', NULL, N'Good                                              ', NULL, N'Bernard                                           ', 2)
INSERT [dbo].[docking_sys] ([DockingID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (2, N'DELL 7-in-1 Multiport Adapter                     ', N'10828617E53F                                      ', NULL, N'Good                                              ', N'New                                               ', N'Rozmadi                                           ', 3)
INSERT [dbo].[docking_sys] ([DockingID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (3, N'HP 2013 UltraSlim Docking                         ', N'5CG913XLRQ                                        ', NULL, N'Good                                              ', NULL, N'Yen Ling                                          ', 1)
INSERT [dbo].[docking_sys] ([DockingID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (4, N'HP 2013 UltraSlim Docking                         ', N'5CG932ZSND                                        ', NULL, N'Good                                              ', NULL, N'Phooi San                                         ', 5)
INSERT [dbo].[docking_sys] ([DockingID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (5, N'HP USB-C Dock G5                                  ', N'5CG152XHQY                                        ', N'2022-10-14                                        ', N'Good                                              ', NULL, N'Meng Yiau                                         ', 4)
INSERT [dbo].[docking_sys] ([DockingID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (6, N'HP USB-C Dock G5                                  ', N'5CG304WZTM                                        ', N'2023-03-21                                        ', N'Good                                              ', N'New                                               ', N'Syakir                                            ', 7)
INSERT [dbo].[docking_sys] ([DockingID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (7, N'HP 2013 UltraSlim Docking                         ', N'5CG820W2H9                                        ', NULL, N'Good                                              ', N'from sir Jasni                                    ', NULL, NULL)
INSERT [dbo].[docking_sys] ([DockingID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (8, N'HP Z Book Thunderbolt 3 Dock                      ', N'CND6293HLW                                        ', NULL, N'Good                                              ', NULL, NULL, NULL)
INSERT [dbo].[docking_sys] ([DockingID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (9, N'HP Z Book Thunderbolt 3 Dock                      ', N'CND8288KZ0                                        ', NULL, N'Good                                              ', N'from sir Roz                                      ', NULL, NULL)
INSERT [dbo].[docking_sys] ([DockingID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (10, N'HP 2013 UltraSlim Docking                         ', N'5CG845WB0V                                        ', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[docking_sys] ([DockingID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (11, N'HP 2013 UltraSlim Docking                         ', N'5CG829XY1Q                                        ', NULL, N'Good                                              ', NULL, N'Xin Hui                                           ', 8)
INSERT [dbo].[docking_sys] ([DockingID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (12, N'HP Z Book Thunderbolt 3 Dock                      ', NULL, NULL, N'Good                                              ', NULL, NULL, NULL)
INSERT [dbo].[docking_sys] ([DockingID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (13, N'try', N'-', NULL, N'', N'-', N'-', NULL)
INSERT [dbo].[docking_sys] ([DockingID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (14, N'new123', N'dejdhusd', N'2024-03-30', N'Good', N'-', N'-', NULL)
SET IDENTITY_INSERT [dbo].[docking_sys] OFF
GO
SET IDENTITY_INSERT [dbo].[headphone] ON 

INSERT [dbo].[headphone] ([hID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (1, N'Jabra HSC016                                      ', N'00272131561                                       ', N'2022-09-01                                        ', N'Good                                              ', NULL, N'Yen Ling                                          ', 1)
INSERT [dbo].[headphone] ([hID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (2, N'Logitech A-00091                                  ', N'1833GXK05608                                      ', NULL, N'Good                                              ', NULL, N'Bernard                                           ', 2)
INSERT [dbo].[headphone] ([hID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (3, N'Jabra HSC016                                      ', N'00272514882                                       ', N'2022-09-01                                        ', N'Good                                              ', NULL, N'Bernard                                           ', 2)
INSERT [dbo].[headphone] ([hID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (4, N'Logitech A-00091                                  ', NULL, NULL, N'Good                                              ', NULL, N'Phooi San                                         ', 5)
INSERT [dbo].[headphone] ([hID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (5, N'Jabra HSC016                                      ', N'00272131550                                       ', N'2022-09-01                                        ', N'Good                                              ', NULL, N'Huey Jing                                         ', 6)
INSERT [dbo].[headphone] ([hID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (6, N'CLiPtec                                           ', N'20070799                                          ', NULL, N'Good                                              ', NULL, N'Huey Jing                                         ', 6)
INSERT [dbo].[headphone] ([hID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (7, N'Logitech A-00044                                  ', N'1826ALC77988                                      ', NULL, N'Good                                              ', NULL, N'Rozmadi                                           ', 3)
INSERT [dbo].[headphone] ([hID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (8, N'Jabra HSC016                                      ', N'00272514864                                       ', N'2022-09-01                                        ', N'Good                                              ', NULL, N'Rozmadi                                           ', 3)
INSERT [dbo].[headphone] ([hID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (9, N'Logitech                                          ', N'2009ALK00W08                                      ', NULL, N'Good                                              ', NULL, N'Meng Yiau                                         ', 4)
INSERT [dbo].[headphone] ([hID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (10, N'Jabra HSC016                                      ', N'00272514877                                       ', N'2022-09-01                                        ', N'Good                                              ', NULL, N'Chong Xin Hui                                     ', 8)
INSERT [dbo].[headphone] ([hID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (11, N'Jabra HSC016                                      ', N'00272519621                                       ', N'2022-09-01                                        ', N'Good                                              ', NULL, N'Ching Yee                                         ', 9)
INSERT [dbo].[headphone] ([hID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (12, N'Jabra HSC016                                      ', N'00279756969                                       ', N'2022-04-11                                        ', N'Good                                              ', N'New                                               ', N'Meng Yiau                                         ', 4)
INSERT [dbo].[headphone] ([hID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (13, N'Jabra HSC016                                      ', N'00272520609                                       ', NULL, N'Good                                              ', N'New                                               ', N'Jie You                                           ', 10)
INSERT [dbo].[headphone] ([hID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (14, N'Logitech A-00044                                  ', N'1721ALC51478                                      ', NULL, N'Bad                                               ', N'mic not functioning                               ', NULL, NULL)
INSERT [dbo].[headphone] ([hID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (15, N'CLiPtec BBH506                                    ', NULL, NULL, N'Good                                              ', N'Grey colour                                       ', NULL, NULL)
INSERT [dbo].[headphone] ([hID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (19, N'treeee', N'-', N'2024-04-22', N'Good', N'-', N'-', NULL)
SET IDENTITY_INSERT [dbo].[headphone] OFF
GO
SET IDENTITY_INSERT [dbo].[keyboard] ON 

INSERT [dbo].[keyboard] ([ID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (1, N'HP KBAR211                                        ', N'BEXHP0BTJBLJG                                     ', NULL, N'Good                                              ', N'From IT Soon Li                                   ', N'Meng Yiau', NULL)
INSERT [dbo].[keyboard] ([ID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (2, N'HP KBAR211                                        ', N'BEXHP0BTJB9LNX                                    ', NULL, N'Good                                              ', N'From IT Soon Li                                   ', N'Xin Hui                                           ', 8)
SET IDENTITY_INSERT [dbo].[keyboard] OFF
GO
SET IDENTITY_INSERT [dbo].[laptop] ON 

INSERT [dbo].[laptop] ([LaptopID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (1, N'Dell Notebook                                     ', N'PYL2583W                                          ', N'2022                                              ', N'Good                                              ', N'PO#5810472699                                     ', N'Bernard                                           ', 2)
INSERT [dbo].[laptop] ([LaptopID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (2, N'Dell Notebook                                     ', N'PYL2583W                                          ', N'2022                                              ', N'Good                                              ', N'PO#5810472699                                     ', N'Rozmadi                                           ', 3)
INSERT [dbo].[laptop] ([LaptopID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (3, N'HP Elitebook                                      ', N'PYL2437W                                          ', N'2020                                              ', N'Good                                              ', N'PO#5803407368                                     ', N'Yen Ling                                          ', 1)
INSERT [dbo].[laptop] ([LaptopID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (4, N'HP Elitebook                                      ', N'PYL2430W                                          ', N'2020                                              ', N'Good                                              ', N'PO#5803389137                                     ', N'Phooi San                                         ', 5)
INSERT [dbo].[laptop] ([LaptopID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (5, N'HP Elitebook                                      ', N'PYL2514W                                          ', N'2021                                              ', N'Good                                              ', N'Used by Ching Yee. PO#5804126780                  ', N'Huey Jing                                         ', 6)
INSERT [dbo].[laptop] ([LaptopID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (6, N'HP Elitebook                                      ', N'PYL2513W                                          ', N'2021                                              ', N'Good                                              ', N'PO#5804126780                                     ', N'Meng Yiau                                         ', 4)
INSERT [dbo].[laptop] ([LaptopID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (7, N'Dell Notebook                                     ', N'PYL2604W                                          ', N'2022                                              ', N'Good                                              ', N'PO#5810482201                                     ', N'Syakir                                            ', 7)
INSERT [dbo].[laptop] ([LaptopID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (8, N'HP Zbook                                          ', N'PYL2689W                                          ', N'2023                                              ', N'Good', N'New. PO#5810527827                                ', N'Phooi San', 5)
INSERT [dbo].[laptop] ([LaptopID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (9, N'HP Elitebook                                      ', N'PYL2359W                                          ', NULL, N'', N'Keyboard malfunction. PO#5803081025               ', N'                                         ', 8)
INSERT [dbo].[laptop] ([LaptopID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (10, N'HP ZBook                                          ', N'PYL2298W                                          ', NULL, NULL, N'Charge all time, cannot login. At Cubicle 4. PO#58', NULL, NULL)
INSERT [dbo].[laptop] ([LaptopID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (11, N'HP ZBook                                          ', N'PYL2253W                                          ', NULL, N'Good                                              ', N'Bottom rubber melt. PO#5803026659                 ', NULL, NULL)
INSERT [dbo].[laptop] ([LaptopID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (12, N'HP Elitebook                                      ', N'PYL2319W                                          ', NULL, N'Good                                              ', N'Charge all time. PO#5802970463                    ', N'Jie You                                           ', 10)
INSERT [dbo].[laptop] ([LaptopID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (13, N'HP Elitebook                                      ', N'PYL2218W                                          ', NULL, NULL, N'Screen displays single green line, cannot login.At', NULL, NULL)
INSERT [dbo].[laptop] ([LaptopID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (14, N'HP Zbook                                          ', N'PYL2066W                                          ', N'2016                                              ', NULL, N'Battery slight swell. PO#4791051939               ', N'N/A                                               ', NULL)
INSERT [dbo].[laptop] ([LaptopID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (15, N'wqwqw', N'-', N'2000', N'', N'-', N'-', NULL)
INSERT [dbo].[laptop] ([LaptopID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (16, N'TestmodelName', N'', N'2024', N'Good', N'', N'', NULL)
INSERT [dbo].[laptop] ([LaptopID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (17, N'fff', N'-', N'2024', N'Good', N'-', N'-', NULL)
INSERT [dbo].[laptop] ([LaptopID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (18, N'trying', N'-', N'2024', N'Bad', N'-', N'-', NULL)
INSERT [dbo].[laptop] ([LaptopID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (19, N'HP Laptop ZBook', N'', N'2024', N'Good', N'', N'', NULL)
SET IDENTITY_INSERT [dbo].[laptop] OFF
GO
SET IDENTITY_INSERT [dbo].[monitor] ON 

INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (1, N'Elite E190i                                       ', N'CN45510HNN                                        ', NULL, N'Good                                              ', NULL, N'Bernard                                           ', 2)
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (2, N'HP E24iG4                                         ', N'CNK1081WCH                                        ', NULL, N'Good                                              ', NULL, N'Bernard                                           ', 2)
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (3, N'HP E243i                                          ', N'6CM82520H7                                        ', NULL, N'Good                                              ', NULL, N'Bernard                                           ', 2)
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (4, N'HP E233                                           ', N'HSTND-9571A                                       ', NULL, N'Good                                              ', N'Home                                              ', N'Rozmadi                                           ', 3)
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (5, N'HP E243i                                          ', N'CNK8331J5R                                        ', NULL, N'Good                                              ', N'Office                                            ', N'Rozmadi                                           ', 3)
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (6, N'HP Z24                                            ', N'6CM00508L0                                        ', NULL, N'Good                                              ', N'Home                                              ', N'Yen Ling                                          ', 1)
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (7, N'HP Z24                                            ', N'6CM00508L3                                        ', NULL, N'Good                                              ', N'Office                                            ', N'Yen Ling                                          ', 1)
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (8, N'HP Z24                                            ', N'6CM00508L5                                        ', NULL, N'Good                                              ', N'Office                                            ', N'Yen Ling                                          ', 1)
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (9, N'HP Z24                                            ', N'6CM00508MC                                        ', NULL, N'Good                                              ', N'Home                                              ', N'Phooi San                                         ', 5)
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (10, N'HP Z24                                            ', N'6CM00508L6                                        ', NULL, N'Good                                              ', N'Home                                              ', N'Phooi San                                         ', 5)
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (11, N'HP Z24                                            ', N'6CM00508L7                                        ', NULL, N'Good                                              ', N'Office                                            ', N'Phooi San                                         ', 5)
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (12, N'HP Z24                                            ', N'6CM00508F2                                        ', NULL, N'Good                                              ', N'Home                                              ', N'Huey Jing                                         ', 6)
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (13, N'HP E24i G4                                        ', N'CNK1081WC7                                        ', NULL, N'Good                                              ', N'S3 Room                                           ', N'Huey Jing                                         ', 6)
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (14, N'HP E243i                                          ', N'CNC8372J5J                                        ', NULL, N'Good                                              ', N'Home                                              ', N'Meng Yiau                                         ', 4)
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (15, N'HP E243i                                          ', N'CNK8290V07                                        ', NULL, N'Good                                              ', N'Office                                            ', N'Meng Yiau                                         ', 4)
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (16, N'HP E24i G4                                        ', N'CNK1081WC2                                        ', NULL, N'Good                                              ', N'Home                                              ', N'Meng Yiau                                         ', 4)
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (17, N'HP Z24N G2                                        ', N'6CM00508MB                                        ', NULL, N'Good                                              ', N'Office                                            ', N'Syakir                                            ', 7)
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (18, N'HP E24q G4 (24'''')                                 ', N'CNK2390RZW                                        ', N'2023-03-08                                        ', N'New                                               ', NULL, N'Syakir                                            ', 7)
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (19, N'HP E243i                                          ', N'6CM8491FSQ                                        ', NULL, N'Good                                              ', N'Office                                            ', N'Ching Yee                                         ', 9)
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (20, N'HP E24i G4                                        ', N'CNK1081WC3                                        ', NULL, N'Good                                              ', N'Office                                            ', N'Xin Hui                                           ', 8)
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (21, N'HP E243i                                          ', N'CNK8331J4J                                        ', NULL, N'Good                                              ', N'Office (Power cord from Jason)                    ', N'Jie You                                           ', 10)
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (22, N'HP Compaq (19'''')                                  ', N'3CQ3460SBX                                        ', NULL, N'Good                                              ', N'Store Room                                        ', NULL, NULL)
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (23, N'HP E24i G4 (24'''')                                 ', N'CNK24719T                                         ', N'2023-03-08                                        ', N'Good                                              ', N'New                                               ', NULL, NULL)
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (24, N'HP E24i G4 (24'''')                                 ', N'CNK24716SH                                        ', N'2023-03-21                                        ', N'Good                                              ', N'New                                               ', NULL, NULL)
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (25, N'AcerTry123', N'rreerer', N'2024-04-01', N'', N'-', N'-', 1)
SET IDENTITY_INSERT [dbo].[monitor] OFF
GO
SET IDENTITY_INSERT [dbo].[mouse] ON 

INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (1, N'MSU-1158                                          ', N'-', NULL, N'', N'-', N'Bernard   ', 2)
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (2, N'MSU-1158                                          ', NULL, NULL, N'Good                                              ', NULL, N'Rozmadi   ', 3)
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (3, N'HP                                                ', NULL, NULL, N'Good                                              ', NULL, N'Yen Ling  ', 1)
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (4, N'Logitech M185                                     ', N'2225LZX6TZF9                                      ', N'2022-09-01                                        ', N'Good                                              ', NULL, N'Phooi San ', 5)
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (5, N'HP                                                ', N'FCMHL0EW2BC2TF                                    ', NULL, N'Good                                              ', NULL, N'Huey Jing ', 6)
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (6, N'HP                                                ', N'QY777AA                                           ', NULL, N'Good                                              ', NULL, N'Meng Yiau ', 4)
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (7, N'HP Wired Desktop 320M Mouse                       ', N'9CP045XG3T                                        ', NULL, N'Good                                              ', N'New                                               ', N'Syakir    ', 7)
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (8, N'HP Wired Desktop 320M Mouse                       ', N'9CP045XG3M                                        ', NULL, N'Good                                              ', N'New                                               ', N'Yen Ling  ', 1)
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (9, N'Logitech M185                                     ', N'2225LZXBSUD8                                      ', N'2022-09-01', N'', N'New                                               ', N'Xin Hui', 8)
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (10, N'Logitech M185                                     ', N'2225LZX9DKS8                                      ', N'2022-09-01                                        ', N'Good                                              ', N'New                                               ', N'Jie You   ', 10)
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (11, N'HP MOFYUO                                         ', N'FCMHH0AHDAETT6                                    ', NULL, N'Good                                              ', N'New                                               ', NULL, NULL)
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (12, N'Logitech M185                                     ', NULL, N'2022-11-04                                        ', N'Good                                              ', N'claimed by PS (New)                               ', NULL, NULL)
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (13, N'HP MSU1158                                        ', N'FCMHL0EW2ANHGM                                    ', NULL, N'Good                                              ', NULL, NULL, NULL)
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (14, N'HP MSU1158                                        ', N'FCMHL0EW2B4ENT                                    ', NULL, N'Good                                              ', NULL, NULL, NULL)
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (15, N'Logitech M100r                                    ', N'1529HS013618                                      ', NULL, N'Good                                              ', N'Old                                               ', NULL, NULL)
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (16, N'HP MOFYUO                                         ', N'FCMHH0AHD414P3                                    ', NULL, N'Good                                              ', N'Old                                               ', NULL, NULL)
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (17, N'Logitech M100r                                    ', N'1819HS01FJ48                                      ', NULL, N'Good                                              ', N'Old                                               ', NULL, NULL)
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (18, N'HP 125 Wired Mouse                                ', N'9CP2415NRD                                        ', N'2023-03-21                                        ', N'Good                                              ', N'New (Missing)                                     ', NULL, NULL)
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (19, N'Logitech M185                                     ', N'2053LZX2PEU8                                      ', N'2022-09-01                                        ', N'Good                                              ', N'No battery                                        ', NULL, NULL)
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (20, N'HP MOHQUO                                         ', N'7CF53400Z7                                        ', NULL, N'Good                                              ', NULL, NULL, NULL)
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (21, N'HP SM-2022                                        ', N'FCMHH0CZD4BPC                                     ', N'2020-03-09                                        ', N'Good                                              ', NULL, N'Ching Yee ', 9)
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (22, N'Logitech M185                                     ', NULL, N'2022-02-11                                        ', N'Good                                              ', N'New (No yet open)                                 ', NULL, NULL)
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [OwnerID]) VALUES (24, N'try1223323', N'10CP045XG3R                ', N'2024-05-02', N'Good', N'The mouse was stored in X''s cubicle.', N'-', 1)
SET IDENTITY_INSERT [dbo].[mouse] OFF
GO
SET IDENTITY_INSERT [dbo].[owner] ON 

INSERT [dbo].[owner] ([ID], [Owner]) VALUES (1, N'Yen Ling                                          ')
INSERT [dbo].[owner] ([ID], [Owner]) VALUES (2, N'Bernard                                           ')
INSERT [dbo].[owner] ([ID], [Owner]) VALUES (3, N'Rozmadi                                           ')
INSERT [dbo].[owner] ([ID], [Owner]) VALUES (4, N'Meng Yiau                                        ')
INSERT [dbo].[owner] ([ID], [Owner]) VALUES (5, N'Phooi San                                        ')
INSERT [dbo].[owner] ([ID], [Owner]) VALUES (6, N'Huey Jing                                        ')
INSERT [dbo].[owner] ([ID], [Owner]) VALUES (7, N'Syakir                                            ')
INSERT [dbo].[owner] ([ID], [Owner]) VALUES (8, N'Xin Hui                                           ')
INSERT [dbo].[owner] ([ID], [Owner]) VALUES (9, N'Ching Yee                                         ')
INSERT [dbo].[owner] ([ID], [Owner]) VALUES (10, N'Jie You
                                         ')
SET IDENTITY_INSERT [dbo].[owner] OFF
GO
INSERT [dbo].[try] ([id]) VALUES (1)
INSERT [dbo].[try] ([id]) VALUES (101)
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([id], [userID], [username], [password]) VALUES (1, N'uig60644', N'Jie You', N'Conti12345')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[docking_sys]  WITH CHECK ADD  CONSTRAINT [FK_docking_sys_owner] FOREIGN KEY([OwnerID])
REFERENCES [dbo].[owner] ([ID])
GO
ALTER TABLE [dbo].[docking_sys] CHECK CONSTRAINT [FK_docking_sys_owner]
GO
ALTER TABLE [dbo].[headphone]  WITH CHECK ADD  CONSTRAINT [FK_headphone_owner] FOREIGN KEY([OwnerID])
REFERENCES [dbo].[owner] ([ID])
GO
ALTER TABLE [dbo].[headphone] CHECK CONSTRAINT [FK_headphone_owner]
GO
ALTER TABLE [dbo].[keyboard]  WITH CHECK ADD  CONSTRAINT [FK_keyboard_owner] FOREIGN KEY([OwnerID])
REFERENCES [dbo].[owner] ([ID])
GO
ALTER TABLE [dbo].[keyboard] CHECK CONSTRAINT [FK_keyboard_owner]
GO
ALTER TABLE [dbo].[laptop]  WITH CHECK ADD  CONSTRAINT [FK_laptop_owner] FOREIGN KEY([OwnerID])
REFERENCES [dbo].[owner] ([ID])
GO
ALTER TABLE [dbo].[laptop] CHECK CONSTRAINT [FK_laptop_owner]
GO
ALTER TABLE [dbo].[monitor]  WITH CHECK ADD  CONSTRAINT [FK_monitor_owner] FOREIGN KEY([OwnerID])
REFERENCES [dbo].[owner] ([ID])
GO
ALTER TABLE [dbo].[monitor] CHECK CONSTRAINT [FK_monitor_owner]
GO
ALTER TABLE [dbo].[mouse]  WITH CHECK ADD  CONSTRAINT [FK_mouse_owner] FOREIGN KEY([OwnerID])
REFERENCES [dbo].[owner] ([ID])
GO
ALTER TABLE [dbo].[mouse] CHECK CONSTRAINT [FK_mouse_owner]
GO
USE [master]
GO
ALTER DATABASE [Equipment] SET  READ_WRITE 
GO
