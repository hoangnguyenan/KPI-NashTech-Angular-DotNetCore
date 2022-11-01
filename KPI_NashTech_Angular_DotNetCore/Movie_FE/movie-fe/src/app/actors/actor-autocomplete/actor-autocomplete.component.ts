import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { MatTable } from '@angular/material/table';
import { actorCreateDTO, actorMovieDTO } from '../actor.model';
import { ActorService } from '../actor.service';

@Component({
  selector: 'app-actor-autocomplete',
  templateUrl: './actor-autocomplete.component.html',
  styleUrls: ['./actor-autocomplete.component.css']
})
export class ActorAutocompleteComponent implements OnInit {

  constructor(private actorService: ActorService) { }

  control: FormControl = new FormControl();

  @Input()
  selectedActors: actorMovieDTO[] = [];

  displayActor : actorMovieDTO[] = [];

  columnToDisplay = ['picture','name', 'character','actions']

  @ViewChild(MatTable) table!: MatTable<any>;

  ngOnInit(): void {
    this.control.valueChanges.subscribe(value => {
      this.actorService.searchByName(value).subscribe(actors =>{
        this.displayActor = actors;
      });
    });
  }
  optionSelected(event: MatAutocompleteSelectedEvent) {
    this.control.patchValue('');
    //checkexist if actor is selected
    if(this.selectedActors.findIndex(x => x.id == event.option.value.id) !== -1)
    {
      return;
    }
    this.selectedActors.push(event.option.value);
    if(this.table !== undefined){
      this.table.renderRows();
    }
  }
  
  remove(actor:any){
    const index = this.selectedActors.findIndex((a:any) => a.nam === actor.name);
    this.selectedActors.splice(index,1);
    this.table.renderRows();
  }

  dropped(event: CdkDragDrop<any[]>){
    const previousIndex = this.selectedActors.findIndex((actor:any) => actor === event.item.data);
    moveItemInArray(this.selectedActors, previousIndex, event.currentIndex)
    this.table.renderRows();
  }
}
