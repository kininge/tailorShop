import { Pipe, PipeTransform } from '@angular/core';

@Pipe
({
  name: 'filter'
})
export class FilterPipe implements PipeTransform 
{
  transform(inputArray: any, element: any, searchFor: string): any 
  {
    //console.log('filter: ' + element + 'typeof ' + typeof element);
    //console.log(inputArray);
    //console.log('searchFor: ' + searchFor);
    const outputArray: any[] = []; 

    if (inputArray != null && element != null) {
      //searchfor userId
      if ((searchFor == 'userId') && Number(element)) {
        for (const item of inputArray) {
          //console.log('item: ' + item.name);
          if (item.userId.toString().includes(element.toString())) {
            outputArray.push(item);
          }
        }
        //console.log('userId');
        //console.log(outputArray);
        return outputArray;
      }
      //searchfor name
      else if ((searchFor == 'name' || searchFor == '') && (typeof element == 'string')) {
        for (const item of inputArray) {
          //console.log('item: ' + item.name);
          if (typeof item.name === 'string') {
            if (item.name.toLowerCase().includes(element.toString().toLowerCase())) {
              outputArray.push(item);
            }
          }
        }
        //console.log('name');
        //console.log(outputArray);
        return outputArray;
      }
      //searchfor workId
      else if ((searchFor == 'workId') && Number(element)) {
        for (const item of inputArray) {
          //console.log('item: ' + item.name);
          if (item.workId.toString().includes(element.toString())) {
            outputArray.push(item);
          }
        }
        //console.log('workId');
        //console.log(outputArray);
        return outputArray;
      }
      
    }
    else
    {
      return outputArray;
    }
  }
}
