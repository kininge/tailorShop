import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

import { AllDataService } from 'src/modules/app/all-data.service';
import { UserData } from 'src/modules/shared/interfaces/user-data.interface';

@Component({
  selector: 'app-logIn',
  templateUrl: './logIn.component.html',
  styleUrls: ['./logIn.component.scss']
})
export class LogInComponent implements OnInit
{

  //LogIn variables
  private logInState: boolean = undefined;
  private userName: string = undefined;
  private password: string = undefined;
  private passwordVisibility: string = 'password';

  constructor(private commonService: AllDataService, private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router: Router) { }

  ngOnInit()
  {

  }

  changePage(page: string)
  {
      if (page == 'register')
      {
          this.router.navigateByUrl('/register');
      }
      else
      {
          this.router.navigateByUrl('/forget-password');
      }
  }

  logIn()
  {
    this.httpClient.get<UserData>(this.baseUrl + 'pritam/log/auth?userName=' + this.userName + '&password=' + this.password)
        .subscribe(result =>
    {
        //console.log(result);
        this.commonService.stichwellLogIn(result);
        this.logInState = true;
        this.router.navigate(['/application']);
    },
    error =>
    {
      //console.log(error);
      this.logInState = false;
    });
  }

  changePasswordVisibility()
  {
    if (this.passwordVisibility == 'text') {
      this.passwordVisibility = 'password';
    }
    else {
      this.passwordVisibility = 'text';
    }
  }
}

