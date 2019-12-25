import { Pipe, PipeTransform } from '@angular/core';

@Pipe
({
  name: 'sort'
})
export class SortPipe implements PipeTransform 
{
  transform(inputArray: any, element: any): any 
  {
    //console.log('sort: ' + element);

    if (inputArray != null && element != null) {
      inputArray.sort((a: any, b: any) => {
        if (a[element] > b[element]) {
          return -1;
        }
        else if (a[element] < b[element]) {
          return 1;
        }
        else {
          return 0;
        }

      })

      return inputArray;
    }
    else {
      return inputArray;
    }
      
  }

}
