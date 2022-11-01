import { actorMovieDTO } from "../actors/actor.model";
import { categoryDTO } from "../category/category.model";
import { movieTheaterDTO } from "../movie-theater/movie-theater.model";

export interface movieCreateDTO {
    title: string;
    summary: string;
    poster: File;
    inCinemas: boolean;
    releaseDate: Date;
    trailer: string;
    categoriesId: number[];
    cinemasId: number[];
    actors: actorMovieDTO[];
}
export interface movieDTO {
    id: number;
    title: string;
    summary: string;
    poster: string;
    inCinemas: boolean;
    releaseDate: Date;
    trailer: string;
    averageVote: number;
    userVote: number;
    categories: categoryDTO[];
    movieTheaters: movieTheaterDTO[];
    actors: actorMovieDTO[];
}

export interface MoviePostGetDTO{
    categories: categoryDTO[];
    movieTheaters: movieTheaterDTO[];
}

export interface homeDTO{
    inCinemas: movieDTO[];
    upComingReleases: movieDTO[];
}

export interface MoviePutGetDTO{
    movie: movieDTO;
    selectedCategories: categoryDTO[];
    nonSelectedCategories: categoryDTO[];
    selectedMovieTheaters: movieTheaterDTO[];
    nonSelectedMovieTheaters: movieTheaterDTO[];
    actors: actorMovieDTO[];
}