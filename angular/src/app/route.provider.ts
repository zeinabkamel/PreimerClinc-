import { RoutesService, eLayoutType } from '@abp/ng.core';
import { provideAppInitializer, inject } from '@angular/core';

export const APP_ROUTE_PROVIDER = [
  provideAppInitializer(() => {
    configureRoutes();
  }),
];

function configureRoutes() {
  const routes = inject(RoutesService);
  routes.add([
    {
      path: '/',
      name: '::Menu:Home',
      iconClass: 'fas fa-home',
      order: 1,
      layout: eLayoutType.application,
    },
    {
      path: '/books',
      name: '::Menu:Books',
      iconClass: 'fas fa-book',
      layout: eLayoutType.application,
      // requiredPolicy: 'PreimerClinc.Books',
    },
     {
      path: '/identity/users',
      name: 'Users',
      iconClass: 'fa fa-users',
      layout: eLayoutType.application,
      // requiredPolicy: 'AbpIdentity.Users',
    },
    {
      path: '/identity/roles',
      name: 'Roles',
      iconClass: 'fa fa-id-badge',
      layout: eLayoutType.application,
      // requiredPolicy: 'AbpIdentity.Roles',
    },
  
    
  ]);
}
