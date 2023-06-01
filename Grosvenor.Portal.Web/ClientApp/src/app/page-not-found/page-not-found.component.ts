import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';


@Component({
  selector: 'app-page-not-found',
  templateUrl: './page-not-found.component.html',
  styles: []
})
export class PageNotFoundComponent implements OnInit {

  constructor(private title:Title) {
    //this.title.setTitle('Page not found');
   }
  ngOnInit() {
    
  }

}
