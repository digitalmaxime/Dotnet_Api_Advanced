import {Component} from '@angular/core';
import {Car, CarState} from "../Domain/CarConstants";
import {HttpClient} from "@angular/common/http";
import {FormBuilder, FormGroup} from "@angular/forms";
import {Plane, PlaneState} from "../Domain/PlaneConstants";

@Component({
  selector: 'app-counter-component',
  templateUrl: './state-machine.component.html',
  styleUrls: ['./state-machine.component.css']
})
export class StateMachineComponent {
  public car?: Car;
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

  public plane?: Plane;
  private _planeId: string | undefined;
  get planeId(): string | undefined {
    return this.plane ? this.plane.id : this._planeId;
  }

  set planeId(value: string | undefined) {
    this._planeId = value;
    if (this.plane) {
      this.plane.id = value ?? '';
    }
  }

  carForm: FormGroup;
  planeForm: FormGroup;

  constructor(private http: HttpClient, builder: FormBuilder) {
    this.carForm = builder.group({id: ""});

    this.planeForm = builder.group({id: ""});
  }

  public getCarById(): void {
    this.http.get<Car>(`https://localhost:7276/api/CarStateMachine/car/${this.carId || "Id1"}`).subscribe(result => {
      this.car = result;
    }, error => console.error(error));
  }

  public getPlaneById(): void {
    console.log(this.planeId)
    this.http.get<Plane>(`https://localhost:7276/api/planestatemachine/plane/${this.planeId || "Id1"}`).subscribe(result => {
      console.log(result)
      this.plane = result;
    }, error => console.error(error));
  }

  public CreateCar(): void {
    let body = {id: this.carId}
    console.log(body.id)

    this.http.post<Car>(`https://localhost:7276/api/CarStateMachine/car`, body).subscribe(result => {
      console.log(result)
      this.car = result;
    }, error => console.error(error));
  }

  public CreatePlane(): void {
    let body = {id: this.planeId || "Id1"}
    this.http.post<Plane>(`https://localhost:7276/api/PlaneStateMachine/plane`, body).subscribe(result => {
      console.log(result)
      this.plane = result;
    }, error => console.error(error));
  }

}

