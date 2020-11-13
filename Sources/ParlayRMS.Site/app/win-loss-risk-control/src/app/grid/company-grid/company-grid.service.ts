import { Injectable } from '@angular/core';
import * as moment from 'moment';
import { HttpService } from 'nap-components/http';
import { GridService } from '../grid.service';
import { Observable } from 'rxjs/Observable';
import { Helper } from 'nap-components/helper';
import { WinLossModel } from 'src/app/common/win-loss.model';
import * as _ from 'lodash';

@Injectable()
export class CompanyGridService {
    constructor(
        private readonly httpService: HttpService,
        private readonly gridService: GridService
    ) { }

    public query(): Observable<WinLossModel> {
        const queryModel = this.createQueryModel();
        let api = !_.isNull(this.gridService.roleId) && this.gridService.roleId > 0 
                ? "api/win-loss-risk-control/agent-win-loss"
                : "api/win-loss-risk-control/company-win-loss";
        return this.httpService.post<WinLossModel>(api, queryModel);
    }

    private createQueryModel() {
        const filterModel = this.gridService.filterModel;
        if (!_.isNull(this.gridService.roleId) && this.gridService.roleId > 0) {
            return {
                fromDate: filterModel.fromDate,
                toDate: filterModel.toDate,
                custId: this.gridService.custId,
                roleId: this.gridService.roleId,
                parlayType: filterModel.parlayType,
                sportType: filterModel.sportType,
                pageSize: filterModel.pageSize,
                pageIndex: filterModel.pageIndex
            };
        }
        
        return {
            fromDate: filterModel.fromDate,
            toDate: filterModel.toDate,
            company: this.gridService.username,
            isDeposit: this.gridService.isDeposit,
            parlayType: filterModel.parlayType,
            sportType: filterModel.sportType,
        };
    }
    // exportExcel() {
    //     const queryModel = this.gridService.filterModel;
    //     const winlostDate = moment(queryModel.winlostDate).format('YYYY_MM_DD');
    //     this.httpService.download('api/voiding-settled-tickets/products-excel', queryModel).subscribe(response => {
    //         const fileName = `Voiding_Settled_Tickets_Report_For_${winlostDate}.xlsx`;
    //         Helper.downloadFile(response, fileName);
    //     });
    // }
}
