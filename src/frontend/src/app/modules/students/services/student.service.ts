import { HttpClientModule } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StudentService {
  baseUrl : string = "https://localhost:8080/";
  constructor(private http: HttpClientModule) { 
  }
}
