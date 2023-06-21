# UserManagementSystem
Follow this step first

1.Use SQL server and create required table , below are the query.

2.Update appsetting.json with your local server name and database name 

<code>	
Create Database GleasonWebAppDB
	
USE [GleasonWebAppDB]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 6/21/2023 11:41:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[IsTrailUser] [bit] NOT NULL,
	[Password] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [IsTrailUser]
GO

----------------------------------
USE [GleasonWebAppDB]
GO

/****** Object:  Table [dbo].[Roles]    Script Date: 6/21/2023 11:52:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Roles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

------------------------

USE [GleasonWebAppDB]
GO

/****** Object:  Table [dbo].[UserRoles]    Script Date: 6/21/2023 11:52:16 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserRoles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[RoleID] [int] NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Roles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([ID])
GO

ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Roles]
GO

------------------------------------

</code>

To Host Application using IIS
-----------------------------
1.Right click the project you want to deploy, and choose publish

<img width="338" alt="image" src="https://github.com/itsakramhere/UserManagementSystem/assets/45992595/726380d5-9780-4473-8823-010f85539b23">

2. Choose Folder => Next
   
<img width="414" alt="image" src="https://github.com/itsakramhere/UserManagementSystem/assets/45992595/9433486e-7537-4bab-b929-48df2fea0958">

4. The folder will include the deployed files, Click Finish
   
   <img width="565" alt="image" src="https://github.com/itsakramhere/UserManagementSystem/assets/45992595/bc6b5442-efc0-4824-b536-4f4ada486612">

Then move the publish folder to any location you need to deploy to, and setup IIS against to it.  This method is most likely used for production deployment.

6. Open IIS Manager and Right Click on Site and click on Add WebSite and provide valid details
   
   <img width="424" alt="image" src="https://github.com/itsakramhere/UserManagementSystem/assets/45992595/d0c2d20a-e86d-4391-a179-9306f199b623">

7.Then goto Application Pool and select created Pool and Right click on that and click on advanced setting and make Enable 32-Bit Application True

<img width="317" alt="image" src="https://github.com/itsakramhere/UserManagementSystem/assets/45992595/031d0e41-1cad-47b8-a097-d94aec33c221">

Now Run your Application.


