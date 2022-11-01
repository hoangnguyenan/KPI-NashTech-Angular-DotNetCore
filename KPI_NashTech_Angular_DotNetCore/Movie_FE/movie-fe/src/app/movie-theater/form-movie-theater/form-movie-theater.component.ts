import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { coordinatesMap, coordinatesMapWithMessage } from 'src/app/shares/map/coordinate';
import { movieTheaterCreateDTO } from '../movie-theater.model';
import { icon } from 'leaflet';
import { IsValidAddress, IsValidName } from 'src/app/validators/IsValidName';
import { firstLetterUppercase } from 'src/app/validators/firstLetterUppercase';

@Component({
  selector: 'app-form-movie-theater',
  templateUrl: './form-movie-theater.component.html',
  styleUrls: ['./form-movie-theater.component.css']
})
export class FormMovieTheaterComponent implements OnInit {

  constructor(private formBuilder: FormBuilder) { }

  form: any = FormBuilder

  @Output()
  onSaveChanges = new EventEmitter<movieTheaterCreateDTO>();

  @Input()
  model: movieTheaterCreateDTO|any;

  initCoordinates: coordinatesMapWithMessage[] = [];
  icon = {
    icon: icon({
      iconSize: [ 25, 41 ],
  		iconAnchor:  [12, 41],
      iconUrl: './assets/marker-icon.png',
      shadowUrl: './assets/marker-shadow.png'
    })
  };

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      name: ['', {
        validators: [
          Validators.required, 
          Validators.minLength(2),
          Validators.maxLength(50),
          IsValidName(),
          firstLetterUppercase()
        ]
      }],
      address: ['', {
        validators: [
          Validators.required, 
          Validators.minLength(2),
          Validators.maxLength(100),
          IsValidAddress(),
          firstLetterUppercase()
        ]
      }],
      latitude: ['', {
        validators: [Validators.required]
      }],
      longitude: ['', {
        validators: [Validators.required]
      }]
    });
    if(this.model !== undefined){
      this.form.patchValue(this.model);
      this.initCoordinates.push({
        latitude:this.model.latitude,
        longitude:this.model.longitude,
        name: this.model.name,
        address: this.model.address
      });
    }
  }
  onSelectedLocation(coordinate: coordinatesMap){
    this.form.patchValue(coordinate);
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
  getErrorMessageAddress(){
    const address = this.form.get('address');
    if (address.hasError('required')) {
      return 'The address is required';
    }
    if (address.hasError('minlength')) {
      return 'The minimum length is 2';
    }
    if (address.hasError('maxlength')) {
      return 'The maximum length is 100';
    }
    if (address.hasError('firstLetterUppercase')) {
      return address.getError('firstLetterUppercase').message;
    }
    if (address.hasError('IsValidAddress')) {
      return address.getError('IsValidAddress').message;
    }    
    return '';
  }

}
