import { Component, OnInit, Inject} from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { Breakpoints, BreakpointObserver, BreakpointState, MediaMatcher } from '@angular/cdk/layout';
import { HttpClient } from '@angular/common/http';

import { map } from 'rxjs/operators';
import { Subscription } from 'rxjs';

import { UserData } from 'src/modules/shared/interfaces/user-data.interface';
import { AllDataService } from 'src/modules/app/all-data.service';
import { Work } from 'src/modules/shared/interfaces/work.interface';
import { Upper } from 'src/modules/shared/interfaces/upper.interface';
import { Lower } from 'src/modules/shared/interfaces/lower.interface';

export interface feedback { fit: number, style: number, workQuality: number, text: string }

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.scss']
})
export class CustomerComponent implements OnInit 
{
  public tablet: boolean;
  public mobile: boolean;
  private user: UserData = undefined;
  private customerWorks: Work[] = undefined;
  private customerUppers: Upper[][] = [];
  private customerLowers: Lower[][] = [];

  constructor(private matDialog: MatDialog, private breakpointObserver: BreakpointObserver, private mediaMatcher: MediaMatcher, private commonData: AllDataService, private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string) { 

    //mobile in portare mode
    breakpointObserver.observe([Breakpoints.HandsetPortrait]).subscribe(result =>
    { 
      if (result.matches)
      {
        this.mobile = true;
        //console.log('constructor mobile');
      }
      else
      {
        this.mobile = false;
        //console.log('constructor not mobile');
      }
    });

    //mobile in ladscape mode
    breakpointObserver.observe([Breakpoints.HandsetLandscape]).subscribe(result =>
    {
      if (result.matches)
      {
        this.tablet = true;
        //console.log('constructor tablet');
      }
      else
      {
        this.tablet = false;
        //console.log('constructor not tablet');
      }
    });
  }

  ngOnInit()
  {
    this.user = this.commonData.getUser();
    this.GetAllWork(this.user.userId);
  }

  //Get All work in this year
  GetAllWork(userId: number) {
    this.httpClient.get<Work[]>(this.baseUrl + 'pritam/masterdata/works?isTestUser=' + this.user.isTestData + '&userId=' + userId)
      .subscribe(result => {
        this.customerWorks = result;
        console.log(this.customerWorks);

        this.customerWorks.forEach(obj => {
          this.GetAllCloths(obj.workId);
        });
      },
        error => {
          console.log(error);
        });
  }

  //Get all cloths in this year
  GetAllCloths(workId: number) {
    this.httpClient.get<Upper[]>(this.baseUrl + 'pritam/masterdata/upperComplete?isTestUser=' + this.user.isTestData + '&workId=' + workId)
      .subscribe(result => {
        console.log(result);

        this.customerUppers.push(result);
        console.log(this.customerUppers);
      },
        error => {
          console.log(error);
        });

    this.httpClient.get<Lower[]>(this.baseUrl + 'pritam/masterdata/lowerComplete?isTestUser=' + this.user.isTestData + '&workId=' + workId)
      .subscribe(result => {
        console.log(result);
        this.customerLowers.push(result);
        console.log(this.customerLowers);
      },
        error => {
          console.log(error);
        });
  }

  public orderStatus: { complete: { feedbackGiven: boolean; feedbackNotGiven: boolean, notDelivered: boolean }, workInProgress: boolean }=
          { complete: { feedbackGiven: false, feedbackNotGiven: false, notDelivered: false }, workInProgress: false };

