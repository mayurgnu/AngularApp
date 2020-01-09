import { Injectable } from '@angular/core';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/map';
import { environment } from 'src/environments/environment';

@Injectable()
export class RoleService {
  constructor(private http: HttpClient) { }

  getRoles(){
   return this.http.get(environment.apiUrl +'api/home/Roles').map(res => 
    { return res });
  }
  //create(bookstore:any){
  //  return this.http.get('/api/bookstores', bookstore)
  //    .map(res => res.json());
  //}
  //update(bookstore:any){
  //  return this.http.put('/api/bookstores/' + bookstore.id, bookstore)
  //    .map(res => res.json());
  //}
  //delete(id:any){
  //  return this.http.delete('/api/bookstores/' + id)
  //    .map(res => res.json());
  //}
  //getBookstores()
  //{
  //  return this.http.get('/api/bookstores')
  //    .map(res => res.json());
  //}
}
