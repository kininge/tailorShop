import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

import { SharedModule } from 'src/modules/shared/shared.module';
import { ForgetPasswordComponent } from 'src/modules/forget-password/forget-password.component';

const routes: Routes = [{ path: 'forget-password', component: ForgetPasswordComponent }];

@NgModule({
    imports: [CommonModule, SharedModule, RouterModule.forChild(routes)],
    declarations: [ForgetPasswordComponent]
})
export class ForgetPasswordModule { }
