# Oregon Strawberry Commission MSFT Lab

## RBAC

```azurecli
az login
az account list --query "[?isDefault]"
az account set --subscription "b8093588-4b1f-49d6-8e3d-cebb694d529e"
az group create -n PeopleDemo -l eastus2
az ad sp create-for-rbac --name OregonStrawberryCommissionPeopleDemo --role contributor --scopes /subscriptions/b8093588-4b1f-49d6-8e3d-cebb694d529e/resourceGroups/PeopleDemo --sdk-auth
```

```json
{
  "clientId": "b4fb1e15-0c59-4e7a-8191-c8991efd9b15",
  "clientSecret": "4Q48Q~uXCAd3pRgUw53vgO5TKhuvaQlIXq3oIac7",
  "subscriptionId": "66effa16-8b4b-4047-b8e1-d390ceddd4a5",
  "tenantId": "9637eecd-fbd4-438a-848a-4e29f4d8eae5",
  "activeDirectoryEndpointUrl": "https://login.microsoftonline.com",
  "resourceManagerEndpointUrl": "https://management.azure.com/",
  "activeDirectoryGraphResourceId": "https://graph.windows.net/",
  "sqlManagementEndpointUrl": "https://management.core.windows.net:8443/",
  "galleryEndpointUrl": "https://gallery.azure.com/",
  "managementEndpointUrl": "https://management.core.windows.net/"
}
```

## Decompile Bicep from JSON

```azurecli
az bicep decompile --file .\servicebus_template.json
```

## Bicep

```azurecli
az deployment group create --resource-group PeopleDemo --template-file .\staticwebapp.bicep    
```

## Validate

```azurecli
az resource list --resource-group ServiceBusDemo
```

## Clean up

```azurecli
az group delete --name ServiceBusDemo
```

## Cloudflare API

UF7pSYdhpwJt5ELrUVathnsoeL-0q53mmiU7H02D

```shell
curl -X GET "https://api.cloudflare.com/client/v4/user/tokens/verify" \
     -H "Authorization: Bearer UF7pSYdhpwJt5ELrUVathnsoeL-0q53mmiU7H02D" \
     -H "Content-Type:application/json"
```

Zone ID: 314f753caf8b5bb78e934cc8a12683d9







az login

subscription id:
"id": "b8093588-4b1f-49d6-8e3d-cebb694d529e"

resourcegroupname: PeopleDemo

az ad app create --display-name peopledemo-github-deployer -o json

"appId": "f53b68a5-23ae-42a7-8e9c-ca92d1a5a6a6"
"id": "2c9b6ccb-5de2-4e7d-b4c3-9c9b2583c28d"

az ad sp create --id f53b68a5-23ae-42a7-8e9c-ca92d1a5a6a6 -o json

assigneeObjectId
"id": "35d1bf4e-8930-438e-bbd8-2b96afb9b0cf"
tenantId
"appOwnerOrganizationId": "d8404fb7-eaea-4e71-9da4-315bc1aae5e6"

az role assignment create --role contributor --subscription b8093588-4b1f-49d6-8e3d-cebb694d529e --assignee-object-id  35d1bf4e-8930-438e-bbd8-2b96afb9b0cf --assignee-principal-type ServicePrincipal --scope /subscriptions/b8093588-4b1f-49d6-8e3d-cebb694d529e/resourceGroups/PeopleDemo

az rest --method POST --uri 'https://graph.microsoft.com/beta/applications/b8093588-4b1f-49d6-8e3d-cebb694d529e/federatedIdentityCredentials' --body '{"name":"github-deploy","issuer":"https://token.actions.githubusercontent.com","subject":"repo:Oregon-Strawberry-Commission-MSFT-Lab/PeopleAPI:ref:refs/heads/main","description":"Deploy from GitHub Actions","audiences":["api://AzureADTokenExchange"]}' --header '{"contenttype": "application/json"}'

GitHub Secrets:

AZURE_CLIENT_ID	f53b68a5-23ae-42a7-8e9c-ca92d1a5a6a6
AZURE_TENANT_ID	d8404fb7-eaea-4e71-9da4-315bc1aae5e6
AZURE_SUBSCRIPTION_ID	b8093588-4b1f-49d6-8e3d-cebb694d529e

https://learn.microsoft.com/en-us/azure/developer/github/connect-from-azure?tabs=azure-cli%2Clinux&WT.mc_id=javascript-70241-aapowell

actions yml:

permissions:
      id-token: write
      contents: read
      
jobs: 
  build-and-deploy:
    runs-on: ubuntu-latest
    steps:
    - name: 'Az CLI login'
      uses: azure/login@v1
      with:
          client-id: ${{ secrets.AZURE_CLIENT_ID }}
          tenant-id: ${{ secrets.AZURE_TENANT_ID }}
          subscription-id: ${{ secrets.AZURE_SUBSCRIPTION_ID }}