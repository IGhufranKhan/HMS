import { Component } from '@angular/core';

@Component({
  selector: 'app-doctor',
  standalone: true,
  imports: [],
  templateUrl: './doctor.component.html',
  
  styleUrl: './doctor.component.css'
})
export class DoctorComponent {
  doctors :any = [
    { id: 1, name: 'Dr. Smith', specialty: 'Cardiology' },
    { id: 2, name: 'Dr. Johnson', specialty: 'Neurology' },
    { id: 3, name: 'Dr. Lee', specialty: 'Oncology' },
    // ...
  ];
}
