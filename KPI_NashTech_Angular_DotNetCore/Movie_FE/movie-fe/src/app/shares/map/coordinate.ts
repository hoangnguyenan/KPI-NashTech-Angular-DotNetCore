export interface coordinatesMap{
    latitude: number;
    longitude: number;
}
//show the popup address on Map
export interface coordinatesMapWithMessage extends coordinatesMap{
    name: string;
    address: string;
} 