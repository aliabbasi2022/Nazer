USE [ParentDB]
GO
/****** Object:  Table [dbo].[AppCategory]    Script Date: 2/27/2019 2:26:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppCategory](
	[Name] [nvarchar](50) NOT NULL,
	[AppsName] [nvarchar](max) NULL,
	[ChildID] [nvarchar](max) NULL,
	[Color] [nvarchar](max) NULL,
 CONSTRAINT [PK_AppCategory] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppsLimit]    Script Date: 2/27/2019 2:26:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppsLimit](
	[ID] [nvarchar](50) NOT NULL,
	[AppName] [nvarchar](50) NOT NULL,
	[AppID] [nvarchar](100) NULL,
	[StartTime] [time](3) NULL,
	[EndTime] [time](3) NULL,
	[Duratin] [time](7) NULL,
	[Act] [nvarchar](50) NULL,
	[ChildID] [nvarchar](max) NULL,
 CONSTRAINT [PK_AppsLimit] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppsLog]    Script Date: 2/27/2019 2:26:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppsLog](
	[ID] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Time] [datetime] NOT NULL,
	[Type] [nvarchar](50) NULL,
	[ChildID] [nvarchar](max) NULL,
 CONSTRAINT [PK_AppsLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppUsage]    Script Date: 2/27/2019 2:26:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppUsage](
	[ID] [nvarchar](50) NOT NULL,
	[AppName] [nvarchar](50) NOT NULL,
	[Seconds] [int] NOT NULL,
	[Minutes] [int] NOT NULL,
	[Hours] [int] NOT NULL,
	[Usage] [float] NOT NULL,
	[ChildID] [nvarchar](max) NULL,
 CONSTRAINT [PK_AppUsage] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlockApps]    Script Date: 2/27/2019 2:26:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlockApps](
	[ID] [nvarchar](50) NOT NULL,
	[AppName] [nvarchar](50) NOT NULL,
	[AppID] [nvarchar](50) NOT NULL,
	[Act] [nvarchar](50) NULL,
	[Category] [nvarchar](max) NULL,
	[ChildID] [nvarchar](max) NULL,
 CONSTRAINT [PK_BlockApps] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlockUrls]    Script Date: 2/27/2019 2:26:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlockUrls](
	[ID] [nvarchar](50) NOT NULL,
	[URL] [nvarchar](max) NOT NULL,
	[Act] [nvarchar](50) NULL,
	[ChildID] [nvarchar](50) NULL,
	[Cat] [nvarchar](50) NULL,
 CONSTRAINT [PK_BlockUrls] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CallLog]    Script Date: 2/27/2019 2:26:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CallLog](
	[ID] [nvarchar](50) NOT NULL,
	[Straem] [smallint] NOT NULL,
	[Number1] [nvarchar](max) NOT NULL,
	[Number2] [nvarchar](max) NOT NULL,
	[Date] [datetime] NOT NULL,
	[ChildID] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_CallLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contact]    Script Date: 2/27/2019 2:26:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[ID] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NULL,
	[Number] [nvarchar](max) NULL,
	[Picture] [image] NULL,
	[ChildID] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Data]    Script Date: 2/27/2019 2:26:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data](
	[ID] [int] NOT NULL,
	[DataContent] [nvarchar](max) NULL,
 CONSTRAINT [PK_Data] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HistoryURL]    Script Date: 2/27/2019 2:26:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistoryURL](
	[ID] [uniqueidentifier] NOT NULL,
	[URL] [nvarchar](max) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Browser] [nvarchar](50) NOT NULL,
	[Category] [nvarchar](max) NULL,
	[ChildID] [nvarchar](max) NULL,
 CONSTRAINT [PK_HistoryURL] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InstalledApps]    Script Date: 2/27/2019 2:26:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InstalledApps](
	[AppID] [nvarchar](100) NOT NULL,
	[DisplayName] [nvarchar](100) NOT NULL,
	[DisplayVersion] [nvarchar](50) NULL,
	[InstallDate] [nvarchar](max) NULL,
	[Publisher] [nvarchar](100) NULL,
	[Path] [nvarchar](max) NULL,
	[ChildID] [nvarchar](max) NOT NULL,
	[ReciveDate] [datetime] NOT NULL,
	[DisplayIcon] [nvarchar](max) NULL,
	[ID] [nvarchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InternetUsage]    Script Date: 2/27/2019 2:26:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InternetUsage](
	[ID] [nvarchar](10) NOT NULL,
	[InterfaceName] [nvarchar](50) NOT NULL,
	[ConnectionType] [nvarchar](50) NOT NULL,
	[DeviceName] [nvarchar](50) NOT NULL,
	[Usage] [float] NOT NULL,
	[Seconds] [int] NOT NULL,
	[Minutes] [int] NOT NULL,
	[Hour] [int] NOT NULL,
	[ChildID] [nvarchar](50) NULL,
 CONSTRAINT [PK_InternetUsage] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Keys]    Script Date: 2/27/2019 2:26:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Keys](
	[Key] [nvarchar](max) NOT NULL,
	[Process] [nvarchar](max) NOT NULL,
	[Date] [nvarchar](50) NOT NULL,
	[ChildID] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Keys] PRIMARY KEY CLUSTERED 
(
	[Date] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LastSeen]    Script Date: 2/27/2019 2:26:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LastSeen](
	[TabelsName] [nvarchar](100) NOT NULL,
	[RowIndex] [bigint] NOT NULL,
	[ChildID] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_LastSeen] PRIMARY KEY CLUSTERED 
(
	[TabelsName] ASC,
	[ChildID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Location]    Script Date: 2/27/2019 2:26:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Location](
	[ChildID] [nvarchar](max) NOT NULL,
	[Date] [nvarchar](30) NOT NULL,
	[Longitude] [nvarchar](50) NOT NULL,
	[Latitude] [nvarchar](50) NOT NULL,
	[ReciveDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED 
(
	[Date] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Network]    Script Date: 2/27/2019 2:26:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Network](
	[ChildID] [nvarchar](max) NOT NULL,
	[DivaceName] [nvarchar](100) NOT NULL,
	[ConnectionInterface] [nvarchar](100) NOT NULL,
	[ModemName] [nvarchar](50) NOT NULL,
	[IPv6] [nvarchar](100) NOT NULL,
	[IPv4] [nvarchar](50) NOT NULL,
	[Status] [nvarchar](50) NULL,
	[ID] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Network] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NetworkAdaptor]    Script Date: 2/27/2019 2:26:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NetworkAdaptor](
	[ChildID] [nvarchar](max) NOT NULL,
	[DeviceName] [nvarchar](max) NOT NULL,
	[InterfaceName] [nvarchar](max) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[ID] [nvarchar](50) NOT NULL,
	[Date] [datetime] NULL,
 CONSTRAINT [PK_NetworkAdaptor] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NetworkLimit]    Script Date: 2/27/2019 2:26:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NetworkLimit](
	[ID] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[StartTime] [time](3) NULL,
	[EndTime] [time](3) NULL,
	[Duration] [time](3) NULL,
	[Act] [nvarchar](50) NULL,
	[ChildID] [nvarchar](50) NULL,
 CONSTRAINT [PK_NetworkLimit] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Process]    Script Date: 2/27/2019 2:26:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Process](
	[ID] [nvarchar](100) NOT NULL,
	[ProcessName] [nvarchar](50) NOT NULL,
	[ProcessId] [nvarchar](50) NOT NULL,
	[ExecutablePath] [nvarchar](max) NOT NULL,
	[StartTime] [nvarchar](50) NULL,
	[EndTime] [nvarchar](50) NULL,
	[OSName] [nvarchar](100) NOT NULL,
	[ChildID] [nvarchar](50) NULL,
 CONSTRAINT [PK_Process] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RunningApps]    Script Date: 2/27/2019 2:26:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RunningApps](
	[Name] [nvarchar](max) NOT NULL,
	[StartTime] [nvarchar](50) NULL,
	[ChildID] [nvarchar](50) NULL,
	[ID] [nvarchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ScreenShot]    Script Date: 2/27/2019 2:26:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScreenShot](
	[Picture] [image] NOT NULL,
	[Date] [datetime] NOT NULL,
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[ChildID] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SMSLog]    Script Date: 2/27/2019 2:26:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SMSLog](
	[ID] [nvarchar](50) NOT NULL,
	[SenderNUmber] [nvarchar](50) NOT NULL,
	[ReciverNumber] [nvarchar](50) NULL,
	[Date] [datetime] NOT NULL,
	[SMSContent] [nvarchar](max) NOT NULL,
	[ChildID] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_SMSLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemLimit]    Script Date: 2/27/2019 2:26:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemLimit](
	[StartTime] [time](3) NULL,
	[EndTime] [time](3) NULL,
	[Duration] [time](3) NULL,
	[Act] [nvarchar](50) NULL,
	[ID] [nvarchar](50) NOT NULL,
	[ChildID] [nvarchar](50) NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_SystemLimit] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemLog]    Script Date: 2/27/2019 2:26:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemLog](
	[ID] [nvarchar](50) NOT NULL,
	[Type] [nvarchar](50) NULL,
	[ChildID] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemUsage]    Script Date: 2/27/2019 2:26:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemUsage](
	[ID] [nvarchar](50) NOT NULL,
	[Usage] [float] NOT NULL,
	[ChildID] [nvarchar](50) NULL,
 CONSTRAINT [PK_SystemUsage] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UninstallOrInstall]    Script Date: 2/27/2019 2:26:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UninstallOrInstall](
	[PreventUninstall] [bit] NOT NULL,
	[PreventInstall] [bit] NOT NULL,
	[ChildID] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[URLCategury]    Script Date: 2/27/2019 2:26:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[URLCategury](
	[Name] [nvarchar](100) NOT NULL,
	[URLs] [nvarchar](max) NULL,
	[ID] [nvarchar](50) NOT NULL,
	[ChildID] [nvarchar](50) NULL,
	[Color] [nvarchar](max) NULL,
 CONSTRAINT [PK_URLCategury] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsedColor]    Script Date: 2/27/2019 2:26:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsedColor](
	[Name] [nvarchar](70) NOT NULL,
 CONSTRAINT [PK_UsedColor] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Voice]    Script Date: 2/27/2019 2:26:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Voice](
	[Date] [nvarchar](50) NOT NULL,
	[Process] [nvarchar](max) NOT NULL,
	[VoiceData] [nvarchar](max) NOT NULL,
	[ChildID] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Voice] PRIMARY KEY CLUSTERED 
(
	[Date] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VPN]    Script Date: 2/27/2019 2:26:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VPN](
	[ID] [nvarchar](50) NOT NULL,
	[StartVPN] [nvarchar](50) NULL,
	[ChildID] [nvarchar](50) NULL,
	[Status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_VPN] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WebCamTable]    Script Date: 2/27/2019 2:26:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebCamTable](
	[Date] [nvarchar](50) NOT NULL,
	[Pic] [nvarchar](max) NOT NULL,
	[ChildID] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_WebCamTable] PRIMARY KEY CLUSTERED 
(
	[Date] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
