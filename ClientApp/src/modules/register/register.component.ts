import { Component, Inject, OnInit, ViewChild, Output, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { MatStepper } from '@angular/material';

import { AllDataService } from 'src/modules/app/all-data.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit
{
  private today: Date = new Date();
  private registerState: boolean = false;

  //Personal Data
  private name: string = undefined;
  private emailAddress: string = undefined;
  private mobileNo: number = undefined;
  private isOnWhatsApp: boolean = false;
  private birthdate: Date = undefined;
  private address: string = '';

  //Organizational Data
  private shopId: number = undefined;
  private shops: { shopId: number, shop: string, branch: string }[] = undefined;
  private questionId: number = undefined;
  private quetions: { questionId: number, question: string }[] = undefined;
  private registerAnswer: string = undefined;

  //LogIn Data
  private isUsernameAvailable: boolean = undefined;
  private registerUsername: string = undefined;
  private registerPassword: string = undefined;
  private registerRePassword: string = undefined;
  private registerPasswordVisibility: string = 'password';
  private registerRePasswordVisibility: string = 'password';

  constructor(private commonService: AllDataService, private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router: Router) { }

  ngOnInit()
  {
    this.httpClient.get<any>(this.baseUrl + 'pritam/masterdata/shop')
      .subscribe(result =>
      {
        this.shops = result;
      },
      error =>
      {
        //console.log(error);
      });

    this.httpClient.get<any>(this.baseUrl + 'pritam/masterdata/security')
      .subscribe(result =>
      {
        this.quetions = result;
      },
      error =>
      {
        //console.log(error);
      });
  }

  goPreviousPage() {
      this.router.navigateByUrl('/logIn');
  }

  getAddres(val: any)
  {
    this.address = val.target.value;
  }

  getWhatsApp()
  {
    this.isOnWhatsApp = !this.isOnWhatsApp;
  }

  register(form: NgForm, step: MatStepper)
  {
    var userId: number = undefined;
    
    this.httpClient.post<number>(this.baseUrl + 'pritam/log/insertUserDetails',
    {
      'name': this.name, 'emailAddress': this.emailAddress, 'mobileNo': this.mobileNo, 'isOnWhatsApp': this.isOnWhatsApp, 'birthdate': this.birthdate,
      'address': this.address, 'shopId': this.shopId, 'createdOn': new Date(), 'createdBy': null, 'updatedOn': null, 'updatedBy': null, 'isTestData': false,
      'isDelete': false, 'userId': userId, 'username': this.registerUsername, 'password': this.registerPassword, 'userTypeId': 4, 'questionId': this.questionId,
      'answer': this.registerAnswer
    }).subscribe(result =>
    {
      //console.log('userId: ' + result);
      form.reset();
      step.reset();
      this.goPreviousPage();
    },
    error =>
    {
      form.reset();
      step.reset();
      this.registerState = true;
      //console.log(error);
    });

  }

  changePasswordVisibility(forChange: string)
  {
    if (forChange== 'registerPassword')
    {
      if (this.registerPasswordVisibility == 'text')
      {
        this.registerPasswordVisibility = 'password';
      }
      else
      {
        this.registerPasswordVisibility = 'text';
      }
    }
    else
    {
      if (this.registerRePasswordVisibility == 'text')
      {
        this.registerRePasswordVisibility = 'password';
      }
      else
      {
        this.registerRePasswordVisibility = 'text';
      }
    }
  }

  checkUsername() {
    if (this.registerUsername != '')
    {
      this.httpClient.get<boolean>(this.baseUrl + 'pritam/log/check?userName='+ this.registerUsername)
        .subscribe(result => {
          this.isUsernameAvailable = result;
        },
        error => {
          //console.log(error);
        });
    }
    
  }
}

