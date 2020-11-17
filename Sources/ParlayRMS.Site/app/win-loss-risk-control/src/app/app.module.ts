import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FilterComponent } from './filter/filter.component';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { ToastService, ToastModule } from 'nap-components/toast';
import { DateModule } from 'nap-components/date';
import { NavbarModule, NavbarService } from 'nap-components/navbar';
import { SpinnerModule } from 'nap-components/spinner';
import { HttpModule } from 'nap-components/http';
import { TreeviewModule } from 'ngx-treeview';
import { CompanyTreeviewPipe } from './filter/company-treeview.pipe';
import { SportTreeviewPipe } from './filter/sport-treeview.pipe';
import { ParlayTreeviewPipe } from './filter/parlay-treeview.pipe';
import { GridModule } from './grid/grid.module';
import { CustomNavbarService } from './custom-navbar-service';

@NgModule({
  declarations: [
    AppComponent,
    FilterComponent,
    CompanyTreeviewPipe,
    SportTreeviewPipe,
    ParlayTreeviewPipe
  ],
  imports: [
    BrowserModule,
    FormsModule,
    DateModule,
    HttpModule,
    NavbarModule,
    SpinnerModule,
    GridModule,
    ToastModule,
    TreeviewModule.forRoot()
  ],
  providers: [
    { provide: NavbarService, useClass: CustomNavbarService },
    ToastService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
