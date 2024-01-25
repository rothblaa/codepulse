import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-blog-details',
  templateUrl: './blog-details.component.html',
  styleUrls: ['./blog-details.component.css']
})
export class BlogDetailsComponent implements OnInit {

  url: string | null = null;

  /**
   *
   */
  constructor(private route: ActivatedRoute) {

  }

  ngOnInit(): void {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
    this.route.paramMap
    .subscribe({
      next: (params) => {
        this.url = params.get('url');
      }
    });

    // Fetch blog details by url



  }

}
