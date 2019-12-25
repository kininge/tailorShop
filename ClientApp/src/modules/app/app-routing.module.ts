import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AllDataService } from 'src/modules/app/all-data.service';
import { CustomePrelodingService } from 'src/modules/app/custome-preloding.service';
import { LogInBaseComponent } from 'src/modules/app/logIn-base/logIn-base.component';

const routes: Routes =
[
    { path: '', component: LogInBaseComponent,
        children:
        [
            { path: '', data: { preload: true }, loadChildren: 'src/modules/logIn/logIn.module#LogInModule' },
            { path: '', data: { preload: false }, loadChildren: 'src/modules/register/register.module#RegisterModule' },
            { path: '', data: { preload: false }, loadChildren: 'src/modules/forget-password/forget-password.module#ForgetPasswordModule' }
        ]
    },
    { path: '', canActivate: [AllDataService], data: { preload: true }, loadChildren: 'src/modules/start-up/start-up.module#StartUpModule' }
];

@NgModule
({
    imports: [RouterModule.forRoot(routes, { preloadingStrategy: CustomePrelodingService })],
    exports: [RouterModule]
})
export class AppRoutingModule { }
