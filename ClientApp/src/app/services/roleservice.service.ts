import { Injectable } from '@angular/core';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/map';
import { environment } from 'src/environments/environment';

@Injectable()
export class RoleService {
  constructor(private http: HttpClient) { }

  getRoles(){
   return this.http.get(environment.apiUrl +'home/Roles').map(res => 
    { return res });
  }
  getRoleById(id:string){
  return this.http.get(environment.apiUrl +'home/RoleById/'+id).map(res => 
    { return res });
  }
  create(model:any){
   return this.http.post(environment.apiUrl +'home/ManageRole', model)
     .map(res => res);
  }
  //update(model:any){
  // return this.http.put(environment.apiUrl +'home/ManageRole', model)
  //   .map(res => res);
  //}
  delete(id:any){
   return this.http.delete(environment.apiUrl +'home/DeleteRole/'+id).map(res => 
    { return res });
  }
}
