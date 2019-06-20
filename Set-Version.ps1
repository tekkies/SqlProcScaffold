param ( $newVersion )

((Get-Content .\SqlProcScaffold\SqlProcScaffold.csproj) `
    -replace '<Version>\d+.\d+.\d+</Version>', "<Version>$newVersion</Version>") `
    | Out-File .\SqlProcScaffold\SqlProcScaffold.csproj
((Get-Content .\choco\SqlProcScaffold\sqlprocscaffold.nuspec) `
    -replace '<Version>\d+.\d+.\d+</Version>', "<Version>$newVersion</Version>") `
    | Out-File .\choco\SqlProcScaffold\sqlprocscaffold.nuspec
