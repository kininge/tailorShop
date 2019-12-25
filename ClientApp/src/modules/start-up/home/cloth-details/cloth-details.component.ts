import { Component, OnInit, Inject, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Breakpoints, BreakpointObserver } from '@angular/cdk/layout';

import { UserData } from 'src/modules/shared/interfaces/user-data.interface';
import { AllDataService } from 'src/modules/app/all-data.service';
import { ClothDetails } from 'src/modules/shared/interfaces/clothDetails.interface';


@Component({
  selector: 'app-cloth-details',
  templateUrl: './cloth-details.component.html',
  styleUrls: ['./cloth-details.component.scss']
})
export class ClothDetailsComponent implements OnInit
{
  private user: UserData;
  private clothInVisibility: boolean= true;
  private clothOutVisibility: boolean = true;
  private clothIn: ClothDetails = { other: 0, pant: 0, safariPant: 0, safariShirt: 0, shirt: 0 };
  private clothOut: ClothDetails = { other: 0, pant: 0, safariPant: 0, safariShirt: 0, shirt: 0 };

  private startDate: Date = undefined;
  private endDate: Date = undefined;

  
  constructor(private breakpointObserver: BreakpointObserver, private allDataManageService: AllDataService, private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string) 
  {
      
  }

  //#region Changing grid tiles sizes
  clothCard= this.breakpointObserver.observe(Breakpoints.Handset).pipe( map(({ matches }) => 
  {
      if (matches) 
      {
        return [ { cols: 12, rows: 1.35 } ];
      }
      else
      {
        return [ { cols: 6, rows: 1 } ];
      }
  }));
  //#endregion

  ngOnInit() {
    this.user = this.allDataManageService.getUser();

    this.getDates();
  }

  //LaxumiPoojaDates
  getDates() {
    var today = new Date();

    this.httpClient.get<Date>(this.baseUrl + 'pritam/masterdata/laxumiPooja?year=' + (today.getFullYear() - 1))
      .subscribe(result => {
        this.startDate = result;
        //console.log('last year laxumi pooja: ' + this.startDate);
        this.clothDetails();
      });

    this.httpClient.get<Date>(this.baseUrl + 'pritam/masterdata/laxumiPooja?year=' + today.getFullYear())
      .subscribe(result => {
        this.endDate = result;
        //console.log('this year laxumi pooja: ' + this.endDate);
        this.clothDetails();
      });

    if (this.endDate < today) {
      this.httpClient.get<Date>(this.baseUrl + 'pritam/masterdata/laxumiPooja?year=' + today.getFullYear())
        .subscribe(result => {
          this.startDate = result;
          //console.log('if last year laxumi pooja: ' + this.startDate);
          this.clothDetails();
        });

      this.httpClient.get<Date>(this.baseUrl + 'pritam/masterdata/laxumiPooja?year=' + (today.getFullYear() + 1))
        .subscribe(result => {
          this.endDate = result;
          //console.log('if this year laxumi pooja: ' + this.endDate);
          this.clothDetails();
        });
    }
  }

  //ClothCounts
  clothDetails() {

    if (this.startDate && this.endDate) {
      this.httpClient.get<ClothDetails>(this.baseUrl + 'pritam/masterdata/clothCame?lastYear=' + this.startDate + '&thisYear=' + this.endDate + '&isTestData=' + this.user.isTestData)
        .subscribe(result => {
          this.clothIn = result;
          console.log('ClothIn');
          console.log(this.clothIn);
        },
          error => {
            console.log(error);
          });

      this.httpClient.get<ClothDetails>(this.baseUrl + 'pritam/masterdata/clothOut?lastYear=' + this.startDate + '&thisYear=' + this.endDate + '&isTestData=' + this.user.isTestData)
        .subscribe(result => {
          this.clothOut = result;
          console.log('ClothOut');
          console.log(this.clothOut);
        },
          error => {
            console.log(error);
          });
    }
  }

  //visibility with animation

  makeUnvisible(vasiable: string)
  {
    if(vasiable=== 'clothInVisibility')
    {
      setTimeout( ()=> { this.clothInVisibility= false; }, 340 );
    }
    else if(vasiable=== 'clothOutVisibility')
    {
      setTimeout( ()=> { this.clothOutVisibility= false; }, 340 );
    }
  }
}
