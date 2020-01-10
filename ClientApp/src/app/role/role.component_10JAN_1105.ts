import { RoleService } from '../services/roleservice.service'
import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpResponse, HttpHeaders } from '@angular/common/http';
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
  constructor(private RoleService: RoleService,private httpClient: HttpClient) { }

  // //datatable table client side 
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


  // //datatable table server side 
  ngOnInit() {
    const that = this;

    
  }

}

class DataTablesResponse {
  data: any[];
  draw: number;
  recordsFiltered: number;
  recordsTotal: number;
}
