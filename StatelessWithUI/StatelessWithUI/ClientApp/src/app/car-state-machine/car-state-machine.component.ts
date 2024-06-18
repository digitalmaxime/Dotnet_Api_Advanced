import {Component, Input} from '@angular/core';
import {Car, CarState} from "../Domain/CarConstants";
import {FormBuilder, FormGroup} from "@angular/forms";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-car-state-machine',
  templateUrl: './car-state-machine.component.html',
  styleUrls: ['./car-state-machine.component.css']
})
export class CarStateMachineComponent {
  @Input() public car!: Car;
  public states: (string | CarState)[] = Object.values(CarState).filter((x: string | CarState) => !isNaN(parseInt(x.toString())));

  form: FormGroup;

  constructor(private http: HttpClient, builder: FormBuilder) {
    this.form = builder.group({
      first: "",
    });
  }

  protected readonly CarState = CarState;
  protected readonly Object = Object;
  protected readonly parseInt = parseInt;
  protected readonly isNaN = isNaN;


  public GoToNextState(): void {
    this.http.post<Car>(`https://localhost:7276/api/CarStateMachine/car/goto-nextstate/${this.car.id}`, {}).subscribe(result => {
      console.log(result)
    }, error => console.error(error));
  }

}
