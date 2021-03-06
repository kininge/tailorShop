USE [master]
GO
/****** Object:  Database [tailorShop]    Script Date: 31-08-2019 13:37:58 ******/
CREATE DATABASE [tailorShop]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'tailorShop', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\tailorShop.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'tailorShop_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\tailorShop_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [tailorShop] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [tailorShop].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [tailorShop] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [tailorShop] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [tailorShop] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [tailorShop] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [tailorShop] SET ARITHABORT OFF 
GO
ALTER DATABASE [tailorShop] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [tailorShop] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [tailorShop] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [tailorShop] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [tailorShop] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [tailorShop] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [tailorShop] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [tailorShop] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [tailorShop] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [tailorShop] SET  ENABLE_BROKER 
GO
ALTER DATABASE [tailorShop] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [tailorShop] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [tailorShop] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [tailorShop] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [tailorShop] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [tailorShop] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [tailorShop] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [tailorShop] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [tailorShop] SET  MULTI_USER 
GO
ALTER DATABASE [tailorShop] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [tailorShop] SET DB_CHAINING OFF 
GO
ALTER DATABASE [tailorShop] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [tailorShop] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [tailorShop] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [tailorShop] SET QUERY_STORE = OFF
GO
USE [tailorShop]
GO
/****** Object:  Table [dbo].[Feedback]    Script Date: 31-08-2019 13:37:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedback](
	[feedbackId] [bigint] IDENTITY(1,1) NOT NULL,
	[fit] [tinyint] NOT NULL,
	[style] [tinyint] NOT NULL,
	[quality] [tinyint] NOT NULL,
	[feedback] [varchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[feedbackId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LaxumiPooja]    Script Date: 31-08-2019 13:38:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LaxumiPooja](
	[year] [decimal](4, 0) NOT NULL,
	[date] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[year] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LowerBody]    Script Date: 31-08-2019 13:38:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LowerBody](
	[workId] [int] NOT NULL,
	[clothId] [tinyint] NOT NULL,
	[clothName] [varchar](30) NOT NULL,
	[height] [decimal](4, 2) NULL,
	[knee] [decimal](4, 2) NULL,
	[waist] [decimal](4, 2) NULL,
	[seat] [decimal](4, 2) NULL,
	[thigh] [decimal](4, 2) NULL,
	[bottom] [decimal](4, 2) NULL,
	[underline] [decimal](4, 2) NULL,
	[Note] [varchar](50) NULL,
	[status] [tinyint] NULL,
	[price] [money] NOT NULL,
	[assignTo] [int] NULL,
	[paidTo] [money] NULL,
	[feedbackId] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[workId] ASC,
	[clothId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Security]    Script Date: 31-08-2019 13:38:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Security](
	[questionId] [smallint] IDENTITY(1,1) NOT NULL,
	[question] [varchar](80) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[questionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[question] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Shops]    Script Date: 31-08-2019 13:38:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shops](
	[shopId] [smallint] IDENTITY(1,1) NOT NULL,
	[shopName] [varchar](40) NOT NULL,
	[branchName] [varchar](60) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[shopId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UpperBody]    Script Date: 31-08-2019 13:38:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UpperBody](
	[workId] [int] NOT NULL,
	[clothId] [tinyint] NOT NULL,
	[clothName] [varchar](30) NOT NULL,
	[height] [decimal](4, 2) NULL,
	[front] [decimal](4, 2) NULL,
	[collar] [decimal](4, 2) NULL,
	[shoulder] [decimal](4, 2) NULL,
	[sleeve] [decimal](4, 2) NULL,
	[sleeveWidth] [decimal](4, 2) NULL,
	[cuff] [decimal](4, 2) NULL,
	[Note] [varchar](50) NULL,
	[status] [tinyint] NULL,
	[price] [money] NOT NULL,
	[assignTo] [int] NULL,
	[paidTo] [money] NULL,
	[feedbackId] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[workId] ASC,
	[clothId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserDetails]    Script Date: 31-08-2019 13:38:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserDetails](
	[userId] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[emailAddress] [varchar](50) NULL,
	[mobileNo] [varchar](10) NULL,
	[isOnWhatsApp] [bit] NULL,
	[birthdate] [date] NOT NULL,
	[address] [varchar](200) NOT NULL,
	[shopId] [smallint] NOT NULL,
	[createdOn] [date] NULL,
	[createdBy] [int] NULL,
	[updatedOn] [date] NULL,
	[updatedBy] [int] NULL,
	[isTestData] [bit] NULL,
	[isDelete] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 31-08-2019 13:38:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[logINId] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NULL,
	[username] [varchar](30) NOT NULL,
	[password] [varchar](16) NOT NULL,
	[userTypeId] [smallint] NOT NULL,
	[questionId] [smallint] NOT NULL,
	[answer] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[logINId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserType]    Script Date: 31-08-2019 13:38:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserType](
	[userTypeId] [smallint] IDENTITY(0,1) NOT NULL,
	[userType] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[userTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[userType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Work]    Script Date: 31-08-2019 13:38:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Work](
	[workId] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NULL,
	[createdOn] [date] NULL,
	[createdBy] [int] NULL,
	[updatedOn] [date] NULL,
	[updatedBy] [int] NULL,
	[deliveryDate] [date] NULL,
	[DeliveredOn] [date] NULL,
	[advance] [money] NULL,
PRIMARY KEY CLUSTERED 
(
	[workId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkOut]    Script Date: 31-08-2019 13:38:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkOut](
	[workOutId] [int] IDENTITY(1,1) NOT NULL,
	[workId] [int] NULL,
	[deliveredOn] [date] NULL,
	[createdBy] [int] NULL,
	[pay] [money] NOT NULL,
	[updatedOn] [date] NULL,
	[updatedBy] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[workOutId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[LowerBody] ADD  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[UpperBody] ADD  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[UserDetails] ADD  DEFAULT ((0)) FOR [isOnWhatsApp]
GO
ALTER TABLE [dbo].[UserDetails] ADD  DEFAULT (getdate()) FOR [createdOn]
GO
ALTER TABLE [dbo].[UserDetails] ADD  DEFAULT ((0)) FOR [isTestData]
GO
ALTER TABLE [dbo].[UserDetails] ADD  DEFAULT ((0)) FOR [isDelete]
GO
ALTER TABLE [dbo].[Work] ADD  DEFAULT (getdate()) FOR [createdOn]
GO
ALTER TABLE [dbo].[Work] ADD  DEFAULT ((0)) FOR [advance]
GO
ALTER TABLE [dbo].[WorkOut] ADD  DEFAULT (getdate()) FOR [deliveredOn]
GO
ALTER TABLE [dbo].[LowerBody]  WITH CHECK ADD FOREIGN KEY([assignTo])
REFERENCES [dbo].[UserDetails] ([userId])
GO
ALTER TABLE [dbo].[LowerBody]  WITH CHECK ADD FOREIGN KEY([feedbackId])
REFERENCES [dbo].[Feedback] ([feedbackId])
GO
ALTER TABLE [dbo].[LowerBody]  WITH CHECK ADD FOREIGN KEY([workId])
REFERENCES [dbo].[Work] ([workId])
GO
ALTER TABLE [dbo].[UpperBody]  WITH CHECK ADD FOREIGN KEY([assignTo])
REFERENCES [dbo].[UserDetails] ([userId])
GO
ALTER TABLE [dbo].[UpperBody]  WITH CHECK ADD FOREIGN KEY([feedbackId])
REFERENCES [dbo].[Feedback] ([feedbackId])
GO
ALTER TABLE [dbo].[UpperBody]  WITH CHECK ADD FOREIGN KEY([workId])
REFERENCES [dbo].[Work] ([workId])
GO
ALTER TABLE [dbo].[UserDetails]  WITH CHECK ADD FOREIGN KEY([createdBy])
REFERENCES [dbo].[UserDetails] ([userId])
GO
ALTER TABLE [dbo].[UserDetails]  WITH CHECK ADD FOREIGN KEY([createdBy])
REFERENCES [dbo].[UserDetails] ([userId])
GO
ALTER TABLE [dbo].[UserDetails]  WITH CHECK ADD FOREIGN KEY([createdBy])
REFERENCES [dbo].[UserDetails] ([userId])
GO
ALTER TABLE [dbo].[UserDetails]  WITH CHECK ADD FOREIGN KEY([shopId])
REFERENCES [dbo].[Shops] ([shopId])
GO
ALTER TABLE [dbo].[UserDetails]  WITH CHECK ADD FOREIGN KEY([shopId])
REFERENCES [dbo].[Shops] ([shopId])
GO
ALTER TABLE [dbo].[UserDetails]  WITH CHECK ADD FOREIGN KEY([shopId])
REFERENCES [dbo].[Shops] ([shopId])
GO
ALTER TABLE [dbo].[UserDetails]  WITH CHECK ADD FOREIGN KEY([updatedBy])
REFERENCES [dbo].[UserDetails] ([userId])
GO
ALTER TABLE [dbo].[UserDetails]  WITH CHECK ADD FOREIGN KEY([updatedBy])
REFERENCES [dbo].[UserDetails] ([userId])
GO
ALTER TABLE [dbo].[UserDetails]  WITH CHECK ADD FOREIGN KEY([updatedBy])
REFERENCES [dbo].[UserDetails] ([userId])
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([questionId])
REFERENCES [dbo].[Security] ([questionId])
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([userId])
REFERENCES [dbo].[UserDetails] ([userId])
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([userTypeId])
REFERENCES [dbo].[UserType] ([userTypeId])
GO
ALTER TABLE [dbo].[Work]  WITH CHECK ADD FOREIGN KEY([createdBy])
REFERENCES [dbo].[UserDetails] ([userId])
GO
ALTER TABLE [dbo].[Work]  WITH CHECK ADD FOREIGN KEY([createdBy])
REFERENCES [dbo].[UserDetails] ([userId])
GO
ALTER TABLE [dbo].[Work]  WITH CHECK ADD FOREIGN KEY([createdBy])
REFERENCES [dbo].[UserDetails] ([userId])
GO
ALTER TABLE [dbo].[Work]  WITH CHECK ADD FOREIGN KEY([updatedBy])
REFERENCES [dbo].[UserDetails] ([userId])
GO
ALTER TABLE [dbo].[Work]  WITH CHECK ADD FOREIGN KEY([updatedBy])
REFERENCES [dbo].[UserDetails] ([userId])
GO
ALTER TABLE [dbo].[Work]  WITH CHECK ADD FOREIGN KEY([updatedBy])
REFERENCES [dbo].[UserDetails] ([userId])
GO
ALTER TABLE [dbo].[Work]  WITH CHECK ADD FOREIGN KEY([userId])
REFERENCES [dbo].[UserDetails] ([userId])
GO
ALTER TABLE [dbo].[Work]  WITH CHECK ADD FOREIGN KEY([userId])
REFERENCES [dbo].[UserDetails] ([userId])
GO
ALTER TABLE [dbo].[Work]  WITH CHECK ADD FOREIGN KEY([userId])
REFERENCES [dbo].[UserDetails] ([userId])
GO
ALTER TABLE [dbo].[WorkOut]  WITH CHECK ADD FOREIGN KEY([createdBy])
REFERENCES [dbo].[UserDetails] ([userId])
GO
ALTER TABLE [dbo].[WorkOut]  WITH CHECK ADD FOREIGN KEY([updatedBy])
REFERENCES [dbo].[UserDetails] ([userId])
GO
ALTER TABLE [dbo].[WorkOut]  WITH CHECK ADD FOREIGN KEY([workId])
REFERENCES [dbo].[Work] ([workId])
GO
USE [master]
GO
ALTER DATABASE [tailorShop] SET  READ_WRITE 
GO

USE [tailorShop]
GO
/* Base data */
INSERT INTO [dbo].[LaxumiPooja]([year],[date]) VALUES(2018, '2018-11-07');
INSERT INTO [dbo].[LaxumiPooja]([year],[date]) VALUES(2019, '2018-10-27');
INSERT INTO [dbo].[LaxumiPooja]([year],[date]) VALUES(2020, '2018-10-31');
INSERT INTO [dbo].[LaxumiPooja]([year],[date]) VALUES(2021, '2018-10-20');
INSERT INTO [dbo].[LaxumiPooja]([year],[date]) VALUES(2022, '2018-10-09');
INSERT INTO [dbo].[LaxumiPooja]([year],[date]) VALUES(2023, '2018-10-28');
INSERT INTO [dbo].[LaxumiPooja]([year],[date]) VALUES(2024, '2018-10-17');
INSERT INTO [dbo].[LaxumiPooja]([year],[date]) VALUES(2025, '2018-10-07');
INSERT INTO [dbo].[LaxumiPooja]([year],[date]) VALUES(2026, '2018-10-26');
INSERT INTO [dbo].[LaxumiPooja]([year],[date]) VALUES(2027, '2018-10-27');
INSERT INTO [dbo].[LaxumiPooja]([year],[date]) VALUES(2028, '2018-10-03');
INSERT INTO [dbo].[LaxumiPooja]([year],[date]) VALUES(2029, '2018-10-22');
INSERT INTO [dbo].[LaxumiPooja]([year],[date]) VALUES(2030, '2018-10-11');
INSERT INTO [dbo].[LaxumiPooja]([year],[date]) VALUES(2031, '2018-10-30');

