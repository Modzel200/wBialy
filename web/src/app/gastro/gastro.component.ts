import { Component } from '@angular/core';

@Component({
  selector: 'app-gastro',
  templateUrl: './gastro.component.html',
  styleUrls: ['./gastro.component.scss']
})
export class GastroComponent {


getDayName(dateStr: string | number | Date, locale: Intl.LocalesArgument)
{
    var date = new Date(dateStr);
    return date.toLocaleDateString(locale, { weekday: 'long' });        
}

 dateStr = new Date();
 day = this.getDayName(this.dateStr, "pl-PL");

 selectedToggleValue: string = this.day;
 
}
