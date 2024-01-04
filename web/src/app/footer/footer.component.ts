import { Component } from '@angular/core';
import { faFacebook, faInstagram, faTwitter, faWhatsapp } from '@fortawesome/free-brands-svg-icons'
import { TranslateService } from "@ngx-translate/core";

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent {

  faFacebook = faFacebook;
  faInstagram = faInstagram;
  faTwitter = faTwitter;
  faWhatsapp = faWhatsapp;
  year = (new Date()).getFullYear();

  constructor(private translate: TranslateService) {
    const storedLanguage = localStorage.getItem('language');
    storedLanguage !== null ? this.translate.setDefaultLang(storedLanguage) : this.translate.setDefaultLang('pl');
  }

  switchLanguage() {
    const currentLanguage = this.translate.currentLang;
    const newLanguage = currentLanguage === 'pl' ? 'en' : 'pl';
    localStorage.setItem('language', newLanguage);
    this.translate.use(newLanguage);
  }
}
