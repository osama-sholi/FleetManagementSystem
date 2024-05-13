import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { RouteHistoryService } from '../../services/route-history.service';

@Component({
  selector: 'route-history',
  templateUrl: './route-history.html',
})
export class RouteHistoryComponent {
  constructor(
    private route: ActivatedRoute,
    public routeHistoryService: RouteHistoryService
  ) {}
  VehicleId = Number(this.route.snapshot.paramMap.get('id'));
  fields: string[] = [
    'VehicleDirection',
    'Status',
    'VehicleSpeed',
    'Address',
    'Latitude',
    'Longitude',
    'RecordTime',
  ];
}
