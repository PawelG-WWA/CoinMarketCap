import { Component } from '@angular/core';
import { OnInit } from '@angular/core';
import { CryptocurrencyDetailsService } from 'src/app/services/cryptocurrency-details.service';
import { ActivatedRoute } from '@angular/router';
import { ICryptocurrencyDetails } from 'src/app/cryptocurrency-details/cryptocurrency-details.model';

@Component({
    selector: 'app-cryptocurrency-details',
    templateUrl: './cryptocurrency-details.component.html',
    styleUrls: ['cryptocurrency-details.component.css']
})
export class CryptocurrencyDetailsComponent implements OnInit {

    cryptocurrency: ICryptocurrencyDetails;

    constructor(private cryptocurrencyDetailsService: CryptocurrencyDetailsService,
        private route: ActivatedRoute) {}

    ngOnInit(): void {
        this.cryptocurrencyDetailsService.getDetails(this.route.snapshot.params['id'])
            .subscribe((result: ICryptocurrencyDetails) => this.cryptocurrency = result);
    }
}
