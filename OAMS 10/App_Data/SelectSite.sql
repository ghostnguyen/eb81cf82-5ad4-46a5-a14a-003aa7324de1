-- select * from [Site]
select ID, 
(select top 1 Name from Geo where ID = s.Geo1ID) as Geo1
,(select top 1 Name from Geo where ID = s.Geo2ID) as Geo2
,(select top 1 Name from Geo where ID = s.Geo3ID) as Geo3
,AddressLine1
,AddressLine2
,[Type]
--,Format
,(select top 1 Format from SiteDetail where SiteID = s.ID) as Format

,(select Name from Product where ID = (select top 1 ProductID from SiteDetail where SiteID = s.ID)) as Product
,(select Name from Client where ID = (select top 1 ClientID from Product where ID = (select top 1 ProductID from SiteDetail where SiteID = s.ID))) as Client

,Height
,Width
,Size
,Lng
,Lat
,CBDViewed
,(case (SurroundingAreaLight) when 1 then 'yes' else '' end) as SurroundingAreaLight
,(case (StreetLight) when 1 then 'yes' else '' end) as StreetLight
,FrontlitNumerOfLamps
,FrontlitTopBottom
,(case (FrontlitSideLighting) when 1 then 'yes' else '' end) as FrontlitSideLighting
,FontlitArmsPlacement
,FontlitIlluminationDistribution
,(case (FontLightArmsStraight) when 1 then 'yes' else '' end) as FontLightArmsStraight
,BacklitFormat
,BacklitIlluninationSpread
,BacklitLightingBlocks
,BacklitLightBoxLeakage
,BacklitVisualLegibility
,(select top 1 Note from CodeMaster where [Type] = 'RoadType1' and Code = cast(s.RoadType1 as nvarchar(MAX))) as RoadType_1
,(select top 1 Note from CodeMaster where [Type] = 'RoadType2' and Code = cast(s.RoadType2 as nvarchar(MAX))) as RoadType_2
,RoadTypePoint
,(select top 1 Note from CodeMaster where [Type] = 'InstallationPosition1' and Code = cast(s.InstallationPosition1 as nvarchar(MAX))) as InstallationPosition_1
,(select top 1 Note from CodeMaster where [Type] = 'InstallationPosition2' and Code = cast(s.InstallationPosition2 as nvarchar(MAX))) as InstallationPosition_2
,InstallationPositionPoint
,(select top 1 Note from CodeMaster where [Type] = 'ViewingDistance' and Code = cast(s.ViewingDistance as nvarchar(MAX))) as ViewingDistance
,(select top 1 Note from CodeMaster where [Type] = 'ViewingSpeed' and Code = cast(s.ViewingSpeed as nvarchar(MAX))) as ViewingSpeed
,(select top 1 Note from CodeMaster where [Type] = 'High' and Code = cast(s.High as nvarchar(MAX))) as High

,(select top 1 Note from CodeMaster where [Type] = 'VisibilityBuilding' and Code = cast(s.VisibilityBuilding as nvarchar(MAX))) as VisibilityBuilding
,(select top 1 Note from CodeMaster where [Type] = 'VisibilityTrees' and Code = cast(s.VisibilityTrees as nvarchar(MAX))) as VisibilityTrees

,(select top 1 Note from CodeMaster where [Type] = 'VisibilityBridgeWalkway' and Code = cast(s.VisibilityBridgeWalkway as nvarchar(MAX))) as VisibilityBridgeWalkway
,(select top 1 Note from CodeMaster where [Type] = 'VisibilityElectricityPolesOther' and Code = cast(s.VisibilityElectricityPolesOther as nvarchar(MAX))) as VisibilityElectricityPolesOther
,(select top 1 Note from CodeMaster where [Type] = 'DirectionalTrafficPublicTransport' and Code = cast(s.DirectionalTrafficPublicTransport as nvarchar(MAX))) as DirectionalTrafficPublicTransport

,(select top 1 Note from CodeMaster where [Type] = 'ShopSignsBillboards' and Code = cast(s.ShopSignsBillboards as nvarchar(MAX))) as ShopSignsBillboards
,(select top 1 Note from CodeMaster where [Type] = 'FlagsTemporaryBannersPromotionalItems' and Code = cast(s.FlagsTemporaryBannersPromotionalItems as nvarchar(MAX))) as FlagsTemporaryBannersPromotionalItems
,(select top 1 Note from CodeMaster where [Type] = 'CompetitiveProductSigns' and Code = cast(s.CompetitiveProductSigns as nvarchar(MAX))) as CompetitiveProductSigns
,(select top 1 Note from CodeMaster where [Type] = 'VisibilityHight' and Code = cast(s.VisibilityHight as nvarchar(MAX))) as VisibilityHight

,(case (CloseToUniversity) when 1 then 'yes' else '' end) as CloseToUniversity
,(case (CloseToHopistal) when 1 then 'yes' else '' end) as CloseToHopistal
,(case (CloseToAirport) when 1 then 'yes' else '' end) as CloseToAirport
,(case (CloseToFactory) when 1 then 'yes' else '' end) as CloseToFactory
,(case (CloseToGasStation) when 1 then 'yes' else '' end) as CloseToGasStation
,(case (CloseToMarket) when 1 then 'yes' else '' end) as CloseToMarket
,(case (CloseToOffice) when 1 then 'yes' else '' end) as CloseToOffice
,(case (CloseToParking) when 1 then 'yes' else '' end) as CloseToParking
,(case (CloseToResident) when 1 then 'yes' else '' end) as CloseToResident
,(case (CloseToSchool) when 1 then 'yes' else '' end) as CloseToSchool
,(case (CloseToShopping) when 1 then 'yes' else '' end) as CloseToShopping
,(case (CloseToStadium) when 1 then 'yes' else '' end) as CloseToStadium
,(case (CloseToStation) when 1 then 'yes' else '' end) as CloseToStation
,(case (CloseToTownCenter) when 1 then 'yes' else '' end) as CloseToTownCenter
,Score
,(select top 1 Name from Contractor where ID = s.ContractorID) as ContractorID


,CreatedDate
,LastUpdatedDate

from [Site] as s
order by ID

--SELECT table_name=sysobjects.name,
--         column_name=syscolumns.name,
--         datatype=systypes.name,
--         length=syscolumns.length
--    FROM sysobjects 
--    JOIN syscolumns ON sysobjects.id = syscolumns.id
--    JOIN systypes ON syscolumns.xtype=systypes.xtype
--   WHERE sysobjects.xtype='U' and sysobjects.name = 'Site'
--ORDER BY sysobjects.name,syscolumns.colid
