import { Component } from '@angular/core';
import { OnInit } from '@angular/core/src/metadata/lifecycle_hooks';
import { CryptocurrencyCardService } from '../services/cryptocurrency-card.service';
import { Input } from '@angular/core';
import { ICryptocurrencyCard } from 'src/app/cryptocurrency-card/cryptocurrency-card.model';
import { Router } from '@angular/router';

@Component({
    selector: 'app-cryptocurrencycard',
    templateUrl: './cryptocurrency-card.component.html',
    styleUrls: ['./cryptocurrency-card.component.css']
})
export class CryptocurrencyCardComponent {
    @Input() cryptocurrencyCard: ICryptocurrencyCard;

    constructor(private cryptocurrencyCardService: CryptocurrencyCardService,
        private router: Router) {
    }

    goToDetails(id: number) {
        this.router.navigate([`details/${id}`]);
    }
}
