import { Observable } from 'rxjs';

export interface IService {
  getAll(): Observable<any[]>;
  add(entity: any): Observable<any>;
  update(entity: any): Observable<any>;
  delete(entity: any): Observable<any>;
  getInfo(id: number): Observable<any>;
}
