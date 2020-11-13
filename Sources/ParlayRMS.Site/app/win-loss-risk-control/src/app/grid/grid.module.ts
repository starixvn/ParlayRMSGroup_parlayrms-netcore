import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GridComponent } from './grid.component';
import { GridService } from './grid.service';
import { SiteGridService } from './site-grid/site-grid.service';
import { SiteGridComponent } from './site-grid/site-grid.component';
import { NumberFormatModule } from 'nap-components/number-format';
import { BreadcrumbModule } from 'nap-components/breadcrumb';
import { PaginationService, PaginationConfig, PaginationModule } from 'nap-components/pagination';
import { CompanyGridComponent } from './company-grid/company-grid.component';
import { CompanyGridService } from './company-grid/company-grid.service';
import { MemberGridComponent } from './member-grid/member-grid.component';
import { MemberGridService } from './member-grid/member-grid.service';
import { CustomPaginationConfig } from '../common/config';

@NgModule({
    imports: [
        CommonModule,
        NumberFormatModule,
        BreadcrumbModule,
        PaginationModule
    ],
    schemas: [ CUSTOM_ELEMENTS_SCHEMA ],
    declarations: [
        GridComponent,
        SiteGridComponent,
        CompanyGridComponent,
        MemberGridComponent
    ],
    exports: [
        GridComponent
    ],
    providers: [
        GridService,
        SiteGridService,
        CompanyGridService,
        MemberGridService,
        PaginationService,
        PaginationConfig,
       { provide: PaginationConfig, useClass: CustomPaginationConfig },
    ]
})

export class GridModule { }
