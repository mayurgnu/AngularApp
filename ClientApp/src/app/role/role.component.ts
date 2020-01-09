import { RoleService } from '../services/roleservice.service'
import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { environment } from 'src/environments/environment';



@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.css'],
  providers: [RoleService]
})
export class RoleComponent implements OnInit {
  rolelist;
  dtOptions: DataTables.Settings = {}
  constructor(private RoleService: RoleService,private http: HttpClient) { }

  // ngOnInit() {
  //     this.dtOptions = {
  //       pageLength: 2,
  //       pagingType: 'full_numbers'
  //     };
  //     this.RoleService.getRoles().subscribe( x => 
  //     { 
  //       this.rolelist = x; 
  //     });
  // }
  ngOnInit() {
    const that = this;
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 2,
      serverSide: true,
      // processing: true,
      ajax: (dataTablesParameters: any, callback) => {
        debugger
      that.http.post<DataTablesResponse>(
            environment.apiUrl +'api/home/GetGridData',
            dataTablesParameters, { }
          ).subscribe(resp => {
            that.rolelist = resp;
            callback({
              recordsTotal: resp.recordsTotal,
              recordsFiltered: resp.recordsFiltered,
              data: []
            });
          });
      },
      columns: [{ data: 'Role' }, { data: 'IsActive' }]
    };
  }

}

class DataTablesResponse {
  data: any[];
  draw: number;
  recordsFiltered: number;
  recordsTotal: number;
  mode:string="Role";
}
