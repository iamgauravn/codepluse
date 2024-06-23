import { Component, OnInit } from '@angular/core';
import { BlogPostService } from '../services/blog-post.service';
import { Observable } from 'rxjs';
import { BlogPost } from '../models/blog-post.model';

@Component({
  selector: 'app-blog-post-list',
  templateUrl: './blog-post-list.component.html',
  styleUrl: './blog-post-list.component.css'
})
export class BlogPostListComponent implements OnInit {

  blogPost$? : Observable<BlogPost[]>;

  constructor(private blogPostService : BlogPostService) {

  }

  ngOnInit(): void {
    this.blogPost$ = this.blogPostService.getAllBlogPost();
  }

}
