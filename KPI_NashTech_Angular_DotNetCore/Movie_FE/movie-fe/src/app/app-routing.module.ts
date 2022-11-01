import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateActorsComponent } from './actors/create-actors/create-actors.component';
import { EditActorComponent } from './actors/edit-actor/edit-actor.component';
import { IndexActorsComponent } from './actors/index-actors/index-actors.component';
import { AdminGuard } from './admin.guard';
import { CreateCategoryComponent } from './category/create-category/create-category.component';
import { EditCategoryComponent } from './category/edit-category/edit-category.component'
import { IndexCategoryComponent } from './category/index-category/index-category.component';
import { HomeComponent } from './home/home.component';
import { CreateMovieTheaterComponent } from './movie-theater/create-movie-theater/create-movie-theater.component';
import { EditMovieTheaterComponent } from './movie-theater/edit-movie-theater/edit-movie-theater.component';
import { IndexMovieTheaterComponent } from './movie-theater/index-movie-theater/index-movie-theater.component';
import { CreateMovieComponent } from './movies/create-movie/create-movie.component';
import { EditMovieComponent } from './movies/edit-movie/edit-movie.component';
import { MovieDetailComponent } from './movies/movie-detail/movie-detail.component';
import { MovieFilterComponent } from './movies/movie-filter/movie-filter.component';
import { IndexUserComponent } from './security/index-user/index-user.component';
import { LoginComponent } from './security/login/login.component';
import { RegisterComponent } from './security/register/register.component';

const routes: Routes = [
  {path: '', component: HomeComponent },
  {path: 'categories', component: IndexCategoryComponent, canActivate: [AdminGuard]},
  {path: 'categories/create', component: CreateCategoryComponent , canActivate: [AdminGuard]},
  {path: 'categories/edit/:id', component: EditCategoryComponent , canActivate: [AdminGuard]},
  {path: 'actors', component: IndexActorsComponent},
  {path: 'actors/create', component: CreateActorsComponent, canActivate: [AdminGuard] },
  {path: 'actors/edit/:id', component: EditActorComponent, canActivate: [AdminGuard]  },
  {path: 'movietheaters', component: IndexMovieTheaterComponent},
  {path: 'movietheaters/create', component: CreateMovieTheaterComponent, canActivate: [AdminGuard] },
  {path: 'movietheaters/edit/:id', component: EditMovieTheaterComponent, canActivate: [AdminGuard] },
  {path: 'movies/create', component: CreateMovieComponent, canActivate: [AdminGuard] },
  {path: 'movies/edit/:id', component: EditMovieComponent, canActivate: [AdminGuard] },
  {path: 'movies/filter', component: MovieFilterComponent },
  {path: 'movies/:id', component: MovieDetailComponent },  
  {path: 'login', component: LoginComponent },  
  {path: 'register', component: RegisterComponent },  
  {path: 'users', component: IndexUserComponent, canActivate: [AdminGuard] },  
  
  {path: '**',redirectTo:''}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
