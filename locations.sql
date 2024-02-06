USE [master]
GO
/****** Object:  Database [Locations]    Script Date: 06-02-2024 9.13.32 PM ******/
CREATE DATABASE [Locations]
 GO
USE [Locations]
GO
/****** Object:  Table [dbo].[Locations]    Script Date: 06-02-2024 9.13.33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Locations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Location_Name] [nvarchar](255) NULL,
	[Opening_Time] [time](7) NULL,
	[Ending_Time] [time](7) NULL,
 CONSTRAINT [PK__Location__3214EC0741FFF736] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Locations] ON 
GO
INSERT [dbo].[Locations] ([Id], [Location_Name], [Opening_Time], [Ending_Time]) VALUES (1, N'supermarket', CAST(N'10:00:00' AS Time), CAST(N'13:00:00' AS Time))
GO
INSERT [dbo].[Locations] ([Id], [Location_Name], [Opening_Time], [Ending_Time]) VALUES (2, N'eiffel tower', CAST(N'08:00:00' AS Time), CAST(N'17:30:00' AS Time))
GO
SET IDENTITY_INSERT [dbo].[Locations] OFF
GO
USE [master]
GO
ALTER DATABASE [Locations] SET  READ_WRITE 
GO
