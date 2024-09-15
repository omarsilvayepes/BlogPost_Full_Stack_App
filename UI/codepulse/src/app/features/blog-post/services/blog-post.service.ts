import { Injectable } from '@angular/core';
import { AddBlogPost } from '../models/add-blog-post.model';
import { Observable } from 'rxjs';
import { BlogPost } from '../models/blog-post.model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BlogPostService {

  constructor(private hhtp:HttpClient) { }

  createBlogPost(data:AddBlogPost):Observable<BlogPost>{
    return this.hhtp.post<BlogPost>(`${environment.apiBaseUrl}/api/blogposts`,data);
  }

  getAllBlogPosts():Observable<BlogPost[]>{
    return this.hhtp.get<BlogPost[]>(`${environment.apiBaseUrl}/api/blogposts`);
  }

  getBlogPostById(id:string):Observable<BlogPost>{
    return this.hhtp.get<BlogPost>(`${environment.apiBaseUrl}/api/blogposts/${id}`);
  }
}