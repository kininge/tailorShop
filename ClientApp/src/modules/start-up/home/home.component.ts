import { Component, OnInit } from '@angular/core';

import { AllDataService } from 'src/modules/app/all-data.service';
import { UserData } from 'src/modules/shared/interfaces/user-data.interface';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit
{
  private user: UserData = undefined;

  constructor(private commonData: AllDataService) { }

  ngOnInit() {
    this.user= this.commonData.getUser();
  }
}
