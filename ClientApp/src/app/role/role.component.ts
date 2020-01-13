import { RoleService } from '../services/roleservice.service'
import { Component, OnInit, ViewChild } from '@angular/core';
import { HttpClient, HttpResponse, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { ModalManager } from 'ngb-modal';
import { FormControl, FormGroup, Validators, FormBuilder } from '@angular/forms';



@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.css'],
  providers: [RoleService]
})

export class RoleComponent implements OnInit {
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

  ////datatable table server side 
  ngOnInit() {
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
            that.httpClient.post<DataTablesResponse>(
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
                  html += '<button class="btn btn-sm btn-dark role-edit" data-id="' + row.RoleId + '" data-tooltip="true" title="Edit"><i class="far fa-edit"></i></button>&nbsp;';
                  html += '<button class="btn btn-sm btn-dark role-delete" data-id="' + row.RoleId + '" data-tooltip="true" title="Delete"><i class="far fa-trash-alt"></i></button>';
              return html;
          }
        } ]
    };
  }

  // Open Modal
  openModal(){
      this.createForm();
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
      //or this.modalRef.close();
  }

  //Submit
  onSubmit(): void {
    if(this.roleForm.valid){
      this.RoleService.create(this.roleForm.value).subscribe( resp => { console.log(resp) });
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
