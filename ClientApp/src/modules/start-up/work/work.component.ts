import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { AllDataService } from 'src/modules/app/all-data.service';

import { UserDetails } from 'src/modules/shared/interfaces/userDetails.interface';
import { UserData } from 'src/modules/shared/interfaces/user-data.interface';



@Component({
  selector: 'app-work',
  templateUrl: './work.component.html',
  styleUrls: ['./work.component.scss']
})
export class WorkComponent implements OnInit
{
  private loggedUser: UserData = undefined;
  private allUsers: { UserId: number, Name: string, MobileNo: string }[] = undefined;
  private allWork: { UserId: number, Name: string, MobileNo: string }[] = undefined;
  private searchFor: string = 'customer name';
  private searching: string = undefined;
  private pageShowing: string = 'news';

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string, private commonData: AllDataService) { }

  ngOnInit() 
  {
    //Get loged in user's data
    this.loggedUser = this.commonData.getUser();

    //allUsers
    this.httpClient.get<{ UserId: number, Name: string, MobileNo: string }[]>(this.baseUrl + 'pritam/masterdata/allUser?isTestUser=' + this.loggedUser.isTestData + '&shopName=' + this.loggedUser.shopName)
      .subscribe(result => {
        //console.log(result);
        this.allUsers = result;
        //console.log('allUsers')
        //console.log(this.allUsers);
      },
        error => {
          //console.log(error);
      });

    //allWork
    this.httpClient.get<{ UserId: number, Name: string, MobileNo: string }[]>(this.baseUrl + 'pritam/masterdata/allWorks?isTestUser=' + this.loggedUser.isTestData + '&shopName=' + this.loggedUser.shopName)
      .subscribe(result => {
        //console.log(result);
        this.allWork = result;
        //console.log('allWork')
        //console.log(this.allWork);
      },
        error => {
          //console.log(error);
        });
  }

  pageChange(page: string)
  {
    //console.log('pageChange: ' + page);
    if (page == 'news') {
      this.searching = undefined;
    }

    this.pageShowing= page;
  }

  search() {
    //console.log('searching: ' + this.searching + ' searchFor: ' + this.searchFor);

    this.pageShowing = 'search';
  }

  searchForChange(val: string) {
    console.log('[(ngModel)]="searchFor" ');
    this.pageShowing = 'news';
    this.searching = undefined;
    this.searchFor = val;
  }

  getPageBack(whaterver: any) {
    console.log('getPageBack to news');
    this.searchFor= 'customer name';
    this.searching= undefined;
    this.pageShowing = 'news';
  }
}
