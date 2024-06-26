import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject, catchError, map } from 'rxjs';
import { IService } from './IService';
import { GVAR } from '../models/gvar.model';
import { DriverService } from './driver.service';
import { WebSocketSubject } from 'rxjs/webSocket';

@Injectable({
  providedIn: 'root',
})
export class VehicleService implements IService {
  private apiUrl = 'http://localhost:5179/api/Vehicles';
  private infoApiUrl = 'http://localhost:5179/api/VehiclesInfo';
  private wsUrl = 'ws://localhost:5179/api/ws';
  private socket$: WebSocketSubject<any>;
  public messages: Subject<any> = new Subject<any>();
  constructor(private http: HttpClient) {
    this.socket$ = new WebSocketSubject(this.wsUrl);
    this.socket$
      .asObservable()
      .pipe(
        catchError((error: any) => {
          console.error(error);
          var gvar = new GVAR();
          gvar.DicOfDic['Tags'] = {};
          gvar.DicOfDic['Tags']['STS'] = '0';
          return this.http.post(this.wsUrl, gvar);
        })
      )
      .subscribe((message) => {
        let vehicle = this.mapMessageToVehicle(message);
        this.messages.next(vehicle);
      });
  }

  close() {
    this.socket$.complete();
  }

  private mapMessageToVehicle(message: any): any {
    var tags = message['DicOfDic']['Tags'];
    return {
      VehicleID: tags.VehicleID,
      LastDirection: tags.VehicleDirection,
      LastStatus: tags.Status,
      LastAddress: tags.Address,
      LastPosition: '(' + tags.Latitude + ',' + tags.Longitude + ')',
    };
  }
  // Get all vehicles
  getAll(): Observable<any> {
    return this.http.get<any>(this.infoApiUrl).pipe(
      map((gvar) =>
        gvar.DicOfDT.VehiclesInformations.map((vehicle: any) => {
          if (
            vehicle.LastPosition === '(0,0)' &&
            vehicle.LastStatus.trim() === ''
          ) {
            vehicle.LastPosition = '';
            vehicle.LastDirection = '';
          }
          return vehicle;
        })
      )
    );
  }

  // Add vehicle
  add(entity: any): Observable<any> {
    var gvar = new GVAR();
    gvar.DicOfDic['Tags'] = entity;
    return this.http.post(this.apiUrl, gvar);
  }

  // Update vehicle
  update(entity: any): Observable<any> {
    var gvar = new GVAR();
    gvar.DicOfDic['Tags'] = entity;
    return this.http.put(this.apiUrl, gvar);
  }

  // Delete vehicle
  delete(entity: any): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${entity.VehicleID}`);
  }

  // Get vehicle details
  getDetails(entity: any): Observable<any> {
    return this.http.get<any>(`${this.infoApiUrl}/${entity.VehicleID}`).pipe(
      map((gvar) =>
        gvar.DicOfDT.VehiclesInformations.map((vehicle: any) => {
          // Since these fields are long in the backend, they are stored as 0 when empty, so we check if the row is empty and set them to empty string
          if (vehicle.LastGPSTime === '0') {
            vehicle.LastPosition = '';
            vehicle.LastGPSTime = '';
          } else {
            let date = new Date(vehicle.LastGPSTime * 1000);
            vehicle.LastGPSTime =
              date.toLocaleDateString() + ' ' + date.toLocaleTimeString();
          }
          // Since these fields are long in the backend, they are stored as 0 when empty, so we check if the row is empty and set them to empty string
          if (vehicle.DriverName === '' && vehicle.PhoneNumber === '0') {
            vehicle.PhoneNumber = '';
          }
          return vehicle;
        })
      )
    );
  }

  getInfo(element: any): Observable<any> {
    const driverService = new DriverService(this.http);
    return driverService.getAll(); // This gets the drivers, this is used for the dropdown in the vehicle info modal.
  }

  // Delete vehicle info
  deleteVehicleInfo(entity: any): Observable<any> {
    return this.http.delete(`${this.infoApiUrl}/${entity.VehicleID}`);
  }

  // Add vehicle info
  addVehicleInfo(entity: any): Observable<any> {
    var gvar = new GVAR();
    gvar.DicOfDic['Tags'] = entity;

    // Since the date is entered as a string, we need to convert it to epoch time
    let date = new Date(entity['PurchaseDate']);
    gvar.DicOfDic['Tags']['PurchaseDate'] = Math.floor(
      date.getTime() / 1000
    ).toString();

    return this.http.post(this.infoApiUrl, gvar);
  }

  // Update vehicle info
  updateVehicleInfo(entity: any): Observable<any> {
    var gvar = new GVAR();
    gvar.DicOfDic['Tags'] = entity;

    // Since the date is entered as a string, we need to convert it to epoch time
    let date = new Date(entity['PurchaseDate']);
    gvar.DicOfDic['Tags']['PurchaseDate'] = Math.floor(
      date.getTime() / 1000
    ).toString();

    return this.http.put(this.infoApiUrl, gvar);
  }
}
