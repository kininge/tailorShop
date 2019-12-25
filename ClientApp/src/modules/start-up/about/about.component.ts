import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

import { AllDataService } from 'src/modules/app/all-data.service';
import { UserData } from 'src/modules/shared/interfaces/user-data.interface';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.scss']
})
export class AboutComponent implements OnInit
{
  private shopName: string = undefined;
  private branchName: string = undefined;

  constructor(private commonData: AllDataService, private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router: Router) { }

  ngOnInit() {
  
  }

  createShop() {
    console.log('shop: ' + this.shopName + ' branch: ' + this.branchName);

    this.httpClient.post<Boolean>(this.baseUrl + 'pritam/works/createShop', { 'shopName': this.shopName, 'branchName': this.branchName })
      .subscribe(result => {
        console.log('shop get created: ' + result);
        this.shopName = undefined;
        this.branchName = undefined;
      },
      error => {
        console.log(error);
      });
  }
}

