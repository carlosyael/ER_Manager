@echo off
setlocal enabledelayedexpansion

REM Cambia la extensión de todos los archivos .co2 y .co2.bak a .sl2 y .sl2.bak respectivamente en el directorio especificado

for %%f in (*.co2.bak) do (
    set "filename=%%~nf"
    ren "%%f" "!filename:.co2=.sl2!.bak"
)
for %%f in (*.co2) do (
    set "filename=%%~nf"
    ren "%%f" "!filename!.sl2"
)
start "" "D:\SteamLibrary\steamapps\common\ELDEN RING\Game\eldenring.exe"