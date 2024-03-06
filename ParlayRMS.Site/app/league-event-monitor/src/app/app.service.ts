import { Injectable } from '@angular/core';
import { HttpService } from 'nap-components/http';
import { Helper } from 'nap-components/helper';
import { Observable } from 'rxjs';
import { EventDetailModel } from './event-detail/event-detail.model';
import { EventGroupModel } from './event/event.model';
import { ExportExcelModel, FilterDetailModel, FilterModel } from './filter/filter.model';
import { LeagueDataModel } from './league/league.model';

@Injectable({
  providedIn: 'root'
})
export class AppService {

  private api = 'api/league-event-control/leagueevent/';
  constructor(private readonly httpService: HttpService)
  { }

  public getLeaguesData(query: FilterModel): Observable<LeagueDataModel[]> {
    return this.httpService.post(`${this.api}league-data`, query);
  }

  public getEvents(query: FilterModel): Observable<EventGroupModel[]> {
    return this.httpService.post(`${this.api}event`, query);
  }

  public getEventDetail(query: FilterDetailModel): Observable<EventDetailModel[]> {
    return this.httpService.post(`${this.api}event-detail`, query);
  }

  public exportExcel(query: ExportExcelModel): void {
    const api = `${this.api}export-excel`;
    this.httpService.download(api, query).subscribe(response => {
        const fileName = `League_EventMonitor.xlsx`;
        Helper.downloadFile(response, fileName);
    });
  }
}
