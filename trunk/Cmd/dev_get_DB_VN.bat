set Deploy_Folder=D:\ghostnguyen\oams-deploy
set App_Data=D:\ghostnguyen\oams_guid\OAMS 10\App_Data

svn.exe update %Deploy_Folder%

7za e %Deploy_Folder%\UDB.zip -o"%App_Data%" -y


