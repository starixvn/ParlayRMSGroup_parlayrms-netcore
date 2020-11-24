import { Injectable } from '@angular/core';
import { HttpService } from 'nap-components/http';
import { Observable } from 'rxjs';
import { Company, ParlayType, Sport } from './filter.model';

@Injectable()
export class FilterService {
  private api = 'api/win-loss-risk-control/filter/';
  constructor(private httpService: HttpService)
  { }

  public getSports(): Observable<Sport[]> {
    return this.httpService.post(`${this.api}sports`);
  }

  public getParlay(): Observable<ParlayType[]> {
    return this.httpService.post(`${this.api}parlay-types`);
  }

  public getCompany(): Observable<Company[]> {
    return this.httpService.post(`${this.api}company`);
  }
}
