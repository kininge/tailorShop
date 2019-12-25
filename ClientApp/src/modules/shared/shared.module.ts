import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { Platform, PlatformModule } from '@angular/cdk/platform';
import { LayoutModule } from '@angular/cdk/layout';
import {MatToolbarModule, MatButtonModule, MatSidenavModule, MatIconModule, MatListModule, MatGridListModule, MatCardModule, MatDividerModule, MatTabsModule,
    MatMenuModule, MatDatepickerModule, MatNativeDateModule, MatInputModule, MatExpansionModule, MatProgressBarModule, MatFormFieldModule, MatRippleModule,
    MatProgressSpinnerModule, MatDialogModule, MatStepperModule, MatSelectModule, MatSnackBarModule, MatTableModule, MatPaginatorModule, MatAutocompleteModule,
    MatCheckboxModule, MatTooltipModule, MatSortModule, DateAdapter, MAT_DATE_LOCALE
} from '@angular/material';

import { FilterPipe } from 'src/modules/shared/pipes/filter.pipe';
import { SortPipe } from 'src/modules/shared/pipes/sort.pipe';
import { IsArrayEmptyPipe } from 'src/modules/shared/pipes/isArrayEmpty.pipe';
import { StringFirstPipe } from 'src/modules/shared/pipes/string-first.pipe';
import { StringFirstAndLastPipe } from 'src/modules/shared/pipes/string-last-first.pipe';
import { UpperCasePipe } from 'src/modules/shared/pipes/upperCase.pipe';
import { AppDateAdapter } from './format-datepicker';


@NgModule({
    imports: [HttpClientModule, PlatformModule, FormsModule, MatToolbarModule, MatButtonModule, MatSidenavModule, MatIconModule, MatListModule,
        MatGridListModule, MatCardModule, MatDividerModule, MatTabsModule, MatMenuModule, MatDatepickerModule, MatNativeDateModule, MatInputModule, MatExpansionModule,
        MatProgressBarModule, MatFormFieldModule, MatRippleModule, MatProgressSpinnerModule, MatDialogModule, MatStepperModule, MatSelectModule, MatSnackBarModule,
        MatTableModule, MatPaginatorModule, MatAutocompleteModule, MatCheckboxModule, MatTooltipModule, MatSortModule, RouterModule, LayoutModule],
    declarations: [FilterPipe, SortPipe, IsArrayEmptyPipe, StringFirstPipe, StringFirstAndLastPipe, UpperCasePipe ],
    providers: [{ provide: DateAdapter, useClass: AppDateAdapter, deps: [MAT_DATE_LOCALE, Platform] }],  //
    exports: [CommonModule, FilterPipe, SortPipe, IsArrayEmptyPipe, StringFirstPipe, StringFirstAndLastPipe, UpperCasePipe, HttpClientModule,
        FormsModule, MatToolbarModule, MatButtonModule, MatSidenavModule, MatIconModule, MatListModule, MatGridListModule, MatCardModule, MatDividerModule, MatTabsModule,
        MatMenuModule, MatDatepickerModule, MatNativeDateModule, MatInputModule, MatExpansionModule, MatProgressBarModule, MatFormFieldModule, MatRippleModule,
        MatProgressSpinnerModule, MatDialogModule, MatStepperModule, MatSelectModule, MatSnackBarModule, MatTableModule, MatPaginatorModule, MatAutocompleteModule,
        MatCheckboxModule, MatTooltipModule, MatSortModule, RouterModule, LayoutModule]
})
export class SharedModule { }

