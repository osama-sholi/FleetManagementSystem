import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { IService } from './IService';
import { GVAR } from '../models/gvar.model';

@Injectable({
  providedIn: 'root',
})
export class VehicleService implements IService {
  private apiUrl = 'http://localhost:5179/api/Vehicles';
  private infoApiIrl = 'http://localhost:5179/api/VehiclesInfo';
  constructor(private http: HttpClient) {}

  getAll(): Observable<any> {
    let gvarObservable: Observable<any> = this.http.get<any>(this.infoApiIrl);
    let dicOfDTObservable = gvarObservable.pipe(
      map((gvar) => gvar.DicOfDT.VehiclesInfo)
    );
    return dicOfDTObservable;
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

  getInfo(VehicleID: number): Observable<any> {
    let gvarObservable: Observable<any> = this.http.get<any>(
      `${this.apiUrl}/${VehicleID}`
    );
    let dicOfDTObservable = gvarObservable.pipe(
      map((gvar) => gvar.DicOfDT.VehiclesInfo)
    );
    return dicOfDTObservable;
  }
}
