DECLARE Site_Cursor CURSOR FOR
SELECT ID, [Type],Height,Width
FROM [Site]

OPEN Site_Cursor;
declare @ID int, 
@Type nvarchar(MAX), 
@Height decimal(18,2), 
@Width decimal(18,2)


FETCH NEXT FROM Site_Cursor INTO @ID,@Type,@Height,@Width;

WHILE @@FETCH_STATUS = 0
   BEGIN
		begin
			update SiteDetail set [Type] = @Type,
									Height = @Height,
									Width = @Width
									 where SiteID = @ID
									 
			update ContractDetail set [Type] = @Type,
									Height = @Height,
									Width = @Width
									 where SiteID = @ID
									 
			update QuoteDetail set [Type] = @Type
									
									 where SiteID = @ID
		end
		FETCH NEXT FROM Site_Cursor INTO @ID,@Type,@Height,@Width;
   END;
CLOSE Site_Cursor;
DEALLOCATE Site_Cursor;
GO

-- select * from SiteDetail



DECLARE SiteDetail_Cursor CURSOR FOR
SELECT ID, ProductID,[Type],Height,Width
FROM SiteDetail

OPEN SiteDetail_Cursor;
declare @ID int, 
@ProductID int,
@Type nvarchar(MAX), 
@Height decimal(18,2), 
@Width decimal(18,2) 

FETCH NEXT FROM SiteDetail_Cursor INTO @ID,@ProductID,@Type,@Height,@Width;

WHILE @@FETCH_STATUS = 0
   BEGIN
		begin
			insert into SiteDetailMore (SiteDetailID,ProductID) 
				values (@ID,@ProductID)
				
			
		end
		
		FETCH NEXT FROM SiteDetail_Cursor INTO @ID,@ProductID,@Type,@Height,@Width;
  
   END;
CLOSE SiteDetail_Cursor;
DEALLOCATE SiteDetail_Cursor;
GO

--select count(1) from SiteDetail
--select count(1) from SiteDetailMore
--select * from SiteDetailMore


--Update ContractDetail Type,Height,Width
--Update QuoteDetail Type