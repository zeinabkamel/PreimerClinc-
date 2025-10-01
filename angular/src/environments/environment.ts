import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

const oAuthConfig = {
  issuer: 'https://localhost:44385/',
  redirectUri: baseUrl,
  clientId: 'PreimerClinc_App',
  responseType: 'code',
  scope: 'offline_access PreimerClinc',
  requireHttps: true,
};

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'Premier Clinic',
  },
  oAuthConfig,
  apis: {
    default: {
      url: 'https://localhost:44385',
      rootNamespace: 'PreimerClinc',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
  },
} as Environment;
