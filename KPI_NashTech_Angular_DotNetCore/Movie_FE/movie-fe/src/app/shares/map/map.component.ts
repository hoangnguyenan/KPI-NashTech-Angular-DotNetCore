import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { latLng, LeafletMouseEvent, icon, marker, Marker, tileLayer } from 'leaflet';
import { coordinatesMap, coordinatesMapWithMessage } from './coordinate';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit {

  constructor() { }
  
  @Output()
  onSelectedLocation = new EventEmitter<coordinatesMap>();
  
  @Input()
  initCoordinates: coordinatesMapWithMessage[] = [];

  @Input()
  editMode: boolean = true;

  ngOnInit(): void {    
    this.layers = this.initCoordinates.map(value => {
      const m = marker([value.latitude, value.longitude],this.icon)
      if(value.address){
          m.bindPopup(value.address,{autoClose: false, autoPan: false});
        }
      return m;
    });
  }

  icon = {
    icon: icon({
      iconSize: [ 25, 41 ],
  		iconAnchor:  [12, 41],
      iconUrl: './assets/marker-icon.png',
      shadowUrl: './assets/marker-shadow.png'
    })
  };
  options = {
    layers: [
      tileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 18, attribution: 'Movie CGV',
      })
    ],
    zoom: 12,
    center: latLng(10.495091017940796, 107.16871261596681),
    
  };
  
  layers: Marker<any>[] = [];

  handleMapClick(event: LeafletMouseEvent|any){
    if(this.editMode){
      const latitude = event.latlng.lat;
      const longitude = event.latlng.lng;   
      this.layers = [];
      this.layers.push(marker([latitude,longitude],this.icon))
      this.onSelectedLocation.emit({latitude,longitude})
    }
  }
  
}
