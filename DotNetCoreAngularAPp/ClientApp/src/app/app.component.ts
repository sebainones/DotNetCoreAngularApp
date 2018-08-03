import { Component } from '@angular/core';
import { Alert } from 'selenium-webdriver';
//import { Http } from '@angular/http';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

@Injectable()
//export -Now visible outside this file
export class AppComponent {
  title = 'ClientApp Sebas';

  constructor(private http: HttpClient) {
  }

  GetWeather() {

    this.title = "Weather";
    //console.log("Clima");
    //let currentDate = " hoy";

    //let weatherDeHoy = "Weather de " + currentDate;

    this.http.get('/api/SampleData').subscribe(data => {

      console.log(data);
      this.title = data[0];
    }
    );  
  }
};


//Test this code trough node cli
//let appComponent = new AppComponent();
//var weatherObtenido = appComponent.GetWeather();
//console.log(weatherObtenido);
