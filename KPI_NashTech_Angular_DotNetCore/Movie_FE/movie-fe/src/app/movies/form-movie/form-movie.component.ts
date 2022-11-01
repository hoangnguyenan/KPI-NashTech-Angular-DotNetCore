import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { actorMovieDTO } from 'src/app/actors/actor.model';
import { SelectModel } from 'src/app/shares/movie-category-select/movie-category-select.model';
import { firstLetterUppercase } from 'src/app/validators/firstLetterUppercase';
import { movieCreateDTO, movieDTO } from '../movie.model';

@Component({
  selector: 'app-form-movie',
  templateUrl: './form-movie.component.html',
  styleUrls: ['./form-movie.component.css']
})
export class FormMovieComponent implements OnInit {

  constructor(private formBuilder: FormBuilder) { }
  form:any = FormBuilder

  @Input()
  model: movieDTO|any

  @Output()
  onSaveChanges = new EventEmitter<movieCreateDTO>();

  @Input()
  nonSelectedCategories: SelectModel[] = [];

  @Input()
  selectedCategories: SelectModel[] = [];

  @Input()
  nonSelectedMovieTheaters: SelectModel[] = []
  
  @Input()
  selectedMovieTheaters: SelectModel[] = [];

  @Input()
  selectedActors : actorMovieDTO[] = [];

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      title: ['',{
        validators: [
          Validators.required, 
          Validators.minLength(2),
          Validators.maxLength(50),
          firstLetterUppercase()
        ]
      }],
      summary:['',{
        validators: [
          Validators.required
        ]
      }],
      inCinemas: false,
      trailer: ['',{
        validators: [
          Validators.required
        ]
      }],
      releaseDate: ['',{
        validators: [
          Validators.required
        ]
      }],
      poster:['',{
        validators: [
          Validators.required
        ]
      }],
      categoriesId:'',
      cinemasId:'',
      actors :''
    });
    if(this.model !== true){
      this.form.patchValue(this.model);
    }
  }

  onImageSelected(file: File){
    this.form.get('poster').setValue(file);
  }
  changeMarkdown(content: string){
    this.form.get('summary').setValue(content);
  }
  
  saveChanges(){
    const categoriesId = this.selectedCategories.map(value => value.key);
    this.form.get('categoriesId').setValue(categoriesId);

    const cinemasId = this.selectedMovieTheaters.map(value => value.key);
    this.form.get('cinemasId').setValue(cinemasId);

    const actors = this.selectedActors.map(value => {
      return {id : value.id, character: value.character}
    });
    this.form.get('actors').setValue(actors);
    this.onSaveChanges.emit(this.form.value);      
  }
  getErrorMessageName(){
    const title = this.form.get('title');
    if (title.hasError('required')) {
      return 'The title is required';
    }
    if (title.hasError('minlength')) {
      return 'The minimum length is 2';
    }
    if (title.hasError('maxlength')) {
      return 'The maximum length is 50';
    }
    if (title.hasError('firstLetterUppercase')) {
      return title.getError('firstLetterUppercase').message;
    }
    return '';
  }
  getErrorMessageTrailer(){
    const trailer = this.form.get('trailer');
    if (trailer.hasError('required')) {
      return 'Trailer is required';
    }
    return '';
  }
  getErrorMessageReleaseDay(){
    const rDay = this.form.get('releaseDate');
    if (rDay.hasError('required')) {
      return 'Release day is required';
    }
    return '';
  }
}

