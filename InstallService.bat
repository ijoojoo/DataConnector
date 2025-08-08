@echo off
:: ============================================================================
:: DataConnector 服务管理脚本
:: 功能: 自动化安装、卸载、启动和停止 DataConnector Windows 服务。
:: 使用: 请将此脚本与 DataConnector.Service.exe 放在同一目录下，
::       然后右键点击此文件，选择 "以管理员身份运行"。
:: ============================================================================

:: --- (1) 用户配置 ---
:: 默认情况下，此脚本会查找与自身位于同一目录下的 DataConnector.Service.exe。
:: 如果您的 .exe 文件在其他位置，请修改下面的 SERVICE_PATH 变量。
set "SERVICE_PATH=%~dp0DataConnector.Service.exe"

:: --- (2) 系统变量 ---
:: 自动查找 .NET Framework 4.x 的 InstallUtil.exe 路径
set "NET_FRAMEWORK_PATH=%SystemRoot%\Microsoft.NET\Framework\v4.0.30319"
set "INSTALLUTIL_PATH=%NET_FRAMEWORK_PATH%\InstallUtil.exe"
set "SERVICE_NAME=DataConnectorService"


:: --- (3) 权限检查 ---
net session >nul 2>&1
if %errorLevel% NEQ 0 (
    echo.
    echo [错误] 此脚本需要管理员权限才能运行。
    echo.
    echo 请右键点击此文件，然后选择 "以管理员身份运行"。
    echo.
    pause
    exit
)


:: --- (4) 主菜单 ---
:menu
cls
echo ===================================================
echo.
echo    DataConnector 服务管理工具
echo.
echo ===================================================
echo.
echo   当前服务路径: %SERVICE_PATH%
echo.
echo ---------------------------------------------------
echo.
echo   [1] 安装服务
echo   [2] 卸载服务
echo.
echo   [3] 启动服务
echo   [4] 停止服务
echo.
echo   [5] 退出
echo.
echo ---------------------------------------------------

choice /C 12345 /N /M "请输入您的选择 [1,2,3,4,5]: "

if errorlevel 5 goto :eof
if errorlevel 4 goto stop_service
if errorlevel 3 goto start_service
if errorlevel 2 goto uninstall_service
if errorlevel 1 goto install_service


:: --- (5) 功能实现 ---

:install_service
cls
echo 正在安装服务: %SERVICE_NAME% ...
echo.
"%INSTALLUTIL_PATH%" "%SERVICE_PATH%"
echo.
echo 安装命令已执行。
pause
goto menu

:uninstall_service
cls
echo 正在卸载服务: %SERVICE_NAME% ...
echo (如果服务正在运行，会先尝试停止它)
echo.
net stop %SERVICE_NAME% >nul 2>&1
"%INSTALLUTIL_PATH%" /u "%SERVICE_PATH%"
echo.
echo 卸载命令已执行。
pause
goto menu

:start_service
cls
echo 正在启动服务: %SERVICE_NAME% ...
echo.
net start %SERVICE_NAME%
echo.
echo 启动命令已执行。
pause
goto menu

:stop_service
cls
echo 正在停止服务: %SERVICE_NAME% ...
echo.
net stop %SERVICE_NAME%
echo.
echo 停止命令已执行。
pause
goto menu
