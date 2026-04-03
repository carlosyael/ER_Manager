@echo off
setlocal enabledelayedexpansion

REM Cambia la extensión de todos los archivos .sl2 y .sl2.bak a .co2 y .co2.bak respectivamente en el directorio especificado

for %%f in (*.sl2.bak) do (
    set "filename=%%~nf"
    ren "%%f" "!filename:.sl2=.co2!.bak"
)
for %%f in (*.sl2) do (
    set "filename=%%~nf"
    ren "%%f" "!filename!.co2"
)

start "" "C:\Users\carlo\AppData\Roaming\EldenRing\76561199009049659\launch_elden_ring_seamlesscoop.exe.lnk"