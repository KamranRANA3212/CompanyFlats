import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styles: []
})
export class AppComponent implements OnInit {
  title = 'Stallions Tech - Human Resources Management';
  values:Array<string> = [];
  constructor(private httpClient:HttpClient) {}
  ngOnInit() : void{
    this.httpClient.get<Array<string>>(`${environment.apiPath}/values`).subscribe(res => this.values = res);
  }
    
}