INSERT INTO [dbo].[UserType]([userType]) VALUES('creater');
INSERT INTO [dbo].[UserType]([userType]) VALUES('master');
INSERT INTO [dbo].[UserType]([userType]) VALUES('manager');
INSERT INTO [dbo].[UserType]([userType]) VALUES('employee');
INSERT INTO [dbo].[UserType]([userType]) VALUES('customer');
INSERT INTO [dbo].[UserType]([userType]) VALUES('maintenance');
	
INSERT INTO [dbo].[Shops]([shopName],[branchName]) VALUES('Stichwell', 'Malgaon');
INSERT INTO [dbo].[Shops]([shopName],[branchName]) VALUES('Stichwell', 'Khanderajuri');

INSERT INTO [dbo].[Security]([question]) VALUES('Who is your favorite actor?');
INSERT INTO [dbo].[Security]([question]) VALUES('Who is your favorite actress?');
INSERT INTO [dbo].[Security]([question]) VALUES('Who is your hero?');
INSERT INTO [dbo].[Security]([question]) VALUES('Who is your favorite cricket player?');
INSERT INTO [dbo].[Security]([question]) VALUES('Which is your favorite TV series?');
INSERT INTO [dbo].[Security]([question]) VALUES('Which is your favorite web series?');
INSERT INTO [dbo].[Security]([question]) VALUES('Which is your favorite movie?');

