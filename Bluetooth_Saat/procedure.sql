USE [E:\BERENN\BLUETOOTH_SAAT V.2 ENG\BLUETOOTH_SAAT\BLUETOOTH_SAAT\BLUETOOTH_SAAT\SAAT_DB.MDF];
GO
alter PROCEDURE clear_data
    @nem nvarchar(10), 
    @sicaklik nvarchar(10)
	
AS 

   select COUNT(*)  from deger 
   if ((select COUNT(*) from deger)= 55)
   truncate table deger
GO