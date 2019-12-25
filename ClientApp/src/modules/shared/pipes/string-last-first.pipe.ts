import { Pipe, PipeTransform } from '@angular/core';

@Pipe
({
  name: 'stringFirstAndLast'
})
export class StringFirstAndLastPipe implements PipeTransform 
{
  transform(inputString: any): string 
  {
    //console.log('stringFirstAndLast: ' + inputString);

    if (inputString != null)
    {
      if (inputString == '')
      {
        return inputString;
      }

      var name: string[] = inputString.split(" ");

      //name.forEach(names =>console.log(names));

      if (name.length == 1)
      {
        return inputString;
      }
      else if (name.length == 2)
      {
        return inputString;
      }
      else
      {
        return (name[0] + ' ' + name[name.length - 1]);
      }
    }
    else {
      return '';
    }
    
  }
}
