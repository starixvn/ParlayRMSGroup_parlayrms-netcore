import { Injectable } from '@angular/core';
import { FilterModel } from '../filter/filter.model';
import { Grid } from './grid.model';
import { Subject } from 'rxjs/Subject';

@Injectable()
export class GridService {
    filterModel: FilterModel;
    activeGrid: Grid;
    custId: number;
    roleId: number;
    username: string;
    isDeposit: boolean;
    companyGridSubject: Subject<number>;
    memberGridSubject: Subject<number>;
    siteGridSubject: Subject<number>;
}
