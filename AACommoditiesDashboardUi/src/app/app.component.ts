import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'analytics-dashboard';
  navLinks: any[];
  activeLinkIndex = -1; 

  constructor(private router: Router) {
    this.navLinks = [
        {
            label: 'Metrics',
            link: './metrics',
            index: 0
        }, {
            label: 'Trends',
            link: './trends',
            index: 1
        }, {
            label: 'History',
            link: './history',
            index: 2
        }, 
    ];
  }

  ngOnInit(): void {
    this.router.events.subscribe(() => {
        this.activeLinkIndex = this.navLinks.indexOf(this.navLinks.find(tab => tab.link === '.' + this.router.url));
    });
  }
}
