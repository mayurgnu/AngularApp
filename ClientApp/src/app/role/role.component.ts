import { RoleService } from '../services/roleservice.service'
import { Component, OnInit, ViewChild } from '@angular/core';
import { HttpClient, HttpResponse, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { ModalManager } from 'ngb-modal';
import { FormControl, FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Subject } from 'rxjs';
import { DataTableDirective } from 'angular-datatables';


@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.css'],
  providers: [RoleService]
})
export class RoleComponent {
  @ViewChild(DataTableDirective, {static: false})
  dtElement: DataTableDirective;
  
  @ViewChild('myModal',null) myModal;
  private modalRef;

  settings;
  dataTableUrl = environment.apiUrl +'home/GetGridData';
  dataTableExportUrl = environment.apiUrl +'home/GetGridData';
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
  dtTrigger = new Subject();

  ngAfterViewInit(): void {
    this.dtTrigger.next();
  }
  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }
  rerender(): void {
    this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
      // Destroy the table first
      dtInstance.destroy();
      // Call the dtTrigger to rerender again
      this.dtTrigger.next();
    });
  }


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
    const that = this;
    $("#tblRole").on("click",".role-edit", function(event){
      that.fnEditRole($(this).attr('data-id'));
    });
    $("#tblRole").on("click",".role-delete", function(event){
      that.fnDeleteRole($(this).attr('data-id'));
    });
    this.GlobalBindGrid();
  }
  GlobalBindGrid(){
    debugger
    this.settings = $.extend({}, this.defaults, this.dtOptions);
    this.settings.mode = "Role";
    this.settings.FixClause = "(IsActive = IsActive)";
    this.dtOptions = {
      pagingType: 'simple_numbers',
      pageLength: 3,
      serverSide: true,
      processing: true,
      ajax: (dataTablesParameters: any, callback,) => {
        this.httpClient.post<DataTablesResponse>(
             this.defaults.Url,Object.assign(dataTablesParameters,this.settings),
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
        backdrop: 'static',
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
      this.RoleService.create(this.roleForm.value).subscribe( resp => { 
        this.closeModal();
        alert('Record saved successfully.');
        this.rerender(); 
      });
    }
  }
  //Delete
  fnDeleteRole (id:string): void {
    if(confirm("Are you sure, you want to delete?")){
      this.RoleService.delete(id).subscribe( resp => { 
        this.rerender(); 
      });
    }
  }
}
class DataTablesResponse {
  data: any[];
  draw: number;
  recordsFiltered: number;
  recordsTotal: number;
}
