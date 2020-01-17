import { RoleService } from '../services/roleservice.service'
import { Component, OnInit, ViewChild } from '@angular/core';
import { HttpClient, HttpResponse, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { ModalManager } from 'ngb-modal';
import { FormControl, FormGroup, Validators, FormBuilder } from '@angular/forms';

@Component({
    selector: 'app-dynamic-data-table',
    templateUrl: './dynamic-data-table.component.html',
    styleUrls: ['./dynamic-data-table.component.css'],
    providers: [RoleService]
  })

export class DynamicDataTableComponent implements OnInit {
  @ViewChild('myModal',null) myModal;
  private modalRef;

  settings;
  dataTableUrl = environment.apiUrl +'api/home/GetGridData';
  dataTableExportUrl = environment.apiUrl +'api/home/GetGridData';
  ////Datatable Configurations Start
  fnAdd(){}
  fnDelete(){}
  fn_drawCallback(){}
  fn_rowCallback(){}
  defaults = { Url: this.dataTableUrl, ExportUrl: this.dataTableExportUrl, 
    PagerInfo: true, SearchParams: {}, RecordPerPage: 10, DataType: 'POST', 
    Columns: [], mode: '', FixClause: '', SortColumn: '0', SortOrder: 'asc',
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
  ////Datatable Configurations End
  roleForm: FormGroup;
  constructor(private RoleService: RoleService,private httpClient: HttpClient,private modalService: ModalManager,private fb: FormBuilder){
    this.createForm();
  }
  createForm() {
    this.roleForm = this.fb.group({
      RoleId:[0],
      Role: ['', Validators.required ]
    });
  }

  fnEditRole(id:string){
    this.RoleService.getRoleById(id).subscribe(resp => {
      console.log(resp);
      this.roleForm.get('RoleId').setValue(resp.RoleId);
      this.roleForm.get('Role').setValue(resp.Role);
      // this.roleForm.setValue(resp);
      this.openModal();
  });
  }
  ////datatable table server side 
  ngOnInit() {
    $("#tblRole").on("click",".role-edit", function(event){
      this.fnEditRole($(this).attr('data-id'));
    });
    const that = this;
    this.settings = $.extend({}, this.defaults, this.dtOptions);
    this.settings.mode = "Role";
    this.settings.FixClause = "(IsActive = IsActive)";
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 2,
      serverSide: true,
      processing: true,
      ajax: (dataTablesParameters: any, callback,) => {
            this.httpClient.post<DataTablesResponse>(
             environment.apiUrl +'home/GetGridData',Object.assign(dataTablesParameters,this.settings),
             {}).subscribe(resp => {
             callback({
               recordsTotal: resp.recordsTotal,
               recordsFiltered: resp.recordsFiltered,
               data: resp.data
             });
            });
      },
      columns: [
        { 
          title:'Role', data: 'Role',
          render: function ( data, type, row, meta ) {
            return '<span style="color:'+row.Clr+'">'+data+'</span>';
          }
        },
        { 
          title:'Active',data: 'IsActive',
          render: function ( data, type, row, meta ) {
            return '<span style="color:'+row.Clr+'">'+data+'</span>';
          }  
        },
        {
          title: 'Action',
          searchable: false,
          orderable: false,
          className: 'table-btns4', width: '100px',
          render: function (data, type, row, meta) {
              var html = '';
                  html += '<button class="btn btn-sm btn-dark role-edit" data-id="' + row.RoleId + '" data-tooltip="true" title="Edit">Edit</button>&nbsp;';
                  html += '<button class="btn btn-sm btn-dark role-delete" data-id="' + row.RoleId + '" data-tooltip="true" title="Delete">Delete</button>';
              return html;
          }
        } ]
    };
  }
  // Open Modal
  openModal(){
      this.modalRef = this.modalService.open(this.myModal, {
          size: "md",
          modalClass: 'mymodal',
          hideCloseButton: false,
          centered: false,
          backdrop: true,
          animation: true,
          keyboard: false,
          closeOnOutsideClick: true,
          backdropClass: "modal-backdrop"
      })
  }
  // Close Modal
  closeModal(){
      this.modalService.close(this.modalRef);
      this.createForm();
      //or this.modalRef.close();
  }

  //Submit
  onSubmit(): void {
    if(this.roleForm.valid){
      this.RoleService.create(this.roleForm.value).subscribe( resp => { this.closeModal(); });
    }
  }
  // fnSave() {
  //   if (this.roleForm.RoleId === 0) {
  //   this.RoleService.create(this.product).pipe().subscribe(data => {
  //   console.log(this.data);
  //   this.fnGetData();
  //   alert('Record inserted successfully.');
  //   });
  //   } else {
  //   this.testService.putData(this.product).pipe().subscribe(data => {
  //   console.log(this.data);
  //   this.divTableShow = true;
  //     this.fnGetData();
  //     alert('Record updated successfully.');
  //     });
  //   }
  // }
}
class DataTablesResponse {
  data: any[];
  draw: number;
  recordsFiltered: number;
  recordsTotal: number;
}
