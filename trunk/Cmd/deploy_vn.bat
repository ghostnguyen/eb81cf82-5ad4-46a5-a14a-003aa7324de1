
SET Save_Folder=D:\oams-deploy
SET OAMS_Folder=D:\oams.vn

svn.exe update %Save_Folder%

7za e %Save_Folder%\oams-deploy.zip -o%OAMS_Folder% -y



