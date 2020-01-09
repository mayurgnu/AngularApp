import { RoleService } from '../app/services/roleservice.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from  '../app/app.component';
import { NavMenuComponent } from '../app/nav-menu/nav-menu.component';
import { HomeComponent } from '../app/home/home.component';
import { FetchDataComponent } from '../app/fetch-data/fetch-data.component';
import { CounterComponent } from '../app/counter/counter.component';
import { RoleComponent } from '../app/role/role.component';



@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent,
        RoleComponent
    ],
    imports: [
        CommonModule,
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot([
            { path: 'role', component: RoleComponent },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [
        RoleService
    ]
})
export class AppModuleShared {
}