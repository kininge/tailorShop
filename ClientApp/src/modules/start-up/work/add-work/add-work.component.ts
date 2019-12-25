import { Component, OnInit, Inject, Output, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { AllDataService } from 'src/modules/app/all-data.service';

import { UserData } from 'src/modules/shared/interfaces/user-data.interface';
import { UserDetails } from 'src/modules/shared/interfaces/userDetails.interface';
import { Product } from 'src/modules/shared/interfaces/product.interface';

@Component({
  selector: 'app-add-work',
  templateUrl: './add-work.component.html',
  styleUrls: ['./add-work.component.scss']
})
export class AddWorkComponent implements OnInit
{
  private loggedUser: UserData = undefined;
  private allUsers: { UserId: number, Name: string, MobileNo: string }[] = undefined;
  @Output() goBackPage = new EventEmitter<{ pageName: string } > ();

  private workId: number = undefined;
  private customerId: number = undefined;
  private whatsAppChecked: boolean = undefined;
  private emailChecked: boolean = undefined;
  private stichwellApp: boolean = true;
  private customerTableDetails: UserDetails = { userId: NaN, name: '', emailAddress: '', mobileNo: '', isOnWhatsApp: false, birthdate: new Date(), address: '', shopId: NaN };
  private deliveryDate: Date = undefined;
  private advance: number = 0;
  private today: Date = new Date();
  private products: Product[] = [{ productId: 1, clothName: undefined, clothType: undefined, remark: undefined, price: undefined,
                                    upper: { height: 0, front: 0, collar: 0, shoulder: 0, sleeve: 0, sleeveWidth: 0, cuff: 0 },
                                    lower: { height: 0, knee: 0, waist: 0, seat: 0, thigh: 0, bottom: 0, underline: 0 }
                                }];
  private productNameList: string[] = ['Shirt', 'Pant', 'Safari Shirt', 'Safari Pant'];
  private printingHeaders: string[] = ['productId', 'clothName', 'price'];

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string, private commonData: AllDataService) { }

  ngOnInit()
  {
    //Get loged in user's data
    this.loggedUser = this.commonData.getUser();

    //allUsers
    this.httpClient.get<{ UserId: number, Name: string, MobileNo: string }[]>(this.baseUrl + 'pritam/masterdata/allUser?isTestUser=' + this.loggedUser.isTestData + '&shopName=' + this.loggedUser.shopName)
      .subscribe(result => {
        //console.log(result);
        this.allUsers = result;
        console.log('allUsers')
        console.log(this.allUsers);
      },
        error => {
          console.log(error);
        });
  }

  getTotalCost()
  {
    return this.products.map(t => t.price).reduce((acc, value) => acc + value, 0);
  }

  addNewProduct()
  {
    const prodId = this.products.length;

    this.products.push
    ({
      productId: prodId + 1, clothName: undefined, clothType: undefined, remark: undefined, price: undefined,
      upper: { height: 0, front: 0, collar: 0, shoulder: 0, sleeve: 0, sleeveWidth: 0, cuff: 0 },
      lower: { height: 0, knee: 0, waist: 0, seat: 0, thigh: 0, bottom: 0, underline: 0 }
    });
  }

  removeLastProduct(index: number)
  {
    this.products.splice((index - 1), 1);
    //console.log('Removed: ' + (index - 1)+'index');
  }

  createWork()
  {
    this.httpClient.post<number>(this.baseUrl + 'pritam/works/createWork',
      {
        'UserId': this.customerTableDetails.userId, 'CreatedOn': new Date(), 'CreatedBy': this.loggedUser.userId, 'UpdatedOn': null, 'UpdatedBy': null,
        'DeliveryDate': this.deliveryDate, 'DeliveredOn': null, 'Advance': this.advance
      }).subscribe(
      result =>
      {
        console.log('workId: ' + result);
        this.workId = result;
        console.log('workId: ' + this.workId);
        this.createProducts();

      },

      error =>
      {
        //console.log(error);
      });

  }

  createProducts()
  {
    this.products.forEach(product =>
    {
      //console.log('product: ' + product.clothType.toString() + ' typeof: ' + (typeof product.clothType.toString()));
      if (product.clothType.toString() == 'true')
      {
        //console.log('clothType: ' + product.clothType);
        this.httpClient.post<number>(this.baseUrl + 'pritam/works/createUpperBody',
          {
            'WorkId': this.workId, 'ClothId': product.productId, 'ClothName': product.clothName, 'Height': product.upper.height, 'Front': product.upper.front,
            'Collar': product.upper.collar, 'Shoulder': product.upper.shoulder, 'Sleeve': product.upper.sleeve, 'SleeveWidth': product.upper.sleeveWidth,
            'Cuff': product.upper.cuff, 'Note': product.remark, 'Status': 2, 'Price': product.price, 'AssignTo': null, 'PaidTo': null, 'FeedbackId': null
          }).subscribe(
          result =>
          {
            //console.log('upper: ' + result);
          },
          error =>
          {
            console.log(error);
          });
      }
      else
      {
        //console.log('clothType: ' + product.clothType);
        this.httpClient.post<number>(this.baseUrl + 'pritam/works/createLowerBody',
          {
            'WorkId': this.workId, 'ClothId': product.productId, 'ClothName': product.clothName, 'Height': product.lower.height, 'Knee': product.lower.knee,
            'Waist': product.lower.waist, 'Seat': product.lower.seat, 'Thigh': product.lower.thigh, 'Bottom': product.lower.bottom, 'Underline': product.lower.underline,
            'Note': product.remark, 'Status': 2, 'Price': product.price, 'AssignTo': null, 'PaidTo': null, 'FeedbackId': null
          }).subscribe(
          result =>
          {
            //console.log('lower: ' + result);
          },
          error =>
          {
            console.log(error);
          });

      }
    });
  }

  getUserDetails()
  {
    console.log('id: ' + this.customerId);

    this.httpClient.get<UserDetails>(this.baseUrl + 'pritam/masterdata/perticularUser?id=' + this.customerId).subscribe
      (result =>
      {
        //console.log(result);
        this.customerTableDetails = result;
        //console.log(this.customerTableDetails);
      },
      error =>
      {
        //console.log(error);
      })
  }

  goBack() {
    console.log('goBack fom newWork');
    this.goBackPage.emit({ pageName: 'news' });
  }
}
