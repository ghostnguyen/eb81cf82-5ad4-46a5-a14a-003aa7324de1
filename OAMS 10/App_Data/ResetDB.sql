update Facility set CountingYY = substring(convert(nvarchar,getdate(),126),3,2), CountingNumber = 0

truncate table Log 


truncate table PackSideEffect


truncate table PackTransaction
truncate table PackRemainDaily

delete PackOrder 
DBCC CHECKIDENT (PackOrder, RESEED, 0)

truncate table StoreFinalize

delete Pack 

truncate table DonationStatusLog 
truncate table DonationTestLog 
delete Donation 

delete [Order] 
DBCC CHECKIDENT ([Order], RESEED, 0)

delete [Return] 
DBCC CHECKIDENT ([Return], RESEED, 0)

delete [People] 
DBCC CHECKIDENT ([People], RESEED, 0)

truncate table CampaignStatusHistory 

delete [Campaign]
DBCC CHECKIDENT ([Campaign], RESEED, 0)

delete [Delete]
DBCC CHECKIDENT ([Delete], RESEED, 0)

















--delete Org
--DBCC CHECKIDENT ([Org], RESEED, 0)


--declare @i int
--set @i = 0
--while @i < 30000
--begin

--insert into Pack([Status],TestResultStatus,DeliverStatus) values (0,0,0)

--set @i = @i + 1

--continue
--end

--update Excel set Imported = null 



-- delete People where ID in (select PeopleID from Pack where Autonum in (1020,1673,3017))
-- update Pack set PeopleID = null, [Status] = 0,CollectedDate = null,Volume = null,HospitalID = null,Note = null,ComponentID = null,Actor = null,CampaignID = null, TestResultStatus = 0, SubstanceID = null,DeliverStatus = 0,MSTM = null,MSNH = null where Autonum in (1020,1673,3017)
-- delete BloodType where PackID in (select ID from Pack where Autonum in (1020,1673,3017))
-- delete TestResult where PackID in (select ID from Pack where Autonum in (1020,1673,3017))
-- delete PackStatusHistory where PackID in (select ID from Pack where Autonum in (1020,1673,3017))
-- delete PackResultHistory where PackID in (select ID from Pack where Autonum in (1020,1673,3017))
-- update Excel set Imported = null where ID in (1020,1673,3017)