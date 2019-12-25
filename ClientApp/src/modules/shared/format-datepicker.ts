import { NativeDateAdapter } from '@angular/material';
import { MatDateFormats } from '@angular/material/core';
import { Injectable } from '@angular/core';
import { Platform } from '@angular/cdk/platform';

@Injectable()
export class AppDateAdapter extends NativeDateAdapter
{
  private months: string[] = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];

  format(date: Date, displayFormat: Object): string
  {
      let day: string = date.getDate().toString();
      day = +day < 10 ? '0' + day : day;
      let month: string = this.months[date.getMonth()];
      let year = date.getFullYear();

      return day + ' ' + month + ' ' + year;
  }
}
export const APP_DATE_FORMATS: MatDateFormats =
{
  parse: { dateInput: { month: 'shor', year: 'numeric', day: 'numeric' },},
  display:
  {
    dateInput: 'input', monthYearLabel: { year: 'numeric', month: 'numeric' },
    dateA11yLabel: { year: 'numeric', month: 'long', day: 'numeric' },
    monthYearA11yLabel: { year: 'numeric', month: 'long' },
  }
};
