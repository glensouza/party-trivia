$headers = @{
    "Authorization" = "Bearer $Env:APIKey"
    "Content-Type"  = "application/json"
}

$records = Invoke-RestMethod `
  -Uri "https://api.cloudflare.com/client/v4/zones/$Env:ZoneId/dns_records?name=www.triviagame.party" `
  -Headers $headers `
  -Method 'GET'

$cnameRecords = $records.result | Where-Object { $_.Type -eq "CNAME" }

if ($null -ne $cnameRecords) {
  # Delete CNAME
  $recordId = $cnameRecords.id
  $url = "https://api.cloudflare.com/client/v4/zones/$Env:ZoneId/dns_records/$recordId"
  Invoke-RestMethod `
    -Uri $url `
    -Headers $headers `
    -Method DELETE `
}
