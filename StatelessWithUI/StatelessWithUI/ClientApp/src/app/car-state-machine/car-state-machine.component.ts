import {Component, Input} from '@angular/core';
import {Car, CarState} from "../Domain/CarConstants";

@Component({
  selector: 'app-car-state-machine',
  templateUrl: './car-state-machine.component.html',
  styleUrls: ['./car-state-machine.component.css']
})
export class CarStateMachineComponent {
  @Input() public car!: Car;
public states: (string | CarState)[] = Object.values(CarState).filter((x: string | CarState) => !isNaN(parseInt(x.toString())));
  constructor() {
  }

  protected readonly CarState = CarState;
  protected readonly Object = Object;
  protected readonly parseInt = parseInt;
  protected readonly isNaN = isNaN;
}
