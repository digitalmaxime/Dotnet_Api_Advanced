export interface Plane {
  id: string;
  state: PlaneState;
  speed: number;
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
