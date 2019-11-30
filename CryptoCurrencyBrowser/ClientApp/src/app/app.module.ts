import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { CryptocurrencyCardComponent } from 'src/app/cryptocurrency-card/cryptocurrency-card.component';
import { CryptocurrencyDetailsComponent } from 'src/app/cryptocurrency-details/cryptocurrency-details.component';

import { CRYPTOCURRENCY_CARD_SERVICE_PROVIDER } from 'src/app/services/service-provider';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    CryptocurrencyCardComponent,
    CryptocurrencyDetailsComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'details/:id', component: CryptocurrencyDetailsComponent }
    ])
  ],
  providers: [
    CRYPTOCURRENCY_CARD_SERVICE_PROVIDER
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
