@ECHO OFF

SET POWERSHELL=%SystemRoot%\system32\WindowsPowerShell\v1.0\powershell.exe

%POWERSHELL% -ExecutionPolicy RemoteSigned ".\build\build.ps1"

PAUSE
