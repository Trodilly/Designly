# Run-all.ps1
# This script starts both the Designly.API and Designly.Client projects.

Write-Host "Starting Designly.API..."
Start-Process dotnet -ArgumentList "run --project Designly.API/Designly.API.csproj --launch-profile https" -NoNewWindow

Write-Host "Waiting briefly for API to start..."
Start-Sleep -Seconds 5 # Give the API some time to spin up

Write-Host "Starting Designly.Client..."
dotnet run --project Designly.Client/Designly.Client.csproj --launch-profile https

Write-Host "All processes started. Press Ctrl+C to stop both."
