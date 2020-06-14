/* 
	- For Stored Procedure
	- File Name Format: "Stored Procedure-<stored procedure name>", eg."Stored Procedure-usp_XXX"
	- Do not remove any history records.
	- Change history will be inside the sp.
*/

IF object_id('usp_Create_Customer') IS NOT NULL DROP PROCEDURE [dbo].[usp_Create_Customer]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_Create_Customer]
(
	 @employeeCount					        NVARCHAR(255)
    ,@companyName 				            NVARCHAR(255)
    ,@location						        NVARCHAR(255)
    ,@firstName 				            NVARCHAR(255)
    ,@lastName 					            NVARCHAR(255)
    ,@email 					            NVARCHAR(255)
    ,@address 						        NTEXT
    ,@contactNo 		  		            NVARCHAR(255)
    ,@customerId 				            NVARCHAR(255)
    ,@companyLogo  				            NVARCHAR(255)
	,@newCustomerUniqueId			        UNIQUEIDENTIFIER OUTPUT
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SET @newCustomerUniqueId = NEWID()
INSERT INTO [dbo].[QF_Customers]
           ([ID]
           ,[Employee count]
           ,[Company name]
           ,[Location]
           ,[First name]
           ,[Last name]
           ,[Email]
           ,[Address]
           ,[Contact number]
           ,[Date created]
           ,[Customer ID]
           ,[Company logo])
     VALUES
			( @newCustomerUniqueId	     -- (<ID, uniqueidentifier,>
			, @employeeCount		     -- ,<Employee count, nvarchar(255),>
			, @companyName 			     -- ,<Company name, nvarchar(255),>
			, @location				     -- ,<Location, nvarchar(255),>
			, @firstName 			     -- ,<First name, nvarchar(255),>
			, @lastName 			     -- ,<Last name, nvarchar(255),>
			, @email 				     -- ,<Email, nvarchar(255),>
			, @address				     -- ,<Address, ntext,>
			, @contactNo 			     -- ,<Contact number, nvarchar(255),>
			, GETDATE()				     -- ,<Date created, datetime,>
			, @customerId 			     -- ,<Customer ID, nvarchar(255),>				
			, @companyLogo)			     -- ,<Company logo, nvarchar(255),>)  					
																				      
	SELECT @newCustomerUniqueId

END
/*
EXEC usp_Temp_GetTest
*/
GO
