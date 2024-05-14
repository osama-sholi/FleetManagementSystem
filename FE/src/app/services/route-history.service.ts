import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { IService } from './IService';
import { GVAR } from '../models/gvar.model';

@Injectable({
  providedIn: 'root',
})
export class RouteHistoryService implements IService {
  private apiUrl = 'http://localhost:5179/api/RoutesHistory';
  constructor(private http: HttpClient) {}

  // Get all route history for a vehicle
  getAll(entity: any): Observable<any> {
    let gvarObservable: Observable<any> = this.http.get<any>(
      `${this.apiUrl}/${entity}/0/${Number.MAX_SAFE_INTEGER}`
    );
    let dicOfDTObservable = gvarObservable.pipe(
      map((gvar) => {
        let routeHistory = gvar.DicOfDT.RouteHistory;
        routeHistory.forEach((item: any) => {
          // Convert the epoch time to a human-readable format
          let date = new Date(item.GPSTime * 1000);
          item.GPSTime =
            date.toLocaleDateString() + ' ' + date.toLocaleTimeString();
        });
        return routeHistory;
      })
    );
    return dicOfDTObservable;
  }

  // Add route history
  add(entity: any): Observable<any> {
    var gvar = new GVAR();
    gvar.DicOfDic['Tags'] = entity;

    // Since the VehicleID is entered as a number, we need to convert it to a string
    gvar.DicOfDic['Tags']['VehicleID'] = entity['VehicleID'].toString();

    // Since the date is entered as a string, we need to convert it to epoch time
    let date = new Date(entity['RecordTime']);
    gvar.DicOfDic['Tags']['RecordTime'] = Math.floor(
      date.getTime() / 1000
    ).toString();

    return this.http.post(this.apiUrl, gvar);
  }

  // Implementing IService interface
  update(entity: any): Observable<any> {
    return new Observable();
  }

  // Implementing IService interface
  delete(entity: any): Observable<any> {
    return new Observable();
  }
}
