import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

import { SharedModule } from 'src/modules/shared/shared.module';
import { LogInComponent } from 'src/modules/logIn/logIn.component';

const routes: Routes = [{ path: 'logIn', component: LogInComponent }];

@NgModule
({
    imports: [CommonModule, SharedModule, RouterModule.forChild(routes)],
    declarations: [LogInComponent]
})
export class LogInModule { }
