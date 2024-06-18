export interface Car {
  id: string;
  speed: number;
  state: CarState;
}
export enum CarState
{
  Stopped,
  Started,
  Running,
  Drifting,
}
