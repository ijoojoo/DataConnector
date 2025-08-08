@echo off
:: ============================================================================
:: DataConnector �������ű�
:: ����: �Զ�����װ��ж�ء�������ֹͣ DataConnector Windows ����
:: ʹ��: �뽫�˽ű��� DataConnector.Service.exe ����ͬһĿ¼�£�
::       Ȼ���Ҽ�������ļ���ѡ�� "�Թ���Ա�������"��
:: ============================================================================

:: --- (1) �û����� ---
:: Ĭ������£��˽ű������������λ��ͬһĿ¼�µ� DataConnector.Service.exe��
:: ������� .exe �ļ�������λ�ã����޸������ SERVICE_PATH ������
set "SERVICE_PATH=%~dp0DataConnector.Service.exe"

:: --- (2) ϵͳ���� ---
:: �Զ����� .NET Framework 4.x �� InstallUtil.exe ·��
set "NET_FRAMEWORK_PATH=%SystemRoot%\Microsoft.NET\Framework\v4.0.30319"
set "INSTALLUTIL_PATH=%NET_FRAMEWORK_PATH%\InstallUtil.exe"
set "SERVICE_NAME=DataConnectorService"


:: --- (3) Ȩ�޼�� ---
net session >nul 2>&1
if %errorLevel% NEQ 0 (
    echo.
    echo [����] �˽ű���Ҫ����ԱȨ�޲������С�
    echo.
    echo ���Ҽ�������ļ���Ȼ��ѡ�� "�Թ���Ա�������"��
    echo.
    pause
    exit
)


:: --- (4) ���˵� ---
:menu
cls
echo ===================================================
echo.
echo    DataConnector ���������
echo.
echo ===================================================
echo.
echo   ��ǰ����·��: %SERVICE_PATH%
echo.
echo ---------------------------------------------------
echo.
echo   [1] ��װ����
echo   [2] ж�ط���
echo.
echo   [3] ��������
echo   [4] ֹͣ����
echo.
echo   [5] �˳�
echo.
echo ---------------------------------------------------

choice /C 12345 /N /M "����������ѡ�� [1,2,3,4,5]: "

if errorlevel 5 goto :eof
if errorlevel 4 goto stop_service
if errorlevel 3 goto start_service
if errorlevel 2 goto uninstall_service
if errorlevel 1 goto install_service


:: --- (5) ����ʵ�� ---

:install_service
cls
echo ���ڰ�װ����: %SERVICE_NAME% ...
echo.
"%INSTALLUTIL_PATH%" "%SERVICE_PATH%"
echo.
echo ��װ������ִ�С�
pause
goto menu

:uninstall_service
cls
echo ����ж�ط���: %SERVICE_NAME% ...
echo (��������������У����ȳ���ֹͣ��)
echo.
net stop %SERVICE_NAME% >nul 2>&1
"%INSTALLUTIL_PATH%" /u "%SERVICE_PATH%"
echo.
echo ж��������ִ�С�
pause
goto menu

:start_service
cls
echo ������������: %SERVICE_NAME% ...
echo.
net start %SERVICE_NAME%
echo.
echo ����������ִ�С�
pause
goto menu

:stop_service
cls
echo ����ֹͣ����: %SERVICE_NAME% ...
echo.
net stop %SERVICE_NAME%
echo.
echo ֹͣ������ִ�С�
pause
goto menu
