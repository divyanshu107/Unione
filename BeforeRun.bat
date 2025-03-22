@echo off
:: Change directory to the project folder
cd Unione

:: Get the current date and time in the format YYYYMMDD-HHMMSS
for /f "tokens=2 delims==" %%I in ('"wmic os get localdatetime /value"') do set datetime=%%I
set year=%datetime:~0,4%
set month=%datetime:~4,2%
set day=%datetime:~6,2%
set hour=%datetime:~8,2%
set minute=%datetime:~10,2%
set second=%datetime:~12,2%

:: Combine them into a unique migration name
set migrationName=Migration-%year%%month%%day%-%hour%%minute%%second%

:: Generate the migration with a unique name based on time
dotnet ef migrations add %migrationName%

:: Update the database
dotnet ef database update

:: Pause to see the output
pause
