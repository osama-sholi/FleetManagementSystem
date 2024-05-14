import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { IService } from './IService';

@Injectable({
  providedIn: 'root',
})
export class GeofenceService implements IService {
  private apiUrl = 'http://localhost:5179/api/Geofences';
  constructor(private http: HttpClient) {}

  // Get all geofences
  getAll(): Observable<any> {
    let gvarObservable: Observable<any> = this.http.get<any>(this.apiUrl);
    let dicOfDTObservable = gvarObservable.pipe(
      map((gvar) => gvar.DicOfDT.Geofences)
    );
    return dicOfDTObservable;
  }

  // Implementing IService interface
  add(entity: any): Observable<any> {
    return new Observable();
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
