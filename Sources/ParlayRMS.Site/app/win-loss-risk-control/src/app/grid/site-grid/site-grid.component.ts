import { Component } from '@angular/core';
import * as _ from 'lodash';
import { Grid } from '../grid.model';
import { GridService } from '../grid.service';
import { SiteGridService } from './site-grid.service';
import { WinLossModel } from 'src/app/common/win-loss.model';

@Component({
    selector: 'app-site-grid',
    templateUrl: './site-grid.component.html',
    styleUrls: ['./site-grid.component.scss']
})
export class SiteGridComponent implements Grid {

    public siteWinLoss: WinLossModel;

    constructor(
        private gridService: GridService,
        private siteGridService: SiteGridService,
    ) { }

    query() {
        this.siteGridService.query().subscribe(data => {
            this.siteWinLoss = data;
            this.gridService.siteGridSubject.next(1);
        });
    }

    exportExcel() {
        //this.siteGridService.exportExcel();
    }

    get hasExcel(): boolean {
        return this.hasData;
    }

    get hasData(): boolean {
        return !_.isNil(this.siteWinLoss) && this.siteWinLoss.rows.length > 0;
    }

    get title(): string {
        return `Site`;
    }

    public drillDownReport(username: string, isDeposit: boolean) {
      this.gridService.username = username;
      this.gridService.isDeposit = isDeposit;
      this.gridService.companyGridSubject.next(0);
    }

    public onCollapsed(site: WinLossModel) {
      site.isCollapsed = !site.isCollapsed;
    }
}
