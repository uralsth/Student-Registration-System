import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProjectFormComponent } from './components/project-form/project-form.component';
import { ProjectListComponent } from './components/project-list/project-list.component';



@NgModule({
  declarations: [
    ProjectFormComponent,
    ProjectListComponent
  ],
  imports: [
    CommonModule
  ]
})
export class ProjectsModule { }
