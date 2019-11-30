import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { OnInit } from '@angular/core/src/metadata/lifecycle_hooks';
import { CryptocurrencyCardService } from 'src/app/services/cryptocurrency-card.service';
import { ICryptocurrencyCard } from 'src/app/cryptocurrency-card/cryptocurrency-card.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {

  cryptocurrencyCards: ICryptocurrencyCard[];

  constructor(private cryptocurrencyCardService: CryptocurrencyCardService) {
  }

  ngOnInit(): void {
    this.cryptocurrencyCardService
      .getCards()
      .subscribe((result: ICryptocurrencyCard[]) => this.cryptocurrencyCards = result);
  }
}
