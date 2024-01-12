import { Injectable } from '@angular/core';
import { HttpService } from 'nap-components/http';
import { Observable } from 'rxjs';
import { Company, SelectItem, Sport } from './filter.model';

@Injectable()
export class FilterService {
  private api = 'api/win-loss-risk-control/filter/';
  constructor(private httpService: HttpService)
  { }

  public getSports(): Observable<Sport[]> {
    return this.httpService.post(`${this.api}sports`);
  }

  public getParlay(): Observable<SelectItem[]> {
    return this.httpService.post(`${this.api}parlay-types`);
  }

  public getCompany(): Observable<Company[]> {
    return this.httpService.post(`${this.api}company`);
  }

  public getCombo(): Observable<SelectItem[]> {
    return this.httpService.post(`${this.api}comboes`);
  }

  public getLiveTypes(): SelectItem[] {
    return [
      { id: 3, name: 'All' },
      { id: 1, name: 'Live' },
      { id: 0, name: 'None Live' },
      { id: 2, name: 'Mix' }
    ]
  }
}
