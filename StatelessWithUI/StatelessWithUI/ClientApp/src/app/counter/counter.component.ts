import {Component} from '@angular/core';
import {Car, CarState} from "../Domain/CarConstants";
import {HttpClient} from "@angular/common/http";
import {FormBuilder, FormGroup} from "@angular/forms";

@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html',
  styleUrls: ['./counter.component.css']
})
export class CounterComponent {
  private _carId: string | undefined;

  get carId(): string | undefined {
    return this.car ? this.car.id : this._carId;
  }

  set carId(value: string | undefined) {
    this._carId = value;

    if (this.car) {
      this.car.id = value ?? '';
    }
  }

  public car?: Car;
  form: FormGroup;

  constructor(private http: HttpClient, builder: FormBuilder) {
    this.form = builder.group({
      first: "",
    });
  }

  public getCarById(): void {
    this.http.get<Car>(`https://localhost:7276/api/VehicleStateMachine/vehicle/${this.carId || "Id2"}`).subscribe(result => {
      this.car = result;
    }, error => console.error(error));
  }

  public CreateCar(): void {
    let body = {id: this.carId, speed: 0, state: CarState.Stopped}

    this.http.post<Car>(`https://localhost:7276/api/VehicleStateMachine/vehicle`, body).subscribe(result => {
      console.log(result)
    }, error => console.error(error));
  }

}

