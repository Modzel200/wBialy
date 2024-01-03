import { Component } from '@angular/core';
import { faFacebook, faInstagram, faTwitter, faWhatsapp } from '@fortawesome/free-brands-svg-icons'
import {TranslateService} from "@ngx-translate/core";

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent {

  faFacebook=faFacebook;
  faInstagram=faInstagram;
  faTwitter=faTwitter;
  faWhatsapp=faWhatsapp;
  year=(new Date()).getFullYear();

  constructor(private translate: TranslateService) {
    this.translate.setDefaultLang('pl');
  }

  switchLanguage(language: string){
    this.translate.use(language);
  }
}
