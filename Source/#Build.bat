set framework=v4.0.30319

REM IG: I first tried to use this:
REM      /target:%1 /property:Configuration=%2
REM     but it didn't work with multiple targets like Binaries;Nuget

"%SystemDrive%\Windows\Microsoft.NET\Framework\%framework%\MSBuild.exe" SwissKnife.build %1 /property:Configuration=%2