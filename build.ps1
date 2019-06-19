dotnet publish -c Release -f netcoreapp2.2 -r win10-x64 --self-contained false --output ..\choco\SqlProcScaffold\tools SqlProcScaffold\SqlProcScaffold.csproj
Push-Location
try {
    Set-Location .\choco\SqlProcScaffold
    choco pack
} finally {
    Pop-Location
}