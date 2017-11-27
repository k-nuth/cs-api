Write-Host "Incrementing version..."
# Get the current patch version
[regex]$rx='<Version>([0-9]+)\.([0-9]+)\.([0-9]+)</Version>'
$patch = $rx.Match([IO.File]::ReadAllText("bitprim\bitprim.csproj")).captures.groups[3].value
# Add 1 to it
$nextVersion = [int]$patch + 1;
# Replace the new value in the project metadata
(Get-Content -path "bitprim\bitprim.csproj") -replace '<Version>([0-9]+)\.([0-9]+)\.([0-9]+)</Version>', '<Version>$1.$2.%%PATCH_PLACEHOLDER%%</Version>' |  Set-Content "bitprim\bitprim.csproj"
(Get-Content -path "bitprim\bitprim.csproj") -replace '%%PATCH_PLACEHOLDER%%', "$nextVersion" |  Set-Content "bitprim\bitprim.csproj"
Write-Host "Patch version updated to $nextVersion"