truncate table aspnet_Profile 

declare @userID uniqueidentifier
set @userID = (select top 1 UserId from aspnet_Users where UserName = 'bypasslogin@gmail.com')

delete aspnet_UsersInRoles where UserId <> @userID
delete aspnet_Membership where UserId <> @userID
delete aspnet_Users where UserId <> @userID

delete Campaign

truncate table SiteDetailMore
truncate table SiteDetailPhoto
truncate table SitePhoto

delete SiteDetail
DBCC CHECKIDENT (SiteDetail, RESEED, 0)

truncate table ContractDetailTimeline
truncate table ContractTimeline

truncate table SiteMonitoringPhoto
truncate table SiteMonitoring

delete ContractDetail
DBCC CHECKIDENT (ContractDetail, RESEED, 0)

delete [Contract]
DBCC CHECKIDENT ([Contract], RESEED, 0)

delete QuoteDetail
DBCC CHECKIDENT (QuoteDetail, RESEED, 0)

delete [Quote]
DBCC CHECKIDENT ([Quote], RESEED, 0)

delete [Site]
DBCC CHECKIDENT ([Site], RESEED, 0)

truncate table ContractorContactDetail

delete ContractorContact
DBCC CHECKIDENT (ContractorContact, RESEED, 0)

delete Contractor
DBCC CHECKIDENT (Contractor, RESEED, 0)

delete Product
DBCC CHECKIDENT (Product, RESEED, 0)


truncate table ClientContactDetail

delete ClientContact
DBCC CHECKIDENT (ClientContact, RESEED, 0)

delete Client
DBCC CHECKIDENT (Client, RESEED, 0)

delete Geo


select * from Geo


declare @geoID uniqueidentifier
declare @cityName nvarchar(Max)
set @cityName = 'Kuala Lumpur'
insert into Geo (Name,[Level],Fullname,FullNameNoDiacritics) values (@cityName,1,@cityName,@cityName)

set @geoID = (select Id from Geo where name = @cityName)

-- select * from AppSetting
update AppSetting set Value = @geoID where [key] = 'DefaultGeoID'
update AppSetting set Value = 3.15753 where [key] = 'FindMapCenterLat'
update AppSetting set Value = 101.71143 where [key] = 'FindMapCenterLng'
update AppSetting set Value = 1 where [key] = 'MapBoundSWLat'
update AppSetting set Value = 97 where [key] = 'MapBoundSWLng'
update AppSetting set Value = 11 where [key] = 'MapBoundNELat'
update AppSetting set Value = 105 where [key] = 'MapBoundNELng'
update AppSetting set Value = 'photo.oams.my' where [key] = 'GoogleUsername'
update AppSetting set Value = 'Oams123!@#' where [key] = 'GooglePassword'
update AppSetting set Value = 100 where [key] = 'ValidRange'
update AppSetting set Value = '' where [key] = 'AlbumAtomUrl'
update AppSetting set Value = 'false' where [key] = 'IsPOSTAR'
update AppSetting set Value = '' where [key] = 'Realm'
update AppSetting set Value = '' where [key] = 'Logo'
