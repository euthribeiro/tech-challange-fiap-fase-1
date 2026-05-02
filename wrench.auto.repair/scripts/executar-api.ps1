# Script para executar a API principal

$ScriptDir = Split-Path $MyInvocation.MyCommand.Path -Parent
Set-Location -Path "$ScriptDir\.."

docker-compose up --build
