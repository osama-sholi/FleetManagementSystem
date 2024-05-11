import { Component } from '@angular/core';
import { DriverService } from '../../services/driver.service';

@Component({
  selector: 'app-drivers',
  templateUrl: './drivers.component.html',
  styleUrl: './drivers.component.css',
})
export class DriversComponent {
  actions = ['EDIT', 'DELETE'];
  constructor(public driverService: DriverService) {}
}
