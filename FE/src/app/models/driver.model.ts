export class Driver {
  DriverID: number;
  DriverName: string;
  PhoneNumber: string;

  constructor(DriverID: number, DriverName: string, PhoneNumber: string) {
    this.DriverID = DriverID;
    this.DriverName = DriverName;
    this.PhoneNumber = PhoneNumber;
  }
}
