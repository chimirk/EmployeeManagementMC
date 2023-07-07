USE [EmployeeManagement]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Address] FOREIGN KEY([AddressId])
REFERENCES [dbo].[Address] ([ID])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Address]
GO