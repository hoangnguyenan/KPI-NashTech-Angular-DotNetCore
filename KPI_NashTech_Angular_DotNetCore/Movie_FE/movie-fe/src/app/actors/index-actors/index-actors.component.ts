import { HttpResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { actorDTO } from '../actor.model';
import { ActorService } from '../actor.service';


@Component({
  selector: 'app-index-actors',
  templateUrl: './index-actors.component.html',
  styleUrls: ['./index-actors.component.css']
})
export class IndexActorsComponent implements OnInit {

  constructor(private actorService: ActorService) { }
  actors: actorDTO[] |any;
  columnToDisplay = ['No.','picture','name','story','actions'];
  totalAmountOfRecords : any;
  currentPage = 1;
  pageSize = 5;

  ngOnInit(): void {
    this.loadData();
    
  }
  loadData(){
    this.actorService.getAll(this.currentPage, this.pageSize).subscribe((response: HttpResponse<actorDTO[]>) => {
      this.actors = response.body;
      this.totalAmountOfRecords = response.headers.get("totalAmountOfRecords");
    });
  }
  
  onSortData(event: MatSort){
    this.actors.sort = event.sort;
  }
  updatePagination(event: PageEvent){
    this.currentPage = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.loadData();
  }  
  delete(id: number){
    this.actorService.delete(id).subscribe(() =>{
      this.loadData();
    });
  }

}
