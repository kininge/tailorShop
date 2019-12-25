import { Pipe, PipeTransform } from '@angular/core';

@Pipe
({
  name: 'upperCase'
})
export class UpperCasePipe implements PipeTransform 
{

  transform(inputString: string): string 
  {
    //console.log('upperCase: ' + inputString);
    if (inputString != null) {
      var start = inputString.substring(0, 1);
      var rest = inputString.substring(1);
      //console.log('start: ' + start.toUpperCase() + ' rest: ' + rest.toLowerCase());
      return start.toUpperCase() + rest.toLowerCase();
    }
    else {
      return '';
    }
  }
}
