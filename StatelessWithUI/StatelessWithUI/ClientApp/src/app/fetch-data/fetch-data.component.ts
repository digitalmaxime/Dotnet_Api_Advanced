import {Component, Inject} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Car} from "../Domain/CarConstants";

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public cars: Car[] = [];

  constructor(http: HttpClient) {
    http.get<Car[]>("https://localhost:7276/api/VehicleStateMachine/vehicle").subscribe(result => {
      this.cars = result;
    }, error => console.error(error));
  }
}

