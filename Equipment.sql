USE [master]
GO
/****** Object:  Database [Equipment]    Script Date: 5/23/2024 4:53:40 PM ******/
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
/****** Object:  User [CW01\uig60644]    Script Date: 5/23/2024 4:53:40 PM ******/
CREATE USER [CW01\uig60644] FOR LOGIN [CW01\uig60644] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [1222]    Script Date: 5/23/2024 4:53:40 PM ******/
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
/****** Object:  Table [dbo].[cable]    Script Date: 5/23/2024 4:53:40 PM ******/
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
/****** Object:  Table [dbo].[docking_history]    Script Date: 5/23/2024 4:53:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[docking_history](
	[HistoryID] [int] IDENTITY(1,1) NOT NULL,
	[DockingID] [int] NOT NULL,
	[Model] [nvarchar](50) NULL,
	[Code_SN] [nvarchar](50) NULL,
	[Received_date] [nvarchar](50) NULL,
	[Condition] [nvarchar](50) NULL,
	[Remarks] [nvarchar](50) NULL,
	[Owner] [nvarchar](50) NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[UpdatedAt] [datetime] NULL,
 CONSTRAINT [PK_docking_history] PRIMARY KEY CLUSTERED 
(
	[HistoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[docking_sys]    Script Date: 5/23/2024 4:53:40 PM ******/
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
 CONSTRAINT [PK_docking_sys] PRIMARY KEY CLUSTERED 
