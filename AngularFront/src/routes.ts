import { Routes } from '@angular/router';
import { PageNotFoundComponent } from './app/pages/page-not-found/page-not-found.component';
import { LoginComponent } from './app/pages/login/login.component';
import { PrincipalComponent } from './app/pages/principal/prinicpal.component';

export const routeConfig: Routes = [
  {
    path: '',
    component: LoginComponent,
   title: 'I.E.S. Comercio',
  },
//{
    //path: '',
   // redirectTo: 'calendario',
  //  pathMatch: 'full',
  //},

  {
    path: 'principal',
    component: PrincipalComponent,
   title: 'I.E.S. Comercio',
  },
 // {
 // path: 'calendario',
 // component: CalendarioComponent,
 // title: 'Calendario de Reservas',
//},

  {
    path: '**',
    component: PageNotFoundComponent,
  },
];

//export default routeConfig;
