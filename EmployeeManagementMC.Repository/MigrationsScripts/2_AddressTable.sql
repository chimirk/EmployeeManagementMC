USE [EmployeeManagement]
GO
IF NOT EXISTS(SELECT * FROM sys.tables WHERE name = 'Address')
CREATE TABLE [dbo].Address(
	[ID] [UniqueIdentifier] DEFAULT NEWID() PRIMARY KEY,
	[StreetName] [nvarchar](150) NOT NULL,
	[StreetNumber] [nvarchar](10) NULL,
	[City] [nvarchar](50) NOT NULL,
	[Region] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NOT NULL,
	[PostalCode] [nvarchar](20) NULL
)
GO