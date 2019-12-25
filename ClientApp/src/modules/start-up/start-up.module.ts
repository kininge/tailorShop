import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';

import { SharedModule } from 'src/modules/shared/shared.module';
import { StartUpComponent } from 'src/modules/start-up/startUp.component';
import { HomeComponent } from 'src/modules/start-up/home/home.component';
import { TaskDetailsComponent } from 'src/modules/start-up/home/task-details/task-details.component';
import { CustomerComponent } from 'src/modules/start-up/home/customer/customer.component';
import { ClothDetailsComponent } from 'src/modules/start-up/home/cloth-details/cloth-details.component';
import { IncomeRegisterShortComponent } from 'src/modules/start-up/home/income-register-short/income-register-short.component';
import { AboutComponent } from 'src/modules/start-up/about/about.component';
import { PartnersComponent } from 'src/modules/start-up/partners/partners.component';
import { WorkComponent } from 'src/modules/start-up/work/work.component';
import { AddWorkComponent } from 'src/modules/start-up/work/add-work/add-work.component';
import { NewsComponent } from 'src/modules/start-up/work/news/news.component';
import { SearchComponent } from 'src/modules/start-up/work/search/search.component';


const routes: Routes = [{ path: 'application', component: StartUpComponent }];

@NgModule
({
    imports: [CommonModule,SharedModule,RouterModule.forChild(routes)],
    declarations: [StartUpComponent, HomeComponent, CustomerComponent, ClothDetailsComponent, IncomeRegisterShortComponent, AboutComponent, PartnersComponent
        , WorkComponent, AddWorkComponent, NewsComponent, SearchComponent, TaskDetailsComponent],
})
export class StartUpModule { }
