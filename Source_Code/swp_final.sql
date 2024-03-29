CREATE DATABASE [ArtworkSharingPlatform]


USE [ArtworkSharingPlatform]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 24/03/2024 2:44:10 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 24/03/2024 2:44:10 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 24/03/2024 2:44:10 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 24/03/2024 2:44:10 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [nvarchar](450) NOT NULL,
	[UserId] [nvarchar](225) NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 24/03/2024 2:44:10 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](225) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 24/03/2024 2:44:10 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](225) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 24/03/2024 2:44:10 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](225) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[AvatarImage] [nvarchar](max) NULL,
	[DateOfBirth] [date] NULL,
	[Discriminator] [nvarchar](21) NOT NULL,
	[FullName] [nvarchar](max) NULL,
	[Gender] [bit] NULL,
	[Status] [bit] NULL,
	[avaiblePost] [int] NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 24/03/2024 2:44:10 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](225) NOT NULL,
	[LoginProvider] [nvarchar](128) NULL,
	[Name] [nvarchar](128) NULL,
	[Value] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblArtwork]    Script Date: 24/03/2024 2:44:10 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblArtwork](
	[artworkID] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](50) NULL,
	[artistID] [nvarchar](225) NULL,
	[categoryID] [int] NULL,
	[description] [nvarchar](max) NULL,
	[imageURL] [nvarchar](max) NULL,
	[createAt] [datetime2](7) NULL,
	[price] [float] NULL,
	[isPremium] [bit] NULL,
	[isBought] [bit] NULL,
	[reportedConfirm] [bit] NULL,
	[buyerID] [nvarchar](225) NULL,
 CONSTRAINT [PK_tblArtwork] PRIMARY KEY CLUSTERED 
