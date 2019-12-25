import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { AllDataService } from 'src/modules/app/all-data.service';

import { UserData } from 'src/modules/shared/interfaces/user-data.interface';
import { FastCard } from 'src/modules/shared/interfaces/fastCard.interface';
import { Cloths } from 'src/modules/shared/interfaces/cloths.interface';

@Component({
  selector: 'app-news',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.scss']
})
export class NewsComponent implements OnInit
{
  private loggedUser: UserData = undefined;

  private todayRestCloth: FastCard = undefined;
  private todayUpperCloths: Cloths[] = undefined;
  private todayLowerCloths: Cloths[] = undefined;
  private noToday: boolean = undefined;
  private yesterdayRestCloth: FastCard = undefined;
  private yesterdayUpperCloths: Cloths[] = undefined;
  private yesterdayLowerCloths: Cloths[] = undefined;
  private noYesterday: boolean = undefined;

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string, private commonData: AllDataService) { }

  ngOnInit()
  {
    //Get loged in user's data
    this.loggedUser = this.commonData.getUser();

    //today fastData
    this.httpClient.get<FastCard>(this.baseUrl + 'pritam/masterdata/fastCard?isTestUser=' + this.loggedUser.isTestData + '&day=' + true)
      .subscribe(result =>
      {
        //console.log(result);
        this.todayRestCloth = result;
        //console.log(this.todayRestCloth);

        if (result[0])
        {
          this.noToday = false;
        }
        else
        {
          this.noToday = true;
        }

        //console.log('this.noToday: ' + this.noToday);
      },
      error =>
      {
        console.log(error);
        this.noToday = true;
      });

    //Yesterday FastData
    this.httpClient.get<FastCard>(this.baseUrl + 'pritam/masterdata/fastCard?isTestUser=' + this.loggedUser.isTestData + '&day=' + false)
      .subscribe(result =>
      {
        //console.log(result);
        this.yesterdayRestCloth = result;
        //console.log(this.yesterdayRestCloth);

        if (result[0])
        {
          this.noYesterday = false;
        }
        else
        {
          this.noYesterday = true;
        }

        //console.log('this.noYesterday: ' + this.noYesterday);
      },
      error =>
      {
        console.log(error);
        this.noYesterday = true;
      });

    //Today upperCloths
    this.httpClient.get<Cloths[]>(this.baseUrl + 'pritam/masterdata/upper?isTestUser=' + this.loggedUser.isTestData + '&day=' + true)
      .subscribe(result =>
      {
        //console.log(this.todayUpperCloths);
        this.todayUpperCloths = result;
        //console.log(this.todayUpperCloths);
      },
      error =>
      {
        console.log(error);
      });

    //Today LowerCloths
    this.httpClient.get<Cloths[]>(this.baseUrl + 'pritam/masterdata/lower?isTestUser=' + this.loggedUser.isTestData + '&day=' + true)
      .subscribe(result =>
      {
        //console.log(result);
        this.todayLowerCloths = result;
        //console.log(this.todayLowerCloths);
      },
      error =>
      {
        console.log(error);
      });

    //Yesterday upperCloths
    this.httpClient.get<Cloths[]>(this.baseUrl + 'pritam/masterdata/upper?isTestUser=' + this.loggedUser.isTestData + '&day=' + false)
      .subscribe(result =>
      {
        //console.log(result);
        this.yesterdayUpperCloths = result;
        //console.log(this.yesterdayUpperCloths);
      },
      error =>
      {
        console.log(error);
      });

    //Today LowerCloths
    this.httpClient.get<Cloths[]>(this.baseUrl + 'pritam/masterdata/lower?isTestUser=' + this.loggedUser.isTestData + '&day=' + false)
      .subscribe(result =>
      {
        //console.log(result);
        this.yesterdayLowerCloths = result;
        //console.log(this.yesterdayLowerCloths);
      },
      error =>
      {
        console.log(error);
      });
  }
}
