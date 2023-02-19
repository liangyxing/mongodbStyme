import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'MongoDBSystem',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44363/',
    redirectUri: baseUrl,
    clientId: 'MongoDBSystem_App',
    responseType: 'code',
    scope: 'offline_access MongoDBSystem',
    requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://localhost:44363',
      rootNamespace: 'Yxing.MongoDBSystem',
    },
  },
} as Environment;
