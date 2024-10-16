USE[master]
Go
/****** Object:  Database [SmartHouseSolutionsDB]    Script Date: 2024-10-15 6:36:03 PM ******/
CREATE DATABASE [SmartHouseSolutionsDB]
Go

USE [SmartHouseSolutionsDB]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 2024-10-15 6:36:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerName] [nvarchar](255) NOT NULL,
	[CustomerEmail] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerDeviceRelation]    Script Date: 2024-10-15 6:36:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerDeviceRelation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[DeviceId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Device]    Script Date: 2024-10-15 6:36:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Device](
	[DeviceId] [int] IDENTITY(1,1) NOT NULL,
	[DeviceName] [nvarchar](255) NOT NULL,
	[DeviceTypeId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DeviceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeviceType]    Script Date: 2024-10-15 6:36:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeviceType](
	[DeviceTypeId] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DeviceTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Customer] ON 

INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail]) VALUES (1, N'Rahul Sharma', N'rahul.sharma@sample.com')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail]) VALUES (2, N'Priya Verma', N'priya.verma@sample.com')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail]) VALUES (3, N'Amit Patel', N'amit.patel@sample.com')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail]) VALUES (4, N'Sneha Rao', N'sneha.rao@sample.com')
INSERT [dbo].[Customer] ([CustomerId], [CustomerName], [CustomerEmail]) VALUES (5, N'Vikram Singh', N'vikram.singh@sample.com')
SET IDENTITY_INSERT [dbo].[Customer] OFF
GO
SET IDENTITY_INSERT [dbo].[CustomerDeviceRelation] ON 

INSERT [dbo].[CustomerDeviceRelation] ([Id], [CustomerId], [DeviceId]) VALUES (1, 1, 1)
INSERT [dbo].[CustomerDeviceRelation] ([Id], [CustomerId], [DeviceId]) VALUES (2, 1, 2)
INSERT [dbo].[CustomerDeviceRelation] ([Id], [CustomerId], [DeviceId]) VALUES (3, 3, 3)
INSERT [dbo].[CustomerDeviceRelation] ([Id], [CustomerId], [DeviceId]) VALUES (4, 4, 4)
INSERT [dbo].[CustomerDeviceRelation] ([Id], [CustomerId], [DeviceId]) VALUES (5, 5, 5)
SET IDENTITY_INSERT [dbo].[CustomerDeviceRelation] OFF
GO
SET IDENTITY_INSERT [dbo].[Device] ON 

INSERT [dbo].[Device] ([DeviceId], [DeviceName], [DeviceTypeId]) VALUES (1, N'Living Room Camera', 1)
INSERT [dbo].[Device] ([DeviceId], [DeviceName], [DeviceTypeId]) VALUES (2, N'Front Door Alarm System', 2)
INSERT [dbo].[Device] ([DeviceId], [DeviceName], [DeviceTypeId]) VALUES (3, N'Backyard Camera', 1)
INSERT [dbo].[Device] ([DeviceId], [DeviceName], [DeviceTypeId]) VALUES (4, N'Garage Alarm System', 2)
INSERT [dbo].[Device] ([DeviceId], [DeviceName], [DeviceTypeId]) VALUES (5, N'Kitchen Camera', 1)
SET IDENTITY_INSERT [dbo].[Device] OFF
GO
SET IDENTITY_INSERT [dbo].[DeviceType] ON 

INSERT [dbo].[DeviceType] ([DeviceTypeId], [TypeName]) VALUES (2, N'Alarm System')
INSERT [dbo].[DeviceType] ([DeviceTypeId], [TypeName]) VALUES (1, N'Camera')
SET IDENTITY_INSERT [dbo].[DeviceType] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Customer__3A0CE74C06E6A97A]    Script Date: 2024-10-15 6:36:03 PM ******/
ALTER TABLE [dbo].[Customer] ADD UNIQUE NONCLUSTERED 
(
	[CustomerEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__DeviceTy__D4E7DFA8E75196B1]    Script Date: 2024-10-15 6:36:03 PM ******/
ALTER TABLE [dbo].[DeviceType] ADD UNIQUE NONCLUSTERED 
(
	[TypeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CustomerDeviceRelation]  WITH CHECK ADD  CONSTRAINT [FK_CustomerDeviceRelation_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[CustomerDeviceRelation] CHECK CONSTRAINT [FK_CustomerDeviceRelation_Customer]
GO
ALTER TABLE [dbo].[CustomerDeviceRelation]  WITH CHECK ADD  CONSTRAINT [FK_CustomerDeviceRelation_Device] FOREIGN KEY([DeviceId])
REFERENCES [dbo].[Device] ([DeviceId])
GO
ALTER TABLE [dbo].[CustomerDeviceRelation] CHECK CONSTRAINT [FK_CustomerDeviceRelation_Device]
GO
ALTER TABLE [dbo].[Device]  WITH CHECK ADD  CONSTRAINT [FK_Device_DeviceType] FOREIGN KEY([DeviceTypeId])
REFERENCES [dbo].[DeviceType] ([DeviceTypeId])
GO
ALTER TABLE [dbo].[Device] CHECK CONSTRAINT [FK_Device_DeviceType]
GO
USE [master]
GO
ALTER DATABASE [SmartHouseSolutionsDB] SET  READ_WRITE 
GO
