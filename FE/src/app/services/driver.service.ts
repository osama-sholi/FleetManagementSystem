import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { IService } from './IService';
import { GVAR } from '../models/gvar.model';

@Injectable({
  providedIn: 'root',
})
export class DriverService implements IService {
  // Implementing IService interface
  private apiUrl = 'http://localhost:5179/api/Drivers';
  constructor(private http: HttpClient) {}

  // Get all drivers
  getAll(): Observable<any> {
    let gvarObservable: Observable<any> = this.http.get<any>(this.apiUrl);
    let dicOfDTObservable = gvarObservable.pipe(
      map((gvar) => gvar.DicOfDT.Drivers)
    );
    return dicOfDTObservable;
  }

  // Add driver
  add(entity: any): Observable<any> {
    var gvar = new GVAR();
    gvar.DicOfDic['Tags'] = entity;
    return this.http.post(this.apiUrl, gvar);
  }

  // Update driver
  update(entity: any): Observable<any> {
    var gvar = new GVAR();
    entity.PhoneNumber = entity.PhoneNumber.toString();
    gvar.DicOfDic['Tags'] = entity;
    return this.http.put(this.apiUrl, gvar);
  }

  // Delete driver
  delete(entity: any): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${entity.DriverID}`);
  }
}
