import { Component, OnInit } from '@angular/core';
import { UserData } from 'src/modules/shared/interfaces/user-data.interface';
import { AllDataService } from 'src/modules/app/all-data.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-start-up',
  templateUrl: './startUp.component.html',
  styleUrls: ['./startUp.component.scss']
})
export class StartUpComponent implements OnInit
{
    private user: UserData = undefined;
    private today: Date = new Date();
    private year = this.today.getFullYear();

  constructor(private commonData: AllDataService, private router: Router) {}

  ngOnInit()
  {
      this.user = this.commonData.getUser();
  }

  logOut()
  {
    this.commonData.logOut();
    this.router.navigate(['']);
  }

}
