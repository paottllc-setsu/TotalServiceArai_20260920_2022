@echo off
::mkdir "C:\template\backup"

if not exist "%1" (
    mkdir "%1"
    echo make backup!
) else (
    echo backup is exist!!
)

::set PASS=
mysqldump -u root --databases total_service_arai > "%1"\backup.sql
echo MySQL backup complete!
::pause
exit
