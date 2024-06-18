export interface Plane {
  id: string;
  speed: number;
  state: PlaneState;
}
export enum PlaneState
{
  Stopped,
  Started,
  Running,
  Flying,
  Landing
}

export enum PlaneAction
{
  Stop,
  Start,
  Accelerate,
  Decelerate,
  Fly,
  Land
}
