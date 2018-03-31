import { NgModule, ErrorHandler } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { WeaponService } from './services/weapon/weapon.service';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { WeaponFormComponent } from './components/weapon-form/weapon-form.component';
import { WeaponListComponent } from './components/weapon-list/weapon-list.component';
import { WeaponDetailsComponent } from './components/weapon-details/weapon-details.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent,
        WeaponFormComponent,
        WeaponListComponent,
        WeaponDetailsComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'weapons', component: WeaponListComponent },
            { path: 'weapons/new', component: WeaponFormComponent },
            { path: 'weapons/edit/:id', component: WeaponFormComponent },
            { path: 'weapons/:id', component: WeaponDetailsComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [
        WeaponService
    ]
})
export class AppModuleShared {
}