(
	[artworkID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblBlog]    Script Date: 24/03/2024 2:44:10 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBlog](
	[blogID] [int] IDENTITY(1,1) NOT NULL,
	[creatorID] [nvarchar](225) NULL,
	[title] [nvarchar](225) NULL,
	[description] [nvarchar](max) NULL,
	[createdAt] [datetime2](7) NULL,
	[imageUrl] [nvarchar](max) NULL,
 CONSTRAINT [PK_tblBlog] PRIMARY KEY CLUSTERED 
(
	[blogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblCategory]    Script Date: 24/03/2024 2:44:10 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCategory](
	[categoryID] [int] IDENTITY(1,1) NOT NULL,
	[categoryName] [nvarchar](50) NULL,
	[displayOrder] [int] NULL,
 CONSTRAINT [PK_tblCategory] PRIMARY KEY CLUSTERED 
(
	[categoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblOrderDetail]    Script Date: 24/03/2024 2:44:10 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblOrderDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[orderHeaderId] [int] NULL,
	[artworkId] [int] NULL,
	[count] [int] NULL,
	[price] [float] NULL,
 CONSTRAINT [PK_tblOrderDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblOrderDetailPackage]    Script Date: 24/03/2024 2:44:10 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblOrderDetailPackage](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[orderHeaderId] [int] NULL,
	[packageId] [int] NULL,
	[price] [float] NULL,
 CONSTRAINT [PK_tblOrderDetailPackage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblOrderHeader]    Script Date: 24/03/2024 2:44:10 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblOrderHeader](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[applicationUserId] [nvarchar](225) NULL,
	[orderDate] [datetime2](7) NULL,
	[orderTotal] [float] NULL,
	[orderStatus] [nvarchar](max) NULL,
	[paymentStatus] [nvarchar](max) NULL,
	[paymentDate] [datetime2](7) NULL,
	[paymentIntentId] [nvarchar](max) NULL,
	[name] [nvarchar](max) NULL,
	[phoneNumber] [nvarchar](max) NULL,
	[sessionId] [nvarchar](max) NULL,
	[isPackageOrder] [bit] NULL,
	[email] [nvarchar](max) NULL,
 CONSTRAINT [PK_tblOrderHeader] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblPackage]    Script Date: 24/03/2024 2:44:10 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblPackage](
	[packageID] [int] IDENTITY(1,1) NOT NULL,
	[packageName] [nvarchar](200) NULL,
	[price] [float] NULL,
	[amountPost] [int] NULL,
	[description] [nvarchar](max) NULL,
	[adminID] [nvarchar](225) NULL,
 CONSTRAINT [PK_tblPackage] PRIMARY KEY CLUSTERED 
(
	[packageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblReportArtist]    Script Date: 24/03/2024 2:44:10 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblReportArtist](
	[reportArtistID] [int] IDENTITY(1,1) NOT NULL,
	[artistID] [nvarchar](225) NULL,
	[reporterUserID] [nvarchar](225) NULL,
	[reason] [nvarchar](max) NULL,
 CONSTRAINT [PK_ReportArtist] PRIMARY KEY CLUSTERED 
(
	[reportArtistID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblReportArtwork]    Script Date: 24/03/2024 2:44:10 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblReportArtwork](
	[reportArtworkID] [int] IDENTITY(1,1) NOT NULL,
	[artworkID] [int] NULL,
	[reporterUserID] [nvarchar](225) NULL,
	[reason] [nvarchar](max) NULL,
 CONSTRAINT [PK_ReportArtwork] PRIMARY KEY CLUSTERED 
(
	[reportArtworkID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblReportBlog]    Script Date: 24/03/2024 2:44:10 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblReportBlog](
	[reportBlogID] [int] IDENTITY(1,1) NOT NULL,
	[blogID] [int] NULL,
	[reporterUserID] [nvarchar](225) NULL,
	[reason] [nvarchar](max) NULL,
 CONSTRAINT [PK_tblReportBlog] PRIMARY KEY CLUSTERED 
(
	[reportBlogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblShoppingCart]    Script Date: 24/03/2024 2:44:10 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblShoppingCart](
	[shoppingCartID] [int] IDENTITY(1,1) NOT NULL,
	[buyerID] [nvarchar](225) NULL,
	[artistID] [nvarchar](225) NULL,
	[artworkID] [int] NULL,
	[orderID] [int] NULL,
	[artisticServiceID] [int] NULL,
	[count] [int] NULL,
	[isNew] [bit] NULL,
 CONSTRAINT [PK_tblShoppingCart_1] PRIMARY KEY CLUSTERED 
(
	[shoppingCartID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240124175253_addIdentityTable', N'8.0.1')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240124180648_ExtendIdentityUser', N'8.0.1')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'52250872-ad29-4dee-b714-03e381be49cf', N'Moderator', N'MODERATOR', NULL)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'8606d05d-9f9c-4264-af8f-41eb6a84c25a', N'Creator', N'CREATOR', NULL)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'94e32895-2eeb-430b-8593-b50a4f044158', N'Customer', N'CUSTOMER', NULL)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'ede018c6-8e8f-4c3f-9758-f98b31890587', N'Admin', N'ADMIN', NULL)
GO
INSERT [dbo].[AspNetUserLogins] ([LoginProvider], [ProviderKey], [ProviderDisplayName], [UserId]) VALUES (N'Google', N'115208942589200227942', N'Google', N'ad25a20f-554b-41e3-9da4-2e4413c21553')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'07c7eadc-5738-464c-bb16-95a5335cb4a5', N'ede018c6-8e8f-4c3f-9758-f98b31890587')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'f8655c5f-6a79-47c3-b71b-c8b87067aaf7', N'52250872-ad29-4dee-b714-03e381be49cf')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a4d3a019-da59-45fb-8148-7f6bd60836bd', N'8606d05d-9f9c-4264-af8f-41eb6a84c25a')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'ad25a20f-554b-41e3-9da4-2e4413c21553', N'8606d05d-9f9c-4264-af8f-41eb6a84c25a')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [AvatarImage], [DateOfBirth], [Discriminator], [FullName], [Gender], [Status], [avaiblePost]) VALUES (N'07c7eadc-5738-464c-bb16-95a5335cb4a5', N'admin@gmail.com', N'ADMIN@GMAIL.COM', N'admin@gmail.com', N'ADMIN@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAENOP8LrS0nP5KjFt4B462cp9hxuKo75PvChQRVslaaIA1isTIzGe7TpEgLwWouec6g==', N'HCD4CKZRH3LQIS4LYLDAGI2GMGFMTLQP', N'5d58d78f-00eb-4ec6-b689-597ce694b2d5', N'0337824924', 0, 0, NULL, 1, 0, NULL, NULL, N'ApplicationUser', N'Admin', 1, 1, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [AvatarImage], [DateOfBirth], [Discriminator], [FullName], [Gender], [Status], [avaiblePost]) VALUES (N'a4d3a019-da59-45fb-8148-7f6bd60836bd', N'nguyentanhung2003@gmail.com', N'NGUYENTANHUNG2003@GMAIL.COM', N'nguyentanhung2003@gmail.com', N'NGUYENTANHUNG2003@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEF+MDMaXzVEMGjUekqUDC8u46UCPwvHJFZOYFCHq8yGYv6Mia6bgP6eUJCeYN+XwPw==', N'SZB47724VHHHCKYFDFHP7YWRQ4TGCDSW', N'1ac23a2f-ce4a-4730-ba11-49c27faa6e50', N'0323456789', 0, 0, CAST(N'2024-03-14T23:25:38.6679486+07:00' AS DateTimeOffset), 1, 0, N'\image\avatar\df15325c-3b25-4cd1-9ff3-4fbab103fff8.jpg', NULL, N'ApplicationUser', N'Nguyen Tan Hung', 1, 1, 98)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [AvatarImage], [DateOfBirth], [Discriminator], [FullName], [Gender], [Status], [avaiblePost]) VALUES (N'ad25a20f-554b-41e3-9da4-2e4413c21553', N'tainhmse170053@fpt.edu.vn', N'TAINHMSE170053@FPT.EDU.VN', N'tainhmse170053@fpt.edu.vn', N'TAINHMSE170053@FPT.EDU.VN', 1, N'AQAAAAIAAYagAAAAEBKFvlHHBBaaRMlmVDJcMf317UYC2TgJvlW9q/UxPIrhGUlJkngkwTjBl4OP2EEubg==', N'G3JLRIAIEAD3P56DW63AKR3FIFIVEDNH', N'd991d9f7-1473-408e-8672-c5260265d3f2', N'0384600000', 0, 0, NULL, 1, 0, NULL, NULL, N'ApplicationUser', N'Minh Tai', 0, 1, 38)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [AvatarImage], [DateOfBirth], [Discriminator], [FullName], [Gender], [Status], [avaiblePost]) VALUES (N'f8655c5f-6a79-47c3-b71b-c8b87067aaf7', N'moderator@gmail.com', N'MODERATOR@GMAIL.COM', N'moderator@gmail.com', N'MODERATOR@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEKoy2V5rPr2dam+1DunuazyiY+DFIG1hzqRytnQIf9e6uA3G2pyJQHLiuCEMhPyGHQ==', N'XQSNHMAHTJJC7OEBRXX2JK42KNBU26Q6', N'87607848-da72-4543-a10b-bd8a2fe04de7', N'0336626193', 0, 0, NULL, 1, 0, NULL, NULL, N'ApplicationUser', N'Pham Le Quy Anh', 1, 1, NULL)
GO
INSERT [dbo].[AspNetUserTokens] ([UserId], [LoginProvider], [Name], [Value]) VALUES (N'07c7eadc-5738-464c-bb16-95a5335cb4a5', N'[AspNetUserStore]', N'AuthenticatorKey', N'UD5MYI7LLF6RFQS6HM7QBXXSOJ5YZXZR')
INSERT [dbo].[AspNetUserTokens] ([UserId], [LoginProvider], [Name], [Value]) VALUES (N'a4d3a019-da59-45fb-8148-7f6bd60836bd', N'[AspNetUserStore]', N'AuthenticatorKey', N'S7OFMV3RBUV6ZL42ZELQO6JYWHL6GJLK')
INSERT [dbo].[AspNetUserTokens] ([UserId], [LoginProvider], [Name], [Value]) VALUES (N'f8655c5f-6a79-47c3-b71b-c8b87067aaf7', N'[AspNetUserStore]', N'AuthenticatorKey', N'XBMYSZIYBLEFWAPYA7AIFCW42AJOH5AR')
GO
SET IDENTITY_INSERT [dbo].[tblArtwork] ON 

INSERT [dbo].[tblArtwork] ([artworkID], [title], [artistID], [categoryID], [description], [imageURL], [createAt], [price], [isPremium], [isBought], [reportedConfirm], [buyerID]) VALUES (2, N'Night’s Glow', N'a4d3a019-da59-45fb-8148-7f6bd60836bd', 1, N'<p>The swirling night sky dances with vibrant hues of blue and yellow, capturing the imagination with its tumultuous energy. A crescent moon hangs serenely amidst the tumult, casting its gentle glow over the quaint village below. The cypress trees reach desperately towards the heavens, their dark silhouettes contrasting sharply against the luminous backdrop. Van Gogh''s masterful strokes create a sense of movement and emotion, inviting the viewer to lose themselves in the mesmerizing beauty of the night.</p>', N'\image\artwork\89a87bbf-5ada-4c70-89bd-26fae02aed4d.jpg', CAST(N'2024-02-02T02:36:29.4000000' AS DateTime2), 100, 1, 0, 0, NULL)
INSERT [dbo].[tblArtwork] ([artworkID], [title], [artistID], [categoryID], [description], [imageURL], [createAt], [price], [isPremium], [isBought], [reportedConfirm], [buyerID]) VALUES (3, N'Harmony', N'a4d3a019-da59-45fb-8148-7f6bd60836bd', 2, N'<p>In Dal&iacute;''s surreal landscape, time appears to melt away like the soft, drooping clocks that litter the scene. The barren landscape is bathed in an eerie light, casting shadows that seem to stretch and warp with the passage of time. A solitary figure, perhaps a represe...</p>', N'\image\artwork\f383ba51-47be-4afb-8587-140efc26ec44.jpg', CAST(N'2024-02-02T02:36:29.4000000' AS DateTime2), 150, 0, 0, 0, NULL)
INSERT [dbo].[tblArtwork] ([artworkID], [title], [artistID], [categoryID], [description], [imageURL], [createAt], [price], [isPremium], [isBought], [reportedConfirm], [buyerID]) VALUES (4, N'Shadowed Night', N'a4d3a019-da59-45fb-8148-7f6bd60836bd', 3, N'<p>A towering wave looms menacingly overhead, its frothy crest frozen in time against a backdrop of serene Mount Fuji. The tiny boats below are dwarfed by the sheer power and scale of nature, struggling to navigate the treacherous waters. Hokusai''s use of bold lines and dynamic composition captures the raw energy and majesty of the ocean, while the distant silhouette of Mount Fuji serves as a symbol of enduring strength and tranquility amidst the chaos.</p>', N'\image\artwork\16d55526-77e7-40bd-8ae6-ee246334e96d.jpg', CAST(N'2024-02-02T02:36:29.4000000' AS DateTime2), 200, 1, 0, 0, NULL)
INSERT [dbo].[tblArtwork] ([artworkID], [title], [artistID], [categoryID], [description], [imageURL], [createAt], [price], [isPremium], [isBought], [reportedConfirm], [buyerID]) VALUES (5, N'Invisible Rhythm', N'a4d3a019-da59-45fb-8148-7f6bd60836bd', 4, N'<p>The artwork depicts a serene landscape with a small boat sailing on a tranquil river. The warm sunlight bathes the scene, casting long shadows and creating a peaceful atmosphere. The artist has captured the beauty of nature with delicate brushstrokes and vibrant colors, inviting the viewer to escape into this idyllic setting.</p>', N'\image\artwork\a84b1524-c9d9-43f3-b9d9-15329e851d40.jpg', CAST(N'2024-02-02T02:36:29.4000000' AS DateTime2), 120, 1, 0, 0, NULL)
INSERT [dbo].[tblArtwork] ([artworkID], [title], [artistID], [categoryID], [description], [imageURL], [createAt], [price], [isPremium], [isBought], [reportedConfirm], [buyerID]) VALUES (6, N'Imprint of Time', N'a4d3a019-da59-45fb-8148-7f6bd60836bd', 5, N'<p>The vibrant colors and dynamic composition of the artwork evoke a sense of energy and movement. The artists bold use of shapes and lines creates a visually striking piece that captivates the viewer attention. Through this artwork, the artist explores themes of life, vitality, and the interconnectedness of all things.</p>', N'\image\artwork\5dd87edd-551b-4321-a159-1d329b010883.jpg', CAST(N'2024-02-02T02:36:29.4000000' AS DateTime2), 180, 1, 0, 0, NULL)
INSERT [dbo].[tblArtwork] ([artworkID], [title], [artistID], [categoryID], [description], [imageURL], [createAt], [price], [isPremium], [isBought], [reportedConfirm], [buyerID]) VALUES (7, N'Illusion', N'a4d3a019-da59-45fb-8148-7f6bd60836bd', 1, N'<p>The artwork portrays a serene countryside scene, with rolling hills and lush greenery. A winding path leads the viewers eye through the landscape, inviting them to explore its beauty. The artist has captured the tranquility of the countryside with delicate brushstrokes and soft, muted colors, creating a sense of peace and harmony.</p>', N'\image\artwork\5d26ebf4-52ee-4b0c-a656-eaf6fa17af72.jpg', CAST(N'2024-02-02T02:36:29.4000000' AS DateTime2), 90, 0, 0, 0, NULL)
INSERT [dbo].[tblArtwork] ([artworkID], [title], [artistID], [categoryID], [description], [imageURL], [createAt], [price], [isPremium], [isBought], [reportedConfirm], [buyerID]) VALUES (9, N'Principle of Concord', N'a4d3a019-da59-45fb-8148-7f6bd60836bd', 3, N'<p>The artwork portrays a tranquil garden scene, with colorful flowers blooming amidst lush green foliage. A gentle breeze rustles the leaves of the trees, creating a sense of movement and life. The artist has captured the beauty of nature with delicate brushstrokes and vibrant colors, inviting the viewer to escape into this idyllic setting.</p>', N'\image\artwork\2e4c8019-9119-4789-b756-15191712eeca.jpg', CAST(N'2024-02-02T02:36:29.4000000' AS DateTime2), 220, 1, 0, 0, NULL)
INSERT [dbo].[tblArtwork] ([artworkID], [title], [artistID], [categoryID], [description], [imageURL], [createAt], [price], [isPremium], [isBought], [reportedConfirm], [buyerID]) VALUES (11, N'Merging with Darkness', N'a4d3a019-da59-45fb-8148-7f6bd60836bd', 5, N'<p>The artwork portrays a tranquil forest scene, with sunlight filtering through the trees and casting dappled shadows on the forest floor. A winding path leads the viewer deeper into the woods, inviting them to explore its beauty. The artist has captured the serenity of the forest with delicate brushstrokes and soft, muted colors, creating a sense of peace and tranquility.</p>', N'\image\artwork\73bd3c7e-62bc-469d-94ae-e9521bf2404d.jpg', CAST(N'2024-02-02T02:36:29.4000000' AS DateTime2), 200, 0, 0, 0, NULL)
INSERT [dbo].[tblArtwork] ([artworkID], [title], [artistID], [categoryID], [description], [imageURL], [createAt], [price], [isPremium], [isBought], [reportedConfirm], [buyerID]) VALUES (16, N'Harmonizing with Light', N'a4d3a019-da59-45fb-8148-7f6bd60836bd', 5, N'<p>The artwork depicts a bustling market scene, with vendors selling their wares amidst a cacophony of sights and sounds. Brightly colored fabrics hang from awnings, while exotic fruits and spices fill the air with their sweet scent. The artist has captured the vibrancy of the market with bold brushstrokes and dynamic composition, creating a sense of excitement and energy.</p>', N'\image\artwork\f73e84c9-75ba-4efc-8dc0-92ba6eb8c8ea.jpg', CAST(N'2024-02-02T02:36:29.4000000' AS DateTime2), 200, 1, 0, 0, NULL)
INSERT [dbo].[tblArtwork] ([artworkID], [title], [artistID], [categoryID], [description], [imageURL], [createAt], [price], [isPremium], [isBought], [reportedConfirm], [buyerID]) VALUES (17, N'Prominent Points', N'a4d3a019-da59-45fb-8148-7f6bd60836bd', 1, N'<p>The artwork portrays a tranquil garden scene, with colorful flowers blooming amidst lush green foliage. A gentle breeze rustles the leaves of the trees, creating a sense of movement and life. The artist has captured the beauty of nature with delicate brushstrokes and vibrant colors, inviting the viewer to escape into this idyllic setting.</p>', N'\image\artwork\3972cfb0-93f6-41e9-8c83-df10bf81e39d.jpg', CAST(N'2024-02-02T02:36:29.4000000' AS DateTime2), 120, 1, 0, 0, NULL)
INSERT [dbo].[tblArtwork] ([artworkID], [title], [artistID], [categoryID], [description], [imageURL], [createAt], [price], [isPremium], [isBought], [reportedConfirm], [buyerID]) VALUES (18, N'From Point to Point', N'a4d3a019-da59-45fb-8148-7f6bd60836bd', 2, N'<p>The artwork depicts a serene beach scene, with palm trees swaying in the gentle breeze and waves lapping at the shore. The warm sunlight bathes the scene, casting long shadows and creating a peaceful atmosphere. The artist has captured the beauty of the beach with delicate brushstrokes and vibrant colors, inviting the viewer to escape into this idyllic setting.</p>', N'\image\artwork\5f87ad76-633c-4dea-9e2c-0db96c57f091.jpg', CAST(N'2024-02-02T02:36:29.4000000' AS DateTime2), 180, 1, 0, 0, NULL)
INSERT [dbo].[tblArtwork] ([artworkID], [title], [artistID], [categoryID], [description], [imageURL], [createAt], [price], [isPremium], [isBought], [reportedConfirm], [buyerID]) VALUES (19, N'Touching Infinity', N'a4d3a019-da59-45fb-8148-7f6bd60836bd', 3, N'<p>The artwork portrays a tranquil forest scene, with sunlight filtering through the trees and casting dappled shadows on the forest floor. A winding path leads the viewer deeper into the woods, inviting them to explore its beauty. The artist has captured the serenity of the forest with delicate brushstrokes and soft, muted colors, creating a sense of peace and tranquility.</p>', N'\image\artwork\f488a6b3-d3a6-44ad-bf4d-7d2ea3e64958.jpg', CAST(N'2024-02-02T02:36:29.4000000' AS DateTime2), 240, 1, 1, 0, NULL)
INSERT [dbo].[tblArtwork] ([artworkID], [title], [artistID], [categoryID], [description], [imageURL], [createAt], [price], [isPremium], [isBought], [reportedConfirm], [buyerID]) VALUES (20, N'Subdued Cadence', N'a4d3a019-da59-45fb-8148-7f6bd60836bd', 4, N'<p>The artwork depicts a bustling cityscape, alive with activity and energy. Bright lights illuminate the streets, casting vibrant reflections in the rain-soaked pavement below. The artist has captured the frenetic pace of urban life with bold brushstrokes and dynamic composition, creating a sense of movement and vitality.</p>', N'\image\artwork\1ea010f4-587f-4255-b079-eedbe86841f3.jpg', CAST(N'2024-02-02T02:36:29.4000000' AS DateTime2), 150, 0, 0, 0, NULL)
INSERT [dbo].[tblArtwork] ([artworkID], [title], [artistID], [categoryID], [description], [imageURL], [createAt], [price], [isPremium], [isBought], [reportedConfirm], [buyerID]) VALUES (21, N'Artwork of Time', N'a4d3a019-da59-45fb-8148-7f6bd60836bd', 5, N'<p>This component of the description will need to refer directly to the artwork at hand. There''s no perfect formula for this, however it does need to be engaging! Remember that it should reflect you and your personality, as well as your enthusiasm for your work. This component of the description will need to refer directly to the artwork at hand. There''s no perfect formula for this, however it does need to be engaging! Remember that it should reflect you and your personality, as well as your enthusiasm for your work.</p>', N'\image\artwork\99b2b267-dc31-4a5d-9f70-4edb4564207b.jpg', CAST(N'2024-02-02T02:36:29.4000000' AS DateTime2), 210, 1, 0, 0, NULL)
INSERT [dbo].[tblArtwork] ([artworkID], [title], [artistID], [categoryID], [description], [imageURL], [createAt], [price], [isPremium], [isBought], [reportedConfirm], [buyerID]) VALUES (22, N'The Raft of the Medusa', N'ad25a20f-554b-41e3-9da4-2e4413c21553', 1, N'<p>The Raft of the Medusa" is an oil painting depicting the "horrifying" and "tragic" moment of the sinking of the French naval ship Meduse on July 2, 1816. The painting shocks and disturbs viewers, prompting them to contemplate the harsh realities behind it. It can be said that "The Raft of the Medusa" transcends the boundaries of art, becoming a famous piece of politically charged artwork that deeply reflects the society of that time.</p>', N'\image\artwork\8432a856-a42d-4090-9e02-0d056e381fa4.jpg', CAST(N'2024-03-24T14:05:12.3605088' AS DateTime2), 200, 1, 0, 0, NULL)
INSERT [dbo].[tblArtwork] ([artworkID], [title], [artistID], [categoryID], [description], [imageURL], [createAt], [price], [isPremium], [isBought], [reportedConfirm], [buyerID]) VALUES (23, N'The Death of Sardanapalus', N'ad25a20f-554b-41e3-9da4-2e4413c21553', 2, N'<p>The Death of Sardanapalus" is an oil painting inspired by Lord Byron''s play "Sardanapalus." The painting is considered one of the most important works of the Romanticism movement. At the centre of the painting is a large bed draped in red fabric, upon which lies a lavishly dressed man amidst a scene of chaos, with all objects in the picture seemingly in positions of violent death. Delacroix depicted this with vivid and bold colours, bringing a sense of intensity and disorder to the artwork.</p>', N'\image\artwork\694b1d8a-731c-4560-bde2-cfb477c48fb4.jpg', CAST(N'2024-03-24T14:06:26.0959497' AS DateTime2), 230, 0, 0, 0, NULL)
INSERT [dbo].[tblArtwork] ([artworkID], [title], [artistID], [categoryID], [description], [imageURL], [createAt], [price], [isPremium], [isBought], [reportedConfirm], [buyerID]) VALUES (24, N'The Moneylender and His Wife', N'ad25a20f-554b-41e3-9da4-2e4413c21553', 2, N'<p>The Moneylender and His Wife" accurately and meticulously depicted the outlines of both characters and the scene in the painting. We can see the husband''s greed and the wife''s calculation reflected in their facial expressions and eyes as they carefully count each coin.</p>', N'\image\artwork\6dec6c0a-f901-45a3-b6d1-4d2a76f4da3a.jpg', CAST(N'2024-03-24T14:07:18.6476217' AS DateTime2), 150, 1, 0, 0, NULL)
INSERT [dbo].[tblArtwork] ([artworkID], [title], [artistID], [categoryID], [description], [imageURL], [createAt], [price], [isPremium], [isBought], [reportedConfirm], [buyerID]) VALUES (25, N'The Coronation of Napoleon', N'ad25a20f-554b-41e3-9da4-2e4413c21553', 1, N'<p>The Coronation of Napoleon" also known as "Le Sacre de Napol&eacute;on," portrays the coronation ceremony of Emperor Napoleon Bonaparte and Empress Josephine at the Notre-Dame Cathedral in Paris on December 2, 1804. The grandeur and authority of Napoleon''s reign are vividly depicted in the painting.</p>', N'\image\artwork\e28c50ef-87fb-444b-afd1-c95a11b221bf.jpg', CAST(N'2024-03-24T14:08:18.0463302' AS DateTime2), 300, 1, 0, 0, NULL)
INSERT [dbo].[tblArtwork] ([artworkID], [title], [artistID], [categoryID], [description], [imageURL], [createAt], [price], [isPremium], [isBought], [reportedConfirm], [buyerID]) VALUES (26, N'The Starry Night', N'ad25a20f-554b-41e3-9da4-2e4413c21553', 2, N'<p>The Starry Night" is recognized as one of the most beautiful works in the history of Western cultural art. It is a painting belonging to the abstract style, depicting the nighttime scenery in the village of Saint-R&eacute;my-de-Provence, Southern France. The artist used prominent shades of blue and yellow to represent the brilliance of the night sky and swirling strokes symbolizing the human desire for freedom. Particularly, the painting was created while the author was undergoing treatment for mental illness, drawing inspiration from the view outside his room window.</p>', N'\image\artwork\c8dfb7c3-58b2-49a5-bbd8-e53d33916ff0.jpg', CAST(N'2024-03-24T14:09:01.3032432' AS DateTime2), 400, 1, 0, 0, NULL)
INSERT [dbo].[tblArtwork] ([artworkID], [title], [artistID], [categoryID], [description], [imageURL], [createAt], [price], [isPremium], [isBought], [reportedConfirm], [buyerID]) VALUES (27, N'The Persistence of Memory', N'ad25a20f-554b-41e3-9da4-2e4413c21553', 1, N'<p>The Persistence of Memory" is an oil painting created by the artist Salvador Dali in 1931. The focal point of the painting revolves around four melted clocks amidst a desolate desert landscape. It is known that "The Persistence of Memory" is a work of surrealism, inspired by Dali''s dreams. In this context, the melting clocks symbolise the indefinite passage of time, flowing without order, and susceptible to distortion and upheaval.</p>', N'\image\artwork\ad9d8216-b658-45f4-906b-e70afa0f6ac1.jpg', CAST(N'2024-03-24T14:09:58.7501887' AS DateTime2), 150, 1, 0, 0, NULL)
INSERT [dbo].[tblArtwork] ([artworkID], [title], [artistID], [categoryID], [description], [imageURL], [createAt], [price], [isPremium], [isBought], [reportedConfirm], [buyerID]) VALUES (28, N'Guernica', N'ad25a20f-554b-41e3-9da4-2e4413c21553', 1, N'<p>Guernica" is a famous artwork completed in 1937 by the painter Picasso. The painting is one of the most significant icons in the cultural history of the 20th century. The artwork depicts the bombing of the town of Guernica by the German Luftwaffe during the Spanish Civil War. This brutal event claimed the lives of hundreds of innocent people. "Guernica" serves as a protest against war, particularly against fascist military aggression.</p>', N'\image\artwork\4c96c604-6e3e-4b99-9646-359105acafc2.jpg', CAST(N'2024-03-24T14:10:47.6920972' AS DateTime2), 120, 1, 0, 0, NULL)
INSERT [dbo].[tblArtwork] ([artworkID], [title], [artistID], [categoryID], [description], [imageURL], [createAt], [price], [isPremium], [isBought], [reportedConfirm], [buyerID]) VALUES (29, N'Las Meninas', N'ad25a20f-554b-41e3-9da4-2e4413c21553', 1, N'<p>Las Meninas" is the most famous painting by the artist Diego Vel&aacute;zquez, completed in 1656. The artwork portrays the portrait of Princess Margarita Teresa and her entourage. Additionally, the painting is a self-portrait of Vel&aacute;zquez himself, as he included his own likeness while working on the piece. The mysterious textures and composition of the painting always evoke a sense of illusion for the viewers.</p>', N'\image\artwork\398f7e8a-82ab-4383-b487-9e0e33cccf80.jpg', CAST(N'2024-03-24T14:13:24.0348274' AS DateTime2), 120, 1, 0, 0, NULL)
INSERT [dbo].[tblArtwork] ([artworkID], [title], [artistID], [categoryID], [description], [imageURL], [createAt], [price], [isPremium], [isBought], [reportedConfirm], [buyerID]) VALUES (30, N'The Girl with Lily Flowers', N'ad25a20f-554b-41e3-9da4-2e4413c21553', 1, N'<p>The Girl with Lily Flowers" depicts the portrait of a young woman wearing a white &aacute;o d&agrave;i (traditional Vietnamese dress), gracefully tilting her head towards a vase of white lily flowers. The figure of the girl, along with the surrounding details and colors, forms a simple yet poignant composition, exuding a subtle sense of melancholy. The lily flowers placed in the vase next to the girl are not the typical small lilies often used on special occasions but rather Western lilies, commonly known as trumpet lilies.</p>', N'\image\artwork\bdc8975b-ed27-473a-971b-dda70ced0b26.jpg', CAST(N'2024-03-24T14:14:04.1444254' AS DateTime2), 300, 1, 0, 0, NULL)
INSERT [dbo].[tblArtwork] ([artworkID], [title], [artistID], [categoryID], [description], [imageURL], [createAt], [price], [isPremium], [isBought], [reportedConfirm], [buyerID]) VALUES (31, N'Em Thuy', N'ad25a20f-554b-41e3-9da4-2e4413c21553', 1, N'<p>The painting portrays a self-portrait of Thuy sitting on a wicker chair. Her hands are clasped together, tucked into her lap, wearing simple white attire. She has short hair, wide open eyes with a bright, innocent expression. The character is not placed in the centre of the painting but rather leans slightly towards the left side. However, balance is achieved in the composition through the lines of the wicker chair, hair, and hands of the character.</p>', N'\image\artwork\80a3903e-a4d2-40ed-98e1-45c4761d152e.jpg', CAST(N'2024-03-24T14:14:58.8557552' AS DateTime2), 100, 1, 0, 0, NULL)
INSERT [dbo].[tblArtwork] ([artworkID], [title], [artistID], [categoryID], [description], [imageURL], [createAt], [price], [isPremium], [isBought], [reportedConfirm], [buyerID]) VALUES (37, N'Uncle Ho in the Viet Bac Base Area', N'ad25a20f-554b-41e3-9da4-2e4413c21553', 1, N'<p>The painting depicts Uncle Ho (Ho Chi Minh) and a horse preparing to cross a stream. Uncle Ho is dressed in simple attire, a brown shirt and cloth bag, calmly preparing to cross the rushing floodwaters. While the mountains and forests may appear turbulent, the flowing water is swift, the human figure remains composed and serene, showing self-reliance. The person does not appear to be intimidated by nature, instead calmly reassuring the horse.</p>', N'\image\artwork\caa0b1ac-539c-4bd9-8af1-b76ba31e1245.jpg', CAST(N'2024-03-24T14:29:52.9757463' AS DateTime2), 200, 1, 0, 0, NULL)
INSERT [dbo].[tblArtwork] ([artworkID], [title], [artistID], [categoryID], [description], [imageURL], [createAt], [price], [isPremium], [isBought], [reportedConfirm], [buyerID]) VALUES (38, N'Giong', N'ad25a20f-554b-41e3-9da4-2e4413c21553', 1, N'<p>The painting of Saint Giong serves as a symbol of the nation''s aspirations for independence, freedom, and resilience. It employs the language of collective artistry. The author explores the ethnic patterns found on Dong Son bronze drums and ceramic motifs, creating a distinct cultural identity.</p>', N'\image\artwork\23da8816-29ab-467a-9613-f94d23cda7fc.jpg', CAST(N'2024-03-24T14:30:38.5251981' AS DateTime2), 200, 1, 0, 0, NULL)
SET IDENTITY_INSERT [dbo].[tblArtwork] OFF
GO
SET IDENTITY_INSERT [dbo].[tblBlog] ON 

INSERT [dbo].[tblBlog] ([blogID], [creatorID], [title], [description], [createdAt], [imageUrl]) VALUES (1, N'a4d3a019-da59-45fb-8148-7f6bd60836bd', N'You Just Bought a Painting at Art Basel. Now What?
', N'Most losses occur in transit, making professional art handlers essential for packing and crating. Consider, for example, a collector who was told by the general carrier he used that the sculpture he purchased overseas and was shipping to his home was lost and could not be located. Or the collector who used a general contents moving company to transport a high-value painting only to see the work arrive with bubble wrap stuck to the acrylic paint and surface scratches from not being properly packed and secured within the truck. Chubb’s specialists can advise on packing best practices and can connect clients with best-in-class transporters. If the artwork needs to travel a long distance, it’s important to confirm whether the shipper will use a subcontractor – and if so, ensure all trucks follow the same protective measures such as air-ride suspension, climate controls, GPS tracking, and alarms, with two drivers so trucks are not unattended. ', CAST(N'2024-04-02T00:00:00.0000000' AS DateTime2), N'https://upload.wikimedia.org/wikipedia/commons/1/16/ART_Basel_2009-06-10.jpg')
INSERT [dbo].[tblBlog] ([blogID], [creatorID], [title], [description], [createdAt], [imageUrl]) VALUES (7, N'a4d3a019-da59-45fb-8148-7f6bd60836bd', N'Ancient Frescoes of Mythological Refugee Siblings ', N'A fresco depicting two Greek mythological siblings Phrixus and Helle has been found in the ancient Roman city Pompeii.

“History has repeated itself,” the director of Pompeii Archaeological Park Gabriel Zuchtriegel said, according to the Guardian, in an update on the excavation and restoration work. “It is a beautiful fresco in an excellent state of conservation. The myth of Phrixus and Helle is widespread at Pompeii but it is topical too. They are two refugees at sea, a brother and sister, forced to flee because their stepmother wants rid of them and she does so with deception and corruption. She [Helle] fell into the water and drowned.”', CAST(N'2024-02-02T00:00:00.0000000' AS DateTime2), N'https://greekreporter.com/wp-content/uploads/2024/03/pompeii-ancient-greek-siblings-credit-archaeological-park-pompeii.jpg')
INSERT [dbo].[tblBlog] ([blogID], [creatorID], [title], [description], [createdAt], [imageUrl]) VALUES (8, N'a4d3a019-da59-45fb-8148-7f6bd60836bd', N'Joan Semmel Details Her Painting on A.i.A.’s', N'I use myself as a model, but I don’t think of my works as self-portraits. Portraits talk about characters in more specific ways, whereas I just use myself as the model because it’s convenient, and because I don’t want to objectify anyone else. In this particular painting, I used two versions of the same model. The couch image has usually been seductive in nature, with a nude spread out and being relatively sexy. I’m painting myself, an older person, in contrast to that. One image is of my older body in a seductive pose, but the second image, [the one on the cover], shows me in a much more defensive posture. The figure is holding her arm up, almost as if warding off the viewer. ', CAST(N'2024-01-01T00:00:00.0000000' AS DateTime2), N'https://static01.nyt.com/images/2021/11/15/t-magazine/15tmag-semmel-slide-NSBO/15tmag-semmel-slide-NSBO-superJumbo.jpg')
INSERT [dbo].[tblBlog] ([blogID], [creatorID], [title], [description], [createdAt], [imageUrl]) VALUES (9, N'a4d3a019-da59-45fb-8148-7f6bd60836bd', N'Cyber Deals on Tech for Artists Continue Today—Updated', N'It’s Cyber Monday! Not all sale items are actually good buys, but we’ve done the research and found real (if sometimes modest) deals on electronics, photography gear, and software. Our recommendations below will help you get a jump on your holiday gift shopping for family, friends, and even yourself. We’ll be updating this page throughout the weekend, so check back often.

How we pick each product:
Our mission is to recommend the most appropriate artists’ tool or supply for your needs. Whether you are looking for top-of-the line equipment or beginners’ basics, we’ll make sure that you get good value for your money by doing the research for you. We scour the Internet for information on how art supplies are used and read customer reviews by real users; we ask experts for their advice; and of course, we rely on our own accumulated expertise as artists, teachers, and craftspeople.', CAST(N'2023-11-21T00:00:00.0000000' AS DateTime2), N'https://img.freepik.com/premium-photo/digital-camera-table_980804-776.jpg')
INSERT [dbo].[tblBlog] ([blogID], [creatorID], [title], [description], [createdAt], [imageUrl]) VALUES (10, N'a4d3a019-da59-45fb-8148-7f6bd60836bd', N'Cyber Deals on Art Supplies and Gifts Continue Today', N'<p><span style="font-family: ''trebuchet ms'', geneva, sans-serif;">It&rsquo;s Cyber Monday! Many US retailers have marked down selected wares, including art and craft supplies. To help you parse the offerings, we&rsquo;ll be tracking some of the best deals on artists&rsquo; tools, from pastels to tablets, to give as gifts or to keep for yourself. We will be updating this page, so check in with us often. A word of advice: Move fast, as many of these products will sell out quickly. (All prices current at time of publication.) How we pick each product: Our mission is to recommend the most appropriate artists&rsquo; tool or supply for your needs. Whether you are looking for top-of-the line equipment or beginners&rsquo; basics, we&rsquo;ll make sure that you get good value for your money by doing the research for you. We scour the Internet for information on how art supplies are used and read customer reviews by real users; we ask experts for their advice; and of course, we rely on our own accumulated expertise as artists, teachers, and craftspeople.</span></p>', CAST(N'2024-01-01T00:00:00.0000000' AS DateTime2), N'https://www.veganeasy.org/wp-content/uploads/2022/08/vegan_art_materials_101.jpg')
INSERT [dbo].[tblBlog] ([blogID], [creatorID], [title], [description], [createdAt], [imageUrl]) VALUES (11, N'a4d3a019-da59-45fb-8148-7f6bd60836bd', N'The Best Gel Pens for Writers, Artists, and Students', N'Overwhelmed by which pen to buy? That’s understandable, considering that there are many types of pens, not to mention hundreds of different brands and designs within these types. Consider the gel pen, which offers a pleasurable and user-friendly writing experience. Like ballpoint and rollerball pens, they feature a small revolving ball at the tip that deposits ink from the pen’s reservoir to the paper. The difference is while ballpoint pens use thick, oil-based inks, and rollerballs use a free-flowing, water- and dye-based ink, gel pens use ink made of pigment in a water-based gel made from xanthan gum and polyacrylate thickeners. What you get are particularly vibrant lines that flow smoothly so you can apply less pressure as you write. They are also generally the best pick for signing documents because of their pigment load, which makes your checks and other vital documents resistant to tampering. The ink does tend to take a bit longer to dry, however, which can be a problem for lefties (but see the Zebra Sarasa below). Dive into some of our favorites in this family, below.

', CAST(N'2024-01-01T00:00:00.0000000' AS DateTime2), N'https://www.truphaeinc.com/cdn/shop/articles/image1.jpg?v=1681507933')
INSERT [dbo].[tblBlog] ([blogID], [creatorID], [title], [description], [createdAt], [imageUrl]) VALUES (12, N'ad25a20f-554b-41e3-9da4-2e4413c21553', N'Art of Abstract Painting: How to Understand and Appreciate?', N'<p dir="ltr">Abstract art is a form of art that does not accurately or explicitly depict objects from the physical world. It often focuses on expressing emotions, meanings, or ideas through the abstraction of imagery.</p>
<p dir="ltr">One of the main characteristics of abstract art is the abstraction of shapes and images, achieved through the use of elements such as curves, colors, light, structure, and proportion. Abstract artworks typically do not aim to represent specific objects; instead, they create a state of mind, explore abstract concepts, or demonstrate the creativity and freedom of the artist. Abstract painting emerged as a modern art form in the late 19th century and flourished in the 20th century. It originated as a resistance to the traditions of portrait painting and realism. In abstract art, artists focus on creating abstract works rather than faithful representations of the outside world.</p>
<p dir="ltr">The first form of abstract art to emerge was geometric abstraction, in which images are formed from simple geometric shapes such as straight lines, squares, and circles. Over time, abstract artists expanded their scope and became more creative in their use of color, shape, and structure.</p>
<p dir="ltr">Abstract art gained significant attention through the art exhibition "The Armory Show" held in New York in 1913. This exhibition introduced works by European abstract artists, including Pablo Picasso and Georges Braque, marking a significant turning point in the history of abstract painting. This led to the development and diversification of various styles and methods such as abstract expressionism, abstract surrealism, and abstraction in abstract art. It has influenced various fields of art, including sculpture, architecture, and design.</p>
<p dir="ltr">Famous artists in the history of abstract art include Piet Mondrian, Wassily Kandinsky, Kazimir Malevich, Joan Mir&oacute;, and Jackson Pollock. Their works reflect the innovation and expansion of abstract painting in exploring emotions and meanings through abstract imagery.</p>
<p dir="ltr">In the 21st century, with the continuous diversity and creativity of modern abstract artists, abstract painting continues to exist and develop. It has become and remains one of the most important and influential art forms globally.</p>
<p>&nbsp;</p>', CAST(N'2024-03-24T14:31:39.1364248' AS DateTime2), N'\image\blog\9299aae9-8262-4414-a807-241ed26d2e6f.jpg')
INSERT [dbo].[tblBlog] ([blogID], [creatorID], [title], [description], [createdAt], [imageUrl]) VALUES (13, N'ad25a20f-554b-41e3-9da4-2e4413c21553', N'Sand Art Painting - Concept, Origin, Materials & Techniques', N'<p dir="ltr">Sand art painting is one of the unique traditional art forms of Vietnam. Originating and developing during the period of modern Vietnamese art, sand art paintings have undergone many changes and advancements in content and sand painting techniques.</p>
<p dir="ltr">A sand art painting, valued for its artistic quality, demands high-quality materials and the creative ability, skill, and meticulousness of the artist. Through this, it can express the core beauty embodying the spirit of the Vietnamese people.</p>
<p dir="ltr">Sand art painting is a form of artistic expression that utilizes colored sand, mineral powder, crystals, or other natural or synthetic pigments to create images according to the artist''s artistic intent. Besides being known as sand paintings, this art form is also referred to as sand drawing or sand painting.&nbsp;</p>
<p dir="ltr">Sand art paintings are divided into two types: dynamic sand paintings and static sand paintings. Each type of sand painting has its own unique characteristics and conveys different meanings, but overall, they all hold high artistic value. In Vietnam, static sand paintings are more common than dynamic ones.</p>
<p>&nbsp;</p>', CAST(N'2024-03-24T14:32:21.1452424' AS DateTime2), N'\image\blog\e63bebfd-fdcd-4a6d-8b09-c75dc09b499b.jpg')
INSERT [dbo].[tblBlog] ([blogID], [creatorID], [title], [description], [createdAt], [imageUrl]) VALUES (14, N'ad25a20f-554b-41e3-9da4-2e4413c21553', N'The Beauty & Meaning of the Lotus Flower in Vietnamese Art', N'<p dir="ltr">For generations, the lotus flower has become a familiar and popular symbol in Vietnamese culture and art. This distinctive image has deeply ingrained itself into the psyche of the Vietnamese people. Throughout the countryside and small villages in Vietnam, you can easily encounter lush ponds of vibrant pink and vivid green lotus flowers. The lotus flower has entered the hearts of the Vietnamese people in a simple, natural way. Moreover, the image of the lotus flower has been infused by artisans into their works, portraying an incredibly simple, delicate, and graceful beauty.</p>
<p dir="ltr">The image of the lotus flower has become one of the iconic elements in the design concept of many works of art. The lotus flower is a species closely associated with human life for a long time. Particularly in Buddhist thought, the lotus flower is highly revered and holds a very significant position. Therefore, in traditional architecture, the lotus flower is always an artistic image imbued with religious belief.</p>
<p dir="ltr">Initially, lotus flowers were planted in front of the three gates, archways, and on both sides of temples. Later, this was embedded into architectural details and became familiar. It is often found in the main worship places of temples, from stone bas-reliefs, stone carvings, to decorative ceramics. Dien Huu Pagoda is one of the earliest temples in the history of Vietnamese architecture. There, the image of the lotus flower appears in a very special way &ndash; the lotus flower is placed amidst lotus ponds. If Dien Huu Pagoda is a thousand-petal lotus flower erected on a pillar, then Pho Minh Pagoda gives us a new perspective on the development of multi-story tower models with the image of the lotus flower through various carved decorations on the lower body.</p>
<p>&nbsp;</p>', CAST(N'2024-03-24T14:32:59.9909656' AS DateTime2), N'\image\blog\141fbf8a-f22b-43b6-a8b0-575421dbf9f0.jpg')
SET IDENTITY_INSERT [dbo].[tblBlog] OFF
GO
SET IDENTITY_INSERT [dbo].[tblCategory] ON 

INSERT [dbo].[tblCategory] ([categoryID], [categoryName], [displayOrder]) VALUES (1, N'Painting', 1)
INSERT [dbo].[tblCategory] ([categoryID], [categoryName], [displayOrder]) VALUES (2, N'Sculpture', 2)
INSERT [dbo].[tblCategory] ([categoryID], [categoryName], [displayOrder]) VALUES (3, N'Photography', 2)
INSERT [dbo].[tblCategory] ([categoryID], [categoryName], [displayOrder]) VALUES (4, N'Digital Art', 5)
INSERT [dbo].[tblCategory] ([categoryID], [categoryName], [displayOrder]) VALUES (5, N'Mixed Media', 4)
SET IDENTITY_INSERT [dbo].[tblCategory] OFF
GO
SET IDENTITY_INSERT [dbo].[tblOrderDetailPackage] ON 

INSERT [dbo].[tblOrderDetailPackage] ([Id], [orderHeaderId], [packageId], [price]) VALUES (1, 1, 4, 99)
SET IDENTITY_INSERT [dbo].[tblOrderDetailPackage] OFF
GO
SET IDENTITY_INSERT [dbo].[tblOrderHeader] ON 

INSERT [dbo].[tblOrderHeader] ([Id], [applicationUserId], [orderDate], [orderTotal], [orderStatus], [paymentStatus], [paymentDate], [paymentIntentId], [name], [phoneNumber], [sessionId], [isPackageOrder], [email]) VALUES (1, N'ad25a20f-554b-41e3-9da4-2e4413c21553', CAST(N'2024-03-24T14:03:03.8752502' AS DateTime2), 99, N'Done', N'Approved', CAST(N'2024-03-24T14:03:28.6577180' AS DateTime2), N'pi_3Oxl5WAKnLcEuWdy0JjuJZle', N'Minh Tai', N'0384600000', N'cs_test_a1Zwdk5SSI9c41ZxCueab0c6drAdtLddIiAOPzA9ndB8uew467nsn7vGWq', 1, N'tainhmse170053@fpt.edu.vn')
SET IDENTITY_INSERT [dbo].[tblOrderHeader] OFF
GO
SET IDENTITY_INSERT [dbo].[tblPackage] ON 

INSERT [dbo].[tblPackage] ([packageID], [packageName], [price], [amountPost], [description], [adminID]) VALUES (2, N'Basic Upgrade', 29, 15, N'Upgrade your account to access additional features.', N'07c7eadc-5738-464c-bb16-95a5335cb4a5')
INSERT [dbo].[tblPackage] ([packageID], [packageName], [price], [amountPost], [description], [adminID]) VALUES (3, N'Premium Upgrade', 59, 30, N'Unlock premium features and priority support.', N'07c7eadc-5738-464c-bb16-95a5335cb4a5')
INSERT [dbo].[tblPackage] ([packageID], [packageName], [price], [amountPost], [description], [adminID]) VALUES (4, N'Deluxe Upgrade', 99, 50, N'Enjoy the ultimate experience with exclusive perks.', N'07c7eadc-5738-464c-bb16-95a5335cb4a5')
SET IDENTITY_INSERT [dbo].[tblPackage] OFF
GO
ALTER TABLE [dbo].[AspNetUsers] ADD  CONSTRAINT [DF__AspNetUse__Discr__6442E2C9]  DEFAULT (N'') FOR [Discriminator]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers]
GO
ALTER TABLE [dbo].[tblArtwork]  WITH CHECK ADD  CONSTRAINT [FK_tblArtwork_AspNetUsers] FOREIGN KEY([artistID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[tblArtwork] CHECK CONSTRAINT [FK_tblArtwork_AspNetUsers]
GO
ALTER TABLE [dbo].[tblArtwork]  WITH CHECK ADD  CONSTRAINT [FK_tblArtwork_tblCategory] FOREIGN KEY([categoryID])
REFERENCES [dbo].[tblCategory] ([categoryID])
GO
ALTER TABLE [dbo].[tblArtwork] CHECK CONSTRAINT [FK_tblArtwork_tblCategory]
GO
ALTER TABLE [dbo].[tblBlog]  WITH CHECK ADD  CONSTRAINT [FK_tblBlog_AspNetUsers] FOREIGN KEY([creatorID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[tblBlog] CHECK CONSTRAINT [FK_tblBlog_AspNetUsers]
GO
ALTER TABLE [dbo].[tblOrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_tblOrderDetail_tblArtwork] FOREIGN KEY([artworkId])
REFERENCES [dbo].[tblArtwork] ([artworkID])
GO
ALTER TABLE [dbo].[tblOrderDetail] CHECK CONSTRAINT [FK_tblOrderDetail_tblArtwork]
GO
ALTER TABLE [dbo].[tblOrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_tblOrderDetail_tblOrderHeader] FOREIGN KEY([orderHeaderId])
REFERENCES [dbo].[tblOrderHeader] ([Id])
GO
ALTER TABLE [dbo].[tblOrderDetail] CHECK CONSTRAINT [FK_tblOrderDetail_tblOrderHeader]
GO
ALTER TABLE [dbo].[tblOrderDetailPackage]  WITH CHECK ADD  CONSTRAINT [FK_tblOrderDetailPackage_tblOrderHeader] FOREIGN KEY([orderHeaderId])
REFERENCES [dbo].[tblOrderHeader] ([Id])
GO
ALTER TABLE [dbo].[tblOrderDetailPackage] CHECK CONSTRAINT [FK_tblOrderDetailPackage_tblOrderHeader]
GO
ALTER TABLE [dbo].[tblOrderDetailPackage]  WITH CHECK ADD  CONSTRAINT [FK_tblOrderDetailPackage_tblPackage] FOREIGN KEY([packageId])
REFERENCES [dbo].[tblPackage] ([packageID])
GO
ALTER TABLE [dbo].[tblOrderDetailPackage] CHECK CONSTRAINT [FK_tblOrderDetailPackage_tblPackage]
GO
ALTER TABLE [dbo].[tblOrderHeader]  WITH CHECK ADD  CONSTRAINT [FK_tblOrderHeader_AspNetUsers] FOREIGN KEY([applicationUserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[tblOrderHeader] CHECK CONSTRAINT [FK_tblOrderHeader_AspNetUsers]
GO
ALTER TABLE [dbo].[tblPackage]  WITH CHECK ADD  CONSTRAINT [FK_tblPackage_AspNetUsers] FOREIGN KEY([adminID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[tblPackage] CHECK CONSTRAINT [FK_tblPackage_AspNetUsers]
GO
ALTER TABLE [dbo].[tblReportArtist]  WITH CHECK ADD  CONSTRAINT [FK_tblReportArtist_AspNetUsers] FOREIGN KEY([artistID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[tblReportArtist] CHECK CONSTRAINT [FK_tblReportArtist_AspNetUsers]
GO
ALTER TABLE [dbo].[tblReportArtist]  WITH CHECK ADD  CONSTRAINT [FK_tblReportArtist_AspNetUsers1] FOREIGN KEY([reporterUserID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[tblReportArtist] CHECK CONSTRAINT [FK_tblReportArtist_AspNetUsers1]
GO
ALTER TABLE [dbo].[tblReportArtwork]  WITH CHECK ADD  CONSTRAINT [FK_tblReportArtwork_tblArtwork] FOREIGN KEY([artworkID])
REFERENCES [dbo].[tblArtwork] ([artworkID])
GO
ALTER TABLE [dbo].[tblReportArtwork] CHECK CONSTRAINT [FK_tblReportArtwork_tblArtwork]
GO
ALTER TABLE [dbo].[tblReportBlog]  WITH CHECK ADD  CONSTRAINT [FK_tblReportBlog_tblBlog] FOREIGN KEY([blogID])
REFERENCES [dbo].[tblBlog] ([blogID])
GO
ALTER TABLE [dbo].[tblReportBlog] CHECK CONSTRAINT [FK_tblReportBlog_tblBlog]
GO
ALTER TABLE [dbo].[tblShoppingCart]  WITH CHECK ADD  CONSTRAINT [FK_tblShoppingCart_AspNetUsers] FOREIGN KEY([buyerID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[tblShoppingCart] CHECK CONSTRAINT [FK_tblShoppingCart_AspNetUsers]
GO
ALTER TABLE [dbo].[tblShoppingCart]  WITH CHECK ADD  CONSTRAINT [FK_tblShoppingCart_tblArtwork] FOREIGN KEY([artworkID])
REFERENCES [dbo].[tblArtwork] ([artworkID])
GO
ALTER TABLE [dbo].[tblShoppingCart] CHECK CONSTRAINT [FK_tblShoppingCart_tblArtwork]
GO
