import type { GetPermissionListResultDto, UpdatePermissionsDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class PermissionsService {
  apiName = 'AbpPermissionManagement';
  

  get = (providerName: string, providerKey: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, GetPermissionListResultDto>({
      method: 'GET',
      url: '/api/permission-management/permissions',
      params: { providerName, providerKey },
    },
    { apiName: this.apiName,...config });
  

  getByGroup = (groupName: string, providerName: string, providerKey: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, GetPermissionListResultDto>({
      method: 'GET',
      url: '/api/permission-management/permissions/by-group',
      params: { groupName, providerName, providerKey },
    },
    { apiName: this.apiName,...config });
  

  update = (providerName: string, providerKey: string, input: UpdatePermissionsDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: '/api/permission-management/permissions',
      params: { providerName, providerKey },
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
