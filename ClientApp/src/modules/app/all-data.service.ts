import { Injectable, Inject } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { ActivatedRouteSnapshot, RouterStateSnapshot, CanActivate, Router } from '@angular/router';
import { Observable } from 'rxjs';

import { UserData } from 'src/modules/shared/interfaces/user-data.interface';


@Injectable
({
  providedIn: 'root'
})

export class AllDataService implements CanActivate
{
  private logedUserData: UserData = undefined;

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean>| Promise<boolean> | boolean
  {
      if (this.logedUserData != undefined)
      {
      return true;
      }
      else
      {
          this.router.navigateByUrl('/logIn');
          return false;
      }
  }

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router: Router) { }

  getLogInStatus()
  {
    if (this.logedUserData != undefined)
    {
      return true;
    }
    else
    {
      return false;
    }
  }

  stichwellLogIn(userData: UserData)
  {
    this.logedUserData = userData;
  }

  getUser()
  {
    return this.logedUserData;
  }

  logOut()
  {
    this.logedUserData = undefined;
  }
}

