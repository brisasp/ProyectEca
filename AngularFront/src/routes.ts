import { Routes } from '@angular/router';
import { PageNotFoundComponent } from './app/pages/page-not-found/page-not-found.component';
import { LoginComponent } from './app/pages/login/login.component';
import { PrincipalComponent } from './app/pages/principal/prinicpal.component';
import { ReservaFormComponent } from './app/pages/reserva-form/reserva-form.component';


export const routeConfig: Routes = [
  {
    path: '',
    component: LoginComponent,
   title: 'I.E.S. Comercio',
  },
  {
    path: 'principal',
    component: PrincipalComponent,
   title: 'I.E.S. Comercio',
  },
{ path: 'nueva-reserva', component: ReservaFormComponent },
  {
    path: '**',
    component: PageNotFoundComponent,
  },
];

//export default routeConfig;
