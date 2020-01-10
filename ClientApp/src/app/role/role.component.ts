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
  settings;
  dataTableUrl = environment.apiUrl +'api/home/GetGridData';
  dataTableExportUrl = environment.apiUrl +'api/home/GetGridData';
  // //Datatable functions
  ////'application/x-www-form-urlencoded'
  headers = new HttpHeaders({'Content-Type':'application/json'});
  fnAdd(){}
  fnDelete(){}
  fn_drawCallback(){}
  fn_rowCallback(){}
  // //Datatable functions
  defaults = { Url: this.dataTableUrl, ExportUrl: this.dataTableExportUrl, 
    PagerInfo: true, SearchParams: {}, RecordPerPage: 10, DataType: 'POST', 
    Columns: [], Mode: '', FixClause: '', SortColumn: '0', SortOrder: 'asc',
    ExportIcon: true, ColumnSelection: true, IsAddShow: true, IsPaging: true, 
    OnAdd: this.fnAdd,IsDeleteShow :false,
    OnDelete:this.fnDelete, 
    IsShowFilter: true, OnAddLabel: '', 
    FixedRightColumns: 0, FixedLeftColumns: 0, 
    GrdLabels: JSON.stringify({ Show: "Showing", To: "to", Of: "of", Entries: "entries", 
    Search: "Search", First: "first", Last: "last", Next: "next", Previous: "previous", 
    SortAsc: "activate to sort column ascending", SortDesc: "activate to sort column descending",
    Add: "Add", ExportTo: "Export To ", Excel: "Excel", Pdf: "Pdf", Csv: "Csv", Word: "Word" }), 
    DrawCallback: this.fn_drawCallback, RowCallback: this.fn_rowCallback };
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
    this.settings = $.extend({}, this.defaults, this.dtOptions);
    this.settings.Mode = "Role";
    //console.log(this.settings);
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 2,
      serverSide: true,
      processing: true,
      ajax: (dataTablesParameters: any, callback) => {
            //console.log(this.settings);
            that.httpClient.post<DataTablesResponse>(
             environment.apiUrl +'api/home/GetGridData',Object.assign(dataTablesParameters),
             { headers:this.headers }).subscribe(resp => {
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
}
