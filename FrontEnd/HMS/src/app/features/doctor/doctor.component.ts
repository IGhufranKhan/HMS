import { Component } from '@angular/core';
import { IDoctor, SeedServiceService } from '../../services/seed-service.service';

@Component({
  selector: 'app-doctor',
  standalone: true,
  imports: [],
  templateUrl: './doctor.component.html',
  
  styleUrl: './doctor.component.css'
})
export class DoctorComponent {
  doctors : IDoctor[] = []
  
  constructor(private SeedServiceService : SeedServiceService)
  {
    console.log("I am a constructor of seed service")
  }
 
  ngOnInit()
  {
    this.doctors = this.SeedServiceService.GetDoctors()
  }
  
}

