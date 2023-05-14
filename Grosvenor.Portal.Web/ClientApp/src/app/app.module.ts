import { APP_INITIALIZER, NgModule } from '@angular/core';
import { HttpClientModule, HttpClientXsrfModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { NgZorroAntdModule, AntdIcons } from "./ng-zorro.module";
import { NZ_ICONS } from "ng-zorro-antd/icon";
import { NZ_I18N } from "ng-zorro-antd/i18n";
import { en_US } from "ng-zorro-antd/i18n";

import { AppComponent } from './app.component';
import { AgGridModule } from 'ag-grid-angular';
import { AppRoutingModule } from './app-routing.module';
import { AppInterceptor } from './app-interceptor';
import { AppService } from './app.service';
import { WelcomeComponent } from './welcome/welcome.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';


@NgModule({
  declarations: [
    AppComponent,
    WelcomeComponent,
    PageNotFoundComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    HttpClientXsrfModule.disable(),
    NgZorroAntdModule,
    FormsModule,
    ReactiveFormsModule,
    AgGridModule,
    AppRoutingModule

  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AppInterceptor, multi: true },
    { provide: NZ_I18N, useValue: en_US },
    { provide: NZ_ICONS, useValue: AntdIcons },
    {
      provide: APP_INITIALIZER,
      useFactory: (appService: AppService) => () => appService.getIdentity(),
      multi: true,
      deps: [AppService],
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
