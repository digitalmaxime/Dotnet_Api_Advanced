import {Component, Input} from '@angular/core';
import {FormBuilder, FormGroup} from "@angular/forms";
import {HttpClient} from "@angular/common/http";
import {Plane, PlaneAction, PlaneState} from "../Domain/PlaneConstants";

@Component({
  selector: 'app-plane-state-machine',
  templateUrl: './plane-state-machine.component.html',
  styleUrls: ['./plane-state-machine.component.css']
})
export class PlaneStateMachineComponent {
  @Input() public plane!: Plane;
  public states: (string | PlaneState)[] = Object.values(PlaneState).filter((x: string | PlaneState) => !isNaN(parseInt(x.toString())));
  public actions: string[] = [];
  // public actions: PlaneAction[] = [];

  form: FormGroup;

  constructor(private http: HttpClient, builder: FormBuilder) {
    this.form = builder.group({
      id: "",
    });
  }

  ngOnChanges(): void {
    console.log("NG ON CHANGES!!")
    this.getAvailableActions();
  }

  protected readonly PlaneState = PlaneState;
  protected readonly Object = Object;
  protected readonly parseInt = parseInt;
  protected readonly isNaN = isNaN;


  public getAvailableActions(): void {
    this.http.get<string[]>(`https://localhost:7276/api/PlaneStateMachine/plane/getpermittedtriggers/${this.plane.id}`, {}).subscribe(result => {
      console.log(result)
      this.actions = result.map((x: string) => x);
    }, error => console.error(error));
  }

  public TakeAction(action: string): void {
    this.http.post<Plane>(`https://localhost:7276/api/PlaneStateMachine/plane/action/${this.plane.id}?action=${action}`, {}).subscribe(result => {
      console.log(result)
      if (result) {
        this.plane = result;
      }
    }, error => console.error(error));
  }

}
