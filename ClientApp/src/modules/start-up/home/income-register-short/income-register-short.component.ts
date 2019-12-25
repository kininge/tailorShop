import { Component, OnInit, ViewChild, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NgForm } from '@angular/forms';
import { MatSnackBar, MatPaginator, MatTableDataSource, MatSort } from '@angular/material';

import { UserData } from 'src/modules/shared/interfaces/user-data.interface';
import { AllDataService } from 'src/modules/app/all-data.service';
import { WorkOut } from 'src/modules/shared/interfaces/workOut.interface';

@Component({
  selector: 'app-income-register-short',
  templateUrl: './income-register-short.component.html',
  styleUrls: ['./income-register-short.component.scss']
})
export class IncomeRegisterShortComponent implements OnInit 
{
  private pageType: string = 'short';
  private noData: boolean = undefined;
  private user: UserData = undefined;
  private workId: number = undefined;
  private amount: number = undefined; 

  //#region workout table
  private data: WorkOut[] = undefined;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  private customerData = new MatTableDataSource<WorkOut>(this.data);

  applyFilter(filterValue: string)
  {
    //console.log('filterValue: ' + filterValue);
    this.customerData.filter = filterValue.trim().toLowerCase();
    console.log(this.customerData);
  };

  getTotalNumbers()
  {
    console.log(this.customerData);
    return this.data.length;
  }

  private workid: number = undefined;

  style(customerId: number, forWhat: string)
  {
    if (this.workid === customerId) {
      if (forWhat === 'boxShadow') {
        return '3px 3px 5px 6px #ccc';
      }
      else if (forWhat === 'backgroundColor') {
        return 'rgba(20, 120, 180, 1)';
      }
      else if (forWhat === 'zIndex') {
        return '1';
      }
      else {
        return 'white';
      }
    }
  }

  check(val: number)
  {
    //console.log('selected: ' + val);
  }

  //#endregion

  constructor(private matSnackBar: MatSnackBar, private commonData: AllDataService, private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  ngOnInit() 
  {
    this.customerData.paginator = this.paginator;
    this.customerData.sort = this.sort;
    this.user = this.commonData.getUser();

    this.clothDetails();
  }


  clothDetails() {
    var d = new Date();

    //console.log('start Date: ' + (d.getFullYear() - 1) + '-' + (d.getMonth() + 1) + '-' + d.getDate() + ' end Date: ' + d.getFullYear() + '-' + (d.getMonth() + 1) + '-' + d.getDate());

    this.httpClient.get<WorkOut[]>(this.baseUrl + 'pritam/masterdata/workOutDetails?startDate=' + (d.getFullYear() - 1) + '-' + (d.getMonth() + 1) + '-' + d.getDate()
      + '&endDate=' + d.getFullYear() + '-' + (d.getMonth() + 1) + '-' + d.getDate() + '&isTestData=' + this.user.isTestData)
      .subscribe(result => {
        //console.log(result);
        this.data = result;
        console.log(this.data);

        if (this.data[0]) {
          this.noData = false;
        }
        else {
          this.noData = true;
        }
      },
        error => {
          console.log(error);
        });
  }

  shortClothOut(form: NgForm)
  {

    if (this.workId && this.amount) {
      this.httpClient.post<Boolean>(this.baseUrl + 'pritam/works/createWorkOut',
        { 'workId': this.workId, 'deliveredOn': new Date(), 'createdBy': this.user.userId, 'pay': this.amount, 'updatedOn': null, 'updatedBy': null, 'isTestData': this.user.isTestData })
        .subscribe(result => {
          if (result) {
            this.matSnackBar.open('Work Id ' + form.value.workId + '\'s ' + form.value.amount + ' rupees get added successfully!', 'Ok', { duration: 4000 });
            form.resetForm();
          }
        },
          error => {
            console.log(error);
          })
    }
    

    
  }

  openIncomeRegister()
  {
    if(this.pageType=== 'incomeRegister')
    {
      this.pageType= 'short';
    }
    else
    {
      this.pageType = 'incomeRegister';
      this.clothDetails();
    }
  }
}

