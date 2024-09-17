import { Component } from '@angular/core';
import { IDepartment, SeedServiceService } from '../../services/seed-service.service';

@Component({
  selector: 'app-department',
  standalone: true,
  imports: [],
  templateUrl: './department.component.html',
  styleUrl: './department.component.css'
})
export class DepartmentComponent {
  departments : IDepartment[] = []; 
  trackDepartmentById(index: number, department: any) {
    return department.id;
  }
  constructor(private SeedServiceService : SeedServiceService)
  {
    console.log("I am a constructor of seed service")
  }
 
  ngOnInit()
  {
    this.departments = this.SeedServiceService.GetDepartment()
  }
}

