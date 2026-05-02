# Script para rodar testes da aplicação via Docker

# Forçar a execução a partir da raiz do projeto
$ScriptDir = Split-Path $MyInvocation.MyCommand.Path -Parent
Set-Location -Path "$ScriptDir\.."

docker-compose -f scripts/docker-compose.test.yml up --build --abort-on-container-exit test-runner
docker-compose -f scripts/docker-compose.test.yml down -v
