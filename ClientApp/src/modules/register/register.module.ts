import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

import { SharedModule } from 'src/modules/shared/shared.module';
import { RegisterComponent } from 'src/modules/register/register.component';

const routes: Routes = [{ path: 'register', component: RegisterComponent }];

@NgModule({
    imports: [CommonModule, SharedModule, RouterModule.forChild(routes)],
    declarations: [RegisterComponent]
})
export class RegisterModule { }
