USE [PropertyHandlerDB]
GO

/****** Object:  Table [dbo].[Properties]    Script Date: 10/05/2021 20:29:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Properties](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RegisterDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
	[Code] [int] NULL,
	[Description] [varchar](250) NOT NULL,
   [Title] [varchar](250) NOT NULL,
	[RentPrice] [float] NULL,
	[SalePrice] [float] NULL,
	[TaxPrice] [float] NOT NULL,
	[CondominiumPrice] [float] NULL,
	[OwnerName] [varchar](100) NOT NULL,
	[IsSpotlight] [bit] NOT NULL,
	[AdressId] [int] NULL,
	[DetailsId] [int] NULL,
	[Status] [int] NOT NULL,
	[Type] [int] NOT NULL,
	[SpecificType] [varchar](50) NULL,
 CONSTRAINT [PK_Properties] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


-------------------------------------------------------
--- 

USE [PropertyHandlerDB]
GO

/****** Object:  Table [dbo].[Images]    Script Date: 10/05/2021 20:29:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Images](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RegisterDate] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Name] [varchar](250) NOT NULL,
	[FileType] [varchar](25) NOT NULL,
	[PropertyId] [int] NOT NULL
) ON [PRIMARY]
GO


-----------------------------------------

USE [PropertyHandlerDB]
GO

/****** Object:  Table [dbo].[Details]    Script Date: 10/05/2021 20:29:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Details](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RegisterDate] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[PropertySize] [int] NOT NULL,
	[BedRoomQuantity] [int] NULL,
	[CarVacancyQuantity] [int] NULL,
	[BathRoomQuantity] [int] NULL,
	[PropertyId] [int] NOT NULL
) ON [PRIMARY]
GO


-------------------------------------------------

USE [PropertyHandlerDB]
GO

/****** Object:  Table [dbo].[Address]    Script Date: 10/05/2021 20:29:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Address](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RegisterDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
	[Street] [varchar](250) NOT NULL,
	[LocationNumber] [int] NOT NULL,
	[Cep] [varchar](10) NOT NULL,
	[District] [varchar](50) NOT NULL,
	[State] [varchar](50) NOT NULL,
	[City] [varchar](50) NOT NULL,
	[PropertyId] [int] NOT NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


---------------------------------------------------