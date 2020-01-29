import { Component, Inject, ChangeDetectorRef } from '@angular/core';
import { StudentServiceService } from './student-service.service';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import {Student} from '../model/student';



/**
 * @title Autocomplete overview
 */
@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html',
  styleUrls: ['./fetch-data.component.css']
})
export class FetchDataComponent {
  public students: Student[]=[];
  IsDetail:boolean = false;
  data : any;
  stateCtrl = new FormControl();
  filteredStudents: Observable<Student[]>;

  constructor(private _studentService: StudentServiceService,@Inject('BASE_URL') url: string, private cd: ChangeDetectorRef) {

    debugger;
    this._studentService.getAllStudents(url).subscribe(res => {
           debugger;
           this.students = res.data;
           console.log(this.students)
         });

    this.filteredStudents = this.stateCtrl.valueChanges
      .pipe(
        startWith(''),
        map(state => state ? this._filterStates(state) : this.students.slice())
      );
  }

  private _filterStates(value: string): Student[] {
    const filterValue = value.toLowerCase();
  return this.students.filter(option => String(option.firstName).toLowerCase().indexOf(filterValue ) > -1 ||
  option.lastName.toLowerCase().indexOf(filterValue ) > -1);
  }

  updateMySelection(_event:any,data){
    if(_event.isUserInput){
    this.IsDetail = true;
    this.data = data;
    }
  }
  getStatus(item) {
    switch (item) {
      case "In Room":     
        return "green";
      case "History":
        return "red";
      case "Reserved":
        return "yellow";
      case "Held":
        return "yellow";      
    }
  }
}


