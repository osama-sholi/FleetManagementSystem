import { Component } from '@angular/core';
import { VehicleService } from '../../services/vehicle.service';

@Component({
  selector: 'app-vehicles',
  templateUrl: './vehicles.component.html',
  styleUrl: './vehicles.component.css',
})
export class VehiclesComponent {
  fields = ['VehicleID', 'VehicleNumber', 'VehicleType'];
  actions = ['MORE-INFO', 'ROUTE-HISTORY', 'EDIT', 'DELETE'];
  constructor(public vehicleService: VehicleService) {}
}
