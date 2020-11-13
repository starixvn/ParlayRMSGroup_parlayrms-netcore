import {
    Component,
    OnInit,
    Input,
    ViewContainerRef,
    ViewChild,
    Renderer2,
    ComponentFactoryResolver,
    ComponentRef,
    ComponentFactory,
    OnDestroy,
    OnChanges,
} from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { FilterModel } from '../filter/filter.model';
import { SiteGridComponent } from './site-grid/site-grid.component';
import { CompanyGridComponent } from './company-grid/company-grid.component';
import { Grid, GridType } from './grid.model';
import { GridService } from './grid.service';
import { Subject } from 'rxjs/Subject';
import { MemberGridComponent } from './member-grid/member-grid.component';

@Component({
    selector: 'app-grid',
    templateUrl: './grid.component.html',
    styleUrls: ['./grid.component.scss'],
    entryComponents: [
        SiteGridComponent,
        CompanyGridComponent, 
        MemberGridComponent
    ]
})
export class GridComponent implements OnInit, OnChanges, OnDestroy {
    @Input() filterModel: FilterModel;
    @ViewChild('container', { read: ViewContainerRef, static: true }) container: ViewContainerRef;
    gridType = GridType.Site;
    breadcrumbItems: string[];
    private showSiteGridSubscription: Subscription;
    private showCompanyGridSubscription: Subscription;
    private showMemberGridSubscription: Subscription;
    private latestComponent: ComponentRef<Grid>;
    private components: ComponentRef<Grid>[] = [];
    private filterChanging: boolean;

    constructor(
        private componentFactoryResolver: ComponentFactoryResolver,
        private renderer: Renderer2,
        private gridService: GridService) { }

    ngOnInit() {
        this.gridService.siteGridSubject = new Subject<number>();
        this.gridService.companyGridSubject = new Subject<number>();
        this.gridService.memberGridSubject = new Subject<number>();

        this.showSiteGridSubscription = this.gridService.siteGridSubject.subscribe(() => this.showSiteGridCompleted());
        this.showCompanyGridSubscription = this.gridService.companyGridSubject.subscribe(() => this.generateCompanyGrid());
        this.showMemberGridSubscription = this.gridService.memberGridSubject.subscribe(() => this.generateMemberGrid());
    }

    ngOnChanges() {
        this.filterChanging = true;
        this.gridService.filterModel = this.filterModel;
        this.latestComponent = this.createGridComponent();
    }

    ngOnDestroy() {
        this.clear();
        this.showSiteGridSubscription.unsubscribe();
        this.showCompanyGridSubscription.unsubscribe();
        this.showMemberGridSubscription.unsubscribe();
    }

    private clear() {
        this.container.clear();
        this.components = [];
    }

    private generateCompanyGrid() {
        this.gridType = GridType.Company;
        this.latestComponent = this.createGridComponent();
    }

    private generateMemberGrid() {
        this.gridType = GridType.Member;
        this.latestComponent = this.createGridComponent();
    }

    private getComponentFactory(): ComponentFactory<Grid> {
        switch (this.gridType) {
            case GridType.Site:
                return this.componentFactoryResolver.resolveComponentFactory(SiteGridComponent);
            case GridType.Company:
                return this.componentFactoryResolver.resolveComponentFactory(CompanyGridComponent);
            case GridType.Member:
                return this.componentFactoryResolver.resolveComponentFactory(MemberGridComponent);
          
            default:
                throw new Error('Not implemented yet');
        }
    }

    private createGridComponent(): ComponentRef<Grid> {
        const factory = this.getComponentFactory();
        const component = this.container.createComponent(factory);
        const grid = component.instance;
        grid.query();
        return component;
    }

    private showSiteGridCompleted() {
        if (this.filterChanging) {
          let removeIndex = 0;
          switch (this.gridType) {
            case GridType.Site:
              removeIndex = 0;
              break;
            case GridType.Company:
              removeIndex = 1;
              break;
          }
          this.destroyComponentsFrom(removeIndex);
        }

        this.components.push(this.latestComponent);
        this.filterChanging = false;
        this.showGridAndBreadcrumb();
    }

    private destroyComponentsFrom(index: number) {
        while (this.components.length - 1 >= index) {
            const component = this.components.pop();
            component.destroy();
        }
    }

    private showGridAndBreadcrumb() {
        this.gridService.activeGrid = this.components[this.components.length - 1].instance;
        this.updateBreadcrumb();
        this.showOnlyLastGrid();
    }

    private updateBreadcrumb() {
        this.breadcrumbItems = [];
        this.components.forEach(component => {
            const componentTitle = component.instance.title;
            this.breadcrumbItems.push(componentTitle);
        });
    }

    private showOnlyLastGrid() {
      const length = this.components.length;
      const currentComponent = this.components[length - 1];
      this.renderer.removeAttribute(currentComponent.location.nativeElement, 'hidden');

      if (length > 1) {
        const lastComponent = this.components[length - 2];
        this.renderer.setAttribute(lastComponent.location.nativeElement, 'hidden', 'hidden');
      }
    }

    onBreadcrumbItemClick(event: Event) {
        this.gridType = GridType.Company;
        this.destroyComponentsFrom(1);
        this.showGridAndBreadcrumb();
    }

    exportExcel() {
        if (this.components.length === 0) {
            return;
        }
        const component = this.components[this.components.length - 1];
        const grid = component.instance;
        grid.exportExcel();
    }

}
