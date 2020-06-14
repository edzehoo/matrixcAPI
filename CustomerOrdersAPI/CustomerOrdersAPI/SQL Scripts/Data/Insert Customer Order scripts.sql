--USE [BLYNKCOREAPP]
--GO

--INSERT INTO [dbo].[SF_Orders Table]
--           ([ID]
--           ,[ParentID]
--           ,[Order ID]
--           ,[Date of order]
--           ,[BYO Domain]
--           ,[Domain name]
--           ,[GSuite Package]
--           ,[Number of users]
--           ,[Admin e-mail]
--           ,[Admin first name]
--           ,[Admin last name]
--           ,[Billing Company name]
--           ,[Billing company reg no]
--           ,[Billing Company Address]
--           ,[Billing Contact Person]
--           ,[Billing e-mail address]
--           ,[Status]
--           ,[Payment status])
--     VALUES
--           (<ID, uniqueidentifier,>
--           ,<ParentID, uniqueidentifier,>
--           ,<Order ID, nvarchar(255),>
--           ,<Date of order, datetime,>
--           ,<BYO Domain, bit,>
--           ,<Domain name, nvarchar(255),>
--           ,<GSuite Package, nvarchar(255),>
--           ,<Number of users, int,>
--           ,<Admin e-mail, nvarchar(255),>
--           ,<Admin first name, nvarchar(255),>
--           ,<Admin last name, nvarchar(255),>
--           ,<Billing Company name, nvarchar(255),>
--           ,<Billing company reg no, nvarchar(255),>
--           ,<Billing Company Address, ntext,>
--           ,<Billing Contact Person, nvarchar(255),>
--           ,<Billing e-mail address, nvarchar(255),>
--           ,<Status, nvarchar(255),>
--           ,<Payment status, nvarchar(255),>)
--GO

USE [BLYNKCOREAPP]
GO

DECLARE @newID UNIQUEIDENTIFIER = NEWID()

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
           (@newID
           ,'5C04581B-A2EE-44EF-8D0B-302D9B1F214D'
           ,'417812125'
           ,'2020-06-02 16:17PM'
		   ,1
           ,'www.domainname.com'
           ,'g suite package A'
           ,5
           ,'admin@themail.com'
           ,'admin First'
           ,'admint Last'
           ,'The Company A'
           ,'AB-392894-X'
           ,'No.A-3-4,Tingkat 3,Bangunan Asap-Asap,Jalan Berasap 59200 Kuala Lumpur'
           ,'Mr. Bill'
           ,'bill-aire@noemail.com'
           ,'ACTIVE'
           ,'UNPAID')
GO


--d":"5C04581B-A2EE-44EF-8D0B-302D9B1F214D",
--":"417812125",
--rder":"2020-06-02 16:17PM",
--in":true,
--ame":"www.domainname.com",
--ackage":"g suite package A",
--ers":5,
--ail":"admin@themail.com",
--rstName":"admin First",
--stName":"admint Last",
--panyName":"The Company A",
--panyRegNo":"AB-392894-X",
--panyAddress":"No.A-3-4,Tingkat 3,Bangunan Asap-Asap,Jalan Berasap 59200 Kuala Lumpur",
--tactPerson":"Mr. Bill",
--il":"bill-aire@noemail.com",
--:"ACTIVE",
--Status":"UNPAID"

