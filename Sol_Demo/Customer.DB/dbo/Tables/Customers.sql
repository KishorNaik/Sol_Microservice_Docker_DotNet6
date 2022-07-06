﻿CREATE TABLE [dbo].[Customers] (
    [Id] NUMERIC (18)  IDENTITY (1, 1) NOT NULL,
    [CustomerID]   UNIQUEIDENTIFIER NULL,
    [FullName] VARCHAR(50) NULL, 
    CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED ([Id] ASC)
);

