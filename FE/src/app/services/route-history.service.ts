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

  getAll(entity: any): Observable<any> {
    let gvarObservable: Observable<any> = this.http.get<any>(
      `${this.apiUrl}/${entity}/0/${Number.MAX_SAFE_INTEGER}`
    );
    let dicOfDTObservable = gvarObservable.pipe(
      map((gvar) => {
        let routeHistory = gvar.DicOfDT.RouteHistory;
        routeHistory.forEach((item: any) => {
          let date = new Date(item.GPSTime * 1000);
          item.GPSTime =
            date.toLocaleDateString() + ' ' + date.toLocaleTimeString();
        });
        return routeHistory;
      })
    );
    return dicOfDTObservable;
  }

  add(entity: any): Observable<any> {
    var gvar = new GVAR();
    gvar.DicOfDic['Tags'] = entity;
    gvar.DicOfDic['Tags']['VehicleID'] = entity['VehicleID'].toString();
    let date = new Date(entity['RecordTime']);
    gvar.DicOfDic['Tags']['RecordTime'] = Math.floor(
      date.getTime() / 1000
    ).toString();
    console.log(gvar);
    return this.http.post(this.apiUrl, gvar);
  }

  update(entity: any): Observable<any> {
    return new Observable();
  }

  delete(entity: any): Observable<any> {
    return new Observable();
  }

  getInfo(id: number): Observable<any> {
    return new Observable();
  }

  getDetails(entity: any): Observable<any> {
    return new Observable();
  }

  addVehicleInfo(entity: any): Observable<any> {
    return new Observable();
  }

  updateVehicleInfo(entity: any): Observable<any> {
    return new Observable();
  }

  deleteVehicleInfo(entity: any): Observable<any> {
    return new Observable();
  }
}
