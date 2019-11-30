import { Component } from '@angular/core';
import { OnInit } from '@angular/core';
import { CryptocurrencyDetailsService } from 'src/app/services/cryptocurrency-details.service';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-cryptocurrency-details',
    templateUrl: './cryptocurrency-details.component.html'
})
export class CryptocurrencyDetailsComponent implements OnInit {

    constructor(private cryptocurrencyDetailsService: CryptocurrencyDetailsService,
        private route: ActivatedRoute) {}

    ngOnInit(): void {
        this.cryptocurrencyDetailsService.getDetails(this.route.snapshot.params['id'])
            .subscribe(result => console.log(result));
    }

}
