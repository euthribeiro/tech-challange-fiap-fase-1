param()

$UnitTestsScript = Join-Path $PSScriptRoot 'run-unit-tests.ps1'
$IntegrationTestsScript = Join-Path $PSScriptRoot 'run-integration-tests.ps1'

Write-Host "Running all tests (unit then integration) in Docker..."

Write-Host "-> Unit tests"
& pwsh -NoProfile -File $UnitTestsScript
if ($LASTEXITCODE -ne 0) { Write-Error "Unit tests failed, aborting."; exit $LASTEXITCODE }

Write-Host "-> Integration tests"
& pwsh -NoProfile -File $IntegrationTestsScript
if ($LASTEXITCODE -ne 0) { Write-Error "Integration tests failed."; exit $LASTEXITCODE }

Write-Host "All tests passed."
