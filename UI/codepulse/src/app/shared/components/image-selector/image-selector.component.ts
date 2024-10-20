import { Component, OnInit, ViewChild } from '@angular/core';
import { ImageService } from './image.service';
import { Observable } from 'rxjs';
import { BlogImage } from '../../models/blog-image.model';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-image-selector',
  templateUrl: './image-selector.component.html',
  styleUrls: ['./image-selector.component.css']
})
export class ImageSelectorComponent implements OnInit {

  private file ?:File;
  fileName:string='';
  title:string='';
  @ViewChild('form',{static:false}) imageUploadForm?:NgForm;

  images$?:Observable<BlogImage[]>;

  constructor(private imageService:ImageService) {


  }
  ngOnInit(): void {
   this.getImages();
  }

  private getImages(){
    this.images$ =this.imageService.getImages();
  }


  onFileUploadChange(event:Event):void{
    const element=event.currentTarget as HTMLInputElement;
    this.file=element.files?.[0];

  }

  uploadImage():void{
    if(this.file && this.fileName !=='' && this.title !==''){
      //image service upload the image

      this.imageService.uploadImage(this.file,this.fileName,this.title)
      .subscribe({
        next:(response)=>{
          this.imageUploadForm?.resetForm(); // clear the form when the image it is uploaded
          this.getImages();
        }
      });
      

    }
  }

}
