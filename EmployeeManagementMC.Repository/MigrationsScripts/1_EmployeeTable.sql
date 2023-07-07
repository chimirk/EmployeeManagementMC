﻿USE [EmployeeManagement]
GO
IF NOT EXISTS(SELECT * FROM sys.tables WHERE name = 'Employee')
CREATE TABLE [dbo].[Employee](
	[Id] [UniqueIdentifier] DEFAULT NEWID() PRIMARY KEY,
	[Name] [nvarchar](50) NOT NULL,
	[Position] [nvarchar](50) NULL,
	[ManagerId] [uniqueidentifier] NULL,
	[AddressId] [uniqueidentifier] NULL
)
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Employee] FOREIGN KEY([ManagerId])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Employee]
GO