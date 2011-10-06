

SET DB_Folder=D:\oams.vn\App_Data
SET Save_Folder=D:\oams-deploy

svn.exe update %Save_Folder%

iisreset -stop

7za a %Save_Folder%\UDB.zip %DB_Folder%\OAMS*.*

iisreset

svn.exe commit %Save_Folder% -m "UDB"
