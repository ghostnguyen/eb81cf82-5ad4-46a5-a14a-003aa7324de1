
DECLARE ContractDetail_Cursor CURSOR FOR
SELECT ID, CategoryID1,CategoryID2,CategoryID3, Product
FROM ContractDetail
where  ProductID is null 
 --where  ProductID is null and CategoryID1 is not null
-- where (Product = 'blank') and ProductID is null
-- where ProductID is null and Product is not null
-- where (Product = 'blank') or Product is null
-- where (Product is not null and Product <> 'blank')
-- group by ClientID, CategoryID1,CategoryID2,CategoryID3, Product


OPEN ContractDetail_Cursor;
declare @ID int, 
@CatID1 uniqueidentifier, 
@CatID2 uniqueidentifier, 
@CatID3 uniqueidentifier,
@Product nvarchar(max)


FETCH NEXT FROM ContractDetail_Cursor INTO @ID,@CatID1,@CatID2,@CatID3,@Product;

WHILE @@FETCH_STATUS = 0
   BEGIN
		declare @ProductID int
		
		set @ProductID = (select top 1 ID from Product where 
			Name = @Product 
			and ((@CatID1 is null and CategoryID1 is null) or CategoryID1 = @CatID1)
			and ((@CatID2 is null and CategoryID2 is null) or CategoryID2 = @CatID2)
			and ((@CatID3 is null and CategoryID3 is null) or CategoryID3 = @CatID3)
			)
			
		if (@ProductID is null or @ProductID <= 0)
		begin
			insert into Product (Name,CategoryID1,CategoryID2,CategoryID3) 
				values (@Product,@CatID1,@CatID2,@CatID3)
			
			set @ProductID = @@IDENTITY		
		end
			
		if (@ProductID > 0)
		begin
			update ContractDetail set ProductID = @ProductID where ID = @ID
		end
		
FETCH NEXT FROM ContractDetail_Cursor INTO @ID,@CatID1,@CatID2,@CatID3,@Product;
  
   END;
CLOSE ContractDetail_Cursor;
DEALLOCATE ContractDetail_Cursor;
GO










---- update SiteDetail set Product = 'blank' where ID = 1231
---- select * from SiteDetail where ProductID is null and Product is not null

---- select * from SiteDetail where (Product is not null and Product <> 'blank') and ClientID is not null
---- select * from Product where Name = 'blank'

--DECLARE SiteDetail_Cursor CURSOR FOR
--SELECT ID, ClientID, CategoryID1,CategoryID2,CategoryID3, Product
--FROM SiteDetail
--where (Product = 'blank') and ProductID is null
---- where ProductID is null and Product is not null
---- where (Product = 'blank') and ClientID is not null
---- where (Product is not null and Product <> 'blank') and ClientID is not null
---- group by ClientID, CategoryID1,CategoryID2,CategoryID3, Product


--OPEN SiteDetail_Cursor;
--declare @ID int, 
--@ClientID int, 
--@CatID1 uniqueidentifier, 
--@CatID2 uniqueidentifier, 
--@CatID3 uniqueidentifier,
--@Product nvarchar(max)


--FETCH NEXT FROM SiteDetail_Cursor INTO @ID,@ClientID,@CatID1,@CatID2,@CatID3,@Product;

--WHILE @@FETCH_STATUS = 0
--   BEGIN
--		declare @ProductID int
		
--		set @ProductID = (select top 1 ID from Product where 
--			Name = @Product 
--			and ((@CatID1 is null and CategoryID1 is null) or CategoryID1 = @CatID1)
--			and ((@CatID2 is null and CategoryID2 is null) or CategoryID2 = @CatID2)
--			and ((@CatID3 is null and CategoryID3 is null) or CategoryID3 = @CatID3)
--			)
			
--		if (@ProductID is null or @ProductID <= 0)
--		begin
--			insert into Product (Name,ClientID,CategoryID1,CategoryID2,CategoryID3) 
--				values (@Product,@ClientID,@CatID1,@CatID2,@CatID3)
			
--			set @ProductID = @@IDENTITY		
--		end
			
--		if (@ProductID > 0)
--		begin
--			update SiteDetail set ProductID = @ProductID where ID = @ID
--		end
		
--FETCH NEXT FROM SiteDetail_Cursor INTO @ID,@ClientID,@CatID1,@CatID2,@CatID3,@Product;
  
--   END;
--CLOSE SiteDetail_Cursor;
--DEALLOCATE SiteDetail_Cursor;
--GO
















--DECLARE Site_Cursor CURSOR FOR
--SELECT ID, CurrentClientID, CategoryID1,CategoryID2,CategoryID3,CurrentProduct,Format
--FROM Site


--OPEN Site_Cursor;
--declare @ID int, @ClientID int, 
--@CatID1 uniqueidentifier, 
--@CatID2 uniqueidentifier, 
--@CatID3 uniqueidentifier,
--@Product nvarchar(max),
--@Format  nvarchar(max)


--FETCH NEXT FROM Site_Cursor INTO @ID,@ClientID,@CatID1,@CatID2,@CatID3,@Product,@Format;

--WHILE @@FETCH_STATUS = 0
--   BEGIN
--		 insert into SiteDetail (SiteID,ClientID,Name,CategoryID1,CategoryID2
--		,CategoryID3,Product,Format)
--		 values(@ID,@ClientID,'Main Site',@CatID1,@CatID2,@CatID3,@Product,@Format)
		
		
--FETCH NEXT FROM Site_Cursor INTO @ID,@ClientID,@CatID1,@CatID2,@CatID3,@Product,@Format;
  
--   END;
--CLOSE Site_Cursor;
--DEALLOCATE Site_Cursor;
--GO

---- select * from Site
---- select * from SiteDetail
---- select distinct(Product) from SiteDetail
---- delete SiteDetail


--DECLARE ContractDetail_Cursor CURSOR FOR
--SELECT ID, SiteID
--FROM ContractDetail


--OPEN ContractDetail_Cursor;
--declare @ID int, @SiteID int,
--@CatID1 uniqueidentifier, 
--@CatID2 uniqueidentifier, 
--@CatID3 uniqueidentifier,
--@Product nvarchar(max),
--@Format  nvarchar(max)



--FETCH NEXT FROM ContractDetail_Cursor INTO @ID,@SiteID;

--WHILE @@FETCH_STATUS = 0
--   BEGIN

--	set @CatID1 = (select CategoryID1 from Site where ID = @SiteID)
--	set @CatID2 = (select CategoryID2 from Site where ID = @SiteID)
--	set @CatID3 = (select CategoryID3 from Site where ID = @SiteID)
--	set @Product = (select CurrentProduct from Site where ID = @SiteID)
--	set @Format = (select Format from Site where ID = @SiteID)
--	--set @Name = (select Format from Site where ID = @SiteID)
	
	
--	update ContractDetail 
--		set SiteDetailName= 'Main Site'
--		,Product = @Product
--		,CategoryID1 = @CatID1
--		,CategoryID2 =@CatID2
--		,CategoryID3 = @CatID3
--		,Format = @Format
--		where ID = @ID
		
		
		
--FETCH NEXT FROM ContractDetail_Cursor INTO @ID,@SiteID;

  
--   END;
--CLOSE ContractDetail_Cursor;
--DEALLOCATE ContractDetail_Cursor;
--GO

---- select * from ContractDetail
---- SiteDetail, ContractDetail,SiteMonitoringPhoto






