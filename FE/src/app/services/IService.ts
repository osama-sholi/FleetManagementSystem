import { Observable } from 'rxjs';

export interface IService {
  getAll(entity: any): Observable<any[]>;
  add(entity: any): Observable<any>;
  update(entity: any): Observable<any>;
  delete(entity: any): Observable<any>;
  getDetails(entity: any): Observable<any>;
  getInfo(entity: any): Observable<any>;
  addVehicleInfo(entity: any): Observable<any>;
  updateVehicleInfo(entity: any): Observable<any>;
  deleteVehicleInfo(entity: any): Observable<any>;
}
