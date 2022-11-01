export interface userCredentials{
    username:string;
    email: string;
    password: string;
}

export interface authenticationResponse{
    token: string;
    expiration: Date;
}

export interface userDTO{
    id: string;
    email: string;
    username: string;
    phonenumber: string;
}