import { Injectable } from '@angular/core';
import { AddBlogPost } from '../models/add-blog-post.model';
import { Observable } from 'rxjs';
import { BlogPost } from '../models/blog-post.model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { UpdateBlogPost } from '../models/update-blog-post.model';

@Injectable({
  providedIn: 'root'
})
export class BlogPostService {

  constructor(private hhtp:HttpClient) { }

  createBlogPost(data:AddBlogPost):Observable<BlogPost>{
    console.log("date: "+data.publishedDate);
    return this.hhtp.post<BlogPost>(`${environment.apiBaseUrl}/api/blogposts`,data);
  }

  getAllBlogPosts():Observable<BlogPost[]>{
    return this.hhtp.get<BlogPost[]>(`${environment.apiBaseUrl}/api/blogposts`);
  }

  getBlogPostById(id:string):Observable<BlogPost>{
    return this.hhtp.get<BlogPost>(`${environment.apiBaseUrl}/api/blogposts/${id}`);
  }

  getBlogPostByUrlHandle(urlHandle:string):Observable<BlogPost>{
    return this.hhtp.get<BlogPost>(`${environment.apiBaseUrl}/api/blogposts/${urlHandle}`);
  }

  updateBlogPost(id:string,data:UpdateBlogPost):Observable<BlogPost>{
    return this.hhtp.put<BlogPost>(`${environment.apiBaseUrl}/api/blogposts/${id}`,data);
  }

  deletePostById(id:string):Observable<BlogPost>{
    return this.hhtp.delete<BlogPost>(`${environment.apiBaseUrl}/api/blogposts/${id}`);
  }
}
