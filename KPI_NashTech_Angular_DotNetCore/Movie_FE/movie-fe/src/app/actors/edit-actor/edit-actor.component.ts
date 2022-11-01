import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { actorCreateDTO, actorDTO } from '../actor.model';
import { ActorService } from '../actor.service';

@Component({
  selector: 'app-edit-actor',
  templateUrl: './edit-actor.component.html',
  styleUrls: ['./edit-actor.component.css']
})
export class EditActorComponent implements OnInit {

  constructor(private activetedRoute: ActivatedRoute,
    private actorService: ActorService,
    private router: Router) { }

  model: actorDTO | any;

  ngOnInit(): void {
    this.activetedRoute.params.subscribe(params => {
      this.actorService.getById(params.id).subscribe(actor => this.model = actor);
    });
  }
  saveChanges(actorCreateDTO: actorCreateDTO) {
    this.actorService.edit(this.model.id, actorCreateDTO).subscribe(() =>{
      this.router.navigate(['/actors']);
    });
  }

}
