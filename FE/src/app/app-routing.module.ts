import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DriversComponent } from './features/drivers/drivers.component';
import { GeofencesComponent } from './features/geofences/geofences.component';
import { VehiclesComponent } from './features/vehicles/vehicles.component';
import { RouteHistoryComponent } from './features/route-history/route-history';

const routes: Routes = [
  {
    path: 'drivers',
    component: DriversComponent,
  },
  {
    path: 'geofences',
    component: GeofencesComponent,
  },
  {
    path: 'vehicles',
    component: VehiclesComponent,
  },
  {
    path: 'vehicles/:id/route-history',
    component: RouteHistoryComponent,
  },
  {
    path: '',
    redirectTo: '/vehicles',
    pathMatch: 'full',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
