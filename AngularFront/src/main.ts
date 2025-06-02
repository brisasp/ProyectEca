import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { provideRouter } from '@angular/router';
import { routeConfig } from './routes'; 
import { provideHttpClient } from '@angular/common/http';
import { registerLocaleData } from '@angular/common';
import localeEs from '@angular/common/locales/es';
import { LOCALE_ID } from '@angular/core';
import { SocialAuthServiceConfig, GoogleLoginProvider } from '@abacritt/angularx-social-login';

registerLocaleData(localeEs);

bootstrapApplication(AppComponent, {
  providers: [
    provideHttpClient(),
    provideRouter(routeConfig),
    { provide: LOCALE_ID, useValue: 'es-ES' },

    {
      provide: 'SocialAuthServiceConfig',
      useValue: {
        autoLogin: false,
        providers: [
          {
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider(
              '930885862523-71g7mi87108etqpdcv3bg64r@u0h1r9e.apps.googleusercontent.com'
            ),
          },
        ],
      } as SocialAuthServiceConfig,
    },
  ],
});
