/* 
	- For Stored Procedure
	- File Name Format: "Stored Procedure-<stored procedure name>", eg."Stored Procedure-usp_XXX"
	- Do not remove any history records.
	- Change history will be inside the sp.
*/

IF object_id('usp_Create_CustomerOrder') IS NOT NULL DROP PROCEDURE [dbo].[usp_Create_CustomerOrder]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_Create_CustomerOrder]
(
	 @parentId                                   UNIQUEIDENTIFIER
	,@orderId                                    NVARCHAR(255)
	,@dateOrder                                  DATETIME
	,@byoDomain                                  BIT
	,@domainName                                 NVARCHAR(255)
	,@gSuitPackage                               NVARCHAR(255)
	,@noOfUsers                                  INT
	,@adminEmail                                 NVARCHAR(255)
	,@adminFirstname                             NVARCHAR(255)
	,@adminLastName                              NVARCHAR(255)
	,@billCompanyName                            NVARCHAR(255)
	,@billCompanyRegNo                           NVARCHAR(255)
	,@billCompanyAddress                         NVARCHAR(255)
	,@billContactPerson                          NTEXT
	,@billEmail                                  NVARCHAR(255)
	,@status                                     NVARCHAR(255)
	,@paymentStatus                              NVARCHAR(255)
	,@newOrderUniqueId							 UNIQUEIDENTIFIER OUTPUT
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SET @newOrderUniqueId = NEWID()
	INSERT INTO [dbo].[SF_Orders Table]
			   ([ID]
			   ,[ParentID]
			   ,[Order ID]
			   ,[Date of order]
			   ,[BYO Domain]
			   ,[Domain name]
			   ,[GSuite Package]
			   ,[Number of users]
			   ,[Admin e-mail]
			   ,[Admin first name]
			   ,[Admin last name]
			   ,[Billing Company name]
			   ,[Billing company reg no]
			   ,[Billing Company Address]
			   ,[Billing Contact Person]
			   ,[Billing e-mail address]
			   ,[Status]
			   ,[Payment status])
		 VALUES
			   ( @newOrderUniqueId
			    ,@parentId  
				,@orderId                   
				,@dateOrder                 
				,@byoDomain                 
				,@domainName                
				,@gSuitPackage              
				,@noOfUsers                 
				,@adminEmail                
				,@adminFirstname            
				,@adminLastName             
				,@billCompanyName           
				,@billCompanyRegNo          
				,@billCompanyAddress        
				,@billContactPerson         
				,@billEmail                 
				,@status                    
				,@paymentStatus )           

	SELECT @newOrderUniqueId

END
/*
EXEC usp_Temp_GetTest
*/
GO
