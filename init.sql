CREATE TABLE [dbo].[Company](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreatorId] [int] NOT NULL,
	[LastModifierId] [int] NULL,
	[LastModifyTime] [datetime] NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
																																						
																																						
INSERT INTO [dbo].[Company]
           ([Name]
           ,[CreateTime]
           ,[CreatorId]
           ,[LastModifierId]
           ,[LastModifyTime])
     VALUES
           ('�ٽ�'
           ,'2015-12-10'
           ,1
           ,1
           ,'2015-12-10')
																																						
																																						
INSERT INTO [dbo].[Company]
           ([Name]
           ,[CreateTime]
           ,[CreatorId]
           ,[LastModifierId]
           ,[LastModifyTime])
     VALUES
           ('��ݸ'
           ,'2015-12-10'
           ,1
           ,1
           ,'2015-12-10')
																																						
																																						
INSERT INTO [dbo].[Company]
           ([Name]
           ,[CreateTime]
           ,[CreatorId]
           ,[LastModifierId]
           ,[LastModifyTime])
     VALUES
           ('�����ղ�'
           ,'2018-12-10'
           ,1
           ,1
           ,'2018-12-10')
																																						

																																						
																																						
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Account] [varchar](100) NOT NULL,
	[Password] [varchar](100) NOT NULL,
	[Email] [varchar](200) NULL,
	[Mobile] [varchar](30) NULL,
	[CompanyId] [int] NULL,
	[CompanyName] [nvarchar](500) NULL,
	[State] [int] NOT NULL,
	[UserType] [int] NOT NULL,
	[LastLoginTime] [datetime] NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreatorId] [int] NOT NULL,
	[LastModifierId] [int] NULL,
	[LastModifyTime] [datetime] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
																																						
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�û�״̬  0���� 1���� 2ɾ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'State'
GO
																																						
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�û�����  1 ��ͨ�û� 2����Ա 4��������Ա' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'UserType'
GO
																					
																																						
INSERT INTO [dbo].[User]
           ([Name]
           ,[Account]
           ,[Password]
           ,[Email]
           ,[Mobile]
           ,[CompanyId]
           ,[CompanyName]
           ,[State]
           ,[UserType]
           ,[LastLoginTime]
           ,[CreateTime]
           ,[CreatorId]
           ,[LastModifierId]
           ,[LastModifyTime])
     VALUES
           ('С��'
           ,'admin'
           ,'e10adc3949ba59abbe56e057f20f883e'
           ,'12'
           ,'133'	
           ,'1'	
           ,'�ٽ�'	
           ,0
           ,2
           ,'2018-12-12'	
           ,'2018-12-12'
           ,1
           ,1
           ,'2018-12-12')
GO

