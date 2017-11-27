Write-Host "Incrementing version..."
(Get-Content -path "bitprim\bitprim.csproj") -replace '<Version>([0-9\.]*)</Version>', '<Version>$1-pre%%TIMESTAMP_PLACEHOLDER%%</Version>' |  Set-Content "bitprim\bitprim.csproj"
$timestamp = Get-Date -format yyyyMMddHHmmss
(Get-Content -path "bitprim\bitprim.csproj") -replace '%%TIMESTAMP_PLACEHOLDER%%', "$timestamp" |  Set-Content "bitprim\bitprim.csproj"