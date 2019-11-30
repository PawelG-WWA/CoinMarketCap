import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class CryptocurrencyCardService {

    constructor(private http: HttpClient) {}

    public getCards() {
        return this.http
            .get('https://localhost:44332/api/CryptocurrencyCards');
    }
}


