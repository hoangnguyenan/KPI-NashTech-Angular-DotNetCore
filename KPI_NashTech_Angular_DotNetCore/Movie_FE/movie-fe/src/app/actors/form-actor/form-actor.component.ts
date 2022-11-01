import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { firstLetterUppercase } from 'src/app/validators/firstLetterUppercase';
import { IsValidName } from 'src/app/validators/IsValidName';
import { actorCreateDTO, actorDTO } from '../actor.model';

@Component({
  selector: 'app-form-actor',
  templateUrl: './form-actor.component.html',
  styleUrls: ['./form-actor.component.css']
})
export class FormActorComponent implements OnInit{

  constructor(private formBuilder: FormBuilder) { }
  form : FormGroup| any;

  @Input()
  model!: actorDTO;

  @Output()
  onSaveChanges = new EventEmitter<actorCreateDTO>();
  
  ngOnInit(): void {
    this.form = this.formBuilder.group({
      name: ['',{
        validators: [
          Validators.required, 
          Validators.minLength(2),
          Validators.maxLength(50),
          IsValidName(),
          firstLetterUppercase()
        ]
      }],
      dob:['',{ 
        validators:[ Validators.required]
      }],
      picture:['',{ 
        validators:[ Validators.required]
      }],
      story:['',{ 
        validators:[ Validators.required, Validators.maxLength(1000)]
      }]
    });
    if (this.model !== undefined){
      this.form.patchValue(this.model);
    }
  }
  onImageSelected(image:any){
    this.form.get('picture').setValue(image);
  }
  changeMarkdown(content: any){
    this.form.get('story').setValue(content);
  }
  saveChanges(){
    this.onSaveChanges.emit(this.form.value);    
  }
  getErrorMessageName(){
    const name = this.form.get('name');
    if (name.hasError('required')) {
      return 'The name is required';
    }
    if (name.hasError('minlength')) {
      return 'The minimum length is 2';
    }
    if (name.hasError('maxlength')) {
      return 'The maximum length is 50';
    }
    if (name.hasError('firstLetterUppercase')) {
      return name.getError('firstLetterUppercase').message;
    }
    if (name.hasError('IsValidName')) {
      return name.getError('IsValidName').message;
    }    
    return '';
  }
  getErrorMessageDob(){
    const dob = this.form.get('dob');
    if (dob.hasError('required')) {
      return 'The name is required';
    }
    return '';
  }
}
