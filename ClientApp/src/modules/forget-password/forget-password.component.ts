import { Component, Inject, OnInit} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { MatStepper } from '@angular/material';

import { AllDataService } from 'src/modules/app/all-data.service';
import { Security } from 'src/modules/shared/interfaces/security.interface';

@Component({
  selector: 'app-forget-password',
  templateUrl: './forget-password.component.html',
  styleUrls: ['./forget-password.component.scss']
})
export class ForgetPasswordComponent implements OnInit
{

  //forgetPassword
  private forgetPasswordUsername: string = undefined;
  private forgetPasswordQuestion: string = undefined;
  private forgetPasswordAnswer: string = undefined;
  private forgetPasswordPassword: string = undefined;
  private forgetPasswordRePassword: string = undefined;
  private forgetPasswordPasswordVisibility: string = 'password';
  private forgetPasswordRePasswordVisibility: string = 'password';
  private forgetPasswordStatus: boolean = false;
  private userNameAvailable: boolean = false;
  private isAnswerRight: boolean = false;
  private makeDesable: boolean = false;
  private count: number = 3;
  private minutes: number = undefined;
  private seconds: number = undefined;

  constructor(private commonService: AllDataService, private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router: Router) { }

  ngOnInit()
  {

  }

  goPreviousPage() {
      this.router.navigateByUrl('/logIn');
  }

  usernameAvailable() {
    //console.log(this.forgetPasswordUsername);

    this.httpClient.get<boolean>(this.baseUrl + 'pritam/masterdata/checkUserName?userName=' + this.forgetPasswordUsername)
      .subscribe(result => {
        this.userNameAvailable = result;
        //console.log('userNameAvailable: ' + this.userNameAvailable);

        if (this.userNameAvailable) {
          this.httpClient.get<Security>(this.baseUrl + 'pritam/masterdata/getQuestion?userName=' + this.forgetPasswordUsername)
            .subscribe(result => {
              this.forgetPasswordQuestion = result.question;
              //console.log(this.forgetPasswordQuestion);
            },
              error => {
                //console.log(error);
                this.forgetPasswordQuestion = undefined;
              });
        }
        else {
          this.userNameAvailable = false;
        }
      },
        error => {
          //console.log(error);
          this.userNameAvailable = false;
        });
  }

  checkAnswer() {
    //console.log('Checking username: ' + this.forgetPasswordUsername + ' answer: ' + this.forgetPasswordAnswer);

    this.httpClient.get<boolean>(this.baseUrl + 'pritam/masterdata/checkAnswer?userName=' + this.forgetPasswordUsername + '&answer=' + this.forgetPasswordAnswer)
      .subscribe(result => {
        this.isAnswerRight = result;
        this.forgetPasswordStatus = false;
        //console.log('isAnswerRight: ' + this.isAnswerRight);

        //Count down dsable
        if (!this.isAnswerRight)
        {
          this.count--;

          if (this.count == 0)
          {

            setTimeout(() => {
              this.goPreviousPage();
            }, 200);
            
          }
        }
      },
        error => {
          //console.log(error);
          this.isAnswerRight = false;

          this.count--;

          if (this.count == 0) {
            setTimeout(() => {
              this.goPreviousPage();
            }, 200);
          }
        });
  }

  chnagePassword(form: NgForm, step: MatStepper) {

    this.httpClient.get<boolean>(this.baseUrl + 'pritam/masterdata/chnagePasssword?userName=' + this.forgetPasswordUsername + '&password=' + this.forgetPasswordPassword)
      .subscribe(result =>
      {
        if (result) {
          this.userNameAvailable = false;
          this.isAnswerRight = false;
          step.reset();
          form.reset();
          this.goPreviousPage();
        }
        else {
          this.userNameAvailable = false;
          this.isAnswerRight = false;
          step.reset();
          form.reset();
          this.forgetPasswordStatus = true;
        }
      },
      error =>
      {
        this.userNameAvailable = false;
        this.isAnswerRight = false;
        step.reset();
        form.reset();
        this.forgetPasswordStatus = true;
      });
  }

  changePasswordVisibility(forChange: string) {
    if (forChange == 'registerPassword') {
      if (this.forgetPasswordPasswordVisibility == 'text') {
        this.forgetPasswordPasswordVisibility = 'password';
      }
      else {
        this.forgetPasswordPasswordVisibility = 'text';
      }
    }
    else {
      if (this.forgetPasswordRePasswordVisibility == 'text') {
        this.forgetPasswordRePasswordVisibility = 'password';
      }
      else {
        this.forgetPasswordRePasswordVisibility = 'text';
      }
    }
  }
 
}

