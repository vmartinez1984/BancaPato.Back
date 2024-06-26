USE [master]
GO
/****** Object:  Database [DuckBank]    Script Date: 16/11/2023 12:53:22 p. m. ******/
CREATE DATABASE [DuckBank]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DuckBank', FILENAME = N'/var/opt/mssql/data/DuckBank.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DuckBank_log', FILENAME = N'/var/opt/mssql/data/DuckBank_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [DuckBank] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DuckBank].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DuckBank] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DuckBank] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DuckBank] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DuckBank] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DuckBank] SET ARITHABORT OFF 
GO
ALTER DATABASE [DuckBank] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DuckBank] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DuckBank] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DuckBank] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DuckBank] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DuckBank] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DuckBank] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DuckBank] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DuckBank] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DuckBank] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DuckBank] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DuckBank] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DuckBank] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DuckBank] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DuckBank] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DuckBank] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DuckBank] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DuckBank] SET RECOVERY FULL 
GO
ALTER DATABASE [DuckBank] SET  MULTI_USER 
GO
ALTER DATABASE [DuckBank] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DuckBank] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DuckBank] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DuckBank] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DuckBank] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DuckBank] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'DuckBank', N'ON'
GO
ALTER DATABASE [DuckBank] SET QUERY_STORE = ON
GO
ALTER DATABASE [DuckBank] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [DuckBank]
GO
/****** Object:  Table [dbo].[Categoria]    Script Date: 16/11/2023 12:53:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categoria](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Categoria] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cuenta]    Script Date: 16/11/2023 12:53:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cuenta](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Guid] [uniqueidentifier] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Clabe] [varchar](50) NULL,
	[Nota] [varchar](255) NULL,
	[Interes] [decimal](4, 2) NULL,
	[Balance] [decimal](10, 2) NULL,
	[FechaInicial] [date] NULL,
	[FechaFinal] [date] NULL,
	[TipoDeCuentaId] [int] NULL,
 CONSTRAINT [PK_Cuenta] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HistorialDeApartados]    Script Date: 16/11/2023 12:53:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistorialDeApartados](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Guid] [uniqueidentifier] NOT NULL,
	[Cantidad] [decimal](10, 2) NOT NULL,
	[FechaDeRegistro] [datetime] NOT NULL,
	[Nota] [varchar](255) NULL,
	[CuentaId] [int] NOT NULL,
	[Interes] [decimal](4, 2) NULL,
 CONSTRAINT [PK_HistorialDeApartados] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movimiento]    Script Date: 16/11/2023 12:53:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movimiento](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Guid] [uniqueidentifier] NOT NULL,
	[Nota] [varchar](255) NULL,
	[PeriodoId] [int] NOT NULL,
	[TransaccionId] [int] NOT NULL,
	[PresupuestoId] [int] NOT NULL,
 CONSTRAINT [PK_Movimiento] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Periodo]    Script Date: 16/11/2023 12:53:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Periodo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[FechaInicial] [date] NOT NULL,
	[FechaFinal] [date] NOT NULL,
	[FechaDeRegistro] [datetime] NOT NULL,
	[EstaActivo] [bit] NOT NULL,
	[Nota] [varchar](255) NULL,
	[Guid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Periodo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Presupuesto]    Script Date: 16/11/2023 12:53:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Presupuesto](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Cantidad] [decimal](10, 2) NOT NULL,
	[SubcategoriaId] [int] NOT NULL,
	[VersionId] [int] NOT NULL,
	[Guid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Presupuesto] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subcategoria]    Script Date: 16/11/2023 12:53:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subcategoria](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Guid] [uniqueidentifier] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Presupuesto] [decimal](10, 2) NOT NULL,
	[EstaActivo] [bit] NOT NULL,
	[EsPrimario] [bit] NOT NULL,
	[CategoriaId] [int] NOT NULL,
 CONSTRAINT [PK_Subcategoria] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoDeCuenta]    Script Date: 16/11/2023 12:53:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoDeCuenta](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Guid] [uniqueidentifier] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripción] [varchar](255) NOT NULL,
 CONSTRAINT [PK_TipoDeCuenta] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transaccion]    Script Date: 16/11/2023 12:53:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaccion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CuentaId] [int] NOT NULL,
	[Guid] [uniqueidentifier] NOT NULL,
	[Cantidad] [decimal](10, 2) NOT NULL,
	[FechaDeRegistro] [datetime] NOT NULL,
	[Tipo] [varchar](20) NOT NULL,
	[Concepto] [varchar](20) NULL,
	[Nota] [varchar](255) NULL,
	[Referencia] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Transaccion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VersionDePresupuesto]    Script Date: 16/11/2023 12:53:22 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VersionDePresupuesto](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Guid] [uniqueidentifier] NOT NULL,
	[FechaInicial] [date] NOT NULL,
	[FechaFinal] [date] NOT NULL,
	[FechaDeRegistro] [datetime] NOT NULL,
	[EstaActivo] [bit] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Version] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categoria] ON 

INSERT [dbo].[Categoria] ([Id], [Nombre]) VALUES (1, N'Entradas')
INSERT [dbo].[Categoria] ([Id], [Nombre]) VALUES (2, N'Apartados')
INSERT [dbo].[Categoria] ([Id], [Nombre]) VALUES (3, N'Gastos')
SET IDENTITY_INSERT [dbo].[Categoria] OFF
GO
SET IDENTITY_INSERT [dbo].[Cuenta] ON 

INSERT [dbo].[Cuenta] ([Id], [Guid], [Nombre], [Clabe], [Nota], [Interes], [Balance], [FechaInicial], [FechaFinal], [TipoDeCuentaId]) VALUES (1006, N'f3fc673c-9854-4169-b1af-afd9ba9c505f', N'Bbva', N'', N'principal', CAST(0.00 AS Decimal(4, 2)), CAST(0.00 AS Decimal(10, 2)), NULL, NULL, NULL)
INSERT [dbo].[Cuenta] ([Id], [Guid], [Nombre], [Clabe], [Nota], [Interes], [Balance], [FechaInicial], [FechaFinal], [TipoDeCuentaId]) VALUES (1007, N'c66c5e25-23ed-47c2-b1bd-e04231f30f0a', N'Techero', N'', N'Nomina', CAST(0.00 AS Decimal(4, 2)), CAST(0.00 AS Decimal(10, 2)), NULL, NULL, NULL)
INSERT [dbo].[Cuenta] ([Id], [Guid], [Nombre], [Clabe], [Nota], [Interes], [Balance], [FechaInicial], [FechaFinal], [TipoDeCuentaId]) VALUES (1008, N'd2eea2f3-c00a-4199-af27-d1a8476f0cdd', N'Techero', N'', N'Afore', CAST(12.00 AS Decimal(4, 2)), CAST(23793.52 AS Decimal(10, 2)), NULL, NULL, NULL)
INSERT [dbo].[Cuenta] ([Id], [Guid], [Nombre], [Clabe], [Nota], [Interes], [Balance], [FechaInicial], [FechaFinal], [TipoDeCuentaId]) VALUES (1009, N'9958dc75-6325-4958-8b01-30295d20c205', N'Techero', N'', N'Ahorro', CAST(10.00 AS Decimal(4, 2)), CAST(325.51 AS Decimal(10, 2)), NULL, NULL, NULL)
INSERT [dbo].[Cuenta] ([Id], [Guid], [Nombre], [Clabe], [Nota], [Interes], [Balance], [FechaInicial], [FechaFinal], [TipoDeCuentaId]) VALUES (1010, N'438c4654-f96a-4a6a-b74e-91aa3fd2309f', N'Techero', N'', N'Sabatino', CAST(5.00 AS Decimal(4, 2)), CAST(10123.38 AS Decimal(10, 2)), NULL, NULL, NULL)
INSERT [dbo].[Cuenta] ([Id], [Guid], [Nombre], [Clabe], [Nota], [Interes], [Balance], [FechaInicial], [FechaFinal], [TipoDeCuentaId]) VALUES (1011, N'154e157c-f880-4e13-b1fd-3dda3857436d', N'Bbva', N'', N'Afore', CAST(4.00 AS Decimal(4, 2)), CAST(58689.91 AS Decimal(10, 2)), NULL, NULL, NULL)
INSERT [dbo].[Cuenta] ([Id], [Guid], [Nombre], [Clabe], [Nota], [Interes], [Balance], [FechaInicial], [FechaFinal], [TipoDeCuentaId]) VALUES (1012, N'1c8295fc-4305-4bca-95df-2d1bb9ec3234', N'Bbva plazo', N'', N'Sabatico 91', CAST(3.70 AS Decimal(4, 2)), CAST(27624.90 AS Decimal(10, 2)), NULL, NULL, NULL)
INSERT [dbo].[Cuenta] ([Id], [Guid], [Nombre], [Clabe], [Nota], [Interes], [Balance], [FechaInicial], [FechaFinal], [TipoDeCuentaId]) VALUES (1013, N'b60bcd4b-13ed-4706-89e7-1b289a063f35', N'Bbva Inversion', N'', N'Camioneta', CAST(9.52 AS Decimal(4, 2)), CAST(107.83 AS Decimal(10, 2)), NULL, NULL, NULL)
INSERT [dbo].[Cuenta] ([Id], [Guid], [Nombre], [Clabe], [Nota], [Interes], [Balance], [FechaInicial], [FechaFinal], [TipoDeCuentaId]) VALUES (1014, N'fe7375e9-7ef3-4f6e-a6dd-ae6ac1ed1132', N'Bbva Inv', N'', N'Entreteniento', CAST(9.52 AS Decimal(4, 2)), CAST(503.25 AS Decimal(10, 2)), NULL, NULL, NULL)
INSERT [dbo].[Cuenta] ([Id], [Guid], [Nombre], [Clabe], [Nota], [Interes], [Balance], [FechaInicial], [FechaFinal], [TipoDeCuentaId]) VALUES (1015, N'48f65609-c90f-454e-a617-ddb43434b313', N'Bbva Inv', N'', N'Ropa', CAST(9.52 AS Decimal(4, 2)), CAST(1204.21 AS Decimal(10, 2)), NULL, NULL, NULL)
INSERT [dbo].[Cuenta] ([Id], [Guid], [Nombre], [Clabe], [Nota], [Interes], [Balance], [FechaInicial], [FechaFinal], [TipoDeCuentaId]) VALUES (1016, N'927232cb-9826-4ddd-abf7-431e63afedd6', N'Bbva inv', N'', N'Gastos medicos', CAST(9.52 AS Decimal(4, 2)), CAST(1802.82 AS Decimal(10, 2)), NULL, NULL, NULL)
INSERT [dbo].[Cuenta] ([Id], [Guid], [Nombre], [Clabe], [Nota], [Interes], [Balance], [FechaInicial], [FechaFinal], [TipoDeCuentaId]) VALUES (1017, N'e6b64054-59d8-4621-9804-e31040ddfb89', N'bbva inv', N'', N'Pc', CAST(9.52 AS Decimal(4, 2)), CAST(1815.30 AS Decimal(10, 2)), NULL, NULL, NULL)
INSERT [dbo].[Cuenta] ([Id], [Guid], [Nombre], [Clabe], [Nota], [Interes], [Balance], [FechaInicial], [FechaFinal], [TipoDeCuentaId]) VALUES (1018, N'8d8a8824-90cd-4167-802e-d2a2b66235cc', N'Bbva inv', N'', N'Titulacion', CAST(9.52 AS Decimal(4, 2)), CAST(13857.42 AS Decimal(10, 2)), NULL, NULL, NULL)
INSERT [dbo].[Cuenta] ([Id], [Guid], [Nombre], [Clabe], [Nota], [Interes], [Balance], [FechaInicial], [FechaFinal], [TipoDeCuentaId]) VALUES (1019, N'ba661e16-f876-4647-a556-587de825eb06', N'Bbbva inv', N'', N'Tlaxcoapan', CAST(9.51 AS Decimal(4, 2)), CAST(1208.22 AS Decimal(10, 2)), NULL, NULL, NULL)
INSERT [dbo].[Cuenta] ([Id], [Guid], [Nombre], [Clabe], [Nota], [Interes], [Balance], [FechaInicial], [FechaFinal], [TipoDeCuentaId]) VALUES (1020, N'c05a6716-8973-4478-aaa8-6222c1d9357f', N'Bbva inv', N'', N'Cdmx', CAST(9.51 AS Decimal(4, 2)), CAST(956.51 AS Decimal(10, 2)), NULL, NULL, NULL)
INSERT [dbo].[Cuenta] ([Id], [Guid], [Nombre], [Clabe], [Nota], [Interes], [Balance], [FechaInicial], [FechaFinal], [TipoDeCuentaId]) VALUES (1021, N'945c1cb7-1e82-4590-b93c-0dce05137c1c', N'Bbva inv', N'', N'Gatos', CAST(9.51 AS Decimal(4, 2)), CAST(1577.40 AS Decimal(10, 2)), CAST(N'2023-01-01' AS Date), CAST(N'2023-12-20' AS Date), NULL)
INSERT [dbo].[Cuenta] ([Id], [Guid], [Nombre], [Clabe], [Nota], [Interes], [Balance], [FechaInicial], [FechaFinal], [TipoDeCuentaId]) VALUES (1022, N'37569a53-41d6-4155-a96f-fc021540a05a', N'Bbva inv', N'', N'Anualidad Tdc', CAST(9.51 AS Decimal(4, 2)), CAST(2064.05 AS Decimal(10, 2)), CAST(N'2023-01-01' AS Date), CAST(N'2023-12-20' AS Date), NULL)
INSERT [dbo].[Cuenta] ([Id], [Guid], [Nombre], [Clabe], [Nota], [Interes], [Balance], [FechaInicial], [FechaFinal], [TipoDeCuentaId]) VALUES (1023, N'92819ab9-0240-458d-b62a-a6993ab4b071', N'Bbva inv', N'', N'Tec, cosas y libros', CAST(9.52 AS Decimal(4, 2)), CAST(1473.81 AS Decimal(10, 2)), CAST(N'2023-01-01' AS Date), CAST(N'2023-12-20' AS Date), NULL)
INSERT [dbo].[Cuenta] ([Id], [Guid], [Nombre], [Clabe], [Nota], [Interes], [Balance], [FechaInicial], [FechaFinal], [TipoDeCuentaId]) VALUES (1024, N'0911c3ca-5256-4343-93d8-c2a388ec6325', N'Bbva inv', N'', N'Vacaciones', CAST(9.51 AS Decimal(4, 2)), CAST(4060.98 AS Decimal(10, 2)), CAST(N'2023-01-01' AS Date), CAST(N'2025-01-01' AS Date), NULL)
INSERT [dbo].[Cuenta] ([Id], [Guid], [Nombre], [Clabe], [Nota], [Interes], [Balance], [FechaInicial], [FechaFinal], [TipoDeCuentaId]) VALUES (1025, N'ba1832e1-7723-4236-bbdf-51e850c0b256', N'Bbva inv', N'', N'Sabatico', CAST(9.51 AS Decimal(4, 2)), CAST(10823.69 AS Decimal(10, 2)), CAST(N'2023-01-01' AS Date), CAST(N'2023-12-20' AS Date), NULL)
INSERT [dbo].[Cuenta] ([Id], [Guid], [Nombre], [Clabe], [Nota], [Interes], [Balance], [FechaInicial], [FechaFinal], [TipoDeCuentaId]) VALUES (1026, N'fbcc28d0-2a25-427e-a41e-2b45cdd7e3e9', N'Bbva inv', N'', N'Yo merengues', CAST(9.66 AS Decimal(4, 2)), CAST(87.33 AS Decimal(10, 2)), CAST(N'2023-01-01' AS Date), CAST(N'2023-12-20' AS Date), NULL)
INSERT [dbo].[Cuenta] ([Id], [Guid], [Nombre], [Clabe], [Nota], [Interes], [Balance], [FechaInicial], [FechaFinal], [TipoDeCuentaId]) VALUES (1027, N'b0b9b48e-c162-4393-99bf-195d560ab8c1', N'Bbva inv', N'', N'Renta', CAST(9.51 AS Decimal(4, 2)), CAST(234.99 AS Decimal(10, 2)), CAST(N'2023-01-01' AS Date), CAST(N'2023-12-20' AS Date), NULL)
INSERT [dbo].[Cuenta] ([Id], [Guid], [Nombre], [Clabe], [Nota], [Interes], [Balance], [FechaInicial], [FechaFinal], [TipoDeCuentaId]) VALUES (1028, N'a3f6c6c9-d0d8-463e-9da6-462563c2fd2a', N'bbva aprtado', N'', N'Camioneta', CAST(0.00 AS Decimal(4, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(N'2023-01-01' AS Date), CAST(N'2023-12-20' AS Date), NULL)
INSERT [dbo].[Cuenta] ([Id], [Guid], [Nombre], [Clabe], [Nota], [Interes], [Balance], [FechaInicial], [FechaFinal], [TipoDeCuentaId]) VALUES (1029, N'871cc235-8045-483d-874d-17770815c499', N'Bbva apartado', N'', N'Gastos medicos', CAST(0.00 AS Decimal(4, 2)), CAST(250.00 AS Decimal(10, 2)), CAST(N'2023-01-01' AS Date), CAST(N'2023-12-20' AS Date), NULL)
INSERT [dbo].[Cuenta] ([Id], [Guid], [Nombre], [Clabe], [Nota], [Interes], [Balance], [FechaInicial], [FechaFinal], [TipoDeCuentaId]) VALUES (1030, N'9adae8c3-8adc-4d1b-8e12-50e23419f44c', N'Bbva apartado', N'', N'Talcoapan', CAST(0.00 AS Decimal(4, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(N'2023-01-01' AS Date), CAST(N'2023-12-20' AS Date), NULL)
INSERT [dbo].[Cuenta] ([Id], [Guid], [Nombre], [Clabe], [Nota], [Interes], [Balance], [FechaInicial], [FechaFinal], [TipoDeCuentaId]) VALUES (1031, N'f8be5c27-4cfb-46f9-87d8-9b1a73c2a4e2', N'Bbva Apartado', N'', N'Ahorro n', CAST(0.00 AS Decimal(4, 2)), CAST(600.00 AS Decimal(10, 2)), CAST(N'2023-01-01' AS Date), CAST(N'2023-12-20' AS Date), NULL)
INSERT [dbo].[Cuenta] ([Id], [Guid], [Nombre], [Clabe], [Nota], [Interes], [Balance], [FechaInicial], [FechaFinal], [TipoDeCuentaId]) VALUES (1032, N'04d62a72-84cb-4721-aab7-549b88dfca03', N'Cetes', N'', N'Vacaciones Nancy', CAST(0.00 AS Decimal(4, 2)), CAST(7500.00 AS Decimal(10, 2)), CAST(N'2023-01-01' AS Date), CAST(N'2025-12-12' AS Date), NULL)
INSERT [dbo].[Cuenta] ([Id], [Guid], [Nombre], [Clabe], [Nota], [Interes], [Balance], [FechaInicial], [FechaFinal], [TipoDeCuentaId]) VALUES (1033, N'4ef6309b-12cf-468e-836a-a0746032c0c9', N'Cetes', N'', N'Prueba', CAST(10.95 AS Decimal(4, 2)), CAST(99.15 AS Decimal(10, 2)), CAST(N'2022-12-01' AS Date), CAST(N'2025-01-01' AS Date), NULL)
INSERT [dbo].[Cuenta] ([Id], [Guid], [Nombre], [Clabe], [Nota], [Interes], [Balance], [FechaInicial], [FechaFinal], [TipoDeCuentaId]) VALUES (1034, N'0023bb54-d31c-4a95-b380-71acda73f371', N'Cetes', N'', N'Vacaciones yop', CAST(11.10 AS Decimal(4, 2)), CAST(7500.00 AS Decimal(10, 2)), CAST(N'2023-01-01' AS Date), CAST(N'2025-12-20' AS Date), NULL)
INSERT [dbo].[Cuenta] ([Id], [Guid], [Nombre], [Clabe], [Nota], [Interes], [Balance], [FechaInicial], [FechaFinal], [TipoDeCuentaId]) VALUES (1035, N'4dc9e8f0-23c5-4b52-9c6b-bf6c725ffd9b', N'Cetes', N'', N'Yo merengues', CAST(11.10 AS Decimal(4, 2)), CAST(12737.79 AS Decimal(10, 2)), CAST(N'2023-01-01' AS Date), CAST(N'2023-12-20' AS Date), NULL)
INSERT [dbo].[Cuenta] ([Id], [Guid], [Nombre], [Clabe], [Nota], [Interes], [Balance], [FechaInicial], [FechaFinal], [TipoDeCuentaId]) VALUES (1036, N'c1104bf6-d9f4-4d63-9ce2-c1a9752291a1', N'Sobre', N'', N'Agua Cdmx', CAST(0.00 AS Decimal(4, 2)), CAST(200.00 AS Decimal(10, 2)), CAST(N'2023-01-01' AS Date), CAST(N'2023-12-20' AS Date), NULL)
INSERT [dbo].[Cuenta] ([Id], [Guid], [Nombre], [Clabe], [Nota], [Interes], [Balance], [FechaInicial], [FechaFinal], [TipoDeCuentaId]) VALUES (1037, N'b0af8f65-de1d-4f3f-9797-25a2f1a39ac9', N'Sobre', N'', N'Gas', CAST(0.00 AS Decimal(4, 2)), NULL, CAST(N'2023-01-01' AS Date), CAST(N'2023-12-20' AS Date), NULL)
INSERT [dbo].[Cuenta] ([Id], [Guid], [Nombre], [Clabe], [Nota], [Interes], [Balance], [FechaInicial], [FechaFinal], [TipoDeCuentaId]) VALUES (1038, N'6934345e-3414-466a-b7b6-f34ff47438f1', N'Sobre', N'', N'Metro', CAST(0.00 AS Decimal(4, 2)), CAST(400.00 AS Decimal(10, 2)), CAST(N'2023-01-01' AS Date), CAST(N'2023-12-20' AS Date), NULL)
INSERT [dbo].[Cuenta] ([Id], [Guid], [Nombre], [Clabe], [Nota], [Interes], [Balance], [FechaInicial], [FechaFinal], [TipoDeCuentaId]) VALUES (1039, N'ce353ef0-cce2-4ffe-84ed-724bcb8a5e0c', N'Sobre', N'', N'Deportes', CAST(0.00 AS Decimal(4, 2)), CAST(1000.00 AS Decimal(10, 2)), CAST(N'2023-01-01' AS Date), CAST(N'2023-12-20' AS Date), NULL)
INSERT [dbo].[Cuenta] ([Id], [Guid], [Nombre], [Clabe], [Nota], [Interes], [Balance], [FechaInicial], [FechaFinal], [TipoDeCuentaId]) VALUES (1040, N'8b5d60d0-22b9-48e9-858a-2b30a7d767c4', N'Sobre', N'', N'Semana 1', CAST(0.00 AS Decimal(4, 2)), CAST(350.00 AS Decimal(10, 2)), CAST(N'2023-01-01' AS Date), CAST(N'2023-12-20' AS Date), NULL)
INSERT [dbo].[Cuenta] ([Id], [Guid], [Nombre], [Clabe], [Nota], [Interes], [Balance], [FechaInicial], [FechaFinal], [TipoDeCuentaId]) VALUES (1041, N'db4928e3-d82e-4df8-a219-118a43f86a7e', N'Sobre', N'', N'Semana 3', CAST(0.00 AS Decimal(4, 2)), NULL, CAST(N'2023-01-01' AS Date), CAST(N'2023-12-02' AS Date), NULL)
INSERT [dbo].[Cuenta] ([Id], [Guid], [Nombre], [Clabe], [Nota], [Interes], [Balance], [FechaInicial], [FechaFinal], [TipoDeCuentaId]) VALUES (1042, N'2cf0ec1b-a202-4686-bf7b-b8b42d24674d', N'Sobre', N'', N'Tianguis', CAST(0.00 AS Decimal(4, 2)), CAST(200.00 AS Decimal(10, 2)), CAST(N'2023-01-01' AS Date), CAST(N'2023-12-20' AS Date), NULL)
INSERT [dbo].[Cuenta] ([Id], [Guid], [Nombre], [Clabe], [Nota], [Interes], [Balance], [FechaInicial], [FechaFinal], [TipoDeCuentaId]) VALUES (1043, N'711f8615-ab31-497b-ae01-e9696c167be8', N'Sobre', N'', N'Alimentacion', CAST(0.00 AS Decimal(4, 2)), CAST(20.00 AS Decimal(10, 2)), CAST(N'2023-01-01' AS Date), CAST(N'2023-12-20' AS Date), NULL)
INSERT [dbo].[Cuenta] ([Id], [Guid], [Nombre], [Clabe], [Nota], [Interes], [Balance], [FechaInicial], [FechaFinal], [TipoDeCuentaId]) VALUES (1044, N'5ea19ba4-cf1a-4d6d-b964-8ae5d12186b2', N'Bbva inv', N'', N'Concentradora', CAST(9.92 AS Decimal(4, 2)), NULL, CAST(N'2023-01-01' AS Date), CAST(N'2023-12-20' AS Date), 3)
SET IDENTITY_INSERT [dbo].[Cuenta] OFF
GO
SET IDENTITY_INSERT [dbo].[HistorialDeApartados] ON 

INSERT [dbo].[HistorialDeApartados] ([Id], [Guid], [Cantidad], [FechaDeRegistro], [Nota], [CuentaId], [Interes]) VALUES (2, N'9e74476b-f68e-4ebf-a40f-7ec41ba9c8e4', CAST(27311.84 AS Decimal(10, 2)), CAST(N'2023-11-13T18:01:32.877' AS DateTime), N'20797.84 bondia 6512.99 Cetes', 1035, CAST(11.10 AS Decimal(4, 2)))
INSERT [dbo].[HistorialDeApartados] ([Id], [Guid], [Cantidad], [FechaDeRegistro], [Nota], [CuentaId], [Interes]) VALUES (3, N'12bce417-0905-4b31-b2a6-7847c00ea521', CAST(41785.00 AS Decimal(10, 2)), CAST(N'2023-11-14T09:33:04.217' AS DateTime), N'41 777.81 Concentradora', 1026, CAST(9.68 AS Decimal(4, 2)))
INSERT [dbo].[HistorialDeApartados] ([Id], [Guid], [Cantidad], [FechaDeRegistro], [Nota], [CuentaId], [Interes]) VALUES (4, N'91311094-0f3c-4e14-adc2-4ba51964bf20', CAST(41802.41 AS Decimal(10, 2)), CAST(N'2023-11-15T13:54:00.560' AS DateTime), N'Concentradora', 1026, CAST(9.88 AS Decimal(4, 2)))
INSERT [dbo].[HistorialDeApartados] ([Id], [Guid], [Cantidad], [FechaDeRegistro], [Nota], [CuentaId], [Interes]) VALUES (5, N'912529fa-63c3-4ebb-960f-36d04a2c53a6', CAST(27318.22 AS Decimal(10, 2)), CAST(N'2023-11-15T13:58:24.150' AS DateTime), N'20,804.22 Bondia, 6512.99 Cetes', 1035, CAST(11.21 AS Decimal(4, 2)))
INSERT [dbo].[HistorialDeApartados] ([Id], [Guid], [Cantidad], [FechaDeRegistro], [Nota], [CuentaId], [Interes]) VALUES (6, N'1f95ee0b-47ef-48f6-8e7d-0b0e308607c7', CAST(27825.22 AS Decimal(10, 2)), CAST(N'2023-11-16T09:56:54.607' AS DateTime), N'6314.25 Bondia, 21 548.18 Cetes', 1035, CAST(11.29 AS Decimal(4, 2)))
INSERT [dbo].[HistorialDeApartados] ([Id], [Guid], [Cantidad], [FechaDeRegistro], [Nota], [CuentaId], [Interes]) VALUES (7, N'b518d01f-6f5d-4d6e-9b25-5000826e4a28', CAST(41813.55 AS Decimal(10, 2)), CAST(N'2023-11-16T09:58:22.437' AS DateTime), N'Concentradora', 1026, CAST(9.92 AS Decimal(4, 2)))
SET IDENTITY_INSERT [dbo].[HistorialDeApartados] OFF
GO
SET IDENTITY_INSERT [dbo].[Presupuesto] ON 

INSERT [dbo].[Presupuesto] ([Id], [Nombre], [Cantidad], [SubcategoriaId], [VersionId], [Guid]) VALUES (2, N'Prueba', CAST(100.00 AS Decimal(10, 2)), 1, 2, N'4eb1c42d-d613-47fd-a662-c6dcd774b2f5')
INSERT [dbo].[Presupuesto] ([Id], [Nombre], [Cantidad], [SubcategoriaId], [VersionId], [Guid]) VALUES (3, NULL, CAST(100.00 AS Decimal(10, 2)), 2, 2, N'7087ebab-98cb-42fa-a843-a01732aa7cf0')
INSERT [dbo].[Presupuesto] ([Id], [Nombre], [Cantidad], [SubcategoriaId], [VersionId], [Guid]) VALUES (4, NULL, CAST(1200.00 AS Decimal(10, 2)), 3, 2, N'37f8e18c-88c2-4634-9ddd-45bbc3e17e23')
INSERT [dbo].[Presupuesto] ([Id], [Nombre], [Cantidad], [SubcategoriaId], [VersionId], [Guid]) VALUES (5, NULL, CAST(100.00 AS Decimal(10, 2)), 2, 2, N'5ae0319f-5e14-4246-b97a-b2e6ea6c7024')
INSERT [dbo].[Presupuesto] ([Id], [Nombre], [Cantidad], [SubcategoriaId], [VersionId], [Guid]) VALUES (6, NULL, CAST(350.00 AS Decimal(10, 2)), 2, 2, N'a4469c19-61e2-4954-8a1e-4207ebb3a05f')
INSERT [dbo].[Presupuesto] ([Id], [Nombre], [Cantidad], [SubcategoriaId], [VersionId], [Guid]) VALUES (7, NULL, CAST(275.00 AS Decimal(10, 2)), 2, 2, N'ebf6cf0e-a18f-4170-a538-908d4df8f7ad')
INSERT [dbo].[Presupuesto] ([Id], [Nombre], [Cantidad], [SubcategoriaId], [VersionId], [Guid]) VALUES (8, NULL, CAST(100.00 AS Decimal(10, 2)), 2, 2, N'59cccde2-7fc4-46db-a2e6-936daba4ca63')
INSERT [dbo].[Presupuesto] ([Id], [Nombre], [Cantidad], [SubcategoriaId], [VersionId], [Guid]) VALUES (9, NULL, CAST(300.00 AS Decimal(10, 2)), 3, 2, N'22dc138a-b3f8-4f82-9dd7-cc1fb7549750')
INSERT [dbo].[Presupuesto] ([Id], [Nombre], [Cantidad], [SubcategoriaId], [VersionId], [Guid]) VALUES (10, NULL, CAST(350.00 AS Decimal(10, 2)), 28, 2, N'fd11f8e9-687b-414e-8a5b-638ffe4d81b4')
INSERT [dbo].[Presupuesto] ([Id], [Nombre], [Cantidad], [SubcategoriaId], [VersionId], [Guid]) VALUES (11, NULL, CAST(1650.00 AS Decimal(10, 2)), 25, 2, N'bd979246-32d7-40ba-9f75-2593e70791bd')
INSERT [dbo].[Presupuesto] ([Id], [Nombre], [Cantidad], [SubcategoriaId], [VersionId], [Guid]) VALUES (12, NULL, CAST(200.00 AS Decimal(10, 2)), 26, 2, N'a4e3f81f-de8e-412b-880d-d055c48068e5')
INSERT [dbo].[Presupuesto] ([Id], [Nombre], [Cantidad], [SubcategoriaId], [VersionId], [Guid]) VALUES (13, NULL, CAST(250.00 AS Decimal(10, 2)), 27, 2, N'30f9c7fd-ef7b-4979-90b9-a89882ef3dab')
INSERT [dbo].[Presupuesto] ([Id], [Nombre], [Cantidad], [SubcategoriaId], [VersionId], [Guid]) VALUES (14, NULL, CAST(500.00 AS Decimal(10, 2)), 12, 2, N'71e65be2-763a-4174-81b3-82bde85debb4')
INSERT [dbo].[Presupuesto] ([Id], [Nombre], [Cantidad], [SubcategoriaId], [VersionId], [Guid]) VALUES (15, NULL, CAST(200.00 AS Decimal(10, 2)), 29, 2, N'19c818d4-7eaf-48bb-939a-9dfa3a39622c')
INSERT [dbo].[Presupuesto] ([Id], [Nombre], [Cantidad], [SubcategoriaId], [VersionId], [Guid]) VALUES (16, NULL, CAST(3050.00 AS Decimal(10, 2)), 30, 2, N'eeaff959-e2db-4f30-aeec-f1e0e4e09aa8')
INSERT [dbo].[Presupuesto] ([Id], [Nombre], [Cantidad], [SubcategoriaId], [VersionId], [Guid]) VALUES (17, NULL, CAST(100.00 AS Decimal(10, 2)), 13, 2, N'9343fe49-1508-42a0-bc0e-6b25254e9fd5')
INSERT [dbo].[Presupuesto] ([Id], [Nombre], [Cantidad], [SubcategoriaId], [VersionId], [Guid]) VALUES (18, NULL, CAST(350.00 AS Decimal(10, 2)), 14, 2, N'2304105a-8241-47c9-bd2a-e79f8150f5f0')
INSERT [dbo].[Presupuesto] ([Id], [Nombre], [Cantidad], [SubcategoriaId], [VersionId], [Guid]) VALUES (19, NULL, CAST(100.00 AS Decimal(10, 2)), 23, 2, N'2d55cba9-4ef7-4a7c-adbd-1425e68c1057')
INSERT [dbo].[Presupuesto] ([Id], [Nombre], [Cantidad], [SubcategoriaId], [VersionId], [Guid]) VALUES (20, NULL, CAST(50.00 AS Decimal(10, 2)), 31, 2, N'7ff52806-d14b-4c47-85d3-435ceadf12b8')
INSERT [dbo].[Presupuesto] ([Id], [Nombre], [Cantidad], [SubcategoriaId], [VersionId], [Guid]) VALUES (21, NULL, CAST(500.00 AS Decimal(10, 2)), 32, 2, N'e1e93155-2be5-463c-bc73-499c5ea58119')
INSERT [dbo].[Presupuesto] ([Id], [Nombre], [Cantidad], [SubcategoriaId], [VersionId], [Guid]) VALUES (22, NULL, CAST(500.00 AS Decimal(10, 2)), 33, 2, N'e5d98f31-e1a6-45aa-a7c6-3b14289fa3fb')
INSERT [dbo].[Presupuesto] ([Id], [Nombre], [Cantidad], [SubcategoriaId], [VersionId], [Guid]) VALUES (23, NULL, CAST(105.00 AS Decimal(10, 2)), 15, 2, N'2efa694b-e9b7-422e-9e25-458664927c9c')
INSERT [dbo].[Presupuesto] ([Id], [Nombre], [Cantidad], [SubcategoriaId], [VersionId], [Guid]) VALUES (24, NULL, CAST(1300.00 AS Decimal(10, 2)), 34, 2, N'4bf2ca4b-1710-4f64-8db8-176de7f2bedd')
INSERT [dbo].[Presupuesto] ([Id], [Nombre], [Cantidad], [SubcategoriaId], [VersionId], [Guid]) VALUES (25, NULL, CAST(100.00 AS Decimal(10, 2)), 16, 2, N'57c17621-0f76-48a5-8e3b-676c4ec683c3')
INSERT [dbo].[Presupuesto] ([Id], [Nombre], [Cantidad], [SubcategoriaId], [VersionId], [Guid]) VALUES (26, NULL, CAST(600.00 AS Decimal(10, 2)), 35, 2, N'5c9b9dac-878f-4c79-ad2e-8834776ecda3')
INSERT [dbo].[Presupuesto] ([Id], [Nombre], [Cantidad], [SubcategoriaId], [VersionId], [Guid]) VALUES (27, NULL, CAST(600.00 AS Decimal(10, 2)), 36, 2, N'51ff5d06-078e-4f71-9690-5808775bc3a6')
INSERT [dbo].[Presupuesto] ([Id], [Nombre], [Cantidad], [SubcategoriaId], [VersionId], [Guid]) VALUES (28, NULL, CAST(100.00 AS Decimal(10, 2)), 17, 2, N'7819d1be-3be1-4fef-b7ff-0a4f96237885')
INSERT [dbo].[Presupuesto] ([Id], [Nombre], [Cantidad], [SubcategoriaId], [VersionId], [Guid]) VALUES (29, NULL, CAST(225.00 AS Decimal(10, 2)), 18, 2, N'0ca965e0-9ed3-46cb-ab84-c6b7cba56dad')
INSERT [dbo].[Presupuesto] ([Id], [Nombre], [Cantidad], [SubcategoriaId], [VersionId], [Guid]) VALUES (30, NULL, CAST(400.00 AS Decimal(10, 2)), 9, 2, N'c999b7d4-7c13-4c40-983f-f8238239539a')
INSERT [dbo].[Presupuesto] ([Id], [Nombre], [Cantidad], [SubcategoriaId], [VersionId], [Guid]) VALUES (31, NULL, CAST(200.00 AS Decimal(10, 2)), 37, 2, N'a3f2b0f3-f972-4044-8dd2-d41cdc5fd8f6')
INSERT [dbo].[Presupuesto] ([Id], [Nombre], [Cantidad], [SubcategoriaId], [VersionId], [Guid]) VALUES (32, NULL, CAST(100.00 AS Decimal(10, 2)), 7, 2, N'8f4c8d8f-539d-4dbb-a5e4-4207ad1fbbc4')
INSERT [dbo].[Presupuesto] ([Id], [Nombre], [Cantidad], [SubcategoriaId], [VersionId], [Guid]) VALUES (33, NULL, CAST(500.00 AS Decimal(10, 2)), 19, 2, N'568d3cec-f916-42fd-a32d-51d7ec6033ba')
INSERT [dbo].[Presupuesto] ([Id], [Nombre], [Cantidad], [SubcategoriaId], [VersionId], [Guid]) VALUES (34, NULL, CAST(500.00 AS Decimal(10, 2)), 20, 2, N'15994f56-9845-4130-9d11-fa0225ec371f')
SET IDENTITY_INSERT [dbo].[Presupuesto] OFF
GO
SET IDENTITY_INSERT [dbo].[Subcategoria] ON 

INSERT [dbo].[Subcategoria] ([Id], [Guid], [Nombre], [Presupuesto], [EstaActivo], [EsPrimario], [CategoriaId]) VALUES (1, N'18fe131f-fe8b-4c91-af58-358b25aabb98', N'Afore', CAST(100.00 AS Decimal(10, 2)), 1, 0, 2)
INSERT [dbo].[Subcategoria] ([Id], [Guid], [Nombre], [Presupuesto], [EstaActivo], [EsPrimario], [CategoriaId]) VALUES (2, N'd8f78850-abc6-41b6-924f-0a649a4027e8', N'Ahorro N', CAST(100.00 AS Decimal(10, 2)), 1, 0, 2)
INSERT [dbo].[Subcategoria] ([Id], [Guid], [Nombre], [Presupuesto], [EstaActivo], [EsPrimario], [CategoriaId]) VALUES (3, N'f4c0ffdf-c684-4f04-ab0d-981b5ca8d41b', N'Vacaciones', CAST(100.00 AS Decimal(10, 2)), 0, 0, 1)
INSERT [dbo].[Subcategoria] ([Id], [Guid], [Nombre], [Presupuesto], [EstaActivo], [EsPrimario], [CategoriaId]) VALUES (4, N'bf591c26-7894-4ce7-8cc6-fe6553a51367', N'Vacaciones', CAST(100.00 AS Decimal(10, 2)), 0, 0, 2)
INSERT [dbo].[Subcategoria] ([Id], [Guid], [Nombre], [Presupuesto], [EstaActivo], [EsPrimario], [CategoriaId]) VALUES (5, N'be5c338a-a714-4f91-afd9-df288973a841', N'Vacaciones', CAST(100.00 AS Decimal(10, 2)), 0, 0, 2)
INSERT [dbo].[Subcategoria] ([Id], [Guid], [Nombre], [Presupuesto], [EstaActivo], [EsPrimario], [CategoriaId]) VALUES (6, N'b965c379-a2d3-4ad4-a6ac-28a61e4b45ef', N'Vacaciones', CAST(100.00 AS Decimal(10, 2)), 0, 0, 2)
INSERT [dbo].[Subcategoria] ([Id], [Guid], [Nombre], [Presupuesto], [EstaActivo], [EsPrimario], [CategoriaId]) VALUES (7, N'0ded2f56-c15b-4345-8bc0-67a67a20dd98', N'Vacaciones', CAST(100.00 AS Decimal(10, 2)), 1, 0, 2)
INSERT [dbo].[Subcategoria] ([Id], [Guid], [Nombre], [Presupuesto], [EstaActivo], [EsPrimario], [CategoriaId]) VALUES (8, N'169ac9ea-527d-48ea-9d33-e404fcacac03', N'Apartado Medico', CAST(100.00 AS Decimal(10, 2)), 1, 0, 2)
INSERT [dbo].[Subcategoria] ([Id], [Guid], [Nombre], [Presupuesto], [EstaActivo], [EsPrimario], [CategoriaId]) VALUES (9, N'b1f422ee-d408-474c-984d-c0784174a220', N'Tlaxcoapan', CAST(400.00 AS Decimal(10, 2)), 1, 0, 2)
INSERT [dbo].[Subcategoria] ([Id], [Guid], [Nombre], [Presupuesto], [EstaActivo], [EsPrimario], [CategoriaId]) VALUES (10, N'b82c5ced-1a1c-4297-be33-9cd93f13db30', N'Camioneta', CAST(350.00 AS Decimal(10, 2)), 1, 0, 2)
INSERT [dbo].[Subcategoria] ([Id], [Guid], [Nombre], [Presupuesto], [EstaActivo], [EsPrimario], [CategoriaId]) VALUES (11, N'fafe0f5b-b2ad-4b4b-af37-09ec78d5040b', N'Cdmx internet y luz', CAST(275.00 AS Decimal(10, 2)), 1, 0, 2)
INSERT [dbo].[Subcategoria] ([Id], [Guid], [Nombre], [Presupuesto], [EstaActivo], [EsPrimario], [CategoriaId]) VALUES (12, N'86cd89e6-57f0-4340-a75b-eaf1b61f2363', N'Gatos', CAST(500.00 AS Decimal(10, 2)), 1, 0, 2)
INSERT [dbo].[Subcategoria] ([Id], [Guid], [Nombre], [Presupuesto], [EstaActivo], [EsPrimario], [CategoriaId]) VALUES (13, N'f1b09119-e65b-4a0d-88a9-1b0a23e4be54', N'Ropa', CAST(100.00 AS Decimal(10, 2)), 1, 0, 2)
INSERT [dbo].[Subcategoria] ([Id], [Guid], [Nombre], [Presupuesto], [EstaActivo], [EsPrimario], [CategoriaId]) VALUES (14, N'e0f255d4-3c89-474e-bd51-8d82f1f2fc13', N'Sabatico', CAST(350.00 AS Decimal(10, 2)), 1, 0, 2)
INSERT [dbo].[Subcategoria] ([Id], [Guid], [Nombre], [Presupuesto], [EstaActivo], [EsPrimario], [CategoriaId]) VALUES (15, N'd08d89fa-ee19-4ccd-9483-e95695d3a373', N'TDC Anualidad', CAST(105.00 AS Decimal(10, 2)), 1, 0, 2)
INSERT [dbo].[Subcategoria] ([Id], [Guid], [Nombre], [Presupuesto], [EstaActivo], [EsPrimario], [CategoriaId]) VALUES (16, N'7db7d22a-9708-4c2d-b570-ab6ba5267002', N'Tech', CAST(100.00 AS Decimal(10, 2)), 1, 0, 2)
INSERT [dbo].[Subcategoria] ([Id], [Guid], [Nombre], [Presupuesto], [EstaActivo], [EsPrimario], [CategoriaId]) VALUES (17, N'b48a9d7c-93db-4b71-9a8c-ee8117146535', N'Titulacion', CAST(100.00 AS Decimal(10, 2)), 1, 0, 2)
INSERT [dbo].[Subcategoria] ([Id], [Guid], [Nombre], [Presupuesto], [EstaActivo], [EsPrimario], [CategoriaId]) VALUES (18, N'a5bb39e0-03e6-47d4-a264-87d125bd7e89', N'Tlax Internet y LUZ', CAST(225.00 AS Decimal(10, 2)), 1, 0, 2)
INSERT [dbo].[Subcategoria] ([Id], [Guid], [Nombre], [Presupuesto], [EstaActivo], [EsPrimario], [CategoriaId]) VALUES (19, N'8267e0df-3322-4ee4-8370-5f39f9a8de3b', N'Vacaciones Cuba Nancy', CAST(500.00 AS Decimal(10, 2)), 1, 0, 2)
INSERT [dbo].[Subcategoria] ([Id], [Guid], [Nombre], [Presupuesto], [EstaActivo], [EsPrimario], [CategoriaId]) VALUES (20, N'746a7733-377b-465b-b650-597824650f7b', N'Vacaciones Cuba Yop', CAST(500.00 AS Decimal(10, 2)), 1, 0, 2)
INSERT [dbo].[Subcategoria] ([Id], [Guid], [Nombre], [Presupuesto], [EstaActivo], [EsPrimario], [CategoriaId]) VALUES (21, N'd80f074c-24d2-4071-9984-504af21acc52', N'Alimentacion ', CAST(1200.00 AS Decimal(10, 2)), 1, 0, 3)
INSERT [dbo].[Subcategoria] ([Id], [Guid], [Nombre], [Presupuesto], [EstaActivo], [EsPrimario], [CategoriaId]) VALUES (22, N'2d007b0a-e998-4ee2-ab55-93fbb7327ce3', N'CdmxAgua', CAST(100.00 AS Decimal(10, 2)), 1, 1, 2)
INSERT [dbo].[Subcategoria] ([Id], [Guid], [Nombre], [Presupuesto], [EstaActivo], [EsPrimario], [CategoriaId]) VALUES (23, N'37ecd9d4-8a5d-48f2-86e7-aa4f45605ed1', N'Saldo cel', CAST(100.00 AS Decimal(10, 2)), 1, 1, 3)
INSERT [dbo].[Subcategoria] ([Id], [Guid], [Nombre], [Presupuesto], [EstaActivo], [EsPrimario], [CategoriaId]) VALUES (24, N'15683d18-7a6c-4de6-a67e-e1737d59d3a1', N'Comodin', CAST(300.00 AS Decimal(10, 2)), 1, 0, 3)
INSERT [dbo].[Subcategoria] ([Id], [Guid], [Nombre], [Presupuesto], [EstaActivo], [EsPrimario], [CategoriaId]) VALUES (25, N'286c5191-4253-4de3-a5cb-888857ace4a3', N'Doña', CAST(1650.00 AS Decimal(10, 2)), 1, 0, 3)
INSERT [dbo].[Subcategoria] ([Id], [Guid], [Nombre], [Presupuesto], [EstaActivo], [EsPrimario], [CategoriaId]) VALUES (26, N'cb331b30-5275-44b3-bbfc-ffcfd545a08c', N'Entretenimineto', CAST(200.00 AS Decimal(10, 2)), 1, 0, 2)
INSERT [dbo].[Subcategoria] ([Id], [Guid], [Nombre], [Presupuesto], [EstaActivo], [EsPrimario], [CategoriaId]) VALUES (27, N'5b26970d-fbeb-4fe2-aa83-d0f907ce4622', N'Gas', CAST(250.00 AS Decimal(10, 2)), 1, 0, 3)
INSERT [dbo].[Subcategoria] ([Id], [Guid], [Nombre], [Presupuesto], [EstaActivo], [EsPrimario], [CategoriaId]) VALUES (28, N'6352b971-867e-44ba-a30f-1b708072fbfc', N'Deportes', CAST(350.00 AS Decimal(10, 2)), 1, 0, 3)
INSERT [dbo].[Subcategoria] ([Id], [Guid], [Nombre], [Presupuesto], [EstaActivo], [EsPrimario], [CategoriaId]) VALUES (29, N'f8f97a6f-f525-43ca-85b7-c77ebbea4df1', N'Natacion', CAST(200.00 AS Decimal(10, 2)), 1, 0, 2)
INSERT [dbo].[Subcategoria] ([Id], [Guid], [Nombre], [Presupuesto], [EstaActivo], [EsPrimario], [CategoriaId]) VALUES (30, N'210a68b6-a9cf-45c9-968b-a0ee27413690', N'Renta', CAST(3050.00 AS Decimal(10, 2)), 1, 1, 3)
INSERT [dbo].[Subcategoria] ([Id], [Guid], [Nombre], [Presupuesto], [EstaActivo], [EsPrimario], [CategoriaId]) VALUES (31, N'1d8b403c-4080-4a8a-b215-7d62fed252c1', N'Seguro bbva', CAST(50.00 AS Decimal(10, 2)), 1, 0, 2)
INSERT [dbo].[Subcategoria] ([Id], [Guid], [Nombre], [Presupuesto], [EstaActivo], [EsPrimario], [CategoriaId]) VALUES (32, N'b70825e9-1b2b-455d-aa91-4d8b7857e18a', N'Semana 1', CAST(500.00 AS Decimal(10, 2)), 1, 1, 2)
INSERT [dbo].[Subcategoria] ([Id], [Guid], [Nombre], [Presupuesto], [EstaActivo], [EsPrimario], [CategoriaId]) VALUES (33, N'76a7a166-e82e-4b3a-bfcf-937153ed4bd0', N'Semana 2', CAST(500.00 AS Decimal(10, 2)), 1, 1, 3)
INSERT [dbo].[Subcategoria] ([Id], [Guid], [Nombre], [Presupuesto], [EstaActivo], [EsPrimario], [CategoriaId]) VALUES (34, N'b1d6fdea-0e73-4ef6-b2df-2d6118dbd0b0', N'Tdc pago', CAST(1300.00 AS Decimal(10, 2)), 1, 0, 3)
INSERT [dbo].[Subcategoria] ([Id], [Guid], [Nombre], [Presupuesto], [EstaActivo], [EsPrimario], [CategoriaId]) VALUES (35, N'cd5725b5-5fd6-44de-8e9c-fed66f12bcb5', N'Tianguis 1', CAST(600.00 AS Decimal(10, 2)), 1, 1, 3)
INSERT [dbo].[Subcategoria] ([Id], [Guid], [Nombre], [Presupuesto], [EstaActivo], [EsPrimario], [CategoriaId]) VALUES (36, N'7ba1b0fa-5a52-4084-85bb-164a178aef32', N'Tianguis 2', CAST(600.00 AS Decimal(10, 2)), 1, 1, 3)
INSERT [dbo].[Subcategoria] ([Id], [Guid], [Nombre], [Presupuesto], [EstaActivo], [EsPrimario], [CategoriaId]) VALUES (37, N'e9137e29-69b0-4afa-962f-d608c930b842', N'Transporte', CAST(200.00 AS Decimal(10, 2)), 1, 1, 3)
SET IDENTITY_INSERT [dbo].[Subcategoria] OFF
GO
SET IDENTITY_INSERT [dbo].[TipoDeCuenta] ON 

INSERT [dbo].[TipoDeCuenta] ([Id], [Guid], [Nombre], [Descripción]) VALUES (1, N'10c58264-8017-4c3e-9add-61351bbb4488', N'Entradas', N'Donde se manejan las nóminas')
INSERT [dbo].[TipoDeCuenta] ([Id], [Guid], [Nombre], [Descripción]) VALUES (2, N'f8b4f4ac-801c-47e8-834d-77c6e052b494', N'Apartados', N'Detalle de los apartados')
INSERT [dbo].[TipoDeCuenta] ([Id], [Guid], [Nombre], [Descripción]) VALUES (3, N'4d1a6881-a2ca-434f-8314-5f4912df66d1', N'Concentradora de apartados', N'Concentradora de apartados')
SET IDENTITY_INSERT [dbo].[TipoDeCuenta] OFF
GO
SET IDENTITY_INSERT [dbo].[Transaccion] ON 

INSERT [dbo].[Transaccion] ([Id], [CuentaId], [Guid], [Cantidad], [FechaDeRegistro], [Tipo], [Concepto], [Nota], [Referencia]) VALUES (9, 1007, N'b10c6b05-329b-43b6-97f8-9bd1b2f0289a', CAST(23793.52 AS Decimal(10, 2)), CAST(N'2023-11-13T14:05:58.027' AS DateTime), N'Deposito', N'Balance', N'', NULL)
INSERT [dbo].[Transaccion] ([Id], [CuentaId], [Guid], [Cantidad], [FechaDeRegistro], [Tipo], [Concepto], [Nota], [Referencia]) VALUES (10, 1009, N'c6b76f72-e910-4b70-a708-98ff939d99dc', CAST(325.51 AS Decimal(10, 2)), CAST(N'2023-11-13T14:16:08.290' AS DateTime), N'Deposito', N'Balance', N'', NULL)
INSERT [dbo].[Transaccion] ([Id], [CuentaId], [Guid], [Cantidad], [FechaDeRegistro], [Tipo], [Concepto], [Nota], [Referencia]) VALUES (11, 1010, N'7e81b72a-01e7-411d-954a-bebcf0061eea', CAST(10123.38 AS Decimal(10, 2)), CAST(N'2023-11-13T14:18:59.257' AS DateTime), N'Deposito', N'Balance', N'', NULL)
INSERT [dbo].[Transaccion] ([Id], [CuentaId], [Guid], [Cantidad], [FechaDeRegistro], [Tipo], [Concepto], [Nota], [Referencia]) VALUES (12, 1011, N'9344697c-ff4d-455c-b465-1246781b9e9c', CAST(58689.91 AS Decimal(10, 2)), CAST(N'2023-11-13T14:22:59.313' AS DateTime), N'Deposito', N'Balance', N'', NULL)
INSERT [dbo].[Transaccion] ([Id], [CuentaId], [Guid], [Cantidad], [FechaDeRegistro], [Tipo], [Concepto], [Nota], [Referencia]) VALUES (13, 1012, N'974267d4-d0e1-4ef7-a3b3-887dcf0b42f0', CAST(27624.90 AS Decimal(10, 2)), CAST(N'2023-11-13T14:25:29.540' AS DateTime), N'Deposito', N'Balance', N'', NULL)
INSERT [dbo].[Transaccion] ([Id], [CuentaId], [Guid], [Cantidad], [FechaDeRegistro], [Tipo], [Concepto], [Nota], [Referencia]) VALUES (14, 1013, N'1f84cd92-3a68-4bfe-8acb-af0a8f6e420c', CAST(107.83 AS Decimal(10, 2)), CAST(N'2023-11-13T14:27:25.677' AS DateTime), N'Deposito', N'Balance', N'', NULL)
INSERT [dbo].[Transaccion] ([Id], [CuentaId], [Guid], [Cantidad], [FechaDeRegistro], [Tipo], [Concepto], [Nota], [Referencia]) VALUES (15, 1014, N'2eee418c-4ceb-43f5-a6de-cf527b49f7cf', CAST(503.25 AS Decimal(10, 2)), CAST(N'2023-11-13T14:30:19.683' AS DateTime), N'Deposito', N'Balance', N'', NULL)
INSERT [dbo].[Transaccion] ([Id], [CuentaId], [Guid], [Cantidad], [FechaDeRegistro], [Tipo], [Concepto], [Nota], [Referencia]) VALUES (16, 1015, N'054aa9ed-630e-498f-8240-bb801dad4251', CAST(1204.21 AS Decimal(10, 2)), CAST(N'2023-11-13T14:32:05.033' AS DateTime), N'Deposito', N'Balance', N'', NULL)
INSERT [dbo].[Transaccion] ([Id], [CuentaId], [Guid], [Cantidad], [FechaDeRegistro], [Tipo], [Concepto], [Nota], [Referencia]) VALUES (17, 1016, N'ab1781bd-1bf5-4094-9a8a-331e7902dcb6', CAST(2138.82 AS Decimal(10, 2)), CAST(N'2023-11-13T16:30:03.423' AS DateTime), N'Deposito', N'Balance', N'', NULL)
INSERT [dbo].[Transaccion] ([Id], [CuentaId], [Guid], [Cantidad], [FechaDeRegistro], [Tipo], [Concepto], [Nota], [Referencia]) VALUES (18, 1016, N'c9c0a013-490f-44b2-9a95-a6da805d2fd6', CAST(336.00 AS Decimal(10, 2)), CAST(N'2023-11-13T16:33:09.110' AS DateTime), N'Retiro', NULL, N'Cicaticure', NULL)
INSERT [dbo].[Transaccion] ([Id], [CuentaId], [Guid], [Cantidad], [FechaDeRegistro], [Tipo], [Concepto], [Nota], [Referencia]) VALUES (19, 1017, N'cae732e3-77a5-464f-b82a-40efddc401e0', CAST(1815.30 AS Decimal(10, 2)), CAST(N'2023-11-13T16:35:54.857' AS DateTime), N'Deposito', N'Balance', N'', NULL)
INSERT [dbo].[Transaccion] ([Id], [CuentaId], [Guid], [Cantidad], [FechaDeRegistro], [Tipo], [Concepto], [Nota], [Referencia]) VALUES (20, 1018, N'b68e0d94-4d54-4403-9ddb-d4663d98a7ef', CAST(13857.42 AS Decimal(10, 2)), CAST(N'2023-11-13T16:37:25.797' AS DateTime), N'Deposito', N'Balance', N'', NULL)
INSERT [dbo].[Transaccion] ([Id], [CuentaId], [Guid], [Cantidad], [FechaDeRegistro], [Tipo], [Concepto], [Nota], [Referencia]) VALUES (21, 1019, N'712cc2a5-171a-4e21-bbd2-7d8fce9e237c', CAST(1208.22 AS Decimal(10, 2)), CAST(N'2023-11-13T16:39:19.663' AS DateTime), N'Deposito', N'Balance', N'', NULL)
INSERT [dbo].[Transaccion] ([Id], [CuentaId], [Guid], [Cantidad], [FechaDeRegistro], [Tipo], [Concepto], [Nota], [Referencia]) VALUES (22, 1020, N'f342d3c7-2af6-4787-b7dd-31ac440a99ac', CAST(956.51 AS Decimal(10, 2)), CAST(N'2023-11-13T16:43:22.220' AS DateTime), N'Deposito', N'Balance', N'', NULL)
INSERT [dbo].[Transaccion] ([Id], [CuentaId], [Guid], [Cantidad], [FechaDeRegistro], [Tipo], [Concepto], [Nota], [Referencia]) VALUES (23, 1021, N'1fc1c2b3-73b7-4901-ac62-e3333b0bc5af', CAST(1577.40 AS Decimal(10, 2)), CAST(N'2023-11-13T16:44:42.770' AS DateTime), N'Deposito', N'Balance', N'', NULL)
INSERT [dbo].[Transaccion] ([Id], [CuentaId], [Guid], [Cantidad], [FechaDeRegistro], [Tipo], [Concepto], [Nota], [Referencia]) VALUES (24, 1022, N'1e132dbd-5795-4aa7-8cf6-3f3987038d7e', CAST(2064.05 AS Decimal(10, 2)), CAST(N'2023-11-13T16:45:48.240' AS DateTime), N'Deposito', N'Balance', N'', NULL)
INSERT [dbo].[Transaccion] ([Id], [CuentaId], [Guid], [Cantidad], [FechaDeRegistro], [Tipo], [Concepto], [Nota], [Referencia]) VALUES (25, 1023, N'c889f489-fa51-4596-9736-0737d39c89f1', CAST(1473.81 AS Decimal(10, 2)), CAST(N'2023-11-13T16:46:58.103' AS DateTime), N'Deposito', N'Balance', N'', NULL)
INSERT [dbo].[Transaccion] ([Id], [CuentaId], [Guid], [Cantidad], [FechaDeRegistro], [Tipo], [Concepto], [Nota], [Referencia]) VALUES (26, 1024, N'2a6807d7-86ab-477f-99a8-8d43057435f5', CAST(4060.98 AS Decimal(10, 2)), CAST(N'2023-11-13T16:48:04.233' AS DateTime), N'Deposito', N'Balance', N'', NULL)
INSERT [dbo].[Transaccion] ([Id], [CuentaId], [Guid], [Cantidad], [FechaDeRegistro], [Tipo], [Concepto], [Nota], [Referencia]) VALUES (27, 1025, N'704eb411-75b3-48d4-8b9d-2e6446c20ecb', CAST(10823.69 AS Decimal(10, 2)), CAST(N'2023-11-13T16:49:18.553' AS DateTime), N'Deposito', N'Balance', N'', NULL)
INSERT [dbo].[Transaccion] ([Id], [CuentaId], [Guid], [Cantidad], [FechaDeRegistro], [Tipo], [Concepto], [Nota], [Referencia]) VALUES (28, 1026, N'b97695ca-43bd-41eb-a71a-c85f69c551c1', CAST(87.33 AS Decimal(10, 2)), CAST(N'2023-11-13T16:50:39.903' AS DateTime), N'Deposito', N'Balance', N'', NULL)
INSERT [dbo].[Transaccion] ([Id], [CuentaId], [Guid], [Cantidad], [FechaDeRegistro], [Tipo], [Concepto], [Nota], [Referencia]) VALUES (29, 1027, N'082495fc-25ff-411d-984f-7ef292ed86ea', CAST(6225.75 AS Decimal(10, 2)), CAST(N'2023-11-13T16:51:42.410' AS DateTime), N'Deposito', N'Balance', N'', NULL)
INSERT [dbo].[Transaccion] ([Id], [CuentaId], [Guid], [Cantidad], [FechaDeRegistro], [Tipo], [Concepto], [Nota], [Referencia]) VALUES (30, 1028, N'1d1080de-a817-4712-b4c1-70d6fb8b6ab8', CAST(350.00 AS Decimal(10, 2)), CAST(N'2023-11-13T16:53:03.313' AS DateTime), N'Deposito', N'Balance', N'', NULL)
INSERT [dbo].[Transaccion] ([Id], [CuentaId], [Guid], [Cantidad], [FechaDeRegistro], [Tipo], [Concepto], [Nota], [Referencia]) VALUES (31, 1029, N'd95eb481-ea7f-4a82-87ce-1d953b1ef3df', CAST(700.00 AS Decimal(10, 2)), CAST(N'2023-11-13T16:54:00.817' AS DateTime), N'Deposito', N'Balance', N'', NULL)
INSERT [dbo].[Transaccion] ([Id], [CuentaId], [Guid], [Cantidad], [FechaDeRegistro], [Tipo], [Concepto], [Nota], [Referencia]) VALUES (32, 1030, N'9a00d1fe-4408-4819-a014-26be06f352cf', CAST(400.00 AS Decimal(10, 2)), CAST(N'2023-11-13T16:54:34.770' AS DateTime), N'Deposito', N'Balance', N'', NULL)
INSERT [dbo].[Transaccion] ([Id], [CuentaId], [Guid], [Cantidad], [FechaDeRegistro], [Tipo], [Concepto], [Nota], [Referencia]) VALUES (33, 1031, N'9137cea7-3409-448e-ae7e-61f35c774b7c', CAST(600.00 AS Decimal(10, 2)), CAST(N'2023-11-13T16:55:14.290' AS DateTime), N'Deposito', N'Balance', N'', NULL)
INSERT [dbo].[Transaccion] ([Id], [CuentaId], [Guid], [Cantidad], [FechaDeRegistro], [Tipo], [Concepto], [Nota], [Referencia]) VALUES (34, 1033, N'8df796d6-c906-4152-ba3c-efe615f830ca', CAST(99.15 AS Decimal(10, 2)), CAST(N'2023-11-13T17:02:09.407' AS DateTime), N'Deposito', N'Balance', N'', NULL)
INSERT [dbo].[Transaccion] ([Id], [CuentaId], [Guid], [Cantidad], [FechaDeRegistro], [Tipo], [Concepto], [Nota], [Referencia]) VALUES (35, 1032, N'4f2ecf89-5052-42b1-8e87-79ce82647bef', CAST(7000.00 AS Decimal(10, 2)), CAST(N'2023-11-13T17:05:57.263' AS DateTime), N'Deposito', N'Balance', N'', NULL)
INSERT [dbo].[Transaccion] ([Id], [CuentaId], [Guid], [Cantidad], [FechaDeRegistro], [Tipo], [Concepto], [Nota], [Referencia]) VALUES (36, 1034, N'31c29f8e-e2fd-44f5-9970-29435d16c6b6', CAST(7500.00 AS Decimal(10, 2)), CAST(N'2023-11-13T17:06:10.560' AS DateTime), N'Deposito', N'Balance', N'', NULL)
INSERT [dbo].[Transaccion] ([Id], [CuentaId], [Guid], [Cantidad], [FechaDeRegistro], [Tipo], [Concepto], [Nota], [Referencia]) VALUES (37, 1035, N'80b92f9b-e29c-4c4a-a866-1e45c8e584fa', CAST(12737.79 AS Decimal(10, 2)), CAST(N'2023-11-13T17:14:56.423' AS DateTime), N'Deposito', N'Balance', N'', NULL)
INSERT [dbo].[Transaccion] ([Id], [CuentaId], [Guid], [Cantidad], [FechaDeRegistro], [Tipo], [Concepto], [Nota], [Referencia]) VALUES (38, 1036, N'e27725d7-dcb9-417a-aa6e-15c6c0f9b879', CAST(200.00 AS Decimal(10, 2)), CAST(N'2023-11-13T18:16:06.923' AS DateTime), N'Deposito', N'Balance', N'', NULL)
INSERT [dbo].[Transaccion] ([Id], [CuentaId], [Guid], [Cantidad], [FechaDeRegistro], [Tipo], [Concepto], [Nota], [Referencia]) VALUES (39, 1038, N'05cf262e-6ce7-4a8b-aa48-b5252fbb68cd', CAST(400.00 AS Decimal(10, 2)), CAST(N'2023-11-13T18:17:28.800' AS DateTime), N'Deposito', N'Balance', N'', NULL)
INSERT [dbo].[Transaccion] ([Id], [CuentaId], [Guid], [Cantidad], [FechaDeRegistro], [Tipo], [Concepto], [Nota], [Referencia]) VALUES (40, 1039, N'd1720a2e-ca96-47a3-8636-2cf10884cfc1', CAST(1000.00 AS Decimal(10, 2)), CAST(N'2023-11-13T18:19:12.483' AS DateTime), N'Deposito', N'Balance', N'', NULL)
INSERT [dbo].[Transaccion] ([Id], [CuentaId], [Guid], [Cantidad], [FechaDeRegistro], [Tipo], [Concepto], [Nota], [Referencia]) VALUES (41, 1040, N'f636467d-0c1b-4b67-bbfb-9030625606a7', CAST(350.00 AS Decimal(10, 2)), CAST(N'2023-11-13T18:20:19.110' AS DateTime), N'Deposito', N'Balance', N'', NULL)
INSERT [dbo].[Transaccion] ([Id], [CuentaId], [Guid], [Cantidad], [FechaDeRegistro], [Tipo], [Concepto], [Nota], [Referencia]) VALUES (42, 1042, N'9f4af2f9-6233-42be-9294-bd8a7a8dcf32', CAST(200.00 AS Decimal(10, 2)), CAST(N'2023-11-13T18:24:42.013' AS DateTime), N'Deposito', N'Balance', N'', NULL)
INSERT [dbo].[Transaccion] ([Id], [CuentaId], [Guid], [Cantidad], [FechaDeRegistro], [Tipo], [Concepto], [Nota], [Referencia]) VALUES (43, 1043, N'6d4e3d9f-86b8-4200-853e-2bb30afa84f9', CAST(20.00 AS Decimal(10, 2)), CAST(N'2023-11-13T18:25:15.413' AS DateTime), N'Deposito', N'Balance', N'', NULL)
INSERT [dbo].[Transaccion] ([Id], [CuentaId], [Guid], [Cantidad], [FechaDeRegistro], [Tipo], [Concepto], [Nota], [Referencia]) VALUES (44, 1027, N'23497d10-721c-4727-84be-050039d76d22', CAST(5990.76 AS Decimal(10, 2)), CAST(N'2023-11-14T09:39:40.547' AS DateTime), N'Retiro', NULL, N'Pago de renta', NULL)
INSERT [dbo].[Transaccion] ([Id], [CuentaId], [Guid], [Cantidad], [FechaDeRegistro], [Tipo], [Concepto], [Nota], [Referencia]) VALUES (45, 1032, N'eb0aeea5-a072-4d1a-a8e1-af9958d3b215', CAST(500.00 AS Decimal(10, 2)), CAST(N'2023-11-15T13:01:10.427' AS DateTime), N'Deposito', N'Nancy', N'', NULL)
INSERT [dbo].[Transaccion] ([Id], [CuentaId], [Guid], [Cantidad], [FechaDeRegistro], [Tipo], [Concepto], [Nota], [Referencia]) VALUES (46, 1028, N'9cac1a0c-49f4-4bad-81d8-dd4efde2c202', CAST(350.00 AS Decimal(10, 2)), CAST(N'2023-11-15T18:14:24.510' AS DateTime), N'Retiro', NULL, N'Pago de reparacion de camioneta', NULL)
INSERT [dbo].[Transaccion] ([Id], [CuentaId], [Guid], [Cantidad], [FechaDeRegistro], [Tipo], [Concepto], [Nota], [Referencia]) VALUES (47, 1030, N'5163874a-d528-43ee-a0cf-7770dac82e2f', CAST(400.00 AS Decimal(10, 2)), CAST(N'2023-11-15T18:15:58.057' AS DateTime), N'Retiro', NULL, N'Pago de reparacion camioneta', NULL)
INSERT [dbo].[Transaccion] ([Id], [CuentaId], [Guid], [Cantidad], [FechaDeRegistro], [Tipo], [Concepto], [Nota], [Referencia]) VALUES (48, 1029, N'4417f6c8-8abe-4d73-b142-3510f76cbe65', CAST(450.00 AS Decimal(10, 2)), CAST(N'2023-11-15T18:17:07.320' AS DateTime), N'Retiro', NULL, N'Reparacion de camioneta', NULL)
SET IDENTITY_INSERT [dbo].[Transaccion] OFF
GO
SET IDENTITY_INSERT [dbo].[VersionDePresupuesto] ON 

INSERT [dbo].[VersionDePresupuesto] ([Id], [Guid], [FechaInicial], [FechaFinal], [FechaDeRegistro], [EstaActivo], [Nombre]) VALUES (2, N'3f20ae93-2d1d-414a-82a0-82e7918f55ee', CAST(N'2023-10-10' AS Date), CAST(N'2023-12-20' AS Date), CAST(N'2023-11-13T19:28:06.370' AS DateTime), 1, N'4 Trimestre')
INSERT [dbo].[VersionDePresupuesto] ([Id], [Guid], [FechaInicial], [FechaFinal], [FechaDeRegistro], [EstaActivo], [Nombre]) VALUES (3, N'db9ad392-bbe8-4a01-b71c-c238c4b95ffe', CAST(N'2023-02-05' AS Date), CAST(N'2023-04-01' AS Date), CAST(N'2023-11-14T13:02:20.733' AS DateTime), 0, N'Prueba 1')
SET IDENTITY_INSERT [dbo].[VersionDePresupuesto] OFF
GO
ALTER TABLE [dbo].[Cuenta]  WITH CHECK ADD  CONSTRAINT [FK_Cuenta_TipoDeCuenta] FOREIGN KEY([TipoDeCuentaId])
REFERENCES [dbo].[TipoDeCuenta] ([Id])
GO
ALTER TABLE [dbo].[Cuenta] CHECK CONSTRAINT [FK_Cuenta_TipoDeCuenta]
GO
ALTER TABLE [dbo].[HistorialDeApartados]  WITH CHECK ADD  CONSTRAINT [FK_HistorialDeApartados_Cuenta] FOREIGN KEY([CuentaId])
REFERENCES [dbo].[Cuenta] ([Id])
GO
ALTER TABLE [dbo].[HistorialDeApartados] CHECK CONSTRAINT [FK_HistorialDeApartados_Cuenta]
GO
ALTER TABLE [dbo].[Movimiento]  WITH CHECK ADD  CONSTRAINT [FK_Movimiento_Periodo] FOREIGN KEY([PeriodoId])
REFERENCES [dbo].[Periodo] ([Id])
GO
ALTER TABLE [dbo].[Movimiento] CHECK CONSTRAINT [FK_Movimiento_Periodo]
GO
ALTER TABLE [dbo].[Movimiento]  WITH CHECK ADD  CONSTRAINT [FK_Movimiento_Presupuesto] FOREIGN KEY([PresupuestoId])
REFERENCES [dbo].[Presupuesto] ([Id])
GO
ALTER TABLE [dbo].[Movimiento] CHECK CONSTRAINT [FK_Movimiento_Presupuesto]
GO
ALTER TABLE [dbo].[Movimiento]  WITH CHECK ADD  CONSTRAINT [FK_Movimiento_Transaccion] FOREIGN KEY([TransaccionId])
REFERENCES [dbo].[Transaccion] ([Id])
GO
ALTER TABLE [dbo].[Movimiento] CHECK CONSTRAINT [FK_Movimiento_Transaccion]
GO
ALTER TABLE [dbo].[Presupuesto]  WITH CHECK ADD  CONSTRAINT [FK_Presupuesto_Subcategoria] FOREIGN KEY([SubcategoriaId])
REFERENCES [dbo].[Subcategoria] ([Id])
GO
ALTER TABLE [dbo].[Presupuesto] CHECK CONSTRAINT [FK_Presupuesto_Subcategoria]
GO
ALTER TABLE [dbo].[Presupuesto]  WITH CHECK ADD  CONSTRAINT [FK_Presupuesto_Version] FOREIGN KEY([VersionId])
REFERENCES [dbo].[VersionDePresupuesto] ([Id])
GO
ALTER TABLE [dbo].[Presupuesto] CHECK CONSTRAINT [FK_Presupuesto_Version]
GO
ALTER TABLE [dbo].[Subcategoria]  WITH CHECK ADD  CONSTRAINT [FK_Subcategoria_Categoria] FOREIGN KEY([CategoriaId])
REFERENCES [dbo].[Categoria] ([Id])
GO
ALTER TABLE [dbo].[Subcategoria] CHECK CONSTRAINT [FK_Subcategoria_Categoria]
GO
ALTER TABLE [dbo].[Transaccion]  WITH CHECK ADD  CONSTRAINT [FK_Transaccion_Cuenta] FOREIGN KEY([CuentaId])
REFERENCES [dbo].[Cuenta] ([Id])
GO
ALTER TABLE [dbo].[Transaccion] CHECK CONSTRAINT [FK_Transaccion_Cuenta]
GO
USE [master]
GO
ALTER DATABASE [DuckBank] SET  READ_WRITE 
GO