INSERT INTO [dbo].[UserDetails]([name],[emailAddress],[mobileNo],[isOnWhatsApp],[birthdate],[address],[shopId],[createdBy],[updatedOn],[updatedBy],[isTestData],[isDelete]) 
VALUES ('Pritam Sukumar Kininge', 'kininge007@gmail.com', '7276053298',	1, '1995-05-08', 'not needed', 1, NULL,	NULL, NULL, 0, 0);
INSERT INTO [dbo].[UserDetails]([name],[emailAddress],[mobileNo],[isOnWhatsApp],[birthdate],[address],[shopId],[createdBy],[updatedOn],[updatedBy],[isTestData],[isDelete]) 
VALUES ('Sukumar Ramchandra Kininge', 'sukumarkininge7972@gmail.com', '9881035618', 1, '1968-12-23', 'Ward No. 6, Kokane Galli, Malgaon', 1, 1, NULL, NULL, 0, 0);
INSERT INTO [dbo].[UserDetails]([name],[emailAddress],[mobileNo],[isOnWhatsApp],[birthdate],[address],[shopId],[createdBy],[updatedOn],[updatedBy],[isTestData],[isDelete]) 
VALUES ('Prasanna Sukumar Kininge', 'prasannakininge@gmail.com', '8766518166', 1, '2002-01-16',	'Ward No. 6, Kokane Galli, Malgaon', 1,	1, NULL, NULL, 0, 0);
INSERT INTO [dbo].[UserDetails]([name],[emailAddress],[mobileNo],[isOnWhatsApp],[birthdate],[address],[shopId],[createdBy],[updatedOn],[updatedBy],[isTestData],[isDelete]) 
VALUES ('Sapna Bisht', 'sapnabisht733@gmail.com', '8859987644',	1, '1997-10-29', 'Nainital Uttarakhand', 1, 1, NULL, NULL, 1, 0);
INSERT INTO [dbo].[UserDetails]([name],[emailAddress],[mobileNo],[isOnWhatsApp],[birthdate],[address],[shopId],[createdBy],[updatedOn],[updatedBy],[isTestData],[isDelete]) 
VALUES ('Abhay Chagan Karade', 'abhay.karade7@gmail.com', '9579998889',	1, '1996-02-23', 'M-7, Sahawas nagar, Jule Solapur- 413006', 1, 1, NULL, NULL, 1, 0);
INSERT INTO [dbo].[UserDetails]([name],[emailAddress],[mobileNo],[isOnWhatsApp],[birthdate],[address],[shopId],[createdBy],[updatedOn],[updatedBy],[isTestData],[isDelete]) 
VALUES ('Smruti Surendra Hanchate', 'smrutihanchate@gmail.com',	'8485040158', 1, '1996-04-26', '315, B-Group, Vidigharakul, Solapur-413005', 1,	1, NULL, NULL, 1, 0);
INSERT INTO [dbo].[UserDetails]([name],[emailAddress],[mobileNo],[isOnWhatsApp],[birthdate],[address],[shopId],[createdBy],[updatedOn],[updatedBy],[isTestData],[isDelete]) 
VALUES ('Vedanti Shrikant Chougule', 'vidyachougule0209@gmail.com', '9834221463', 1, '1995-09-02', 'Near L. K. Highschool, Shikshank colony Palus', 1, 1, null, null, 1, 0);
INSERT INTO [dbo].[UserDetails]([name],[emailAddress],[mobileNo],[isOnWhatsApp],[birthdate],[address],[shopId],[createdBy],[updatedOn],[updatedBy],[isTestData],[isDelete]) 
VALUES ('Sujit Chougule', 'sujitchougule2003@gmail.com', '9767470771', 1, '2003-07-07', 'Near Jain Mandir Malgaon', 1, 1, null, null, 1, 0);
INSERT INTO [dbo].[UserDetails]([name],[emailAddress],[mobileNo],[isOnWhatsApp],[birthdate],[address],[shopId],[createdBy],[updatedOn],[updatedBy],[isTestData],[isDelete]) 
VALUES ('Sanket Shantinath Chougule', 'nirmalachougule159@gmail.com', '9860005263', 1, '1999-09-28', 'Dhamne galli Malgaon', 1, 1, null, null, 1, 0);
INSERT INTO [dbo].[UserDetails]([name],[emailAddress],[mobileNo],[isOnWhatsApp],[birthdate],[address],[shopId],[createdBy],[updatedOn],[updatedBy],[isTestData],[isDelete]) 
VALUES ('Atal Bihari Vajpai', 'vajpai@myhero.com', NULL, 0, '1930-06-16', 'in our heart', 1, 1, NULL, NULL, 1, 0);	
INSERT INTO [dbo].[UserDetails]([name],[emailAddress],[mobileNo],[isOnWhatsApp],[birthdate],[address],[shopId],[createdBy],[updatedOn],[updatedBy],[isTestData],[isDelete]) 
VALUES ('Leonardo da Vinci', 'vinci@myhero.com', NULL, 0, '1452-04-15', 'in our heart', 1, 1, NULL, NULL, 1, 0);	
INSERT INTO [dbo].[UserDetails]([name],[emailAddress],[mobileNo],[isOnWhatsApp],[birthdate],[address],[shopId],[createdBy],[updatedOn],[updatedBy],[isTestData],[isDelete]) 
VALUES ('Nikola Tesla', 'tesla@myhero.com', NULL, 0, '1856-07-10', 'in our heart', 1, 1, NULL, NULL, 1, 0);	
INSERT INTO [dbo].[UserDetails]([name],[emailAddress],[mobileNo],[isOnWhatsApp],[birthdate],[address],[shopId],[createdBy],[updatedOn],[updatedBy],[isTestData],[isDelete]) 
VALUES ('Nicolas Léonard Sadi Carnot', 'carnot@myhero.com', NULL, 0, '1796-06-01', 'in our heart', 1, 1, NULL, NULL, 1, 0);	
INSERT INTO [dbo].[UserDetails]([name],[emailAddress],[mobileNo],[isOnWhatsApp],[birthdate],[address],[shopId],[createdBy],[updatedOn],[updatedBy],[isTestData],[isDelete]) 
VALUES ('Alan Turing', 'turing@myhero.com', NULL, 0, '1912-06-23', 'in our heart', 1, 1, NULL, NULL, 1, 0);	
INSERT INTO [dbo].[UserDetails]([name],[emailAddress],[mobileNo],[isOnWhatsApp],[birthdate],[address],[shopId],[createdBy],[updatedOn],[updatedBy],[isTestData],[isDelete]) 
VALUES ('Isaac Newton', 'newton@myhero.com', NULL, 0, '1643-01-04', 'in our heart', 1, 1, NULL, NULL, 1, 0);
INSERT INTO [dbo].[UserDetails]([name],[emailAddress],[mobileNo],[isOnWhatsApp],[birthdate],[address],[shopId],[createdBy],[updatedOn],[updatedBy],[isTestData],[isDelete]) 
VALUES ('Steve Jobs', 'jobs@myhero.com', NULL, 0, '1955-02-24', 'in our heart', 1, 1, NULL, NULL, 1, 0);	
INSERT INTO [dbo].[UserDetails]([name],[emailAddress],[mobileNo],[isOnWhatsApp],[birthdate],[address],[shopId],[createdBy],[updatedOn],[updatedBy],[isTestData],[isDelete]) 
VALUES ('Elon Musk', 'musk@myhero.com', NULL, 0, '1971-06-28', 'in our heart', 1, 1, NULL, NULL, 1, 0);	
INSERT INTO [dbo].[UserDetails]([name],[emailAddress],[mobileNo],[isOnWhatsApp],[birthdate],[address],[shopId],[createdBy],[updatedOn],[updatedBy],[isTestData],[isDelete]) 
VALUES ('Charles Babbage', 'babbage@myhero.com', NULL, 0, '1791-12-26', 'in our heart', 1, 1, NULL, NULL, 1, 0);		
INSERT INTO [dbo].[UserDetails]([name],[emailAddress],[mobileNo],[isOnWhatsApp],[birthdate],[address],[shopId],[createdBy],[updatedOn],[updatedBy],[isTestData],[isDelete]) 
VALUES ('Ada Lovelace', 'lovelace@myhero.com', NULL, 0, '1815-12-10', 'in our heart', 1, 1, NULL, NULL, 1, 0);

