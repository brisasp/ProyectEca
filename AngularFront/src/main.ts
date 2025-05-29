// main.ts
import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { provideRouter } from '@angular/router';
import { routeConfig } from './routes'; 
import { provideHttpClient } from '@angular/common/http';
import { registerLocaleData } from '@angular/common';
import localeEs from '@angular/common/locales/es';
import { LOCALE_ID } from '@angular/core'; // 👈 IMPORTANTE

registerLocaleData(localeEs);

bootstrapApplication(AppComponent, {
  providers: [
    provideHttpClient(),
    provideRouter(routeConfig),{ provide: LOCALE_ID, useValue: 'es-ES' }
  ]
});