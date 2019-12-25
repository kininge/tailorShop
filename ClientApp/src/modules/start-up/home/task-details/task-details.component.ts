import { Component, OnInit, Inject, Input } from '@angular/core';
import { map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { Breakpoints, BreakpointObserver } from '@angular/cdk/layout';

import { UserData } from 'src/modules/shared/interfaces/user-data.interface';
import { AllDataService } from 'src/modules/app/all-data.service';
import { UpperTask } from 'src/modules/shared/interfaces/upperTask.interface';
import { LowerTask } from 'src/modules/shared/interfaces/lowerTask.interface';


@Component({
  selector: 'app-task-details',
  templateUrl: './task-details.component.html',
  styleUrls: ['./task-details.component.scss']
})
export class TaskDetailsComponent implements OnInit 
{
  private taskBeforeDays: number = 2;
  private user: UserData= undefined;

  private upperCuttingTask: UpperTask[] = undefined;
  private noUpperCuttingTask: boolean;
  private lowerCuttingTask: LowerTask[] = undefined;
  private noLowerCuttingTask: boolean;
  private upperStichingTask: UpperTask[] = undefined;
  private noUpperStichingTask: boolean;
  private lowerStichingTask: LowerTask[] = undefined
  private noLowerStichingTask: boolean;

  

  employeeList: { userId: number, name: string, mobileNumber: string }[] = undefined;
  //#region Changing grid tiles sizes
  description= this.breakpointObserver.observe(Breakpoints.Handset).pipe( map(({ matches }) => 
  {
      if (matches) 
      {
        return [ { desplay: true} ];
      }
      else
      {
        return [ { desplay: false} ];
      }
  }));
  //#endregion

  constructor(private breakpointObserver: BreakpointObserver, private commonData: AllDataService, private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  ngOnInit() 
  {
    this.user = this.commonData.getUser();

    this.httpClient.get<{ userId: number, name: string, mobileNumber: string }[]>(this.baseUrl + 'pritam/masterdata/allEmployees?isTestUser=' + this.user.isTestData + '&shopName=' + this.user.shopName)
      .subscribe(result => {
        //console.log(result);
        this.employeeList = result;
        //console.log(this.employeeList);
      },
      error => {
        console.log(error);
      });

    this.callTask();
  }

  dayChange() {
    //console.log('callTask called with daybefore: ' + this.taskBeforeDays);
    //this.ngOnInit();
    if (this.taskBeforeDays) {
      this.callTask();
    }
    
  }

  assignEmployee(workId: number, clothId: number, array: number, employeeId: number, employeeName: string, clothType: boolean) {
    //console.log("changeStatus get workId: " + workId + " productId: " + clothId + " employeeId: " + employeeId + " employeeName: " + employeeName);

    if (array == 1) {
      this.upperCuttingTask.forEach(element => {
        if (element.workId == workId && element.clothId == clothId) {
          element.assignTo = employeeName;
          element.status = 60;
        }
      });
    }
    else if (array == 2) {
      this.lowerCuttingTask.forEach(element => {
        if (element.workId == workId && element.clothId == clothId) {
          element.assignTo = employeeName;
          element.status = 60;
        }
      });
    }
    else if (array == 3) {
      this.upperStichingTask.forEach(element => {
        if (element.workId == workId && element.clothId == clothId) {
          element.assignTo = employeeName;
          element.status = 60;
        }
      });
    }
    else if(array == 4){
      this.lowerStichingTask.forEach(element => {
        if (element.workId == workId && element.clothId == clothId) {
          element.assignTo = employeeName;
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

        this.callTask();
      },
        error => {
          console.log(error);
      });

  }

  //#region work status
  changeStatus(workId: number, clothId: number, array: number, newStatus: number, clothType: boolean)
  {
    //console.log("changeStatus get workId: " + workId + " productId: " + clothId + " newStatus: " + newStatus + " clothType: " + clothType);

    if (array == 1)
    {
      this.upperCuttingTask.forEach(element =>
      {
        if (element.workId == workId && element.clothId == clothId)
        {
          element.status = newStatus;
        }
      });
    }
    else if (array == 2)
    {
      this.lowerCuttingTask.forEach(element =>
      {
        if (element.workId == workId && element.clothId == clothId)
        {
          element.status = newStatus;
        }
      });
    }
    else if (array == 3)
    {
      this.upperStichingTask.forEach(element =>
      {
        if (element.workId == workId && element.clothId == clothId)
        {
          element.status = newStatus;
        }
      });
    }
    else (array == 4)
    {
      this.lowerStichingTask.forEach(element =>
      {
        if (element.workId == workId && element.clothId == clothId)
        {
          element.status = newStatus;
        }
      });
    }

    
    this.httpClient.put<boolean>(this.baseUrl + 'pritam/works/changeStatus',
      { 'WorkId': workId, 'ClothId': clothId, 'AssignTo': 0, 'Status': (newStatus/20)+1, PaidTo: 0, 'ClothType': clothType}).subscribe(
      result => {
        //console.log('status change: ' + result);

        if (newStatus == 80) {
          this.callTask();
        }
      },
      error => {
        console.log(error);
      });
      

    
    
  }
 //#endregion

  callTask() {
    //console.log('callTask called');

    if (this.user.userType1 == 'master') {
      this.httpClient.get<UpperTask[]>(this.baseUrl + 'pritam/masterdata/upperWorkTask?ShopName=' + this.user.shopName + '&Days=' + this.taskBeforeDays + '&isTestData=' + this.user.isTestData+'&AssignTo='+0)
        .subscribe(result => {
          //console.log(result);
          this.upperCuttingTask = result;
          //console.log(this.upperCuttingTask);

          if (this.upperCuttingTask[0]) {
            this.noUpperCuttingTask = false;
          }
          else {
            this.noUpperCuttingTask = true;
          }
        });

      this.httpClient.get<LowerTask[]>(this.baseUrl + 'pritam/masterdata/lowerWorkTask?ShopName='
        + this.user.shopName + '&Days=' + this.taskBeforeDays + '&isTestData=' + this.user.isTestData + '&AssignTo=' + 0)
        .subscribe(result => {
          //console.log(result);
          this.lowerCuttingTask = result;
          //console.log(this.lowerCuttingTask);

          if (this.lowerCuttingTask[0]) {
            this.noLowerCuttingTask = false;
          }
          else {
            this.noLowerCuttingTask = true;
          }
        });
    }

    this.httpClient.get<UpperTask[]>(this.baseUrl + 'pritam/masterdata/upperWorkTask?ShopName='
      + this.user.shopName + '&Days=' + this.taskBeforeDays + '&isTestData=' + this.user.isTestData + '&AssignTo=' + this.user.userId)
      .subscribe(result => {
        //console.log(result);
        this.upperStichingTask = result;
        //console.log(this.upperStichingTask);

        if (this.upperStichingTask[0]) {
          this.noUpperStichingTask = false;
        }
        else {
          this.noUpperStichingTask = true;
        }
      });

    this.httpClient.get<LowerTask[]>(this.baseUrl + 'pritam/masterdata/lowerWorkTask?ShopName='
      + this.user.shopName + '&Days=' + this.taskBeforeDays + '&isTestData=' + this.user.isTestData + '&AssignTo=' + this.user.userId)
      .subscribe(result => {
        //console.log(result);
        this.lowerStichingTask = result;
        //console.log(this.lowerStichingTask);

        if (this.lowerStichingTask[0]) {
          this.noLowerStichingTask = false;
        }
        else {
          this.noLowerStichingTask = true;
        }
      });
    
  }
}
