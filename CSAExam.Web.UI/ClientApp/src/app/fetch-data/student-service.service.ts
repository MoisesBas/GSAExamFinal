import { Injectable, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { Http, Headers } from '@angular/http';
@Injectable({
  providedIn: 'root'
})
export class StudentServiceService {
  baseurl:string;

  constructor(private http:Http) {
  }
  getAllStudents(path):Observable<any>{
    return this.http.get(path + 'weatherforecast'
    ).pipe(
      map((result: any) => {
        debugger;
        if (result instanceof Array) {
          return result.pop();
        }
        return result.json();
      }));
  }
}
