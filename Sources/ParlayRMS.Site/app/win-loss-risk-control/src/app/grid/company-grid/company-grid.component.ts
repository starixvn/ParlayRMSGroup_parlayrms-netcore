import { Component } from '@angular/core';
import * as _ from 'lodash';
import { Grid } from '../grid.model';
import { GridService } from '../grid.service';
import { CompanyGridService } from './company-grid.service';
import { WinLossModel } from 'src/app/common/win-loss.model';

@Component({
    selector: 'app-company-grid',
    templateUrl: './company-grid.component.html',
    styleUrls: ['./company-grid.component.scss']
})
export class CompanyGridComponent implements Grid {

    public winLoss: WinLossModel;

    constructor(
        private gridService: GridService,
        private companyGridService: CompanyGridService,
    ) { }

    query() {
        this.companyGridService.query().subscribe(data => {
            this.winLoss = data;
            this.gridService.siteGridSubject.next(0);
        });
    }

    exportExcel() {
      //this.ticketService.exportExcel();
    }

    get hasExcel(): boolean {
        return this.hasData;
    }

    get hasData(): boolean {
        return !_.isNil(this.winLoss) && this.winLoss.rows.length > 0;
    }

    get title(): string {
      return this.gridService.username;
    }

    public drillDown(username: string, custId: number, roleId: number) {
      this.gridService.username = username;
      this.gridService.custId = custId;
      this.gridService.roleId = roleId;
      if(roleId === 2) {
        this.gridService.memberGridSubject.next(0);    
      } else {
        this.gridService.companyGridSubject.next(0);
      }
    }
}
