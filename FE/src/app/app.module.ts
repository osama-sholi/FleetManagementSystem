import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SidebarComponent } from './core/sidebar/sidebar.component';
import { DriversComponent } from './features/drivers/drivers.component';
import { DriverService } from './services/driver.service';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule } from '@angular/material/dialog';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatInputModule } from '@angular/material/input';
import { DialogComponent } from './shared/modal/modal.component';
import { ReactiveFormsModule } from '@angular/forms';
import { GeofencesComponent } from './features/geofences/geofences.component';
import { VehiclesComponent } from './features/vehicles/vehicles.component';
import { MatMenuModule } from '@angular/material/menu';
import { DataTableComponent } from './shared/data-table/data-table.component';
import { MatOption } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';

@NgModule({
  declarations: [
    AppComponent,
    SidebarComponent,
    DriversComponent,
    DialogComponent,
    GeofencesComponent,
    VehiclesComponent,
    DataTableComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    MatSidenavModule,
    MatListModule,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatInputModule,
    ReactiveFormsModule,
    MatMenuModule,
    MatOption,
    MatSelectModule,
  ],
  providers: [provideAnimationsAsync(), DriverService],
  bootstrap: [AppComponent],
})
export class AppModule {}