INSERT INTO [dbo].[Users]([userId],[username],[password],[userTypeId],[questionId],[answer]) VALUES(1, 'kininge007', 'Pkininge@007', 0, 1, 'RDJ');
INSERT INTO [dbo].[Users]([userId],[username],[password],[userTypeId],[questionId],[answer]) VALUES(2, 'sukumar', 'sukumarTestingId', 1, 1, 'RDJ');
INSERT INTO [dbo].[Users]([userId],[username],[password],[userTypeId],[questionId],[answer]) VALUES(1, 'pritam', 'Pkininge@007', 5, 1, 'RDJ');
INSERT INTO [dbo].[Users]([userId],[username],[password],[userTypeId],[questionId],[answer]) VALUES(3, 'prasanna', 'prasannaTestingId', 3, 1, 'RDJ');
INSERT INTO [dbo].[Users]([userId],[username],[password],[userTypeId],[questionId],[answer]) VALUES(4, 'sapnaMaster', 'sapnaTestingId', 1, 1, 'RDJ');
INSERT INTO [dbo].[Users]([userId],[username],[password],[userTypeId],[questionId],[answer]) VALUES(4, 'sapnaEmployee', 'sapnaTestingId', 3, 1, 'RDJ');
INSERT INTO [dbo].[Users]([userId],[username],[password],[userTypeId],[questionId],[answer]) VALUES(4, 'sapnaCustome', 'sapnaTestingId', 4, 1, 'RDJ');
INSERT INTO [dbo].[Users]([userId],[username],[password],[userTypeId],[questionId],[answer]) VALUES(5, 'abhayMaster', 'abhayTestingId', 1, 1, 'RDJ');
INSERT INTO [dbo].[Users]([userId],[username],[password],[userTypeId],[questionId],[answer]) VALUES(5, 'abhayEmployee', 'abhayTestingId', 3, 1, 'RDJ');
INSERT INTO [dbo].[Users]([userId],[username],[password],[userTypeId],[questionId],[answer]) VALUES(5, 'abhayCustomer', 'abhayTestingId', 4, 1, 'RDJ');
INSERT INTO [dbo].[Users]([userId],[username],[password],[userTypeId],[questionId],[answer]) VALUES(6, 'smrutiMaster', 'smrutiTestingId', 1, 1, 'RDJ');
INSERT INTO [dbo].[Users]([userId],[username],[password],[userTypeId],[questionId],[answer]) VALUES(6, 'smrutiEmployee', 'smrutiTestingId', 3, 1, 'RDJ');
INSERT INTO [dbo].[Users]([userId],[username],[password],[userTypeId],[questionId],[answer]) VALUES(6, 'smrutiCustomer', 'smrutiTestingId', 4, 1, 'RDJ');
INSERT INTO [dbo].[Users]([userId],[username],[password],[userTypeId],[questionId],[answer]) VALUES(7, 'vedantiMaster', 'vedantiTestingId', 1, 1, 'RDJ');
INSERT INTO [dbo].[Users]([userId],[username],[password],[userTypeId],[questionId],[answer]) VALUES(7, 'vedantiEmployee', 'vedantiTestingId', 3, 1, 'RDJ');
INSERT INTO [dbo].[Users]([userId],[username],[password],[userTypeId],[questionId],[answer]) VALUES(7, 'vedantiCustome', 'vedantiTestingId', 4, 1, 'RDJ');
INSERT INTO [dbo].[Users]([userId],[username],[password],[userTypeId],[questionId],[answer]) VALUES(8, 'sujitMaster', 'sujitTestingId', 1, 1, 'RDJ');
INSERT INTO [dbo].[Users]([userId],[username],[password],[userTypeId],[questionId],[answer]) VALUES(8, 'sujitEmployee', 'sujitTestingId', 3, 1, 'RDJ');
INSERT INTO [dbo].[Users]([userId],[username],[password],[userTypeId],[questionId],[answer]) VALUES(8, 'sujitCustome', 'sujitTestingId', 4, 1, 'RDJ');
INSERT INTO [dbo].[Users]([userId],[username],[password],[userTypeId],[questionId],[answer]) VALUES(9, 'sanketMaster', 'sanketTestingId', 1, 1, 'RDJ');
INSERT INTO [dbo].[Users]([userId],[username],[password],[userTypeId],[questionId],[answer]) VALUES(9, 'sanketEmployee', 'sanketTestingId', 3, 1, 'RDJ');
INSERT INTO [dbo].[Users]([userId],[username],[password],[userTypeId],[questionId],[answer]) VALUES(9, 'sanketCustome', 'sanketTestingId', 4, 1, 'RDJ');
INSERT INTO [dbo].[Users]([userId],[username],[password],[userTypeId],[questionId],[answer]) VALUES(10, 'Atal', 'test', 4, 1, 'RDJ');
INSERT INTO [dbo].[Users]([userId],[username],[password],[userTypeId],[questionId],[answer]) VALUES(11, 'Leonardo', 'test', 4, 1, 'RDJ');
INSERT INTO [dbo].[Users]([userId],[username],[password],[userTypeId],[questionId],[answer]) VALUES(12, 'Nikola', 'test', 4, 1, 'RDJ');
INSERT INTO [dbo].[Users]([userId],[username],[password],[userTypeId],[questionId],[answer]) VALUES(13, 'Nicolas', 'test', 4, 1, 'RDJ');
INSERT INTO [dbo].[Users]([userId],[username],[password],[userTypeId],[questionId],[answer]) VALUES(14, 'Alan', 'test', 4, 1, 'RDJ');
INSERT INTO [dbo].[Users]([userId],[username],[password],[userTypeId],[questionId],[answer]) VALUES(15, 'Isaac', 'test', 4, 1, 'RDJ');
INSERT INTO [dbo].[Users]([userId],[username],[password],[userTypeId],[questionId],[answer]) VALUES(16, 'Steve', 'test', 4, 1, 'RDJ');
INSERT INTO [dbo].[Users]([userId],[username],[password],[userTypeId],[questionId],[answer]) VALUES(17, 'Elon', 'test', 4, 1, 'RDJ');
INSERT INTO [dbo].[Users]([userId],[username],[password],[userTypeId],[questionId],[answer]) VALUES(18, 'Charles', 'test', 4, 1, 'RDJ');
INSERT INTO [dbo].[Users]([userId],[username],[password],[userTypeId],[questionId],[answer]) VALUES(19, 'Ada', 'test', 4, 1, 'RDJ');

