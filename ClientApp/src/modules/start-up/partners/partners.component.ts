import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { AllDataService } from 'src/modules/app/all-data.service';
import { UserData } from 'src/modules/shared/interfaces/user-data.interface';

@Component({
  selector: 'app-partners',
  templateUrl: './partners.component.html',
  styleUrls: ['./partners.component.scss']
})
export class PartnersComponent implements OnInit 
{
  private loggedUser: UserData = undefined;
  private userId: number = undefined;
  private registrStatus: boolean = false;

  //Personal Data
  private name: string = undefined;
  private emailAddress: string = undefined;
  private mobileNo: number = undefined;
  private isOnWhatsApp: boolean = false;
  private birthdate: Date = undefined;
  private address: string = '';
  private isTestData: boolean = false;

  //Organizational Data
  private shopId: number = undefined;
  private shops: { shopId: number, shop: string, branch: string }[] = undefined;
  private userTypeId: number = undefined;
  private userType: { userTypeId: number, userType: string }[] = undefined;
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

  constructor(private commonData: AllDataService, private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router: Router) { }

  ngOnInit()
  {
      this.loggedUser = this.commonData.getUser();

      if (this.loggedUser.userId != 1) {
          this.isTestData = this.loggedUser.isTestData;
      }

    this.httpClient.get<any>(this.baseUrl + 'pritam/masterdata/shop')
      .subscribe(result => {
        this.shops = result;
      },
        error => {
          //console.log(error);
        });

    this.httpClient.get<any>(this.baseUrl + 'pritam/masterdata/security')
      .subscribe(result => {
        this.quetions = result;
      },
        error => {
         //console.log(error);
      });

    this.httpClient.get<any>(this.baseUrl + 'pritam/masterdata/userType')
      .subscribe(result => {
        this.userType = result;
      },
        error => {
          //console.log(error);
        });
  }

  /* Address */
  getAddres(val: any) {
    this.address = val.target.value;
  }

  /* WhatsApp */
  getWhatsApp() {
    this.isOnWhatsApp = !this.isOnWhatsApp;
  }

  /* Test data */
  getTestDataStatus() {
    this.isTestData = !this.isTestData;
  }

  /*Password visibility */
  changePasswordVisibility(valueToSet: string, forWhat: string) {
    if (forWhat === 'registerPassword') {
      this.registerPasswordVisibility = valueToSet;
    }
    else {
      this.registerRePasswordVisibility = valueToSet;
    }
  }

  //Register
  register() {
    var userId: number = undefined;

    this.httpClient.post<number>(this.baseUrl + 'pritam/log/insertUserDetails',
      {
        'name': this.name, 'emailAddress': this.emailAddress, 'mobileNo': this.mobileNo, 'isOnWhatsApp': this.isOnWhatsApp, 'birthdate': this.birthdate,
        'address': this.address, 'shopId': this.shopId, 'createdOn': new Date(), 'createdBy': this.loggedUser.userId, 'updatedOn': null, 'updatedBy': null, 'isTestData': this.isTestData,
        'isDelete': false, 'userId': userId, 'username': this.registerUsername, 'password': this.registerPassword, 'userTypeId': this.userTypeId, 'questionId': this.questionId,
        'answer': this.registerAnswer
      }).subscribe(result => {
        this.userId = result;
        this.registrStatus = true;
        this.resetAll();
        this.userIdValueErase();
      },
        error => {
          //console.log(error);
          this.resetAll();
          this.registrStatus = true;
        });

  }

  resetAll() {
    //Personal Data
    this.name= undefined;
    this.emailAddress= undefined;
    this.mobileNo= undefined;
    this.isOnWhatsApp= false;
    this.birthdate= undefined;
    this.address= '';

  //Organizational Data
    this.shopId= undefined;
    this.userTypeId= undefined;
    this.questionId= undefined;
    this.registerAnswer= undefined;

  //LogIn Data
    this.isUsernameAvailable= undefined;
    this.registerUsername= undefined;
    this.registerPassword= undefined;
    this.registerRePassword= undefined;
    this.registerPasswordVisibility= 'password';
    this.registerRePasswordVisibility= 'password';
  }

  userIdValueErase()
  {
    setTimeout(() =>
    {
      this.userId = undefined;
      this.registrStatus = false;
    },
      3000);
  }

  //username checking
  checkUsername() {
      if (this.registerUsername != '')
      {
          this.httpClient.get<boolean>(this.baseUrl + 'pritam/log/check?userName='+this.registerUsername)
        .subscribe(result => {
          this.isUsernameAvailable = result;
        },
          error => {
            //console.log(error);
          });
    }

  }
}
