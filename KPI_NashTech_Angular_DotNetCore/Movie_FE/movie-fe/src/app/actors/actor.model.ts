export interface actorCreateDTO {
    name: string;
    dob: Date;
    story: string;
    picture: File;
}
export interface actorDTO {
    id: number
    name: string;
    dob: Date;
    story: string;
    picture: string; //truyen xuong DB
}

export interface actorMovieDTO{
    id: number
    name: string;
    character: Date;
    picture: string;
}