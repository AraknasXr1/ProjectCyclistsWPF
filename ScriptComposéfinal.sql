CREATE TABLE [dbo].[Car](
	[IdCar] [int] IDENTITY(1,1) NOT NULL,
	[IdCarClient] [int] NOT NULL,
	[CarSeat] [int] NULL,
	[CarBike] [int] NULL,
	[CarBrand] [nvarchar](50) NULL,
 CONSTRAINT [PK_Car] PRIMARY KEY CLUSTERED 
(
	[IdCar] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]




SET IDENTITY_INSERT [dbo].[Car] ON 
INSERT [dbo].[Car] ([IdCar], [IdCarClient], [CarSeat], [CarBike], [CarBrand]) VALUES (1, 1, 5, 1, N'Volvo')
INSERT [dbo].[Car] ([IdCar], [IdCarClient], [CarSeat], [CarBike], [CarBrand]) VALUES (3, 3, 4, 2, N'Smart')
INSERT [dbo].[Car] ([IdCar], [IdCarClient], [CarSeat], [CarBike], [CarBrand]) VALUES (6, 2, 3, 2, N'Volvo')
INSERT [dbo].[Car] ([IdCar], [IdCarClient], [CarSeat], [CarBike], [CarBrand]) VALUES (9, 2, 1, 1, N'Peugot')
SET IDENTITY_INSERT [dbo].[Car] OFF




CREATE TABLE [dbo].[Category](
	[IdCat] [int] IDENTITY(1,1) NOT NULL,
	[CatName] [nvarchar](50) NOT NULL,
	[IdCatResp] [int] NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[IdCat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([IdCat], [CatName], [IdCatResp]) VALUES (1, N'VTT', 1)
INSERT [dbo].[Category] ([IdCat], [CatName], [IdCatResp]) VALUES (2, N'Cyclo', 2)
SET IDENTITY_INSERT [dbo].[Category] OFF




CREATE TABLE [dbo].[Clients](
	[IdClient] [int] IDENTITY(1,1) NOT NULL,
	[ClientLogin] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Tel] [nvarchar](max) NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Wallet] [int] NULL,
 CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED 
(
	[IdClient] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

SET IDENTITY_INSERT [dbo].[Clients] ON
INSERT [dbo].[Clients] ([IdClient], [ClientLogin], [FirstName], [LastName], [Tel], [Password], [Wallet]) VALUES (1, N'Admin', N'Armin', N'Ferai', N'0478495668', N'Admin', 13)
INSERT [dbo].[Clients] ([IdClient], [ClientLogin], [FirstName], [LastName], [Tel], [Password], [Wallet]) VALUES (2, N'Jan', N'Jensen', N'Ackles', N'0458765898', N'Jan', 18)
INSERT [dbo].[Clients] ([IdClient], [ClientLogin], [FirstName], [LastName], [Tel], [Password], [Wallet]) VALUES (3, N'Cristy', N'Cristy', N'Smeers', N'024798549', N'Cristy', 20)
SET IDENTITY_INSERT [dbo].[Clients] OFF

CREATE TABLE [dbo].[LinkCarToRide](
	[IdLinkCarToLinkRide] [int] IDENTITY(1,1) NOT NULL,
	[IdCar] [int] NULL,
	[IdLinkRide] [int] NULL,
 CONSTRAINT [PK_LinkCarToRide] PRIMARY KEY CLUSTERED 
(
	[IdLinkCarToLinkRide] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

SET IDENTITY_INSERT [dbo].[LinkCarToRide] ON 

INSERT [dbo].[LinkCarToRide] ([IdLinkCarToLinkRide], [IdCar], [IdLinkRide]) VALUES (1, 1, 1)
INSERT [dbo].[LinkCarToRide] ([IdLinkCarToLinkRide], [IdCar], [IdLinkRide]) VALUES (2, 1, 7)
INSERT [dbo].[LinkCarToRide] ([IdLinkCarToLinkRide], [IdCar], [IdLinkRide]) VALUES (3, 6, 7)
SET IDENTITY_INSERT [dbo].[LinkCarToRide] OFF

CREATE TABLE [dbo].[LinkCat](
	[IdLink] [int] IDENTITY(1,1) NOT NULL,
	[IdCliCat] [int] NOT NULL,
	[IdCatLink] [int] NOT NULL,
 CONSTRAINT [PK_LinkCat] PRIMARY KEY CLUSTERED 
(
	[IdLink] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

SET IDENTITY_INSERT [dbo].[LinkCat] ON 

INSERT [dbo].[LinkCat] ([IdLink], [IdCliCat], [IdCatLink]) VALUES (18, 2, 1)
INSERT [dbo].[LinkCat] ([IdLink], [IdCliCat], [IdCatLink]) VALUES (22, 1, 1)
INSERT [dbo].[LinkCat] ([IdLink], [IdCliCat], [IdCatLink]) VALUES (23, 3, 1)
INSERT [dbo].[LinkCat] ([IdLink], [IdCliCat], [IdCatLink]) VALUES (25, 1, 2)
SET IDENTITY_INSERT [dbo].[LinkCat] OFF


CREATE TABLE [dbo].[LinkRide](
	[IdLink] [int] IDENTITY(1,1) NOT NULL,
	[IdCliRide] [int] NULL,
	[IdRide] [int] NULL,
 CONSTRAINT [PK_LinkRide] PRIMARY KEY CLUSTERED 
(
	[IdLink] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

SET IDENTITY_INSERT [dbo].[LinkRide] ON 

INSERT [dbo].[LinkRide] ([IdLink], [IdCliRide], [IdRide]) VALUES (1, 1, 7)
INSERT [dbo].[LinkRide] ([IdLink], [IdCliRide], [IdRide]) VALUES (6, 2, 7)
INSERT [dbo].[LinkRide] ([IdLink], [IdCliRide], [IdRide]) VALUES (7, 1, 3)
SET IDENTITY_INSERT [dbo].[LinkRide] OFF

CREATE TABLE [dbo].[Ride](
	[IdRide] [int] IDENTITY(1,1) NOT NULL,
	[DeparturePlace] [nvarchar](50) NOT NULL,
	[DepartureDate] [nvarchar](50) NULL,
	[DepartureHour] [nvarchar](50) NULL,
	[RidePrice] [int] NULL,
	[IdCatRide] [int] NULL,
	[MaxClient] [int] NULL,
	[Bike] [int] NULL,
	[CurrentClients] [int] NULL,
 CONSTRAINT [PK_Ride] PRIMARY KEY CLUSTERED 
(
	[IdRide] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

SET IDENTITY_INSERT [dbo].[Ride] ON 

INSERT [dbo].[Ride] ([IdRide], [DeparturePlace], [DepartureDate], [DepartureHour], [RidePrice], [IdCatRide], [MaxClient], [Bike], [CurrentClients]) VALUES (1, N'Cycling Club', N'20/04/2022', N'12:00', 2, 1, 5, 0, 1)
INSERT [dbo].[Ride] ([IdRide], [DeparturePlace], [DepartureDate], [DepartureHour], [RidePrice], [IdCatRide], [MaxClient], [Bike], [CurrentClients]) VALUES (2, N'Cycling Club', N'21/04/2022', N'14:00', 3, 2, 3, 0, 0)
INSERT [dbo].[Ride] ([IdRide], [DeparturePlace], [DepartureDate], [DepartureHour], [RidePrice], [IdCatRide], [MaxClient], [Bike], [CurrentClients]) VALUES (3, N'Blackwood Shop', N'22/04/2022', N'13:00', 2, 2, 2, 0, 0)
INSERT [dbo].[Ride] ([IdRide], [DeparturePlace], [DepartureDate], [DepartureHour], [RidePrice], [IdCatRide], [MaxClient], [Bike], [CurrentClients]) VALUES (7, N'Everfall', N'24/04/2022', N'16:00', 1, 1, 1, 0, 1)
SET IDENTITY_INSERT [dbo].[Ride] OFF

CREATE TABLE [dbo].[Tresorier](
	[IdTresorier] [int] IDENTITY(1,1) NOT NULL,
	[IdCliTreso] [int] NOT NULL,
 CONSTRAINT [PK_Tresorier] PRIMARY KEY CLUSTERED 
(
	[IdTresorier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
SET IDENTITY_INSERT [dbo].[Tresorier] ON 

INSERT [dbo].[Tresorier] ([IdTresorier], [IdCliTreso]) VALUES (1, 3)
SET IDENTITY_INSERT [dbo].[Tresorier] OFF
