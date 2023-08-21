# Commands

## Decompile Bicep from JSON

`az bicep decompile --file .\servicebus_template.json`

## Bicep

```shell
az group delete --name GSNVSearchDemo
az keyvault purge --name gsnvsearchdemokeyvault
az deployment sub create --location centralus --template-file .\main.bicep --name manual-deployment
`

## Validate

`az resource list --resource-group ServiceBusDemo`

## Clean up

`az group delete --name ServiceBusDemo`

## Cloudflare API

UF7pSYdhpwJt5ELrUVathnsoeL-0q53mmiU7H02D

```shell
curl -X GET "https://api.cloudflare.com/client/v4/user/tokens/verify" \
     -H "Authorization: Bearer UF7pSYdhpwJt5ELrUVathnsoeL-0q53mmiU7H02D" \
     -H "Content-Type:application/json"
```

Zone ID: 314f753caf8b5bb78e934cc8a12683d9

## RBAC

```shell
az login
az account show
az account set --subscription 66effa16-8b4b-4047-b8e1-d390ceddd4a5
```

subscription id:
"id": "66effa16-8b4b-4047-b8e1-d390ceddd4a5"

resourcegroupname: SearchDemo

`az ad app create --display-name transcribetranslatedemo-github-deployer -o json`

"appId": "3e6c9e93-decd-48b1-9efa-f9035c50856f"
"id": "bc4d6180-e261-4cc9-b241-f3a7e7d33a5d"

`az ad sp create --id 3e6c9e93-decd-48b1-9efa-f9035c50856f -o json`

assigneeObjectId
"id": "abca8f9e-0e8a-4ffe-9ceb-5614521809ab"
tenantId
"appOwnerOrganizationId": "9637eecd-fbd4-438a-848a-4e29f4d8eae5"

`az role assignment create --role owner --subscription 66effa16-8b4b-4047-b8e1-d390ceddd4a5 --assignee-object-id  abca8f9e-0e8a-4ffe-9ceb-5614521809ab --assignee-principal-type ServicePrincipal --scope /subscriptions/66effa16-8b4b-4047-b8e1-d390ceddd4a5`

`az rest --method POST --uri 'https://graph.microsoft.com/beta/applications/bc4d6180-e261-4cc9-b241-f3a7e7d33a5d/federatedIdentityCredentials' --body "{'name':'github-deploy','issuer':'https://token.actions.githubusercontent.com','subject':'repo:glensouza/azure-cognitive-transcribe-translate-demo:ref:refs/heads/main','description':'Deploy from GitHub Actions','audiences':['api://AzureADTokenExchange']}" --header "{'contenttype': 'application/json'}"`

CLEANUP:

`az group delete --name GSNVSearchDemo`

GitHub Secrets:

`AZURE_CLIENT_ID` `3e6c9e93-decd-48b1-9efa-f9035c50856f`
`AZURE_TENANT_ID` `9637eecd-fbd4-438a-848a-4e29f4d8eae5`
`AZURE_SUBSCRIPTION_ID` `66effa16-8b4b-4047-b8e1-d390ceddd4a5`

<https://learn.microsoft.com/en-us/azure/developer/github/connect-from-azure?tabs=azure-cli%2Clinux&WT.mc_id=javascript-70241-aapowell>

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





 & .\rbac.ps1 -tenantId '9637eecd-fbd4-438a-848a-4e29f4d8eae5' -subscriptionId '66effa16-8b4b-4047-b8e1-d390ceddd4a5' -applicationName 'transcribetranslatedemo-github-deployer' -repoName 'glensouza/azure-cognitive-transcribe-translate-demo'
