//source: https://medium.com/google-developer-experts/angular-2-introduction-to-new-http-module-1278499db2a0
// main.ts

//Besides the changes to our components using Http, we also need to include HttpModule during bootstrap.

import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
//import { HttpModule } from '@angular/http';  -->Old one??
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    // import HttpClientModule after BrowserModule.
    HttpClientModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
