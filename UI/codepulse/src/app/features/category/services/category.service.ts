import { Injectable } from '@angular/core';
import { AddCategoryRequest } from '../models/add-category-request.model';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Category } from '../models/category.model';
import { environment } from 'src/environments/environment.development';
import { UpdateCategoryRequest } from '../models/update-category-request.model';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

/* instead of pass it manually this header :  {headers:{
        'Authorization':this.cookieService.get('Authorization')
      }} to  each request we use interceptor component plus adding ?addAuth=true in the url*/

  constructor(private http:HttpClient,
    private cookieService:CookieService
  ) {

   }

  addCategory(model:AddCategoryRequest):Observable<void>{
    return this.http.post<void>(`${environment.apiBaseUrl}/api/Categories?addAuth=true`,model);
  }

  getAllCtegories(query?:string,sortBy?:string,sortDirection?:string):Observable<Category[]>{
    //Add Query string parameters (?)

    let params=new HttpParams();

    if(query){
      params=params.set('query',query);
    }

    if(sortBy){
      params=params.set('sortBy',sortBy);
    }

    if(sortDirection){
      params=params.set('sortDirection',sortDirection);
    }

    return this.http.get<Category[]>(`${environment.apiBaseUrl}/api/Categories`,{
      params:params
    });
  }

  getCategoryById(id:string):Observable<Category>{
    return this.http.get<Category>(`${environment.apiBaseUrl}/api/Categories/${id}`);
  }

  updateCategory(id:string,updateCategoryRequest:UpdateCategoryRequest):Observable<Category>{
     return this.http.put<Category>(`${environment.apiBaseUrl}/api/Categories/${id}?addAuth=true`,
      updateCategoryRequest
    );
  }

  deleteCategoryById(id:string):Observable<Category>{
    return this.http.delete<Category>(`${environment.apiBaseUrl}/api/Categories/${id}?addAuth=true`);
  }

}
