import { CryptocurrencyCardService } from './cryptocurrency-card.service';
import { CryptocurrencyDetailsService } from 'src/app/services/cryptocurrency-details.service';

export const CRYPTOCURRENCY_CARD_SERVICE_PROVIDER: Array<any> = [
    { provide: CryptocurrencyCardService, useClass: CryptocurrencyCardService },
    { provide: CryptocurrencyDetailsService, useClass: CryptocurrencyDetailsService }
];
