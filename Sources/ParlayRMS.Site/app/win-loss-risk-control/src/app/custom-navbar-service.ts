import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import 'rxjs/add/operator/map';
import 'rxjs/observable/of';
import { HttpService } from 'nap-components/http';
import { NavbarService, UserMenu, Menu, Navbar } from 'nap-components/navbar';

@Injectable()
export class CustomNavbarService extends NavbarService {
    userMenus: UserMenu[] = [];
    constructor(
        private customHttpService: HttpService
    ) {
        super(customHttpService);
    }

    query(): Observable<Navbar> {
      const url = 'api/application/header';
      return this.customHttpService.post<Navbar>(url).pipe(map(data => {
          this.userMenus = [
              {
                  name: 'Sign Out',
                  link: '/UserLogin/Login/LogOut'
              }
          ];

          return data;
      }));
  }
}