  //Call feedback
  callFeedback(workId: number, clothId: number, productName: string, remark: string, feedbackId: number, fit: number, style: number, sticgingStyle: number, text: string, clothType: boolean)
  {
    console.log('workId: ' + workId + ' clothId: ' + clothId + ' productName: ' + productName + ' remarks: ' + remark + ' feedbackId: ' + feedbackId+ ' fit: ' + fit + ' style: ' + style + ' workQuality: ' + sticgingStyle + ' text: ' + text);

    if (remark) {
      var notes: string[] = remark.split(", ");
    }
    else {
      var notes: string[] = ['Regular'];
    }
    
    notes.forEach(obj => {
      console.log('note: ' + obj);
    });

    if (feedbackId== 0) {
      if (this.mobile) {
        const dialogRef = this.matDialog.open(DialogComponent,
          {
            width: '200%',
            data: { workId: workId, productName: productName, remarks: notes, fit: fit, style: style, workQuality: sticgingStyle, text: '' }
          });

        dialogRef.afterClosed().subscribe(result => {
          console.log('The dialog was closed'); 
          console.log('workId: ' + workId + ' productName: ' + productName + ' fit: ' + result.fit + ' style: ' + result.style + ' workQuality: ' + result.quality + ' text: ' + result.text);

          this.httpClient.post<Boolean>(this.baseUrl + 'pritam/works/createFeedback',
            { 'Fit': result.fit, 'Style': result.style, 'Quality': result.quality, 'Feedback1': result.text, 'WorkId': workId, 'ClothId': clothId, 'ClothType': clothType })
            .subscribe(here => {
              console.log('create feedback: ' + here);

               this.customerWorks= undefined;
               this.customerUppers= [];
              this.customerLowers = [];

              this.GetAllWork(this.user.userId);
            },
              error => {
                console.log(error);
            });

        });
      }
      else if (this.tablet) {
        const dialogRef = this.matDialog.open(DialogComponent,
          {
            width: '90%',
            data: { workId: workId, productName: productName, remarks: notes, fit: fit, style: style, workQuality: sticgingStyle, text: '' }
          });

        dialogRef.afterClosed().subscribe(result => {
          console.log('The dialog was closed');
          console.log('workId: ' + workId + ' productName: ' + productName + ' fit: ' + result.fit + ' style: ' + result.style + ' workQuality: ' + result.quality + ' text: ' + result.text);

          this.httpClient.post<Boolean>(this.baseUrl + 'pritam/works/createFeedback',
            { 'Fit': result.fit, 'Style': result.style, 'Quality': result.quality, 'Feedback1': result.text, 'WorkId': workId, 'ClothId': clothId, 'ClothType': clothType })
            .subscribe(here => {
              console.log('create feedback: ' + here);

              this.customerWorks = undefined;
              this.customerUppers = [];
              this.customerLowers = [];

              this.GetAllWork(this.user.userId);
            },
              error => {
                console.log(error);
              });
        });
      }
      else {
        const dialogRef = this.matDialog.open(DialogComponent,
          {
            width: '60%',
            data: { workId: workId, productName: productName, remarks: notes, fit: fit, style: style, workQuality: sticgingStyle, text: '' }
          });

        dialogRef.afterClosed().subscribe(result => {
          console.log('The dialog was closed');
          console.log('workId: ' + workId + ' productName: ' + productName + ' fit: ' + result.fit + ' style: ' + result.style + ' workQuality: ' + result.quality + ' text: ' + result.text);

          this.httpClient.post<Boolean>(this.baseUrl + 'pritam/works/createFeedback',
            { 'Fit': result.fit, 'Style': result.style, 'Quality': result.quality, 'Feedback1': result.text, 'WorkId': workId, 'ClothId': clothId, 'ClothType': clothType })
            .subscribe(here => {
              console.log('create feedback: ' + here);

              this.customerWorks = undefined;
              this.customerUppers = [];
              this.customerLowers = [];

              this.GetAllWork(this.user.userId);
            },
              error => {
                console.log(error);
              });
        });
      }
    }
    else {
      if (this.mobile) {
        const dialogRef = this.matDialog.open(DialogComponent,
          {
            width: '200%',
            data: { workId: workId, productName: productName, remarks: notes, fit: fit, style: style, workQuality: sticgingStyle, text: text }
          });

        dialogRef.afterClosed().subscribe(result => {
          console.log('The dialog was closed');
          console.log('workId: ' + workId + ' productName: ' + productName + ' fit: ' + result.fit + ' style: ' + result.style + ' workQuality: ' + result.quality + ' text: ' + result.text);

          this.httpClient.put<boolean>(this.baseUrl + 'pritam/works/changeFeedback',
            { 'FeedbackId': feedbackId, 'Fit': result.fit, 'Style': result.style, 'Quality': result.quality, 'Feedback1': result.text }).subscribe(
              result => {
                console.log('feedback change: ' + result);
                this.customerWorks = undefined;
                this.customerUppers = [];
                this.customerLowers = [];

                this.GetAllWork(this.user.userId);
              },
              error => {
                console.log(error);
              });
        });
      }
      else if (this.tablet) {
        const dialogRef = this.matDialog.open(DialogComponent,
          {
            width: '90%',
            data: { workId: workId, productName: productName, remarks: notes, fit: fit, style: style, workQuality: sticgingStyle, text: text }
          });

        dialogRef.afterClosed().subscribe(result => {
          console.log('The dialog was closed');
          console.log('workId: ' + workId + ' productName: ' + productName + ' fit: ' + result.fit + ' style: ' + result.style + ' workQuality: ' + result.quality + ' text: ' + result.text);

          this.httpClient.put<boolean>(this.baseUrl + 'pritam/works/changeFeedback',
            { 'FeedbackId': feedbackId, 'Fit': result.fit, 'Style': result.style, 'Quality': result.quality, 'Feedback1': result.text }).subscribe(
              result => {
                console.log('feedback change: ' + result);
                this.customerWorks = undefined;
                this.customerUppers = [];
                this.customerLowers = [];

                this.GetAllWork(this.user.userId);
              },
              error => {
                console.log(error);
              });
        });
      }
      else {
        const dialogRef = this.matDialog.open(DialogComponent,
          {
            width: '60%',
            data: { workId: workId, productName: productName, remarks: notes, fit: fit, style: style, workQuality: sticgingStyle, text: text }
          });

        dialogRef.afterClosed().subscribe(result => {
          console.log('The dialog was closed');
          console.log('workId: ' + workId + ' productName: ' + productName + ' fit: ' + result.fit + ' style: ' + result.style + ' workQuality: ' + result.quality + ' text: ' + result.text);

          this.httpClient.put<boolean>(this.baseUrl + 'pritam/works/changeFeedback',
            { 'FeedbackId': feedbackId, 'Fit': result.fit, 'Style': result.style, 'Quality': result.quality, 'Feedback1': result.text }).subscribe(
              result => {
                console.log('feedback change: ' + result);
                this.customerWorks = undefined;
                this.customerUppers = [];
                this.customerLowers = [];

                this.GetAllWork(this.user.userId);
              },
              error => {
                console.log(error);
              });
        });
      }
    }
    
    
  }
}


@Component
({
  selector: 'dialogBox',
  templateUrl: './feedback.html',
  styleUrls: ['./feedback.scss']
})
export class DialogComponent
{
  constructor(public dialogRef: MatDialogRef<DialogComponent>, @Inject(MAT_DIALOG_DATA)
  public data: { workId: number, productName: string, remarks: string[], fit: number, style: number, workQuality: number, text: string, clothType: boolean }) { }

}

