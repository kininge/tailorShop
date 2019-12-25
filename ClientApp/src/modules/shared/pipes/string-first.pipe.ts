import { Pipe, PipeTransform } from '@angular/core';

@Pipe
({
  name: 'stringFirst'
})
export class StringFirstPipe implements PipeTransform 
{

  transform(inputString: any): string 
  {
    //console.log('stringFirst: ' + inputString);
    if (inputString != null) {
      return inputString.split(" ")[0];
    }
    else {
      return '';
    }
  }
}
