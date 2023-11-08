import { Component } from '@angular/core';
import { faFacebook, faInstagram, faTwitter, faWhatsapp } from '@fortawesome/free-brands-svg-icons'

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

}
