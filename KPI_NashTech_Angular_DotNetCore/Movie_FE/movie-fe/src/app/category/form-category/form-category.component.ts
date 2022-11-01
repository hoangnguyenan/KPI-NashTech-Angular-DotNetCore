import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { firstLetterUppercase } from 'src/app/validators/firstLetterUppercase';
import { IsValidName } from 'src/app/validators/IsValidName';
import { categoryCreateDTO } from '../category.model';

@Component({
  selector: 'app-form-category',
  templateUrl: './form-category.component.html',
  styleUrls: ['./form-category.component.css']
})
export class FormCategoryComponent implements OnInit {

  constructor(private formBuilder: FormBuilder) { }

  @Input()
  model: categoryCreateDTO | any;

  form: FormGroup| any;

  
  @Output()
  onSaveChanges: EventEmitter<categoryCreateDTO> = new EventEmitter<categoryCreateDTO>();

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      name: ['',{
        validators: [
          Validators.required,
          Validators.minLength(2),
          Validators.maxLength(256),
          IsValidName(),
          firstLetterUppercase()
        ]
      }]      
    });
    if (this.model !== undefined){
      this.form.patchValue(this.model);
    }
  }

  saveChanges() {
    this.onSaveChanges.emit(this.form.value);
  }
  getErrorMessageName() {
    const name = this.form.get('name');
    if (name.hasError('required')) {
      return 'The name name is required';
    }
    if (name.hasError('minlength')) {
      return 'The minimum length is 2';
    }
    if (name.hasError('maxlength')) {
      return 'The maximum length is 256';
    }
    if (name.hasError('IsValidName')) {
      return name.getError('IsValidName').message;
    }   
    if (name.hasError('firstLetterUppercase')) {
      return name.getError('firstLetterUppercase').message;
    }
    return '';
  }

}
