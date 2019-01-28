Start-Process dotnet -ArgumentList "run -p .\HalIntegrationApi\HalIntegrationApi.csproj"
Write-Host "Service running"

Start-Process dotnet -ArgumentList "run -p .\WebApp\WebApp.csproj"
Write-Host "Website running"

Write-Host "Starting browser..."
Start-Process "https://localhost:44341/"
