REM svn.exe update D:\oams-deploy\

SET DB_Folder=D:\oams.vn\App_Data
SET Save_Folder=D:\oams-deploy\DB_VN

SET TimeStamp=%date:~-4,4%%date:~-7,2%%date:~-10,2%_%time:~0,2%%time:~3,2%%time:~6,2%

iisreset -stop

REM copy %DB_Folder%\OAMS.mdf D:\oams-deploy\DB_VN\OAMS_%date:~-4,4%%date:~-7,2%%date:~-10,2%_%time:~0,2%%time:~3,2%%time:~6,2%.mdf
REM copy %DB_Folder%\OAMS_log.ldf D:\oams-deploy\DB_VN\OAMS_%date:~-4,4%%date:~-7,2%%date:~-10,2%_%time:~0,2%%time:~3,2%%time:~6,2%_log.ldf

7za a %Save_Folder%\OAMS_VN_%TimeStamp%.zip %DB_Folder%\OAMS*.*

iisreset

