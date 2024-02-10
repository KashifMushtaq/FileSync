@echo OFF

start msiexec /i "Synchronization-v2.1-x64-ENU.msi" /passive

REM start msiexec /i "ProviderServices-v2.1-x64-ENU.msi" /passive /L*V "install_ProviderServices.log"
start msiexec /i "ProviderServices-v2.1-x64-ENU.msi" /passive

