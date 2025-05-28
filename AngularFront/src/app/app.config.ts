/*!
 * @license
 * Copyright Google LLC All Rights Reserved.
 *
 * Use of this source code is governed by an MIT-style license that can be
 * found in the LICENSE file at https://angular.dev/license
 */
import { routeConfig } from '../routes';
import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';



export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routeConfig) ],
};
