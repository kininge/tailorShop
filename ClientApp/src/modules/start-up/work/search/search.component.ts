import { Component, OnInit, Inject, Input, Output, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { AllDataService } from 'src/modules/app/all-data.service';

import { UserData } from 'src/modules/shared/interfaces/user-data.interface';
import { Upper } from 'src/modules/shared/interfaces/upper.interface';
import { Lower } from 'src/modules/shared/interfaces/lower.interface';
import { Customer } from 'src/modules/shared/interfaces/customer.interface';
import { Work } from 'src/modules/shared/interfaces/work.interface';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent implements OnInit
{
  @Input() set searchingData({ search, by })
  {
    this.crearAll();
    this.searching = search;
    this.searchFor = by;
    console.log('this.searching: ' + this.searching + ' this.searchFor: ' + this.searchFor);
    this.ngOnInit();
    this.search();
  }

  private loggedUser: UserData = undefined;
  private employeeList: { userId: number, name: string, mobileNumber: string }[] = undefined;
  @Output() goBackPage = new EventEmitter<{ pageName: string }>();

  private searchFor: string = undefined;
  private searching: string = undefined;
  private customer: Customer = undefined;
  private customerWorks: Work[] = undefined;
  private customerUppers: Upper[][] = [];
  private customerLowers: Lower[][] = [];
  private works: Work = undefined;
  private upper: Upper[] = undefined;
  private lower: Lower[] = undefined;

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string, private commonData: AllDataService) { }

  ngOnInit()
  {
    //Get loged in user's data
    this.loggedUser = this.commonData.getUser();

    //Employee list
    this.httpClient.get<{ userId: number, name: string, mobileNumber: string }[]>(this.baseUrl + 'pritam/masterdata/allEmployees?isTestUser=' + this.loggedUser.isTestData + '&shopName=' + this.loggedUser.shopName)
      .subscribe(result => {
        //console.log(result);
        this.employeeList = result;
        //console.log(this.employeeList);
      },
        error => {
          console.log(error);
        });
  }

  //Search
  search()
  {
    console.log('searchFor: ' + this.searchFor + ' searchBy: ' + this.searching);

    if (this.searchFor == 'customer name') {
      this.httpClient.get<Customer>(this.baseUrl + 'pritam/masterdata/customer?isTestUser=' + this.loggedUser.isTestData + '&searchByName=' + this.searching + '&searchByUserId=' + 0 + '&searchType=' + true)
        .subscribe(result =>
        {
          console.log(result);
          this.customer = result;

          this.GetAllWork(this.customer.userId);
        },
        error =>
        {
            console.log(error);
        });
    }
    else if (this.searchFor == 'customer id')
    {
      this.httpClient.get<Customer>(this.baseUrl + 'pritam/masterdata/customer?isTestUser=' + this.loggedUser.isTestData + '&searchByName=' + '' + '&searchByUserId=' + this.searching + '&searchType=' + false)
        .subscribe(result =>
        {
          console.log(result);
          this.customer = result;

          this.GetAllWork(this.customer.userId);
        },
        error =>
        {
          console.log(error);
          this.customer = undefined;
        });
    }
    else
    {
      this.httpClient.get<Work>(this.baseUrl + 'pritam/masterdata/work?isTestUser=' + this.loggedUser.isTestData + '&workId=' + this.searching)
        .subscribe(result =>
        {
          console.log(result);
          this.works = result;
          console.log(this.works);
        },
        error =>
        {
          console.log(error);
          this.works = undefined;
        });

      this.httpClient.get<Upper[]>(this.baseUrl + 'pritam/masterdata/upperComplete?isTestUser=' + this.loggedUser.isTestData + '&workId=' + this.searching)
        .subscribe(result =>
        {
          console.log(result);
          this.upper = result;
          console.log(this.upper);
        },
        error =>
        {
          console.log(error);
        });

      this.httpClient.get<Lower[]>(this.baseUrl + 'pritam/masterdata/lowerComplete?isTestUser=' + this.loggedUser.isTestData + '&workId=' + this.searching)
        .subscribe(result =>
        {
          console.log(result);
          this.lower = result;
          console.log(this.lower);
        },
        error =>
        {
          console.log(error);
        });
    }
  }

  crearAll() {
    this.searchFor= undefined;
    this.searching= undefined;
    this.customer= undefined;
    this.customerWorks= undefined;
    this.customerUppers= [];
    this.customerLowers= [];
    this.works= undefined;
    this.upper= undefined;
    this.lower= undefined;
  }

  //Get All work in this year
  GetAllWork(userId: number)
  {
    this.httpClient.get<Work[]>(this.baseUrl + 'pritam/masterdata/works?isTestUser=' + this.loggedUser.isTestData + '&userId=' + userId)
      .subscribe(result =>
      {
        this.customerWorks = result;
        console.log(this.customerWorks);

        this.customerWorks.forEach(obj =>
        {
          this.GetAllCloths(obj.workId);
        });

        console.log(this.customerLowers);
        console.log(this.customerUppers);
      },
      error =>
      {
        console.log(error);
      });
  }

  //Get all cloths in this year
  GetAllCloths(workId: number)
  {
    this.httpClient.get<Upper[]>(this.baseUrl + 'pritam/masterdata/upperComplete?isTestUser=' + this.loggedUser.isTestData + '&workId=' + workId)
      .subscribe(result =>
      {
        console.log(result);

        this.customerUppers.push(result);
      },
      error =>
      {
        console.log(error);
      });

    this.httpClient.get<Lower[]>(this.baseUrl + 'pritam/masterdata/lowerComplete?isTestUser=' + this.loggedUser.isTestData + '&workId=' + workId)
      .subscribe(result =>
      {
        console.log(result);
        this.customerLowers.push(result);
      },
      error =>
      {
        console.log(error);
      });
  }

  //Go privious page
  goBack() {
    console.log('goBack fom newWork');
    this.goBackPage.emit({ pageName: 'news' });
  }

  //Change status
  changeStatus(workId: number, clothId: number, array: number, newStatus: number, clothType: boolean) {
    //console.log("changeStatus get workId: " + workId + " productId: " + clothId + " array: " + array+ " newStatus: " + newStatus + " clothType: " + clothType);

    if (array == 1) {
      this.customerUppers.forEach(element => {
        element.forEach(innerArray => {
          if (innerArray.workId == workId && innerArray.clothId == clothId) {
            innerArray.status = newStatus;
          }
        });
      });
    }
    else if (array == 2) {
      this.customerLowers.forEach(element => {
        element.forEach(innerArray => {
          if (innerArray.workId == workId && innerArray.clothId == clothId) {
            innerArray.status = newStatus;
          }
        });
      });
    }
    else if (array == 3) {
      this.upper.forEach(element => {
        if (element.workId == workId && element.clothId == clothId) {
          element.status = newStatus;
        }
      });
    }
    else if(array == 4)
    {
      this.lower.forEach(element => {
        if (element.workId == workId && element.clothId == clothId) {
          element.status = newStatus;
        }
      });
    }


    this.httpClient.put<boolean>(this.baseUrl + 'pritam/works/changeStatus',
      { 'WorkId': workId, 'ClothId': clothId, 'AssignTo': 0, 'Status': (newStatus / 20) + 1, PaidTo: 0, 'ClothType': clothType }).subscribe(
        result => {
          //console.log('status change: ' + result);
        },
        error => {
          console.log(error);
        });
  }

  //Assign work to emplyee 
  assignEmployee(workId: number, clothId: number, array: number, employeeId: number, employeeName: string, clothType: boolean) {
    //console.log("changeStatus get workId: " + workId + " productId: " + clothId + " employeeId: " + employeeId + " employeeName: " + employeeName);

    if (array == 1) {
      this.customerUppers.forEach(element => {
        element.forEach(innerArray => {
          if (innerArray.workId == workId && innerArray.clothId == clothId) {
            innerArray.status = 60;
          }
        });
      });
    }
    else if (array == 2) {
      this.customerLowers.forEach(element => {
        element.forEach(innerArray => {
          if (innerArray.workId == workId && innerArray.clothId == clothId) {
            innerArray.status = 60;
          }
        });
      });
    }
    else if (array == 3) {
      this.upper.forEach(element => {
        if (element.workId == workId && element.clothId == clothId) {
          element.status = 60;
        }
      });
    }
    else if(array == 4)
    {
      this.lower.forEach(element => {
        if (element.workId == workId && element.clothId == clothId) {
          element.status = 60;
        }
      });
    }

    this.httpClient.put<boolean>(this.baseUrl + 'pritam/works/changeStatus',
      {
        'WorkId': workId, 'ClothId': clothId, 'AssignTo': employeeId, 'Status': 4, PaidTo: 0, 'ClothType': clothType
      }).subscribe(
        result => {
          //console.log('status change: ' + result);
        },
        error => {
          console.log(error);
        });

  }
}
