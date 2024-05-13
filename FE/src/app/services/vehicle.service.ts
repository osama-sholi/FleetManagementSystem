import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { IService } from './IService';
import { GVAR } from '../models/gvar.model';
import { DriverService } from './driver.service';

@Injectable({
  providedIn: 'root',
})
export class VehicleService implements IService {
  private apiUrl = 'http://localhost:5179/api/Vehicles';
  private infoApiUrl = 'http://localhost:5179/api/VehiclesInfo';
  constructor(private http: HttpClient) {}

  getAll(): Observable<any> {
    return this.http.get<any>(this.infoApiUrl).pipe(
      map((gvar) =>
        gvar.DicOfDT.VehiclesInformations.map((vehicle: any) => {
          if (
            vehicle.LastPosition === '(0,0)' &&
            vehicle.LastStatus.trim() === ''
          ) {
            vehicle.LastPosition = '';
          }
          return vehicle;
        })
      )
    );
  }

  add(entity: any): Observable<any> {
    var gvar = new GVAR();
    gvar.DicOfDic['Tags'] = entity;
    return this.http.post(this.apiUrl, gvar);
  }

  update(entity: any): Observable<any> {
    var gvar = new GVAR();
    gvar.DicOfDic['Tags'] = entity;
    return this.http.put(this.apiUrl, gvar);
  }

  delete(entity: any): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${entity.VehicleID}`);
  }

  getDetails(entity: any): Observable<any> {
    return this.http.get<any>(`${this.infoApiUrl}/${entity.VehicleID}`).pipe(
      map((gvar) =>
        gvar.DicOfDT.VehiclesInformations.map((vehicle: any) => {
          if (vehicle.LastGPSTime === '0') {
            vehicle.LastPosition = '';
            vehicle.LastGPSTime = '';
          } else {
            let date = new Date(vehicle.LastGPSTime * 1000);
            vehicle.LastGPSTime =
              date.toLocaleDateString() + ' ' + date.toLocaleTimeString();
          }
          if (vehicle.DriverName === '' && vehicle.PhoneNumber === '0') {
            vehicle.DriverName = '';
            vehicle.PhoneNumber = '';
          }
          return vehicle;
        })
      )
    );
  }

  getInfo(element: any): Observable<any> {
    const driverService = new DriverService(this.http);
    return driverService.getAll();
  }

  deleteVehicleInfo(entity: any): Observable<any> {
    console.log(entity);
    return this.http.delete(`${this.infoApiUrl}/${entity.VehicleID}`);
  }

  addVehicleInfo(entity: any): Observable<any> {
    var gvar = new GVAR();
    gvar.DicOfDic['Tags'] = entity;
    return this.http.post(this.infoApiUrl, gvar);
  }

  updateVehicleInfo(entity: any): Observable<any> {
    var gvar = new GVAR();
    gvar.DicOfDic['Tags'] = entity;
    return this.http.put(this.infoApiUrl, gvar);
  }
}
