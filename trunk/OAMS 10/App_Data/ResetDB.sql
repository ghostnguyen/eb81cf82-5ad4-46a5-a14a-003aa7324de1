truncate table aspnet_Profile 

declare @userID uniqueidentifier
set @userID = (select top 1 UserId from aspnet_Users where UserName = 'bypasslogin@gmail.com')

delete aspnet_UsersInRoles where UserId <> @userID
-- 5E3571C2-C391-4FBA-9653-977AD68BB6D4	F8285997-7EB1-423F-AB1F-546E97B63844
-- 5E3571C2-C391-4FBA-9653-977AD68BB6D4	C3F3ED51-F663-473C-B279-F1EF1C68A8FC

delete aspnet_Membership where UserId <> @userID
delete aspnet_Users where UserId <> @userID


--OAMS	oams	D0B3E743-1474-47BF-8B1C-D3CDBEADDEBF	NULL
--D0B3E743-1474-47BF-8B1C-D3CDBEADDEBF	5E3571C2-C391-4FBA-9653-977AD68BB6D4	bypasslogin@gmail.com	bypasslogin@gmail.com	NULL	0	2010-10-03 14:07:06.000


delete Campaign

truncate table SitePhoto
truncate table SiteDetail

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
