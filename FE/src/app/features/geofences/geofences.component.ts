import { Component } from '@angular/core';
import { GeofenceService } from '../../services/geofence.service';

@Component({
  selector: 'app-geofences',
  templateUrl: './geofences.component.html',
  styleUrl: './geofences.component.css',
})
export class GeofencesComponent {
  constructor(public geofenceService: GeofenceService) {}
}
