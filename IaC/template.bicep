param sites_partytrivia_name string = 'partytrivia'
param staticSites_PartyTrivia_name string = 'PartyTrivia'
param components_PartyTrivia_name string = 'PartyTrivia'
param SignalR_partytrivia_name string = 'partytrivia'
param serverfarms_ASP_PartyTrivia_af10_name string = 'ASP-PartyTrivia-af10'
param databaseAccounts_partytrivia_name string = 'partytrivia'
param storageAccounts_partytrivia8343_name string = 'partytrivia8343'
param workspaces_DefaultWorkspace_66effa16_8b4b_4047_b8e1_d390ceddd4a5_CUS_name string = 'DefaultWorkspace-66effa16-8b4b-4047-b8e1-d390ceddd4a5-CUS'
param workspaces_DefaultWorkspace_66effa16_8b4b_4047_b8e1_d390ceddd4a5_CUS_externalid string = '/subscriptions/66effa16-8b4b-4047-b8e1-d390ceddd4a5/resourceGroups/DefaultResourceGroup-CUS/providers/Microsoft.OperationalInsights/workspaces/DefaultWorkspace-66effa16-8b4b-4047-b8e1-d390ceddd4a5-CUS'

resource components_PartyTrivia_name_resource 'microsoft.insights/components@2020-02-02' = {
  name: components_PartyTrivia_name
  location: 'centralus'
  kind: 'other'
  properties: {
    Application_Type: 'web'
    Flow_Type: 'Bluefield'
    RetentionInDays: 90
    WorkspaceResourceId: workspaces_DefaultWorkspace_66effa16_8b4b_4047_b8e1_d390ceddd4a5_CUS_externalid
    IngestionMode: 'LogAnalytics'
    publicNetworkAccessForIngestion: 'Enabled'
    publicNetworkAccessForQuery: 'Enabled'
  }
}

resource workspaces_DefaultWorkspace_66effa16_8b4b_4047_b8e1_d390ceddd4a5_CUS_name_resource 'Microsoft.OperationalInsights/workspaces@2021-12-01-preview' = {
  name: workspaces_DefaultWorkspace_66effa16_8b4b_4047_b8e1_d390ceddd4a5_CUS_name
  location: 'centralus'
  properties: {
    sku: {
      name: 'pergb2018'
    }
    retentionInDays: 30
    features: {
      enableLogAccessUsingOnlyResourcePermissions: true
    }
    workspaceCapping: {
      dailyQuotaGb: -1
    }
    publicNetworkAccessForIngestion: 'Enabled'
    publicNetworkAccessForQuery: 'Enabled'
  }
}

resource SignalR_partytrivia_name_resource 'Microsoft.SignalRService/SignalR@2023-06-01-preview' = {
  name: SignalR_partytrivia_name
  location: 'centralus'
  sku: {
    name: 'Free_F1'
    tier: 'Free'
    size: 'F1'
    capacity: 1
  }
  kind: 'SignalR'
  properties: {
    tls: {
      clientCertEnabled: false
    }
    features: [
      {
        flag: 'ServiceMode'
        value: 'Serverless'
        properties: {}
      }
      {
        flag: 'EnableConnectivityLogs'
        value: 'True'
        properties: {}
      }
      {
        flag: 'EnableMessagingLogs'
        value: 'False'
        properties: {}
      }
      {
        flag: 'EnableLiveTrace'
        value: 'False'
        properties: {}
      }
    ]
    cors: {
      allowedOrigins: [
        '*'
      ]
    }
    serverless: {
      connectionTimeoutInSeconds: 30
    }
    upstream: {}
    networkACLs: {
      defaultAction: 'Deny'
      publicNetwork: {
        allow: [
          'ServerConnection'
          'ClientConnection'
          'RESTAPI'
          'Trace'
        ]
      }
      privateEndpoints: []
    }
    publicNetworkAccess: 'Enabled'
    disableLocalAuth: false
    disableAadAuth: false
  }
}

resource storageAccounts_partytrivia8343_name_resource 'Microsoft.Storage/storageAccounts@2023-01-01' = {
  name: storageAccounts_partytrivia8343_name
  location: 'westus'
  sku: {
    name: 'Standard_LRS'
    tier: 'Standard'
  }
  kind: 'Storage'
  properties: {
    defaultToOAuthAuthentication: true
    minimumTlsVersion: 'TLS1_2'
    allowBlobPublicAccess: true
    networkAcls: {
      bypass: 'AzureServices'
      virtualNetworkRules: []
      ipRules: []
      defaultAction: 'Allow'
    }
    supportsHttpsTrafficOnly: true
    encryption: {
      services: {
        file: {
          keyType: 'Account'
          enabled: true
        }
        blob: {
          keyType: 'Account'
          enabled: true
        }
      }
      keySource: 'Microsoft.Storage'
    }
  }
}

resource serverfarms_ASP_PartyTrivia_af10_name_resource 'Microsoft.Web/serverfarms@2022-09-01' = {
  name: serverfarms_ASP_PartyTrivia_af10_name
  location: 'West US'
  sku: {
    name: 'Y1'
    tier: 'Dynamic'
    size: 'Y1'
    family: 'Y'
    capacity: 0
  }
  kind: 'functionapp'
  properties: {
    perSiteScaling: false
    elasticScaleEnabled: false
    maximumElasticWorkerCount: 1
    isSpot: false
    reserved: false
    isXenon: false
    hyperV: false
    targetWorkerCount: 0
    targetWorkerSizeId: 0
    zoneRedundant: false
  }
}

