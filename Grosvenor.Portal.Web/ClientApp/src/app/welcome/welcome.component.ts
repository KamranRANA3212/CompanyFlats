import { Component, OnInit } from '@angular/core';
import { AppService } from '../app.service';

@Component({
  selector: 'app-welcome',
  templateUrl: './welcome.component.html',
  styles: [
  ]
})
export class WelcomeComponent implements OnInit {
  identity;
  constructor(private appService: AppService) {
    this.identity = appService.identity;
  }

  ngOnInit(): void {
  }

}
