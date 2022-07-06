CREATE TABLE [dbo].[Address]
(
	[Id] NUMERIC NOT NULL PRIMARY KEY, 
    [AddressID] UNIQUEIDENTIFIER NULL, 
    [FlatNo] VARCHAR(50) NULL, 
    [Apartment] VARCHAR(50) NULL, 
    [Street] VARCHAR(50) NULL, 
    [LandMark] VARCHAR(50) NULL, 
    [City] VARCHAR(50) NULL, 
    [State] VARCHAR(50) NULL, 
    [Pincode] VARCHAR(50) NULL, 
    [IsDeliveryAddress] BIT NULL, 
    [CustomerID] UNIQUEIDENTIFIER NULL
)
