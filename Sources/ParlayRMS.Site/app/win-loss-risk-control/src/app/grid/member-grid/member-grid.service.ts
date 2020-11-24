import { Injectable } from '@angular/core';
import * as moment from 'moment';
import { HttpService } from 'nap-components/http';
import { GridService } from '../grid.service';
import { Observable } from 'rxjs/Observable';
import { Helper } from 'nap-components/helper';
import { WinLossModel } from 'src/app/common/win-loss.model';
import { MemberGridModel, MemberWinLossModel } from './member-grid.model';
import { Role } from 'src/app/common/role.model';

@Injectable()
export class MemberGridService {
    public model: MemberGridModel;
    constructor(
        private readonly httpService: HttpService,
        private readonly gridService: GridService
    ) { }

    public query(): Observable<MemberWinLossModel> {
        const queryModel = this.createQueryModel();
        return this.httpService.post<MemberWinLossModel>('api/win-loss-risk-control/agent-win-loss', queryModel);
    }

    private createQueryModel() {
        const filterModel = this.gridService.filterModel;
        return {
            fromDate: filterModel.fromDate,
            toDate: filterModel.toDate,
            custId: this.model.custId,
            roleId: Role.agent,
            parlayType: filterModel.parlayType,
            sportType: filterModel.sportType,
            pageSize: filterModel.pageSize,
            pageIndex: filterModel.pageIndex
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
