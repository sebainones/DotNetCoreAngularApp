"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
exports.__esModule = true;
var core_1 = require("@angular/core");
var core_2 = require("@angular/core");
var MyService = /** @class */ (function () {
    function MyService(http) {
        this.http = http;
    }
    MyService = __decorate([
        core_1.Component({
            selector: 'app-root',
            templateUrl: './app.component.html',
            styleUrls: ['./app.component.css']
        }),
        core_2.Injectable()
    ], MyService);
    return MyService;
}());
exports.MyService = MyService;
//Now visible outside this file
var AppComponent = /** @class */ (function () {
    function AppComponent(http) {
        this.http = http;
        this.title = 'ClientApp Sebas';
    }
    AppComponent.prototype.GetWeather = function () {
        this.title = "Weather";
        //console.log("Clima");
        //let currentDate = " hoy";
        //let weatherDeHoy = "Weather de " + currentDate;
        var result = this.http.get('/api/SampleData');
        //function successCallback(response) {
        //  //success code                
        //  data = response.data;
        //  title = " Automatismos cargados correctamente";
        //}
        //function errorCallback(error) {
        //  //error code
        //  title = "Algo ha salido mal! Intentar nuevamente";
        //}
    };
    AppComponent = __decorate([
        core_2.Injectable()
    ], AppComponent);
    return AppComponent;
}());
exports.AppComponent = AppComponent;
;
//Test this code trough node cli
//let appComponent = new AppComponent();
//var weatherObtenido = appComponent.GetWeather();
//console.log(weatherObtenido);
