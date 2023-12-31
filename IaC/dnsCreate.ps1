########
# play #
########

$headers = @{
    "Authorization" = "Bearer $Env:APIKey"
    "Content-Type"  = "application/json"
}

$records = Invoke-RestMethod `
  -Uri "https://api.cloudflare.com/client/v4/zones/$Env:ZoneId/dns_records?name=play.triviagame.party" `
  -Headers $headers `
  -Method 'GET'

$cnameRecords = $records.result | Where-Object { $_.Type -eq "CNAME" }

if ($null -ne $cnameRecords) {
  if ($cnameRecords.content -ne $Env:HostName) {
    # Update CNAME
    $recordId = $cnameRecords.id
    $url = "https://api.cloudflare.com/client/v4/zones/$Env:ZoneId/dns_records/$recordId"
    $body = @{ "content" = $Env:HostName } | ConvertTo-Json
    Invoke-RestMethod `
      -Uri $url `
      -Headers $headers `
      -Method PATCH `
      -Body $body
  }
}
else {
    # Create CNAME
    Write-Host "Creating CNAME to play with value $Env:HostName"
    $body = @{
      "type" = "CNAME"
      "name" = "play"
      "content" = $Env:HostName
      "ttl" = 300
      "proxied" = $False
    } | ConvertTo-Json
    Invoke-RestMethod `
      -Uri "https://api.cloudflare.com/client/v4/zones/$Env:ZoneId/dns_records" `
      -Headers $headers `
      -Method POST `
      -Body $body
}



#######
# WWW #
#######

$ghPageUrl = "glensouza.github.io"

$records = Invoke-RestMethod `
  -Uri "https://api.cloudflare.com/client/v4/zones/$Env:ZoneId/dns_records?name=www.triviagame.party" `
  -Headers $headers `
  -Method 'GET'

$cnameRecords = $records.result | Where-Object { $_.Type -eq "CNAME" }

if ($null -ne $cnameRecords) {
  if ($cnameRecords.content -ne $Env:HostName) {
    # Update CNAME
    $recordId = $cnameRecords.id
    $url = "https://api.cloudflare.com/client/v4/zones/$Env:ZoneId/dns_records/$recordId"
    $body = @{ "content" = $ghPageUrl } | ConvertTo-Json
    Invoke-RestMethod `
      -Uri $url `
      -Headers $headers `
      -Method PATCH `
      -Body $body
  }
}
else {
    # Create CNAME
    Write-Host "Creating CNAME to www with value $ghPageUrl"
    $body = @{
      "type" = "CNAME"
      "name" = "www"
      "content" = $ghPageUrl
      "ttl" = 300
      "proxied" = $False
    } | ConvertTo-Json
    Invoke-RestMethod `
      -Uri "https://api.cloudflare.com/client/v4/zones/$Env:ZoneId/dns_records" `
      -Headers $headers `
      -Method POST `
      -Body $body
}
