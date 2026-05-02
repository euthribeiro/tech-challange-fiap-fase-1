param()

$ComposeFile = Join-Path $PSScriptRoot '..\docker-compose.yml'

Write-Host "Ensuring previous compose stack is down (removing orphans/networks)..."
docker-compose -f $ComposeFile down --remove-orphans --volumes
if ($LASTEXITCODE -ne 0) { Write-Warning "docker-compose down returned non-zero exit code; continuing." }

Write-Host "Starting database service..."
docker-compose -f $ComposeFile up -d db
if ($LASTEXITCODE -ne 0) { Write-Error "Failed to start DB service"; exit $LASTEXITCODE }

$DbContainerId = (docker-compose -f $ComposeFile ps -q db).Trim()
if ([string]::IsNullOrWhiteSpace($DbContainerId)) {
	Write-Error "Could not resolve the db container id"
	exit 1
}

$maxAttempts = 30
$attempt = 0
Write-Host "Waiting for Postgres healthcheck to report healthy (up to $($maxAttempts*2) seconds)..."
while ($attempt -lt $maxAttempts) {
	$healthStatus = docker inspect --format "{{if .State.Health}}{{.State.Health.Status}}{{else}}{{.State.Status}}{{end}}" $DbContainerId
	if ($LASTEXITCODE -eq 0 -and $healthStatus.Trim() -eq 'healthy') {
		Write-Host "Postgres is ready."
		break
	}
	Start-Sleep -Seconds 2
	$attempt++
}
if ($attempt -ge $maxAttempts) {
	Write-Error "Postgres did not become ready in time"
	docker-compose -f $ComposeFile logs db
	exit 1
}

Write-Host "Starting API (detached) and rebuilding image..."
docker-compose -f $ComposeFile up -d --build api
if ($LASTEXITCODE -ne 0) { Write-Error "Failed to start API"; exit $LASTEXITCODE }

Write-Host "API started. Use 'docker-compose -f $ComposeFile logs -f api' to follow logs."
