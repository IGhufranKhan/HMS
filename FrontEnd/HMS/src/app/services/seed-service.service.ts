import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SeedServiceService {

  constructor() { }

public GetPatient(): IPatient[]
{
  const patients : IPatient[] = [
    { id: 1, name: 'John Doe', age: 30 },
    { id: 2, name: 'Jane Smith', age: 25 },
    { id: 3, name: 'Bob Johnson', age: 40 },
    // ...

  ];
  return patients
}
public GetDoctors(): IDoctor[]
{
  const doctors : IDoctor[] =[
    { id: 1, name: 'Dr. Smith', speciality: 'Cardiology' },
    { id: 2, name: 'Dr. Johnson', speciality: 'Neurology' },
    { id: 3, name: 'Dr. Lee', speciality: 'Oncology' },
  ]
  return doctors
}
public GetDepartment(): IDepartment[]
{
  
  const departments = [
      { id: 1, name: 'Cardiology' },
      { id: 2, name: 'Neurology' },
      { id: 3, name: 'Oncology' },
      // ...
    ];
    return departments    
}

}
export interface IPatient
{
  id: number;
  name: string;
  age: number
}
export interface IDoctor
{
  id: number;
  name: string;
  speciality: string
}

export interface IDepartment
{
  id: number;
  name: string;
}