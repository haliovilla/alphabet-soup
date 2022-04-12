import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { AlphabetModel } from '../models/alphabet-model';
import { ApiResult } from '../models/api-result';

@Injectable({
  providedIn: 'root'
})
export class AlphabetService {

  apiUrl: string = `${environment.apiUrl}/Alphabet`;

  constructor(private http: HttpClient) { }

  validateIfWordExists(data: AlphabetModel) {
    return this.http.post(this.apiUrl, data)
      .pipe(map((res: any) => {
        const response = res as ApiResult;
        return response;
      }));
  }
}
