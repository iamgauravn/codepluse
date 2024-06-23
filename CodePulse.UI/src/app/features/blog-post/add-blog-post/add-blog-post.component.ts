import { Component } from '@angular/core';
import { AddBlogPost } from '../models/add-blog-posts.model';
import { BlogPostService } from '../services/blog-post.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-blog-post',
  templateUrl: './add-blog-post.component.html',
  styleUrl: './add-blog-post.component.css'
})
export class AddBlogPostComponent {

  model: AddBlogPost;

  constructor(private blogPostService : BlogPostService, private router : Router) {
    this.model = {  
      title :'',
      urlHandle:'',
      shortDescription:'',
      featuredImageUrl:'',
      author :'',
      content: '',
      isVisible: true,
      publishedDate : new Date()
    }
  }

  onSubmitForm():void {
    this.blogPostService.createBlogPost(this.model).subscribe({
      next: (response) => {
        this.router.navigate(['/admin/blogposts']);
      }, 
      error: (error) => {

      }
    });
  }

}