(
	[DockingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[headphone]    Script Date: 5/23/2024 4:53:40 PM ******/
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
 CONSTRAINT [PK_headphone] PRIMARY KEY CLUSTERED 
(
	[hID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[headphone_history]    Script Date: 5/23/2024 4:53:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[headphone_history](
	[HistoryID] [int] IDENTITY(1,1) NOT NULL,
	[hID] [int] NOT NULL,
	[Model] [nvarchar](50) NULL,
	[Code_SN] [nvarchar](50) NULL,
	[Received_date] [nvarchar](50) NULL,
	[Condition] [nvarchar](50) NULL,
	[Remarks] [nvarchar](50) NULL,
	[Owner] [nvarchar](50) NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[UpdatedAt] [datetime] NULL,
 CONSTRAINT [PK_headphone_history] PRIMARY KEY CLUSTERED 
(
	[HistoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[keyboard]    Script Date: 5/23/2024 4:53:40 PM ******/
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
 CONSTRAINT [PK_keyboard] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[laptop]    Script Date: 5/23/2024 4:53:40 PM ******/
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
 CONSTRAINT [PK_laptop] PRIMARY KEY CLUSTERED 
(
	[LaptopID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[laptop_history]    Script Date: 5/23/2024 4:53:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[laptop_history](
	[HistoryID] [int] IDENTITY(1,1) NOT NULL,
	[LaptopID] [int] NOT NULL,
	[Model] [nvarchar](50) NULL,
	[Code_SN] [nvarchar](50) NULL,
	[Received_date] [nvarchar](50) NULL,
	[Condition] [nvarchar](50) NULL,
	[Remarks] [nvarchar](50) NULL,
	[Owner] [nvarchar](50) NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[UpdatedAt] [datetime] NULL,
 CONSTRAINT [PK_laptop_history] PRIMARY KEY CLUSTERED 
(
	[HistoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[monitor]    Script Date: 5/23/2024 4:53:40 PM ******/
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
 CONSTRAINT [PK_monitor] PRIMARY KEY CLUSTERED 
(
	[MonitorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[monitor_history]    Script Date: 5/23/2024 4:53:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[monitor_history](
	[HistoryID] [int] IDENTITY(1,1) NOT NULL,
	[MonitorID] [int] NOT NULL,
	[Model] [nvarchar](50) NULL,
	[Code_SN] [nvarchar](50) NULL,
	[Received_date] [nvarchar](50) NULL,
	[Condition] [nvarchar](50) NULL,
	[Remarks] [nvarchar](50) NULL,
	[Owner] [nvarchar](50) NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[UpdatedAt] [datetime] NULL,
 CONSTRAINT [PK_monitor_history] PRIMARY KEY CLUSTERED 
(
	[HistoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mouse]    Script Date: 5/23/2024 4:53:40 PM ******/
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
 CONSTRAINT [PK_mouse] PRIMARY KEY CLUSTERED 
(
	[MouseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mouse_history]    Script Date: 5/23/2024 4:53:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mouse_history](
	[HistoryID] [int] IDENTITY(1,1) NOT NULL,
	[MouseID] [int] NOT NULL,
	[Model] [nvarchar](50) NULL,
	[Code_SN] [nvarchar](50) NULL,
	[Received_date] [nvarchar](50) NULL,
	[Condition] [nvarchar](50) NULL,
	[Remarks] [nvarchar](50) NULL,
	[Owner] [nvarchar](50) NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[UpdatedAt] [datetime] NULL,
 CONSTRAINT [PK_mouse_history] PRIMARY KEY CLUSTERED 
(
	[HistoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 5/23/2024 4:53:40 PM ******/
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

INSERT [dbo].[cable] ([CableID], [cables], [quantity]) VALUES (1, N'HDMI', 0)
INSERT [dbo].[cable] ([CableID], [cables], [quantity]) VALUES (2, N'Display Port', 5)
INSERT [dbo].[cable] ([CableID], [cables], [quantity]) VALUES (3, N'VGA', 6)
INSERT [dbo].[cable] ([CableID], [cables], [quantity]) VALUES (4, N'UTP (Ethernet cable)', 2)
INSERT [dbo].[cable] ([CableID], [cables], [quantity]) VALUES (5, N'USB type A to B', 10)
INSERT [dbo].[cable] ([CableID], [cables], [quantity]) VALUES (6, N'USB type A to C', 4)
INSERT [dbo].[cable] ([CableID], [cables], [quantity]) VALUES (7, N'HP Dual Head Keyed Cable Lock', 3)
INSERT [dbo].[cable] ([CableID], [cables], [quantity]) VALUES (8, N'HP Sure Key Cable Lock', 2)
INSERT [dbo].[cable] ([CableID], [cables], [quantity]) VALUES (9, N'2 Pin Male Power Cable', 3)
INSERT [dbo].[cable] ([CableID], [cables], [quantity]) VALUES (10, N'Audio Cable', 1)
INSERT [dbo].[cable] ([CableID], [cables], [quantity]) VALUES (11, N'HP Thunderbolt Docking Power Cable', 1)
INSERT [dbo].[cable] ([CableID], [cables], [quantity]) VALUES (12, N'C5 Power Cord', 8)
INSERT [dbo].[cable] ([CableID], [cables], [quantity]) VALUES (13, N'Extension Socket', 1)
INSERT [dbo].[cable] ([CableID], [cables], [quantity]) VALUES (14, N'Laptop Battery', 8)
INSERT [dbo].[cable] ([CableID], [cables], [quantity]) VALUES (15, N'Monitor Cable', 1)
INSERT [dbo].[cable] ([CableID], [cables], [quantity]) VALUES (16, N'LAN Cable', 0)
SET IDENTITY_INSERT [dbo].[cable] OFF
GO
SET IDENTITY_INSERT [dbo].[docking_history] ON 

INSERT [dbo].[docking_history] ([HistoryID], [DockingID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [UpdatedBy], [UpdatedAt]) VALUES (1, 13, N'Try', N'', NULL, N'Good', N'', N'', N'Ching Yee', CAST(N'2024-05-21T16:54:51.570' AS DateTime))
INSERT [dbo].[docking_history] ([HistoryID], [DockingID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [UpdatedBy], [UpdatedAt]) VALUES (2, 13, N'Try', N'-', N'2024-05-21', N'Good', N'-', N'-', N'Ching Yee', CAST(N'2024-05-21T17:15:51.740' AS DateTime))
INSERT [dbo].[docking_history] ([HistoryID], [DockingID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [UpdatedBy], [UpdatedAt]) VALUES (3, 11, N'HP 2013 UltraSlim Docking', N'5CG829XY1Q', NULL, N'Good', N'At the Table2', N'-', N'Ching Yee', CAST(N'2024-05-23T13:23:20.700' AS DateTime))
SET IDENTITY_INSERT [dbo].[docking_history] OFF
GO
SET IDENTITY_INSERT [dbo].[docking_sys] ON 

INSERT [dbo].[docking_sys] ([DockingID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (1, N'HP 2013 UltraSlim Docking', N'5CG845WB2T', NULL, N'Good', N'-', N'Bernard')
INSERT [dbo].[docking_sys] ([DockingID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (2, N'DELL 7-in-1 Multiport Adapter', N'10828617E53F', NULL, N'Good', N'New', N'Rozmadi')
INSERT [dbo].[docking_sys] ([DockingID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (3, N'HP 2013 UltraSlim Docking', N'5CG913XLRQ', NULL, N'Good', N'', N'Yen Ling')
INSERT [dbo].[docking_sys] ([DockingID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (4, N'HP 2013 UltraSlimn Docking', N'5CG932ZSND', NULL, N'Good', N'', N'Phooi San')
INSERT [dbo].[docking_sys] ([DockingID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (5, N'HP USB-C Dock G5', N'5CG152XHQY', N'2022-10-14', N'Good', N'', N'Meng Yiau')
INSERT [dbo].[docking_sys] ([DockingID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (6, N'HP USB-C Dock G5', N'5CG304WZTM', N'2023-03-21', N'Good', N'New', N'Syakir')
INSERT [dbo].[docking_sys] ([DockingID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (7, N'HP 2013 UltraSlim Docking', N'5CG820W2H9', NULL, N'Good', N'from sir Jasni', N'')
INSERT [dbo].[docking_sys] ([DockingID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (8, N'HP Z Book Thunderbolt 3 Dock', N'CND6293HLW', NULL, N'Good', N'', N'')
INSERT [dbo].[docking_sys] ([DockingID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (9, N'HP Z Book Thunderbolt 3 Dock', N'CND8288KZ0', NULL, N'Good', N'from sir Roz', N'')
INSERT [dbo].[docking_sys] ([DockingID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (10, N'HP 2013 UltraSlim Docking', N'5CG845WB0V', NULL, N'Good', N'', N'')
INSERT [dbo].[docking_sys] ([DockingID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (11, N'HP 2013 UltraSlim Docking', N'5CG829XY1Q', NULL, N'Good', N'At the Table2', N'-')
INSERT [dbo].[docking_sys] ([DockingID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (12, N'HP Z Book Thunderbolt 3 Dock', N'', NULL, N'Good', N'', N'')
SET IDENTITY_INSERT [dbo].[docking_sys] OFF
GO
SET IDENTITY_INSERT [dbo].[headphone] ON 

INSERT [dbo].[headphone] ([hID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (1, N'Jabra HSC016                                      ', N'00272131561                                       ', N'2022-09-01                                        ', N'Good                                              ', NULL, N'Yen Ling                                          ')
INSERT [dbo].[headphone] ([hID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (2, N'Logitech A-00091                                  ', N'1833GXK05608                                      ', NULL, N'Good                                              ', NULL, N'Bernard                                           ')
INSERT [dbo].[headphone] ([hID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (3, N'Jabra HSC016                                      ', N'00272514882                                       ', N'2022-09-01                                        ', N'Good                                              ', NULL, N'Bernard                                           ')
INSERT [dbo].[headphone] ([hID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (4, N'Logitech A-00091                                  ', NULL, NULL, N'Good                                              ', NULL, N'Phooi San                                         ')
INSERT [dbo].[headphone] ([hID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (5, N'Jabra HSC016                                      ', N'00272131550                                       ', N'2022-09-01                                        ', N'Good                                              ', NULL, N'Huey Jing                                         ')
INSERT [dbo].[headphone] ([hID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (6, N'CLiPtec                                           ', N'20070799                                          ', NULL, N'Good                                              ', NULL, N'Huey Jing                                         ')
INSERT [dbo].[headphone] ([hID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (7, N'Logitech A-00044                                  ', N'1826ALC77988                                      ', NULL, N'Good                                              ', NULL, N'Rozmadi                                           ')
INSERT [dbo].[headphone] ([hID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (8, N'Jabra HSC016                                      ', N'00272514864                                       ', N'2022-09-01                                        ', N'Good                                              ', NULL, N'Rozmadi                                           ')
INSERT [dbo].[headphone] ([hID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (9, N'Logitech                                          ', N'2009ALK00W08                                      ', NULL, N'Good                                              ', NULL, N'Meng Yiau                                         ')
INSERT [dbo].[headphone] ([hID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (10, N'Jabra HSC016                                      ', N'00272514877                                       ', N'2022-09-01                                        ', N'Good                                              ', NULL, N'Chong Xin Hui                                     ')
INSERT [dbo].[headphone] ([hID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (11, N'Jabra HSC016                                      ', N'00272519621                                       ', N'2022-09-01                                        ', N'Good                                              ', NULL, N'Ching Yee                                         ')
INSERT [dbo].[headphone] ([hID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (12, N'Jabra HSC016                                      ', N'00279756969                                       ', N'2022-04-11                                        ', N'Good                                              ', N'New                                               ', N'Meng Yiau                                         ')
INSERT [dbo].[headphone] ([hID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (13, N'Jabra HSC016                                      ', N'00272520609                                       ', NULL, N'Good                                              ', N'New                                               ', N'Jie You                                           ')
INSERT [dbo].[headphone] ([hID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (14, N'Logitech A-00044                                  ', N'1721ALC51478                                      ', NULL, N'Bad                                               ', N'mic not functioning                               ', NULL)
INSERT [dbo].[headphone] ([hID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (15, N'CLiPtec BBH506                                    ', NULL, NULL, N'Good                                              ', N'Grey colour                                       ', NULL)
INSERT [dbo].[headphone] ([hID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (19, N'treeee12333', N'-', N'2024-05-23', N'Good', N'-', N'-')
SET IDENTITY_INSERT [dbo].[headphone] OFF
GO
SET IDENTITY_INSERT [dbo].[headphone_history] ON 

INSERT [dbo].[headphone_history] ([HistoryID], [hID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [UpdatedBy], [UpdatedAt]) VALUES (1, 19, N'treeee', N'-', N'2024-04-22', N'Good', N'-', N'-', N'Ching Yee', CAST(N'2024-05-23T16:32:01.660' AS DateTime))
SET IDENTITY_INSERT [dbo].[headphone_history] OFF
GO
SET IDENTITY_INSERT [dbo].[keyboard] ON 

INSERT [dbo].[keyboard] ([ID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (1, N'HP KBAR211                                        ', N'BEXHP0BTJBLJG                                     ', NULL, N'Good                                              ', N'From IT Soon Li                                   ', N'Meng Yiau')
INSERT [dbo].[keyboard] ([ID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (2, N'HP KBAR211                                        ', N'BEXHP0BTJB9LNX                                    ', NULL, N'Good                                              ', N'From IT Soon Li                                   ', N'Xin Hui                                           ')
SET IDENTITY_INSERT [dbo].[keyboard] OFF
GO
SET IDENTITY_INSERT [dbo].[laptop] ON 

INSERT [dbo].[laptop] ([LaptopID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (1, N'Dell Notebook                                     ', N'PYL2583W                                          ', N'2022                                              ', N'Good                                              ', N'PO#5810472699                                     ', N'Bernard                                           ')
INSERT [dbo].[laptop] ([LaptopID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (2, N'Dell Notebook                                     ', N'PYL2583W                                          ', N'2022                                              ', N'Good                                              ', N'PO#5810472699                                     ', N'Rozmadi                                           ')
INSERT [dbo].[laptop] ([LaptopID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (3, N'HP Elitebook                                      ', N'PYL2437W                                          ', N'2020                                              ', N'Good                                              ', N'PO#5803407368                                     ', N'Yen Ling                                          ')
INSERT [dbo].[laptop] ([LaptopID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (4, N'HP Elitebook                                      ', N'PYL2430W                                          ', N'2020                                              ', N'Good                                              ', N'PO#5803389137                                     ', N'Phooi San                                         ')
INSERT [dbo].[laptop] ([LaptopID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (5, N'HP Elitebook                                      ', N'PYL2514W                                          ', N'2021                                              ', N'Good                                              ', N'Used by Ching Yee. PO#5804126780                  ', N'Huey Jing                                         ')
INSERT [dbo].[laptop] ([LaptopID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (6, N'HP Elitebook                                      ', N'PYL2513W                                          ', N'2021                                              ', N'Good                                              ', N'PO#5804126780                                     ', N'Meng Yiau                                         ')
INSERT [dbo].[laptop] ([LaptopID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (7, N'Dell Notebook                                     ', N'PYL2604W                                          ', N'2022                                              ', N'Good                                              ', N'PO#5810482201                                     ', N'Syakir                                            ')
INSERT [dbo].[laptop] ([LaptopID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (8, N'HP Zbook                                          ', N'PYL2689W                                          ', N'2023                                              ', N'Good', N'New. PO#5810527827                                ', N'Phooi San')
INSERT [dbo].[laptop] ([LaptopID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (9, N'HP Elitebook                                      ', N'PYL2359W                                          ', NULL, N'', N'Keyboard malfunction. PO#5803081025               ', N'                                         ')
INSERT [dbo].[laptop] ([LaptopID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (10, N'HP ZBook                                          ', N'PYL2298W                                          ', NULL, NULL, N'Charge all time, cannot login. At Cubicle 4. PO#58', NULL)
INSERT [dbo].[laptop] ([LaptopID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (11, N'HP ZBook                                          ', N'PYL2253W                                          ', NULL, N'Good                                              ', N'Bottom rubber melt. PO#5803026659                 ', NULL)
INSERT [dbo].[laptop] ([LaptopID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (12, N'HP Elitebook                                      ', N'PYL2319W                                          ', NULL, N'Good                                              ', N'Charge all time. PO#5802970463                    ', N'Jie You                                           ')
INSERT [dbo].[laptop] ([LaptopID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (13, N'HP Elitebook                                      ', N'PYL2218W                                          ', NULL, NULL, N'Screen displays single green line, cannot login.At', NULL)
INSERT [dbo].[laptop] ([LaptopID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (14, N'HP Zbook                                          ', N'PYL2066W                                          ', N'2016                                              ', NULL, N'Battery slight swell. PO#4791051939               ', N'N/A                                               ')
INSERT [dbo].[laptop] ([LaptopID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (15, N'wqwqw', N'-', N'2000', N'', N'-', N'-')
INSERT [dbo].[laptop] ([LaptopID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (17, N'NEW', N'-', N'2024', N'Good', N'-', N'-')
INSERT [dbo].[laptop] ([LaptopID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (18, N'tried', N'-', N'2024', N'Good', N'-', N'-')
INSERT [dbo].[laptop] ([LaptopID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (19, N'HP Laptop ZBook', N'', N'2024', N'Good', N'', N'')
SET IDENTITY_INSERT [dbo].[laptop] OFF
GO
SET IDENTITY_INSERT [dbo].[laptop_history] ON 

INSERT [dbo].[laptop_history] ([HistoryID], [LaptopID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [UpdatedBy], [UpdatedAt]) VALUES (1, 17, N'Test for update', N'-', N'2000', N'Good', N'-', N'-', N'Ching Yee', CAST(N'2024-05-21T15:36:19.290' AS DateTime))
INSERT [dbo].[laptop_history] ([HistoryID], [LaptopID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [UpdatedBy], [UpdatedAt]) VALUES (2, 16, N'TestmodelName', N'', N'2024', N'Good', N'', N'', N'Ching Yee', CAST(N'2024-05-21T16:24:47.033' AS DateTime))
INSERT [dbo].[laptop_history] ([HistoryID], [LaptopID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [UpdatedBy], [UpdatedAt]) VALUES (3, 16, N'123', N'-', N'2024', N'Good', N'-', N'-', N'Ching Yee', CAST(N'2024-05-23T08:42:09.020' AS DateTime))
INSERT [dbo].[laptop_history] ([HistoryID], [LaptopID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [UpdatedBy], [UpdatedAt]) VALUES (4, 18, N'trying', N'-', N'2024', N'Bad', N'-', N'-', N'Ching Yee', CAST(N'2024-05-23T11:44:36.477' AS DateTime))
INSERT [dbo].[laptop_history] ([HistoryID], [LaptopID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [UpdatedBy], [UpdatedAt]) VALUES (5, 18, N'tried', N'-', N'2025', N'Good', N'-', N'-', N'Ching Yee', CAST(N'2024-05-23T13:12:58.947' AS DateTime))
SET IDENTITY_INSERT [dbo].[laptop_history] OFF
GO
SET IDENTITY_INSERT [dbo].[monitor] ON 

INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (1, N'Elite E190i                                       ', N'CN45510HNN                                        ', NULL, N'Good                                              ', NULL, N'Bernard                                           ')
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (2, N'HP E24iG4                                         ', N'CNK1081WCH                                        ', NULL, N'Good                                              ', NULL, N'Bernard                                           ')
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (3, N'HP E243i                                          ', N'6CM82520H7                                        ', NULL, N'Good                                              ', NULL, N'Bernard                                           ')
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (4, N'HP E233                                           ', N'HSTND-9571A                                       ', NULL, N'Good                                              ', N'Home                                              ', N'Rozmadi                                           ')
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (5, N'HP E243i                                          ', N'CNK8331J5R                                        ', NULL, N'Good                                              ', N'Office                                            ', N'Rozmadi                                           ')
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (6, N'HP Z24                                            ', N'6CM00508L0                                        ', NULL, N'Good                                              ', N'Home                                              ', N'Yen Ling                                          ')
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (7, N'HP Z24                                            ', N'6CM00508L3                                        ', NULL, N'Good                                              ', N'Office                                            ', N'Yen Ling                                          ')
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (8, N'HP Z24                                            ', N'6CM00508L5                                        ', NULL, N'Good                                              ', N'Office                                            ', N'Yen Ling                                          ')
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (9, N'HP Z24                                            ', N'6CM00508MC                                        ', NULL, N'Good                                              ', N'Home                                              ', N'Phooi San                                         ')
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (10, N'HP Z24                                            ', N'6CM00508L6                                        ', NULL, N'Good                                              ', N'Home                                              ', N'Phooi San                                         ')
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (11, N'HP Z24                                            ', N'6CM00508L7                                        ', NULL, N'Good                                              ', N'Office                                            ', N'Phooi San                                         ')
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (12, N'HP Z24                                            ', N'6CM00508F2                                        ', NULL, N'Good                                              ', N'Home                                              ', N'Huey Jing                                         ')
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (13, N'HP E24i G4                                        ', N'CNK1081WC7                                        ', NULL, N'Good                                              ', N'S3 Room                                           ', N'Huey Jing                                         ')
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (14, N'HP E243i                                          ', N'CNC8372J5J                                        ', NULL, N'Good                                              ', N'Home                                              ', N'Meng Yiau                                         ')
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (15, N'HP E243i                                          ', N'CNK8290V07                                        ', NULL, N'Good                                              ', N'Office                                            ', N'Meng Yiau                                         ')
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (16, N'HP E24i G4                                        ', N'CNK1081WC2                                        ', NULL, N'Good                                              ', N'Home                                              ', N'Meng Yiau                                         ')
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (17, N'HP Z24N G2                                        ', N'6CM00508MB                                        ', NULL, N'Good                                              ', N'Office                                            ', N'Syakir                                            ')
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (18, N'HP E24q G4 (24'''')                                 ', N'CNK2390RZW                                        ', N'2023-03-08                                        ', N'New                                               ', NULL, N'Syakir                                            ')
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (19, N'HP E243i                                          ', N'6CM8491FSQ                                        ', NULL, N'Good                                              ', N'Office                                            ', N'Ching Yee                                         ')
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (20, N'HP E24i G4                                        ', N'CNK1081WC3                                        ', NULL, N'Good                                              ', N'Office                                            ', N'Xin Hui                                           ')
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (21, N'HP E243i                                          ', N'CNK8331J4J                                        ', NULL, N'Good                                              ', N'Office (Power cord from Jason)                    ', N'Jie You                                           ')
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (22, N'HP Compaq (19'''')                                  ', N'3CQ3460SBX                                        ', NULL, N'Good                                              ', N'Store Room                                        ', NULL)
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (23, N'HP E24i G4 (24'''')                                 ', N'CNK24719T                                         ', N'2023-03-08                                        ', N'Good                                              ', N'New                                               ', NULL)
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (24, N'HP E24i G4 (24'''')                                 ', N'CNK24716SH                                        ', N'2023-03-21                                        ', N'Good                                              ', N'New                                               ', NULL)
INSERT [dbo].[monitor] ([MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (25, N'AcerTry123', N'rreerer', N'2024-05-23', N'Good', N'-', N'-')
SET IDENTITY_INSERT [dbo].[monitor] OFF
GO
SET IDENTITY_INSERT [dbo].[monitor_history] ON 

INSERT [dbo].[monitor_history] ([HistoryID], [MonitorID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [UpdatedBy], [UpdatedAt]) VALUES (1, 25, N'AcerTry123', N'rreerer', N'2024-04-01', N'', N'-', N'-', N'Ching Yee', NULL)
SET IDENTITY_INSERT [dbo].[monitor_history] OFF
GO
SET IDENTITY_INSERT [dbo].[mouse] ON 

INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (1, N'MSU-1158                                          ', N'-', NULL, N'', N'-', N'Bernard   ')
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (2, N'MSU-1158                                          ', NULL, NULL, N'Good                                              ', NULL, N'Rozmadi   ')
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (3, N'HP                                                ', NULL, NULL, N'Good                                              ', NULL, N'Yen Ling  ')
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (4, N'Logitech M185                                     ', N'2225LZX6TZF9                                      ', N'2022-09-01                                        ', N'Good                                              ', NULL, N'Phooi San ')
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (5, N'HP                                                ', N'FCMHL0EW2BC2TF                                    ', NULL, N'Good                                              ', NULL, N'Huey Jing ')
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (6, N'HP                                                ', N'QY777AA                                           ', NULL, N'Good                                              ', NULL, N'Meng Yiau ')
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (7, N'HP Wired Desktop 320M Mouse                       ', N'9CP045XG3T                                        ', NULL, N'Good                                              ', N'New                                               ', N'Syakir    ')
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (8, N'HP Wired Desktop 320M Mouse                       ', N'9CP045XG3M                                        ', NULL, N'Good                                              ', N'New                                               ', N'Yen Ling  ')
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (9, N'Logitech M185                                     ', N'2225LZXBSUD8                                      ', N'2022-09-01', N'', N'New                                               ', N'Xin Hui')
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (10, N'Logitech M185                                     ', N'2225LZX9DKS8                                      ', N'2022-09-01                                        ', N'Good                                              ', N'New                                               ', N'Jie You   ')
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (11, N'HP MOFYUO                                         ', N'FCMHH0AHDAETT6                                    ', NULL, N'Good                                              ', N'New                                               ', NULL)
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (12, N'Logitech M185                                     ', NULL, N'2022-11-04                                        ', N'Good                                              ', N'claimed by PS (New)                               ', NULL)
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (13, N'HP MSU1158                                        ', N'FCMHL0EW2ANHGM                                    ', NULL, N'Good                                              ', NULL, NULL)
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (14, N'HP MSU1158                                        ', N'FCMHL0EW2B4ENT                                    ', NULL, N'Good                                              ', NULL, NULL)
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (15, N'Logitech M100r                                    ', N'1529HS013618                                      ', NULL, N'Good                                              ', N'Old                                               ', NULL)
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (16, N'HP MOFYUO                                         ', N'FCMHH0AHD414P3                                    ', NULL, N'Good                                              ', N'Old                                               ', NULL)
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (17, N'Logitech M100r                                    ', N'1819HS01FJ48                                      ', NULL, N'Good                                              ', N'Old                                               ', NULL)
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (18, N'HP 125 Wired Mouse                                ', N'9CP2415NRD                                        ', N'2023-03-21                                        ', N'Good                                              ', N'New (Missing)                                     ', NULL)
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (19, N'Logitech M185                                     ', N'2053LZX2PEU8                                      ', N'2022-09-01                                        ', N'Good                                              ', N'No battery                                        ', NULL)
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (20, N'HP MOHQUO                                         ', N'7CF53400Z7                                        ', NULL, N'Good                                              ', NULL, NULL)
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (21, N'HP SM-2022                                        ', N'FCMHH0CZD4BPC                                     ', N'2020-03-09                                        ', N'Good                                              ', NULL, N'Ching Yee ')
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (22, N'Logitech M185                                     ', NULL, N'2022-02-11                                        ', N'Good                                              ', N'New (No yet open)                                 ', NULL)
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (24, N'try1223323', N'10CP045XG3R                ', N'2024-05-02', N'Good', N'The mouse was stored in X''s cubicle.', N'-')
INSERT [dbo].[mouse] ([MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner]) VALUES (26, N'USE', N'-', N'2024-05-01', N'Bad', N'-', N'-')
SET IDENTITY_INSERT [dbo].[mouse] OFF
GO
SET IDENTITY_INSERT [dbo].[mouse_history] ON 

INSERT [dbo].[mouse_history] ([HistoryID], [MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [UpdatedBy], [UpdatedAt]) VALUES (1, 25, N'Testing123', N'-', NULL, N'Good', N'-', N'-', N'Ching Yee', CAST(N'2024-05-23T13:13:33.583' AS DateTime))
INSERT [dbo].[mouse_history] ([HistoryID], [MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [UpdatedBy], [UpdatedAt]) VALUES (2, 25, N'Testing123', N'-', N'May 23 2024 12:00AM', N'Good', N'-', N'-', N'Ching Yee', CAST(N'2024-05-23T13:14:15.390' AS DateTime))
INSERT [dbo].[mouse_history] ([HistoryID], [MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [UpdatedBy], [UpdatedAt]) VALUES (3, 25, N'Testing123', N'-', N'May 23 2024 12:00AM', N'Good', N'-', N'-', N'Ching Yee', CAST(N'2024-05-23T13:16:04.777' AS DateTime))
INSERT [dbo].[mouse_history] ([HistoryID], [MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [UpdatedBy], [UpdatedAt]) VALUES (4, 25, N'Testing123', N'-', N'2024-05-23', N'Good', N'-', N'-', N'Ching Yee', CAST(N'2024-05-23T13:16:11.700' AS DateTime))
INSERT [dbo].[mouse_history] ([HistoryID], [MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [UpdatedBy], [UpdatedAt]) VALUES (5, 25, N'Testing123', N'-', NULL, N'Good', N'-', N'-', N'Ching Yee', CAST(N'2024-05-23T13:16:46.243' AS DateTime))
INSERT [dbo].[mouse_history] ([HistoryID], [MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [UpdatedBy], [UpdatedAt]) VALUES (6, 25, N'Testing123', N'-', NULL, N'Good', N'-', N'-', N'Ching Yee', CAST(N'2024-05-23T13:16:50.110' AS DateTime))
INSERT [dbo].[mouse_history] ([HistoryID], [MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [UpdatedBy], [UpdatedAt]) VALUES (7, 25, N'Testing123', N'-', NULL, N'Bad', N'-', N'-', N'Ching Yee', CAST(N'2024-05-23T13:23:57.310' AS DateTime))
INSERT [dbo].[mouse_history] ([HistoryID], [MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [UpdatedBy], [UpdatedAt]) VALUES (8, 25, N'Testing123', N'-', NULL, N'Bad', N'-', N'-', N'Ching Yee', CAST(N'2024-05-23T13:24:00.337' AS DateTime))
INSERT [dbo].[mouse_history] ([HistoryID], [MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [UpdatedBy], [UpdatedAt]) VALUES (9, 24, N'try1223323', N'10CP045XG3R                ', N'2024-05-02', N'Good', N'The mouse was stored in X''s cubicle.', N'-', N'Ching Yee', CAST(N'2024-05-23T13:24:30.380' AS DateTime))
INSERT [dbo].[mouse_history] ([HistoryID], [MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [UpdatedBy], [UpdatedAt]) VALUES (10, 25, N'Try', N'-', NULL, N'Bad', N'-', N'-', N'Ching Yee', CAST(N'2024-05-23T13:24:56.037' AS DateTime))
INSERT [dbo].[mouse_history] ([HistoryID], [MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [UpdatedBy], [UpdatedAt]) VALUES (11, 25, N'Try', N'-', NULL, N'Bad', N'-', N'-', N'Ching Yee', CAST(N'2024-05-23T13:24:59.080' AS DateTime))
INSERT [dbo].[mouse_history] ([HistoryID], [MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [UpdatedBy], [UpdatedAt]) VALUES (12, 25, N'Try', N'-', NULL, N'Good', N'-', N'-', N'Ching Yee', CAST(N'2024-05-23T13:36:16.483' AS DateTime))
INSERT [dbo].[mouse_history] ([HistoryID], [MouseID], [Model], [Code_SN], [Received_date], [Condition], [Remarks], [Owner], [UpdatedBy], [UpdatedAt]) VALUES (13, 26, N'USE', N'', N'2024-05-01', N'Good', N'', N'', N'Ching Yee', CAST(N'2024-05-23T13:37:29.027' AS DateTime))
SET IDENTITY_INSERT [dbo].[mouse_history] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([id], [userID], [username], [password]) VALUES (1, N'uig60644', N'Jie You', N'Conti12345')
INSERT [dbo].[Users] ([id], [userID], [username], [password]) VALUES (2, N'uig60643', N'Ching Yee', N'Conti12345')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[docking_history] ADD  CONSTRAINT [DF__docking_hi__Updat__41B8C09B]  DEFAULT (getdate()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[headphone_history] ADD  CONSTRAINT [DF__headphone_hi__Updat__41B8C09B]  DEFAULT (getdate()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[laptop_history] ADD  CONSTRAINT [DF__laptop_hi__Updat__41B8C09B]  DEFAULT (getdate()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[mouse_history] ADD  CONSTRAINT [DF__mouse_hi__Updat__41B8C09B]  DEFAULT (getdate()) FOR [UpdatedAt]
GO
USE [master]
GO
ALTER DATABASE [Equipment] SET  READ_WRITE 
GO
