import { Observable } from 'rxjs';

export interface IService {
  getAll(entity: any): Observable<any[]>;
  add(entity: any): Observable<any>;
  update(entity: any): Observable<any>;
  delete(entity: any): Observable<any>;
}
