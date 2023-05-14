import { Component, OnInit } from "@angular/core";
import { Title } from "@angular/platform-browser";
import {
  NavigationEnd,
  NavigationStart,
  RouteConfigLoadEnd,
  RouteConfigLoadStart,
  Router,
} from "@angular/router";
import { AppService } from "./app.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  isLoading: boolean = false;
  isCollapsed = false;
  identity;
  filter = "";
  title = "Compnay Flats";
  year = new Date().getFullYear();
  breadcrumb: Array<any> = [];
  constructor(
    private pageTitle: Title,
    private appService: AppService,
    router: Router
  ) {
    this.identity = appService.identity;
    router.events.subscribe((event) => {
      if (event instanceof RouteConfigLoadStart) {
        this.isLoading = true;
      } else if (event instanceof RouteConfigLoadEnd) {
        this.isLoading = false;
      }
    });
  }
  ngOnInit(): void {
    this.pageTitle.setTitle(this.title);
    this.appService.breadcrumbMenu.subscribe((data: any) => {
      this.breadcrumb = data;
    });
  }
  onSearchChange(value: string) {
    this.appService.searchInput.next(value);
  }
  onCollapseClick() {
    this.isCollapsed = !this.isCollapsed;
    this.appService.gridSize.next(true);
  }
}