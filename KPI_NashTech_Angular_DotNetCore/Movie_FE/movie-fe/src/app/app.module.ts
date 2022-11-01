import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http'
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MoviesListComponent } from './movies/movies-list/movies-list.component';
import { GeneralListComponent } from './shares/general-list/general-list.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MarkdownModule } from 'ngx-markdown'
import { LeafletModule } from '@asymmetrik/ngx-leaflet';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2'


import { MaterialModule } from './material/material.module';
import { MenuComponent } from './menu/menu.component';
import { RatingComponent } from './shares/rating/rating.component';
import { HomeComponent } from './home/home.component';
import { IndexCategoryComponent } from './category/index-category/index-category.component';
import { CreateCategoryComponent } from './category/create-category/create-category.component';
import { IndexActorsComponent } from './actors/index-actors/index-actors.component';
import { CreateActorsComponent } from './actors/create-actors/create-actors.component';
import { IndexMovieTheaterComponent } from './movie-theater/index-movie-theater/index-movie-theater.component';
import { CreateMovieTheaterComponent } from './movie-theater/create-movie-theater/create-movie-theater.component';
import { CreateMovieComponent } from './movies/create-movie/create-movie.component';
import { EditActorComponent } from './actors/edit-actor/edit-actor.component';
import { EditMovieTheaterComponent } from './movie-theater/edit-movie-theater/edit-movie-theater.component';
import { EditMovieComponent } from './movies/edit-movie/edit-movie.component';
import { EditCategoryComponent } from './category/edit-category/edit-category.component';
import { FormCategoryComponent } from './category/form-category/form-category.component';
import { MovieFilterComponent } from './movies/movie-filter/movie-filter.component';
import { FormActorComponent } from './actors/form-actor/form-actor.component';
import { InputImgComponent } from './shares/input-img/input-img.component';
import { MarkdownComponent } from './shares/markdown/markdown.component';
import { FormMovieTheaterComponent } from './movie-theater/form-movie-theater/form-movie-theater.component';
import { MapComponent } from './shares/map/map.component';
import { FormMovieComponent } from './movies/form-movie/form-movie.component';
import { MovieCategorySelectComponent } from './shares/movie-category-select/movie-category-select.component';
import { ActorAutocompleteComponent } from './actors/actor-autocomplete/actor-autocomplete.component';
import { ShowErrorsComponent } from './shares/show-errors/show-errors.component';
import { MovieDetailComponent } from './movies/movie-detail/movie-detail.component';
import { AuthorizeViewComponent } from './security/authorize-view/authorize-view.component';
import { LoginComponent } from './security/login/login.component';
import { RegisterComponent } from './security/register/register.component';
import { AuthenticationFormComponent } from './security/authentication-form/authentication-form.component';
import { JwtInterceptorService } from './security/jwt-interceptor.service';
import { IndexUserComponent } from './security/index-user/index-user.component';


@NgModule({
  declarations: [
    AppComponent,
    MoviesListComponent,
    GeneralListComponent,
    MenuComponent,
    RatingComponent,
    HomeComponent,
    IndexCategoryComponent,
    CreateCategoryComponent,
    IndexActorsComponent,
    CreateActorsComponent,
    IndexMovieTheaterComponent,
    CreateMovieTheaterComponent,
    CreateMovieComponent,
    EditActorComponent,
    EditMovieTheaterComponent,
    EditMovieComponent,
    EditCategoryComponent,
    FormCategoryComponent,
    MovieFilterComponent,
    FormActorComponent,
    InputImgComponent,
    MarkdownComponent,
    FormMovieTheaterComponent,
    MapComponent,
    FormMovieComponent,
    MovieCategorySelectComponent,
    ActorAutocompleteComponent,
    ShowErrorsComponent,
    MovieDetailComponent,
    AuthorizeViewComponent,
    LoginComponent,
    RegisterComponent,
    AuthenticationFormComponent,
    IndexUserComponent,
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    ReactiveFormsModule,
    FormsModule,
    LeafletModule,
    HttpClientModule,
    MarkdownModule.forRoot(),
    SweetAlert2Module.forRoot()
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: JwtInterceptorService,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
