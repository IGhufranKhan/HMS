import { Routes } from '@angular/router';
import { DoctorComponent } from './features/doctor/doctor.component';
import { DepartmentComponent } from './features/department/department.component';
import { PatientComponent } from './features/patient/patient.component';

export const routes: Routes = [
    {path : 'doctor',component : DoctorComponent},
    { path: 'department', component: DepartmentComponent },
    { path: 'patient', component: PatientComponent },  
];
