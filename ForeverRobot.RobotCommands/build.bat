@echo off
for /f %%i in ("%0") do set curpath=%%~dpi
cd /d %curpath% 

library\phantom\phantom.exe -f:scripts\%1.boo