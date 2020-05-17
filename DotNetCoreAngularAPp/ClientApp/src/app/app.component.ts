import { Component } from '@angular/core';
import { Alert } from 'selenium-webdriver';
//import { Http } from '@angular/http';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { WeatherForeCast } from './WeatherForeCast';
import { Observable } from 'rxjs';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

@Injectable()
//export -Now visible outside this file
export class AppComponent {
  title = 'ClientApp Sebastian Inones';
  foreCast: Observable<WeatherForeCast>;
  myCurrentForecast: WeatherForeCast;

  constructor(private http: HttpClient) {
  }

  GetWeather() {

    this.title = "Clima button has been pressed!";
    console.log("Clima");
    //let currentDate = " hoy";

    //let weatherDeHoy = "Weather de " + currentDate;


    this.http.get<Observable<WeatherForeCast>>('/api/SampleData/').subscribe(data => {
      console.log(data);

      this.foreCast = data;

      console.log(this.foreCast);

      this.title = this.foreCast[1].summary;
      this.myCurrentForecast = this.foreCast[1];
      console.log(this.myCurrentForecast);

    },
      error => {
        console.log("error ocurred");
      }
    );
  }
};


//Test this code trough node cli
//let appComponent = new AppComponent();
//var weatherObtenido = appComponent.GetWeather();
//console.log(weatherObtenido);
