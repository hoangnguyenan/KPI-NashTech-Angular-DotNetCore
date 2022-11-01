import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { showAPIErrors } from 'src/app/shares/Utilities';
import { actorCreateDTO } from '../actor.model';
import { ActorService } from '../actor.service';

@Component({
  selector: 'app-create-actors',
  templateUrl: './create-actors.component.html',
  styleUrls: ['./create-actors.component.css']
})
export class CreateActorsComponent implements OnInit {

  errors: string[] = [];
  constructor(private actorService: ActorService, private router: Router) { }

  ngOnInit(): void {
  }
  saveChanges(actorCreateDTO: actorCreateDTO){
    this.actorService.create(actorCreateDTO).subscribe(() => {
      this.router.navigate(['/actors']);      
    },error => this.errors = showAPIErrors(error));
  }
}
