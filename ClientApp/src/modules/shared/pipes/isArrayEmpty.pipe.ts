import { Pipe, PipeTransform } from '@angular/core';

@Pipe
({
  name: 'isarrayempty'
})
export class IsArrayEmptyPipe implements PipeTransform 
{

  transform(inputString: any): boolean 
  {
    for (var key in inputString)
    {
      if (inputString.hasOwnProperty(key))
      {
        return false;
      }
    }
    return true;
  }
}
