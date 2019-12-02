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
  private pageNumber = 0;

  constructor(private cryptocurrencyCardService: CryptocurrencyCardService) {
  }

  ngOnInit(): void {
    this.getCardsPage(++this.pageNumber);
  }

  nextPage(): void {
    this.getCardsPage(++this.pageNumber);
  }

  private getCardsPage(pageNumber: number) {
    this.cryptocurrencyCardService
      .getCards(pageNumber)
      .subscribe((result: ICryptocurrencyCard[]) => this.cryptocurrencyCards = result.sort((x, y) => x.rank - y.rank));
  }
}