resource staticSites_PartyTrivia_name_resource 'Microsoft.Web/staticSites@2022-09-01' = {
  name: staticSites_PartyTrivia_name
  location: 'Central US'
  tags: {
    'hidden-link: /app-insights-resource-id': '/subscriptions/66effa16-8b4b-4047-b8e1-d390ceddd4a5/resourceGroups/PartyTrivia/providers/microsoft.insights/components/PartyTrivia'
    'hidden-link: /app-insights-instrumentation-key': '76c40ca8-cb31-4ccb-8b4b-55059535289a'
    'hidden-link: /app-insights-conn-string': 'InstrumentationKey=76c40ca8-cb31-4ccb-8b4b-55059535289a;IngestionEndpoint=https://centralus-2.in.applicationinsights.azure.com/;LiveEndpoint=https://centralus.livediagnostics.monitor.azure.com/'
  }
  sku: {
    name: 'Standard'
    tier: 'Standard'
  }
  properties: {
    repositoryUrl: 'https://github.com/glensouza/party-trivia'
    branch: 'main'
    stagingEnvironmentPolicy: 'Enabled'
    allowConfigFileUpdates: true
    provider: 'GitHub'
    enterpriseGradeCdnStatus: 'Disabled'
  }
}

resource sites_partytrivia_name_resource 'Microsoft.Web/sites@2022-09-01' = {
  name: sites_partytrivia_name
  location: 'West US'
  tags: {
    'hidden-link: /app-insights-resource-id': '/subscriptions/66effa16-8b4b-4047-b8e1-d390ceddd4a5/resourceGroups/PartyTrivia/providers/Microsoft.Insights/components/PartyTrivia'
  }
  kind: 'functionapp'
  properties: {
    enabled: true
    hostNameSslStates: [
      {
        name: '${sites_partytrivia_name}.azurewebsites.net'
        sslState: 'Disabled'
        hostType: 'Standard'
      }
      {
        name: '${sites_partytrivia_name}.scm.azurewebsites.net'
        sslState: 'Disabled'
        hostType: 'Repository'
      }
    ]
    serverFarmId: serverfarms_ASP_PartyTrivia_af10_name_resource.id
    reserved: false
    isXenon: false
    hyperV: false
    vnetRouteAllEnabled: false
    vnetImagePullEnabled: false
    vnetContentShareEnabled: false
    siteConfig: {
      numberOfWorkers: 1
      acrUseManagedIdentityCreds: false
      alwaysOn: false
      http20Enabled: false
      functionAppScaleLimit: 200
      minimumElasticInstanceCount: 0
    }
    scmSiteAlsoStopped: false
    clientAffinityEnabled: false
    clientCertEnabled: false
    clientCertMode: 'Required'
    hostNamesDisabled: false
    customDomainVerificationId: '7D3D825747C79DF9C9261AAD7870C2254CD5B49B2DA3EFAD6C98BE36B69873C1'
    containerSize: 1536
    dailyMemoryTimeQuota: 0
    httpsOnly: true
    redundancyMode: 'None'
    publicNetworkAccess: 'Enabled'
    storageAccountRequired: false
    keyVaultReferenceIdentity: 'SystemAssigned'
  }
}

resource sites_partytrivia_name_web 'Microsoft.Web/sites/config@2022-09-01' = {
  parent: sites_partytrivia_name_resource
  name: 'web'
  location: 'West US'
  tags: {
    'hidden-link: /app-insights-resource-id': '/subscriptions/66effa16-8b4b-4047-b8e1-d390ceddd4a5/resourceGroups/PartyTrivia/providers/Microsoft.Insights/components/PartyTrivia'
  }
  properties: {
    numberOfWorkers: 1
    defaultDocuments: [
      'Default.htm'
      'Default.html'
      'Default.asp'
      'index.htm'
      'index.html'
      'iisstart.htm'
      'default.aspx'
      'index.php'
    ]
    netFrameworkVersion: 'v6.0'
    requestTracingEnabled: false
    remoteDebuggingEnabled: false
    httpLoggingEnabled: false
    acrUseManagedIdentityCreds: false
    logsDirectorySizeLimit: 35
    detailedErrorLoggingEnabled: false
    publishingUsername: '$partytrivia'
    scmType: 'None'
    use32BitWorkerProcess: true
    webSocketsEnabled: false
    alwaysOn: false
    managedPipelineMode: 'Integrated'
    virtualApplications: [
      {
        virtualPath: '/'
        physicalPath: 'site\\wwwroot'
        preloadEnabled: false
      }
    ]
    loadBalancing: 'LeastRequests'
    experiments: {
      rampUpRules: []
    }
    autoHealEnabled: false
    vnetRouteAllEnabled: false
    vnetPrivatePortsCount: 0
    publicNetworkAccess: 'Enabled'
    cors: {
      allowedOrigins: [
        'https://portal.azure.com'
      ]
      supportCredentials: false
    }
    localMySqlEnabled: false
    ipSecurityRestrictions: [
      {
        ipAddress: 'Any'
        action: 'Allow'
        priority: 2147483647
        name: 'Allow all'
        description: 'Allow all access'
      }
    ]
    scmIpSecurityRestrictions: [
      {
        ipAddress: 'Any'
        action: 'Allow'
        priority: 2147483647
        name: 'Allow all'
        description: 'Allow all access'
      }
    ]
    scmIpSecurityRestrictionsUseMain: false
    http20Enabled: false
    minTlsVersion: '1.2'
    scmMinTlsVersion: '1.2'
    ftpsState: 'FtpsOnly'
    preWarmedInstanceCount: 0
    functionAppScaleLimit: 200
    functionsRuntimeScaleMonitoringEnabled: false
    minimumElasticInstanceCount: 0
    azureStorageAccounts: {}
  }
}
