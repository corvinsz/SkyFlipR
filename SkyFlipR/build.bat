﻿@echo off
setlocal enabledelayedexpansion

if "%~1"=="" (
    echo Version number is required.
    echo Usage: build.bat [version] [extra_args...]
    exit /b 1
)

set "version=%~1"

echo.
echo Compiling SkyFlipR with dotnet...
dotnet publish -c Release --no-self-contained -r win-x64 -o %~dp0publish

echo.
echo Building Velopack Release v%version%
vpk pack -u SkyFlipR -o %~dp0releases -p %~dp0publish -f net9-x64-desktop -v %*