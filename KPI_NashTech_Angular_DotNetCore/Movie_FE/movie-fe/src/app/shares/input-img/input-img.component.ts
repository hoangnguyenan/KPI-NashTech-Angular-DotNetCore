import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { toBase64 } from '../Utilities';

@Component({
  selector: 'app-input-img',
  templateUrl: './input-img.component.html',
  styleUrls: ['./input-img.component.css']
})
export class InputImgComponent implements OnInit {

  constructor() { }

  imgBase64! : string;
  @Input()
  urlCurrentImage: File|any;

  @Output()
  onImageSelected = new EventEmitter<File>();
  
  ngOnInit(): void {

  }

  change(event: any) {
    if (event.target.files.length > 0) {
      const file: File = event.target.files[0];
      toBase64(file).then((value: string|any) => this.imgBase64 = value);
      this.onImageSelected.emit(file);
      this.urlCurrentImage = null as any;
    }
  }
}
