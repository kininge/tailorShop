import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';

import { AllDataService } from 'src/modules/app/all-data.service';
import { CustomePrelodingService } from 'src/modules/app/custome-preloding.service';
import { AppRoutingModule } from 'src/modules/app/app-routing.module';
import { AppComponent } from 'src/modules/app/app/app.component';
import { LogInBaseComponent } from 'src/modules/app/logIn-base/logIn-base.component';
import { SharedModule } from 'src/modules/shared/shared.module';

@NgModule
({
    declarations: [AppComponent, LogInBaseComponent],
    imports: [BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }), BrowserAnimationsModule, AppRoutingModule, SharedModule],
    providers: [AllDataService,CustomePrelodingService],
    bootstrap: [AppComponent]
})
export class AppModule { }
