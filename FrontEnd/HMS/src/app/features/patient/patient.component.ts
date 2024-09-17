import { Component } from '@angular/core';
import { IPatient , SeedServiceService} from '../../services/seed-service.service';


@Component({
  selector: 'app-patient',
  standalone: true,
  imports: [],
  templateUrl: './patient.component.html',
  styleUrl: './patient.component.css'
})
export class PatientComponent {
  patients : IPatient[] = []
  trackPatientById(index: number, patient: any) {
    return patient.id;
  }
  constructor(private SeedServiceService : SeedServiceService)
  {
    console.log("I am a constructor of seed service")
  }
 
  ngOnInit()
  {
    this.patients = this.SeedServiceService.GetPatient()
  }
}
