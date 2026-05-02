param()

$ComposeFile = Join-Path $PSScriptRoot '..\docker-compose.ci.yml'

Write-Host "Running integration tests in Docker (using docker-compose.ci.yml)..."
docker-compose -f $ComposeFile up --abort-on-container-exit --force-recreate integration-tests
$exit = $LASTEXITCODE

Write-Host "Bringing down test services..."
docker-compose -f $ComposeFile down

if ($exit -ne 0) {
    Write-Error "Integration tests failed (exit code $exit)"
    exit $exit
}

Write-Host "Integration tests completed successfully."
