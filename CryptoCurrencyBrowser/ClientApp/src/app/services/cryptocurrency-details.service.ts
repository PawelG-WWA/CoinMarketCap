import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class CryptocurrencyDetailsService {

    constructor(private http: HttpClient) {}

    public getDetails(id: number) {
        return this.http
            .get(`https://localhost:44332/api/Cryptocurrency/${id}`);
    }
}
